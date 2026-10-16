param([string]$Dir, [string]$Out)
Add-Type -AssemblyName PresentationCore, PresentationFramework, WindowsBase
New-Item -ItemType Directory -Force $Out | Out-Null
foreach ($f in Get-ChildItem -LiteralPath $Dir -Filter *.xaml) {
  $xaml = [IO.File]::ReadAllText($f.FullName)
  $ctx = New-Object Windows.Markup.ParserContext
  $ctx.BaseUri = New-Object Uri ($f.DirectoryName + '\')
  $el = [Windows.Markup.XamlReader]::Parse($xaml, $ctx)
  $w = [int]$el.Width; $h = [int]$el.Height
  $scales = @(1)
  if ($el.Tag) { $scales = ($el.Tag.ToString() -split ',') | ForEach-Object { [double]$_ } }
  foreach ($s in $scales) {
    $pw = [Math]::Ceiling($w * $s); $ph = [Math]::Ceiling($h * $s)
    $root = New-Object Windows.Controls.Viewbox
    $root.Width = $pw; $root.Height = $ph
    $el2 = [Windows.Markup.XamlReader]::Parse($xaml, $ctx)
    $root.Child = $el2
    $root.Measure((New-Object Windows.Size $pw, $ph))
    $root.Arrange((New-Object Windows.Rect 0, 0, $pw, $ph))
    $root.UpdateLayout()
    $bmp = New-Object Windows.Media.Imaging.RenderTargetBitmap ([int]$pw), ([int]$ph), 96, 96, ([Windows.Media.PixelFormats]::Pbgra32)
    $bmp.Render($root)
    $enc = New-Object Windows.Media.Imaging.PngBitmapEncoder
    $enc.Frames.Add([Windows.Media.Imaging.BitmapFrame]::Create($bmp))
    $name = $f.BaseName
    if ($scales.Count -gt 1) { $name = $name + '.scale-' + [int]($s * 100) }
    $fs = [IO.File]::Create((Join-Path $Out ($name + '.png')))
    $enc.Save($fs); $fs.Close()
    "$name.png $pw x $ph"
  }
}
