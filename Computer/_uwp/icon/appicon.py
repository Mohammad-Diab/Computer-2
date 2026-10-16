# -*- coding: utf-8 -*-
import os
HERE = os.path.dirname(os.path.abspath(__file__))
ROOT = os.path.normpath(os.path.join(HERE, '..', '..', '..'))
WORK = os.path.join(ROOT, 'bin', 'art-work')
D = os.path.join(WORK, 'img', 'iconxaml')
os.makedirs(D, exist_ok=True)
for f in os.listdir(D):
    os.remove(os.path.join(D, f))
NS = 'xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"'
PLATE, L, M, C, G = '#2B2B2B', '#E8EDF2', '#A7B1BB', '#00E5FF', '#99FF2F'
SCREEN = '<LinearGradientBrush StartPoint="0,0" EndPoint="1,1"><GradientStop Color="#2FE0FA" Offset="0"/><GradientStop Color="#0A8DB0" Offset="1"/></LinearGradientBrush>'


def r(x, y, w, h, fill, rad=0, extra=''):
    return '<Rectangle Canvas.Left="%s" Canvas.Top="%s" Width="%s" Height="%s" RadiusX="%s" RadiusY="%s" Fill="%s" %s/>' % (x, y, w, h, rad, rad, fill, extra)


glyph = ''.join([
    r(20, 58, 156, 116, L, 12),
    '<Rectangle Canvas.Left="32" Canvas.Top="70" Width="132" Height="88" RadiusX="4" RadiusY="4"><Rectangle.Fill>%s</Rectangle.Fill></Rectangle>' % SCREEN,
    '<Path Data="M32,70 L104,70 L60,158 L32,158 Z" Fill="#FFFFFF" Opacity="0.18"/>',
    r(86, 174, 24, 20, M),
    r(56, 192, 84, 14, M, 7),
    r(186, 46, 52, 162, L, 10),
    r(196, 62, 32, 8, '#3A434C', 4),
    r(196, 78, 32, 8, '#3A434C', 4),
    '<Ellipse Canvas.Left="199" Canvas.Top="112" Width="26" Height="26" Stroke="%s" StrokeThickness="6"/>' % C,
    '<Ellipse Canvas.Left="205" Canvas.Top="176" Width="14" Height="14" Fill="%s"/>' % G,
])


def icon(size, plated, glyph_share):
    body = ''
    g = glyph
    if not plated:
        g = g.replace('Fill="%s" />' % L, 'Fill="%s" Stroke="#3A434C" StrokeThickness="12" />' % L).replace('Fill="%s" />' % M, 'Fill="%s" Stroke="#3A434C" StrokeThickness="8" />' % M)
    if plated:
        body += r(0, 0, 256, 256, PLATE, 44)
    s = glyph_share
    off = (256 - 256 * s) / 2
    body += '<Canvas RenderTransform="%s,0,0,%s,%s,%s">%s</Canvas>' % (s, s, off, off + 4 * s, g)
    return '<Canvas Width="256" Height="256">%s</Canvas>' % body


def write(name, w, h, inner, tag=None):
    t = ' Tag="%s"' % tag if tag else ''
    open(os.path.join(D, name + '.xaml'), 'w', encoding='utf-8').write('<Canvas %s Width="%d" Height="%d"%s>%s</Canvas>' % (NS, w, h, t, inner))


def place(w, h, box, plated=False, share=0.92):
    k = box / 256.0
    return '<Canvas RenderTransform="%s,0,0,%s,%s,%s">%s</Canvas>' % (k, k, (w - box) / 2.0, (h - box) / 2.0, icon(256, plated, share))


SC = '1,1.25,1.5,2,4'
write('Square44x44Logo', 44, 44, place(44, 44, 44, True, 0.86), SC)
for t in (16, 24, 32, 48, 256):
    write('Square44x44Logo.targetsize-%d' % t, t, t, place(t, t, t, True, 0.86))
    write('Square44x44Logo.targetsize-%d_altform-unplated' % t, t, t, place(t, t, t, False, 1.0))
write('Logo', 150, 150, place(150, 150, 96), SC)
write('Square71x71Logo', 71, 71, place(71, 71, 48), SC)
write('Square310x310Logo', 310, 310, place(310, 310, 180), SC)
write('Wide310x150Logo', 310, 150, place(310, 150, 96), SC)
write('StoreLogo', 50, 50, place(50, 50, 50, True, 0.86), SC)
write('SmallLogo', 30, 30, place(30, 30, 30, False, 1.0), '1,1.4,1.8')
for t in (16, 32, 48, 256):
    write('SmallLogo.targetsize-%d' % t, t, t, place(t, t, t, False, 1.0))
splash = place(620, 220, 150).replace('RenderTransform="', 'RenderTransform="', 1)
splash += '<TextBlock Canvas.Left="0" Canvas.Top="206" Width="620" TextAlignment="Center" FontFamily="Segoe UI Light" FontSize="44" Foreground="White" Text="Computer"/>'
write('SplashScreen', 620, 300, splash, SC)
write('icon', 256, 256, icon(256, True, 0.86))
print(len(os.listdir(D)), 'xaml')
