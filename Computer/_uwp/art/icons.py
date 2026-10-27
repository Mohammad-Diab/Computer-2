# -*- coding: utf-8 -*-
import os, sys, argparse
ap = argparse.ArgumentParser()
ap.add_argument('--research', help='research folder that contains "Computer2 (Projects Temp Files)"')
RES = ap.parse_args().research or input('Research folder: ').strip().strip('"')
HERE = os.path.dirname(os.path.abspath(__file__))
ROOT = os.path.normpath(os.path.join(HERE, '..', '..', '..'))
WORK = os.path.join(ROOT, 'bin', 'art-work')
D = os.path.join(HERE, 'xaml')
MISC = os.path.join(RES, 'Computer2 (Projects Temp Files)')
if not RES or not os.path.isdir(MISC):
    sys.exit('Research folder not found or without "Computer2 (Projects Temp Files)": ' + RES)
os.makedirs(D, exist_ok=True)
for f in os.listdir(D):
    os.remove(os.path.join(D, f))

L, M, DK, K, C, C2, G, AU, PCB, TR = '#E4E9EE', '#A7B1BB', '#39424B', '#232A31', '#00E5FF', '#00A9C7', '#99FF2F', '#E3B341', '#24635A', '#2F7D72'
SCREEN = '<LinearGradientBrush StartPoint="0,0" EndPoint="1,1"><GradientStop Color="#1BD1F0" Offset="0"/><GradientStop Color="#0B5F78" Offset="1"/></LinearGradientBrush>'
NS = 'xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"'


def r(x, y, w, h, fill, rad=0, extra=''):
    return '<Rectangle Canvas.Left="%s" Canvas.Top="%s" Width="%s" Height="%s" RadiusX="%s" RadiusY="%s" Fill="%s" %s/>' % (x, y, w, h, rad, rad, fill, extra)


def rb(x, y, w, h, brush, rad=0):
    return '<Rectangle Canvas.Left="%s" Canvas.Top="%s" Width="%s" Height="%s" RadiusX="%s" RadiusY="%s"><Rectangle.Fill>%s</Rectangle.Fill></Rectangle>' % (x, y, w, h, rad, rad, brush)


def e(x, y, w, h, fill='Transparent', stroke=None, t=0, extra=''):
    s = ' Stroke="%s" StrokeThickness="%s"' % (stroke, t) if stroke else ''
    return '<Ellipse Canvas.Left="%s" Canvas.Top="%s" Width="%s" Height="%s" Fill="%s"%s %s/>' % (x, y, w, h, fill, s, extra)


def p(data, fill='Transparent', stroke=None, t=0, extra=''):
    s = ' Stroke="%s" StrokeThickness="%s" StrokeStartLineCap="Round" StrokeEndLineCap="Round" StrokeLineJoin="Round"' % (stroke, t) if stroke else ''
    return '<Path Data="%s" Fill="%s"%s %s/>' % (data, fill, s, extra)


def group(body, m):
    return '<Canvas RenderTransform="%s">%s</Canvas>' % (m, body)


def canvas(body, w=256, h=256, tag=None):
    t = ' Tag="%s"' % tag if tag else ''
    return '<Canvas %s Width="%d" Height="%d"%s>%s</Canvas>' % (NS, w, h, t, body)


icons = {}
cpu = ''
for i in range(8):
    cpu += r(66 + i * 17, 32, 8, 24, M, 2) + r(66 + i * 17, 200, 8, 24, M, 2) + r(32, 66 + i * 17, 24, 8, M, 2) + r(200, 66 + i * 17, 24, 8, M, 2)
cpu += r(52, 52, 152, 152, DK, 16) + r(86, 86, 84, 84, C2, 8) + r(100, 100, 56, 56, C, 6) + e(66, 66, 12, 12, G)
icons['cpu'] = cpu

board = r(20, 28, 216, 200, PCB, 12)
board += p('M46,140 L120,140 L140,160 M128,52 L128,30 M100,126 L100,150 L40,150 M200,150 L230,150 M200,120 L230,120', stroke=TR, t=4)
board += r(42, 48, 76, 76, L, 6) + r(54, 60, 52, 52, M, 4) + e(60, 66, 8, 8, G)
for x in (136, 158, 180):
    board += r(x, 44, 14, 100, K, 3) + r(x + 3, 40, 8, 8, C2, 2) + r(x + 3, 136, 8, 8, C2, 2)
board += r(36, 160, 156, 14, K, 3) + r(36, 188, 156, 14, K, 3) + r(178, 160, 14, 14, C2, 3) + r(178, 188, 14, 14, C2, 3)
board += r(204, 168, 24, 24, M, 4) + r(208, 40, 22, 14, M, 2) + r(208, 60, 22, 14, M, 2) + r(208, 80, 22, 22, DK, 3)
icons['board'] = board

gpu = r(14, 58, 20, 130, M, 3) + r(18, 76, 12, 18, K, 2) + r(18, 104, 12, 30, K, 2) + r(18, 146, 12, 18, K, 2)
gpu += r(30, 66, 200, 110, DK, 12) + r(30, 66, 200, 16, C2, 8) + r(30, 74, 200, 8, C2)
gpu += e(88, 88, 84, 84, K)
blades = ''
for a in range(5):
    blades += '<Path Data="M130,130 C138,108 150,104 160,108 C152,118 144,126 130,130 Z" Fill="%s"><Path.RenderTransform><RotateTransform Angle="%d" CenterX="130" CenterY="130"/></Path.RenderTransform></Path>' % (M, a * 72)
gpu += blades + e(120, 120, 20, 20, C)
for i in range(5):
    gpu += r(186, 98 + i * 14, 32, 6, '#2C333A', 3)
gpu += r(64, 176, 112, 14, AU, 2) + ''.join(r(76 + i * 14, 176, 2, 14, '#B48A2C') for i in range(7))
icons['gpu'] = gpu

hdd = r(56, 20, 144, 216, M, 14) + r(64, 28, 128, 200, L, 10)
hdd += e(76, 40, 104, 104, '#CFD6DD', M, 3) + e(108, 72, 40, 40, '#BCC5CE') + e(120, 84, 16, 16, DK)
hdd += p('M168,198 L134,104', stroke=DK, t=10) + e(156, 186, 26, 26, DK) + e(163, 193, 12, 12, M)
hdd += r(76, 206, 14, 8, G, 2) + r(98, 160, 52, 6, M, 3) + r(98, 172, 36, 6, M, 3)
icons['hdd'] = hdd

ssd = r(32, 62, 186, 132, DK, 14) + r(48, 78, 154, 70, C2, 6) + r(48, 78, 154, 24, C, 6) + r(48, 92, 154, 10, C)
ssd += r(62, 160, 84, 8, M, 4) + r(62, 174, 56, 8, M, 4) + e(178, 162, 14, 14, G)
ssd += r(214, 96, 16, 64, K, 3) + ''.join(r(217, 102 + i * 9, 10, 4, AU, 1) for i in range(6))
icons['ssd'] = ssd

ram = r(12, 88, 232, 86, PCB, 6) + ''.join(r(24 + i * 27, 100, 22, 40, K, 3) for i in range(8))
ram += r(18, 146, 220, 6, C2, 2) + r(18, 160, 98, 14, AU, 2) + r(130, 160, 108, 14, AU, 2)
ram += ''.join(r(24 + i * 9, 160, 2, 14, '#B48A2C') for i in range(10)) + ''.join(r(136 + i * 9, 160, 2, 14, '#B48A2C') for i in range(11))
ram += e(228, 94, 8, 8, G)
icons['ram'] = ram

psu = r(28, 50, 200, 156, M, 12) + r(36, 58, 184, 140, L, 8)
psu += e(52, 66, 124, 124, DK) + e(64, 78, 100, 100, 'Transparent', M, 4) + e(82, 96, 64, 64, 'Transparent', M, 4) + e(104, 118, 20, 20, M)
psu += p('M114,70 L114,186 M58,128 L170,128', stroke=M, t=4)
psu += r(188, 74, 20, 30, K, 3) + r(191, 77, 14, 12, G, 2) + r(186, 122, 24, 32, K, 3) + r(191, 128, 4, 10, M) + r(201, 128, 4, 10, M)
psu += p('M206,206 C206,232 230,238 248,226', stroke=K, t=9)
icons['psu'] = psu

case = r(68, 12, 120, 230, DK, 12) + r(68, 12, 22, 230, '#4A545E', 10) + r(80, 12, 10, 230, '#4A545E')
case += r(98, 34, 74, 20, K, 3) + r(98, 60, 74, 20, K, 3) + r(104, 42, 62, 3, C2)
case += e(122, 96, 26, 26, K, C, 4) + e(156, 104, 9, 9, G)
case += ''.join(r(98, 150 + i * 13, 74, 6, '#2C333A', 3) for i in range(5))
case += r(80, 238, 26, 8, K, 3) + r(150, 238, 26, 8, K, 3)
icons['case'] = case

monitor = r(18, 28, 220, 152, DK, 12) + rb(30, 40, 196, 118, SCREEN, 4)
monitor += p('M30,40 L130,40 L60,158 L30,158 Z', fill='#FFFFFF', extra='Opacity="0.12"') + e(124, 164, 8, 8, G)
monitor += r(114, 180, 28, 32, M) + r(68, 208, 120, 16, M, 8)
icons['monitor'] = monitor

mouse = p('M201,6 C176,22 132,10 128,48', stroke=DK, t=6)
mouse += e(74, 44, 108, 176, L, M, 4) + p('M128,48 L128,116 M78,116 L178,116', stroke=M, t=4) + r(119, 64, 18, 36, C2, 9) + r(123, 70, 10, 10, C, 5)
icons['mouse'] = mouse

kb = r(10, 82, 236, 104, DK, 12)
for row in range(4):
    for col in range(11):
        x, y = 24 + col * 19, 96 + row * 20
        if row == 3 and 3 <= col <= 8:
            continue
        fill = C2 if (row == 1 and col == 10) else L
        kb += r(x, y, 15, 15, fill, 3)
kb += r(24 + 3 * 19, 96 + 60, 6 * 19 - 4, 15, L, 3) + e(226, 88, 6, 6, G)
icons['keyboard'] = kb

disc = '<Ellipse Canvas.Left="64" Canvas.Top="18" Width="128" Height="128" Stroke="%s" StrokeThickness="3"><Ellipse.Fill><RadialGradientBrush><GradientStop Color="#F4F7FA" Offset="0.2"/><GradientStop Color="#C3CCD6" Offset="0.8"/><GradientStop Color="#9FD9E6" Offset="1"/></RadialGradientBrush></Ellipse.Fill></Ellipse>' % M
optical = disc + p('M92,46 A50,50 0 0 1 164,46', stroke=C, t=5, extra='Opacity="0.7"') + e(110, 64, 36, 36, M) + e(120, 74, 16, 16, DK)
optical += r(16, 118, 224, 80, M, 8) + r(24, 126, 208, 64, L, 5) + r(38, 148, 150, 8, DK, 4) + r(198, 166, 22, 10, DK, 3) + e(205, 142, 9, 9, G)
icons['optical'] = optical

lid = r(46, 38, 164, 120, DK, 8) + rb(56, 48, 144, 100, SCREEN, 3) + p('M56,48 L120,48 L80,148 L56,148 Z', fill='#FFFFFF', extra='Opacity="0.12"')
laptop = lid + p('M20,162 L236,162 L224,194 Q221,200 213,200 L43,200 Q35,200 32,194 Z', fill=M) + r(16, 156, 224, 10, L, 5) + r(104, 178, 48, 10, L, 4)
icons['laptop'] = laptop

tablet = r(62, 16, 132, 224, DK, 18) + rb(72, 36, 112, 176, SCREEN, 3) + p('M72,36 L130,36 L90,212 L72,212 Z', fill='#FFFFFF', extra='Opacity="0.12"')
tablet += e(119, 218, 18, 18, 'Transparent', M, 3) + e(125, 24, 6, 6, M)
icons['tablet'] = tablet

phone = r(84, 20, 88, 216, DK, 16) + rb(92, 42, 72, 158, SCREEN, 3) + p('M92,42 L130,42 L104,200 L92,200 Z', fill='#FFFFFF', extra='Opacity="0.12"')
phone += r(114, 29, 28, 5, M, 2) + e(119, 206, 18, 18, 'Transparent', M, 3)
icons['phone'] = phone

arrows = p('M98,108 L158,108 M146,96 L160,108 L146,120 M158,148 L98,148 M110,136 L96,148 L110,160', stroke=G, t=8)
icons['compare'] = group(laptop, '0.46,0,0,0.46,-10,66') + group(tablet, '0.5,0,0,0.5,130,64') + arrows
icons['compare_mobile'] = group(phone, '0.62,0,0,0.62,-28,48') + group(phone, '0.62,0,0,0.62,124,48') + arrows

choose = r(36, 26, 148, 196, L, 10) + r(36, 26, 148, 30, C2, 10) + r(36, 42, 148, 14, C2)
for i in range(5):
    y = 74 + i * 28
    choose += r(52, y, 16, 16, 'Transparent', 3, 'Stroke="%s" StrokeThickness="3"' % M) + r(78, y + 4, 80 - (i % 2) * 24, 8, M, 4)
choose += p('M54,80 L59,86 L67,75 M54,136 L59,142 L67,131', stroke='#2E9E3F', t=4)
choose += p('M194,198 L232,236', stroke=DK, t=18) + e(120, 110, 100, 100, '#3300E5FF', C, 12)
icons['choose'] = choose

spec = r(52, 16, 152, 224, L, 10) + r(52, 16, 152, 40, C2, 10) + r(52, 40, 152, 16, C2)
spec += group(cpu, '0.22,0,0,0.22,100,6')
for i in range(6):
    y = 72 + i * 26
    spec += r(68, y, 44, 10, M, 5) + r(122, y, 66 - (i % 3) * 12, 10, '#C9D1D9', 5)
spec += r(68, 220, 60, 8, G, 4)
icons['spec'] = spec

box = p('M128,30 L222,78 L128,126 L34,78 Z', fill=L) + p('M34,78 L128,126 L128,232 L34,184 Z', fill=M) + p('M222,78 L128,126 L128,232 L222,184 Z', fill=DK)
box += p('M80,54 L174,102 L174,128', stroke=C2, t=10) + e(156, 160, 20, 20, G)
icons['generic'] = box

desktop = group(monitor, '0.72,0,0,0.72,6,50') + group(case, '0.72,0,0,0.72,124,40')
icons['desktop'] = desktop

for name, body in icons.items():
    open(os.path.join(D, name + '.xaml'), 'w', encoding='utf-8').write(canvas(body))

BG = '<Rectangle Width="{w}" Height="{h}"><Rectangle.Fill><LinearGradientBrush StartPoint="0,0" EndPoint="0.4,1"><GradientStop Color="#505050" Offset="0"/><GradientStop Color="#2B2B2B" Offset="1"/></LinearGradientBrush></Rectangle.Fill></Rectangle>'


def tile(name, inner):
    body = BG.format(w=300, h=250) + r(0, 244, 300, 6, C) + inner
    open(os.path.join(D, 'tile_' + name + '.xaml'), 'w', encoding='utf-8').write(canvas(body, 300, 250))


def img(path, x, y, w, h):
    return '<Image Canvas.Left="%s" Canvas.Top="%s" Width="%s" Height="%s" Stretch="Uniform" Source="%s"/>' % (x, y, w, h, os.path.relpath(path, D).replace('\\', '/'))


tile('desktop', group(icons['desktop'], '0.86,0,0,0.86,40,6'))
tile('specification', group(icons['board'], '0.62,0,0,0.62,40,30') + group(icons['cpu'], '0.42,0,0,0.42,150,96'))
tile('laptop', group(icons['laptop'], '0.78,0,0,0.78,18,28') + group(icons['tablet'], '0.62,0,0,0.62,172,62'))
tile('compare', group(icons['compare'], '0.82,0,0,0.82,45,14'))
tile('choose', group(icons['choose'], '0.78,0,0,0.78,50,18'))
tile('mobile', group(icons['phone'], '0.86,0,0,0.86,40,8'))
tile('compare_mobile', group(icons['compare_mobile'], '0.82,0,0,0.82,45,14'))

hero = '<Rectangle Width="780" Height="768"><Rectangle.Fill><LinearGradientBrush StartPoint="0,0" EndPoint="0.6,1"><GradientStop Color="#4A4A4A" Offset="0"/><GradientStop Color="#1F1F1F" Offset="1"/></LinearGradientBrush></Rectangle.Fill></Rectangle>'
grid = ''.join('M%d,0 L%d,768 ' % (x, x) for x in range(0, 781, 52)) + ''.join('M0,%d L780,%d ' % (y, y) for y in range(0, 769, 52))
hero += p(grid, stroke='#FFFFFF', t=1, extra='Opacity="0.035"')
hero += e(120, 180, 560, 560, 'Transparent', C, 3, 'Opacity="0.22"') + e(170, 230, 460, 460, 'Transparent', C, 2, 'Opacity="0.12"')
hero += e(90, 600, 620, 70, '#000000', extra='Opacity="0.35"')
hero += group(icons['monitor'], '1.75,0,0,1.75,150,250') + group(icons['case'], '1.55,0,0,1.55,470,250')
hero += group(icons['laptop'], '1.25,0,0,1.25,40,420') + group(icons['tablet'], '0.9,0,0,0.9,430,450') + group(icons['phone'], '0.72,0,0,0.72,560,490')
hero += r(0, 762, 780, 6, C)
open(os.path.join(D, 'hero.xaml'), 'w', encoding='utf-8').write(canvas(hero, 780, 768))

try:
    SYM = os.path.relpath(os.path.join(MISC, 'computer-symbolic.png'), D)
except ValueError:
    SYM = os.path.abspath(os.path.join(MISC, 'computer-symbolic.png'))
splash = '<Rectangle Canvas.Left="250" Canvas.Top="40" Width="120" Height="137" Fill="White"><Rectangle.OpacityMask><ImageBrush Stretch="Uniform" ImageSource="%s"/></Rectangle.OpacityMask></Rectangle>' % (SYM.replace(chr(92), '/'))
splash += '<TextBlock Canvas.Left="0" Canvas.Top="196" Width="620" TextAlignment="Center" FontFamily="Segoe UI Light" FontSize="44" Foreground="White" Text="Computer"/>'
splash += r(250, 254, 120, 4, C, 2)
open(os.path.join(D, 'SplashScreen.xaml'), 'w', encoding='utf-8').write(canvas(splash, 620, 300, '1,2'))
print('written', len(os.listdir(D)))
