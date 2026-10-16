param([string]$Dir, [string]$Out, [int]$Cell = 150, [int]$Cols = 8, [string]$Filter = '*', [string]$Bg = 'White')
Add-Type -AssemblyName System.Drawing
$files = Get-ChildItem -LiteralPath $Dir -File | Where-Object { $_.Name -like $Filter -and $_.Extension -match '\.(png|jpe?g|gif|bmp)$' } | Sort-Object Name
$rows = [Math]::Ceiling($files.Count / $Cols)
$bmp = New-Object System.Drawing.Bitmap ($Cols * $Cell), ($rows * ($Cell + 16))
$g = [System.Drawing.Graphics]::FromImage($bmp)
$g.Clear([System.Drawing.Color]::FromName($Bg))
$font = New-Object System.Drawing.Font 'Segoe UI', 7
$i = 0
foreach ($f in $files) {
  $x = ($i % $Cols) * $Cell; $y = [Math]::Floor($i / $Cols) * ($Cell + 16)
  try {
    $img = [System.Drawing.Image]::FromFile($f.FullName)
    $s = [Math]::Min(($Cell - 4) / $img.Width, ($Cell - 4) / $img.Height)
    $w = [int]($img.Width * $s); $h = [int]($img.Height * $s)
    $g.DrawImage($img, $x + 2, $y + 2, $w, $h)
    $img.Dispose()
  } catch { }
  $g.DrawString($f.BaseName, $font, [System.Drawing.Brushes]::Gray, $x + 2, $y + $Cell)
  $i++
}
$bmp.Save($Out, [System.Drawing.Imaging.ImageFormat]::Png)
"$($files.Count) images -> $Out"
