# -*- coding: utf-8 -*-
import os, sys, json, re, collections
sys.stdout.reconfigure(encoding='utf-8')
HERE = os.path.dirname(os.path.abspath(__file__))
ROOT = os.path.normpath(os.path.join(HERE, '..', '..', '..'))
WORK = os.path.join(ROOT, 'bin', 'art-work')
I = os.path.join(WORK, 'img')
DATA = os.path.join(ROOT, 'Data', 'Computer')
ASSETS = os.path.join(ROOT, 'Computer', 'Assets', 'Images', 'Desktop')
ISO2 = os.path.join(WORK, 'isox', '2', 'Asus Laptop')
media = json.load(open(os.path.join(I, 'docx_media.json'), encoding='utf-8'))
existing = {f.lower(): f for f in os.listdir(ASSETS)}


def norm(s):
    s = s.lower().replace('™', '').replace('®', '')
    return re.sub(r'[\s\-_/\\().]+', '', s)


def safe(name):
    return re.sub(r'[\\/:*?"<>|]', '_', name)


def lines(p):
    return [x.rstrip('\r') for x in open(p, encoding='utf-8-sig').read().split('\n')]


usage = collections.Counter()
for k, v in media.items():
    seen = set()
    for t, m in v.items():
        for x in m:
            seen.add(x['file'])
    for f in seen:
        usage[f] += 1


def docx_images(key_parts):
    for k, v in media.items():
        if [norm(x) for x in k.split('\\')][-len(key_parts):] == [norm(x) for x in key_parts]:
            for t in ('Computer', 'Computer 2 Files'):
                if v.get(t):
                    return v[t], k + '.docx (' + t + ')'
    return [], None


def by_model(category, model):
    for k, v in media.items():
        parts = k.split('\\')
        if parts[0] == category and norm(parts[-1]) == norm(model):
            for t in ('Computer', 'Computer 2 Files'):
                if v.get(t):
                    return v[t], k + '.docx (' + t + ')'
    return [], None


plan = []
report = collections.OrderedDict()


def add(cat, item, src, dest, kind):
    plan.append({'category': cat, 'item': item, 'source': src, 'dest': dest, 'kind': kind})
    report.setdefault(cat, collections.Counter())[kind] += 1


families = collections.OrderedDict()
for dp, dn, fn in os.walk(os.path.join(DATA, 'Processors')):
    for f in fn:
        fam = os.path.basename(dp)
        families.setdefault(fam, []).append(f[:-4])
for fam, names in families.items():
    ex = [existing[e] for e in (fam.lower() + '.png', fam.lower() + '.jpg') if e in existing]
    if ex:
        for n in names:
            add('Processor', fam + ' ' + n, 'existing C3 asset Assets\\Images\\Desktop\\' + ex[0], None, 'real (existing)')
        continue
    c = collections.Counter()
    srcs = {}
    for n in names:
        imgs, src = docx_images([fam, n])
        for x in imgs:
            c[x['file']] += 1
            srcs.setdefault(x['file'], src)
    if c:
        best = c.most_common(1)[0][0]
        for i, n in enumerate(names):
            add('Processor', fam + ' ' + n, os.path.join(I, 'docx_media', best) + ' | from ' + srcs[best], 'Desktop\\' + safe(fam) + '.png' if i == 0 else None, 'real (docx)')
    else:
        for n in names:
            add('Processor', fam + ' ' + n, None, None, 'icon')

for dp, dn, fn in os.walk(os.path.join(DATA, 'Motherboard')):
    for f in fn:
        name = lines(os.path.join(dp, f))[5].strip()
        file = safe(name.replace('/', '_').replace('\\', '_')) + '.jpg'
        if file.lower() in existing:
            add('Motherboard', name, 'existing C3 asset Assets\\Images\\Desktop\\' + existing[file.lower()], None, 'real (existing)')
            continue
        imgs, src = by_model('Motherboard', f[:-4])
        imgs = [x for x in imgs if usage[x['file']] <= 3]
        if imgs:
            best = max(imgs, key=lambda x: x['bytes'])
            add('Motherboard', name, os.path.join(I, 'docx_media', best['file']) + ' | from ' + src, 'Desktop\\' + file, 'real (docx)')
        else:
            add('Motherboard', name, None, None, 'icon')

for dp, dn, fn in os.walk(os.path.join(DATA, 'Graphic Card')):
    for f in fn:
        name = lines(os.path.join(dp, f))[4].strip()
        rel = os.path.relpath(os.path.join(dp, f), DATA)[:-4]
        imgs, src = docx_images(rel.split('\\'))
        imgs = [x for x in imgs if usage[x['file']] <= 3]
        if imgs:
            best = max(imgs, key=lambda x: x['bytes'])
            add('Graphic Card', name, os.path.join(I, 'docx_media', best['file']) + ' | from ' + src, 'Graphic Card\\' + safe(name) + '.jpg', 'real (docx)')
        else:
            add('Graphic Card', name, None, None, 'icon')

for dp, dn, fn in os.walk(os.path.join(DATA, 'Tablet')):
    for f in fn:
        name = lines(os.path.join(dp, f))[3].strip()
        rel = os.path.relpath(os.path.join(dp, f), DATA)[:-4]
        imgs, src = docx_images(rel.split('\\'))
        if imgs:
            best = max(imgs, key=lambda x: x['bytes'])
            add('Tablet', name, os.path.join(I, 'docx_media', best['file']) + ' | from ' + src, 'Tablet\\' + safe(name) + '.jpg', 'real (docx)')
        else:
            add('Tablet', name, None, None, 'icon')

laptop_pages = {'TAICHI 31': os.path.join(ISO2, 'ASUS - TAICHI 31_files', 'TAICHI31_data', 'P_500.jpg'),
                'VivoBook S200E': os.path.join(ISO2, 'ASUS - ASUS VivoBook S200E_files', 'S200E_data', 'P_500.jpg')}
for dp, dn, fn in os.walk(os.path.join(DATA, 'Laptop')):
    for f in fn:
        L = lines(os.path.join(dp, f))
        name = None
        for i, x in enumerate(L):
            if x.strip() == 'اسم الجهاز':
                name = L[i + 1].strip()
                break
        if name in laptop_pages:
            add('Laptop', name, laptop_pages[name] + ' | from New Computer 2 Pieces.iso, Asus Laptop\\' + os.path.basename(os.path.dirname(os.path.dirname(laptop_pages[name]))) , 'Laptop\\' + safe(name) + '.jpg', 'real (iso page)')
        else:
            add('Laptop', name, None, None, 'icon')

for dp, dn, fn in os.walk(os.path.join(DATA, 'Hard Disk')):
    for f in fn:
        add('Storage Device', f[:-4], None, None, 'icon')

json.dump(plan, open(os.path.join(I, 'plan.json'), 'w', encoding='utf-8'), ensure_ascii=False, indent=1)
for k, v in report.items():
    print(k, dict(v))
for p in plan:
    if p['dest']:
        print('COPY', p['category'], '|', p['item'], '->', p['dest'], '<-', os.path.basename(p['source'].split(' | ')[0]))
