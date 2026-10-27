# -*- coding: utf-8 -*-
import os, sys, zipfile, json, hashlib, struct, argparse
sys.stdout.reconfigure(encoding='utf-8')
ap = argparse.ArgumentParser()
ap.add_argument('--research', help='research folder that contains "Computer 2 (Old Projects Temp)"')
RES = ap.parse_args().research or input('Research folder: ').strip().strip('"')
HERE = os.path.dirname(os.path.abspath(__file__))
ROOT = os.path.normpath(os.path.join(HERE, '..', '..', '..'))
WORK = os.path.join(ROOT, 'bin', 'art-work')
R = os.path.join(RES, 'Computer 2 (Old Projects Temp)')
if not RES or not os.path.isdir(R):
    sys.exit('Research folder not found or without "Computer 2 (Old Projects Temp)": ' + RES)
DATA = os.path.join(ROOT, 'Data', 'Computer')
OUT = os.path.join(WORK, 'img', 'docx_media')
os.makedirs(OUT, exist_ok=True)


def size(d):
    if d[:8] == b'\x89PNG\r\n\x1a\n':
        return struct.unpack('>II', d[16:24])
    if d[:2] == b'\xff\xd8':
        i = 2
        while i < len(d) - 9:
            if d[i] != 0xFF:
                i += 1
                continue
            m = d[i + 1]
            if m in (0xC0, 0xC1, 0xC2):
                h, w = struct.unpack('>HH', d[i + 5:i + 9])
                return (w, h)
            l = struct.unpack('>H', d[i + 2:i + 4])[0]
            i += 2 + l
    if d[:3] == b'GIF':
        return struct.unpack('<HH', d[6:10])
    return None


data_rel = {}
for dp, dn, fn in os.walk(DATA):
    for f in fn:
        if f.endswith('.txt'):
            rel = os.path.relpath(os.path.join(dp, f), DATA)[:-4]
            data_rel[rel.lower()] = rel
res = {}
for tree in ('Computer', 'Computer 2 Files'):
    base = os.path.join(R, tree)
    for dp, dn, fn in os.walk(base):
        for f in fn:
            if not f.lower().endswith('.docx'):
                continue
            p = os.path.join(dp, f)
            rel = os.path.relpath(p, base)[:-5]
            try:
                z = zipfile.ZipFile(p)
            except zipfile.BadZipFile:
                print('bad zip', p)
                continue
            media = []
            for i in z.infolist():
                if i.filename.startswith('word/media/'):
                    b = z.read(i)
                    h = hashlib.sha1(b).hexdigest()[:12]
                    ext = os.path.splitext(i.filename)[1].lower()
                    target = os.path.join(OUT, h + ext)
                    if not os.path.exists(target):
                        open(target, 'wb').write(b)
                    media.append({'file': h + ext, 'size': size(b), 'bytes': len(b)})
            res.setdefault(rel, {})[tree] = media
match = sum(1 for r in res if r.lower() in data_rel)
print('docx keys', len(res), 'matching a data file path', match)
print('data without docx:', [v for k, v in data_rel.items() if k not in {r.lower() for r in res}][:80])
print('docx without data:', [r for r in res if r.lower() not in data_rel][:40])
json.dump(res, open(os.path.join(os.path.dirname(OUT), 'docx_media.json'), 'w', encoding='utf-8'), ensure_ascii=False, indent=1)
