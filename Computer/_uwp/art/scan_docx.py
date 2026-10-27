# -*- coding: utf-8 -*-
import os, sys, zipfile, collections, hashlib, json, argparse
sys.stdout.reconfigure(encoding='utf-8')
ap = argparse.ArgumentParser()
ap.add_argument('--research', help='research folder that contains "Computer 2 (Old Projects Temp)"')
RES = ap.parse_args().research or input('Research folder: ').strip().strip('"')
HERE = os.path.dirname(os.path.abspath(__file__))
ROOT = os.path.normpath(os.path.join(HERE, '..', '..', '..'))
WORK = os.path.join(ROOT, 'bin', 'art-work')
if not RES or not os.path.isdir(os.path.join(RES, 'Computer 2 (Old Projects Temp)')):
    sys.exit('Research folder not found or without "Computer 2 (Old Projects Temp)": ' + RES)
roots = [RES]
out = []
cnt = collections.Counter()
withimg = collections.Counter()
for root in roots:
    for dp, dn, fn in os.walk(root):
        for f in fn:
            if not f.lower().endswith('.docx'):
                continue
            p = os.path.join(dp, f)
            rel = os.path.relpath(p, root)
            cat = rel.split(os.sep)[1] if rel.split(os.sep)[0].startswith('Computer 2') and len(rel.split(os.sep)) > 2 else rel.split(os.sep)[0]
            cnt[cat] += 1
            try:
                z = zipfile.ZipFile(p)
            except Exception as e:
                out.append({'path': p, 'error': str(e)})
                continue
            media = [(i.filename, i.file_size) for i in z.infolist() if i.filename.startswith('word/media/')]
            if media:
                withimg[cat] += 1
            out.append({'path': p, 'media': media})
print('docx per category', dict(cnt))
print('with images', dict(withimg))
json.dump(out, open(os.path.join(WORK, 'img', 'docx.json'), 'w', encoding='utf-8'), ensure_ascii=False, indent=1)
dirs = collections.Counter(os.path.dirname(os.path.relpath(o['path'], roots[0])) for o in out)
for d, c in sorted(dirs.items()):
    print(c, d)
