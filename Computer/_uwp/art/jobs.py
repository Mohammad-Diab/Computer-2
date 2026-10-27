# -*- coding: utf-8 -*-
import os, sys, json, argparse
sys.stdout.reconfigure(encoding='utf-8')
ap = argparse.ArgumentParser()
ap.add_argument('--research', help='research folder that contains "logos"')
RES = ap.parse_args().research or input('Research folder: ').strip().strip('"')
HERE = os.path.dirname(os.path.abspath(__file__))
ROOT = os.path.normpath(os.path.join(HERE, '..', '..', '..'))
WORK = os.path.join(ROOT, 'bin', 'art-work')
I = os.path.join(WORK, 'img')
OUT = os.path.join(WORK, 'newassets', 'Assets', 'Images')
LOGOS = os.path.join(RES, 'logos')
if not RES or not os.path.isdir(LOGOS):
    sys.exit('Research folder not found or without "logos": ' + RES)
ISO1 = os.path.join(WORK, 'isox', '1', 'Temp')
plan = json.load(open(os.path.join(I, 'plan.json'), encoding='utf-8'))
jobs = []
sources = []
for p in plan:
    if p['dest']:
        src = p['source'].split(' | ')[0]
        jobs.append({'src': src, 'dest': os.path.join(OUT, p['dest']), 'mode': 'white'})
newipad = os.path.join(ISO1, 'Tablet', 'Apple', 'Apple - The new iPad - View all the technical specifications._files', 'new_ipad_box.png')
jobs.append({'src': newipad, 'dest': os.path.join(OUT, 'Tablet', 'The new iPad Wi-Fi.jpg'), 'mode': 'white'})
for p in plan:
    if p['category'] == 'Tablet' and p['item'] == 'The new iPad Wi-Fi':
        p['kind'] = 'real (iso page)'
        p['source'] = newipad + ' | from Computer.iso, Temp\\Tablet\\Apple\\Apple - The new iPad - View all the technical specifications_files\\new_ipad_box.png'
        p['dest'] = 'Tablet\\The new iPad Wi-Fi.jpg'
logos = {
    'nvidia': 'nividia-logo.png', 'evga': 'EVGA_logo_black.png', 'sapphire': 'sapphire-logo.png',
    'western digital': 'western-digital-logo.jpg', 'dell': '1394552117_Dell.png', 'hp': '1394551995_HP.png',
    'lenovo': 'lenovo-logo.jpg', 'sony': 'Sony-Logo.jpeg', 'toshiba': 'toshiba-logo.jpg',
    'fujistu semens': 'FUJITSU.jpg', 'apple': '1394553472_193-Apple.png',
}
for k, v in logos.items():
    jobs.append({'src': os.path.join(LOGOS, v), 'dest': os.path.join(OUT, 'Desktop', k + '.png'), 'mode': 'white'})
    plan.append({'category': 'Company logo', 'item': k, 'source': os.path.join(LOGOS, v) + ' | from logos\\' + v, 'dest': 'Desktop\\' + k + '.png', 'kind': 'real (logos folder)'})
json.dump(jobs, open(os.path.join(I, 'jobs.json'), 'w', encoding='utf-8'), ensure_ascii=False, indent=1)
json.dump(plan, open(os.path.join(I, 'plan2.json'), 'w', encoding='utf-8'), ensure_ascii=False, indent=1)
print(len(jobs), 'jobs')
