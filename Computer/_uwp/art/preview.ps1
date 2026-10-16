param([string]$Dir, [string]$Out)
Add-Type -AssemblyName System.Drawing
$bmp = New-Object System.Drawing.Bitmap 1400, 900
$g = [System.Drawing.Graphics]::FromImage($bmp)
$g.Clear([System.Drawing.Color]::FromArgb(255, 30, 30, 30))
$g.FillRectangle((New-Object System.Drawing.SolidBrush ([System.Drawing.Color]::FromArgb(255, 243, 243, 243))), 0, 450, 1400, 450)
$x = 10
foreach ($t in 16, 24, 32, 48) {
  foreach ($row in @(@(10, ''), @(460, ''))) {
    foreach ($kind in @('', '_altform-unplated')) {
      $f = Join-Path $Dir ("Square44x44Logo.targetsize-$t$kind.png")
      $img = [System.Drawing.Image]::FromFile($f)
      $yy = $row[0] + ($(if ($kind) { 220 } else { 0 }))
      $g.DrawImage($img, $x, $yy, $t, $t)
      $g.InterpolationMode = [System.Drawing.Drawing2D.InterpolationMode]::NearestNeighbor
      $g.PixelOffsetMode = [System.Drawing.Drawing2D.PixelOffsetMode]::Half
      $k = [Math]::Floor(192 / $t)
      $g.DrawImage($img, $x + 60, $yy, $t * $k, $t * $k)
      $img.Dispose()
    }
  }
  $x += 280
}
$img = [System.Drawing.Image]::FromFile((Join-Path $Dir 'Square44x44Logo.targetsize-256.png')); $g.InterpolationMode = 'HighQualityBicubic'; $g.DrawImage($img, 1130, 10, 256, 256); $img.Dispose()
$bmp.Save($Out, [System.Drawing.Imaging.ImageFormat]::Png)
