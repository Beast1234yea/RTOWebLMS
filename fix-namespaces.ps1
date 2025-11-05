# Fix namespace references from RTODesktopLMS to RTOWebLMS
$projectPath = "C:\Users\nickb\Projects\RTOWebLMS"

# Get all C# files (excluding obj and bin folders)
$files = Get-ChildItem -Path $projectPath -Filter "*.cs" -Recurse | Where-Object {
    $_.FullName -notmatch "\\obj\\" -and $_.FullName -notmatch "\\bin\\"
}

$filesUpdated = 0

foreach ($file in $files) {
    $content = Get-Content -Path $file.FullName -Raw

    # Check if the file contains the old namespace
    if ($content -match "RTODesktopLMS") {
        # Replace all occurrences
        $newContent = $content -replace "RTODesktopLMS", "RTOWebLMS"

        # Write back to file
        Set-Content -Path $file.FullName -Value $newContent -NoNewline

        Write-Host "Updated: $($file.FullName)"
        $filesUpdated++
    }
}

Write-Host "`nTotal files updated: $filesUpdated"
