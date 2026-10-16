param([string]$Jobs)
Add-Type -AssemblyName System.Drawing
$list = Get-Content -LiteralPath $Jobs -Raw -Encoding utf8 | ConvertFrom-Json
$jpg = [System.Drawing.Imaging.ImageCodecInfo]::GetImageEncoders() | Where-Object { $_.MimeType -eq 'image/jpeg' }
$ep = New-Object System.Drawing.Imaging.EncoderParameters 1
$ep.Param[0] = New-Object System.Drawing.Imaging.EncoderParameter ([System.Drawing.Imaging.Encoder]::Quality), 92L
$n = 0
foreach ($j in $list) {
  $dir = Split-Path -Parent $j.dest
  New-Item -ItemType Directory -Force $dir | Out-Null
  $src = [System.Drawing.Image]::FromFile($j.src)
  $bmp = New-Object System.Drawing.Bitmap 256, 256, ([System.Drawing.Imaging.PixelFormat]::Format32bppArgb)
  $g = [System.Drawing.Graphics]::FromImage($bmp)
  $g.InterpolationMode = [System.Drawing.Drawing2D.InterpolationMode]::HighQualityBicubic
  $g.SmoothingMode = [System.Drawing.Drawing2D.SmoothingMode]::HighQuality
  $g.PixelOffsetMode = [System.Drawing.Drawing2D.PixelOffsetMode]::HighQuality
  $g.Clear([System.Drawing.Color]::White)
  $m = 8
  $s = [Math]::Min((256 - 2 * $m) / $src.Width, (256 - 2 * $m) / $src.Height)
  $w = [int]($src.Width * $s); $h = [int]($src.Height * $s)
  $g.DrawImage($src, [int]((256 - $w) / 2), [int]((256 - $h) / 2), $w, $h)
  $g.Dispose(); $src.Dispose()
  if ($j.dest.ToLower().EndsWith('.jpg')) { $bmp.Save($j.dest, $jpg, $ep) } else { $bmp.Save($j.dest, [System.Drawing.Imaging.ImageFormat]::Png) }
  $bmp.Dispose()
  $n++
}
"$n images written"
