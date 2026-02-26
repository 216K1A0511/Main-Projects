$baseDir = "e:\data storage  laptop for given mani\downloads\Telegram Desktop\all\dude projects\Inmrgh-CMS"

$UTF8 = New-Object System.Text.UTF8Encoding $False

Write-Host "Replacing text in files..."
Get-ChildItem -Path $baseDir -Recurse -File | Where-Object { 
    $_.Extension -match "\.(cs|csproj|json|md|html|yml|bat|txt|js|css)$" -and $_.FullName -notmatch "\\(bin|obj|\.git)\\"
} | ForEach-Object {
    $initialContent = [IO.File]::ReadAllText($_.FullName)
    $newContent = $initialContent
    
    $newContent = $newContent.Replace("Inmrgh", "IDEAL")
    $newContent = $newContent.Replace("inmrgh", "ideal")
    $newContent = $newContent.Replace("INMRGH", "IDEAL")
    $newContent = $newContent.Replace("Inmegh", "IDEAL")
    $newContent = $newContent.Replace("inmegh", "ideal")
    $newContent = $newContent.Replace("INMEGH", "IDEAL")
    
    if ($newContent -cne $initialContent) {
        [IO.File]::WriteAllText($_.FullName, $newContent, $UTF8)
        Write-Host "Updated text in: $($_.Name)"
    }
}

Write-Host "Renaming files..."
$files = Get-ChildItem -Path $baseDir -Recurse -File | Where-Object { 
    $_.FullName -notmatch "\\(bin|obj|\.git)\\" -and $_.Name -match "(Inmrgh|Inmegh)" 
}
foreach ($f in $files) {
    $newName = $f.Name -creplace "Inmrgh", "IDEAL" -creplace "Inmegh", "IDEAL"
    Rename-Item -Path $f.FullName -NewName $newName -PassThru
    Write-Host "Renamed file to: $newName"
}

Write-Host "Renaming directories..."
$dirs = Get-ChildItem -Path $baseDir -Recurse -Directory | Where-Object { 
    $_.FullName -notmatch "\\(bin|obj|\.git)\\" -and $_.Name -match "(Inmrgh|Inmegh)" 
} | Sort-Object -Property @{Expression={$_.FullName.Length}; Descending=$true}

foreach ($d in $dirs) {
    $newName = $d.Name -creplace "Inmrgh", "IDEAL" -creplace "Inmegh", "IDEAL"
    Rename-Item -Path $d.FullName -NewName $newName -PassThru
    Write-Host "Renamed dir to: $newName"
}

Write-Host "Done."
