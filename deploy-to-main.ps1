#!/usr/bin/env pwsh
# Push current master branch to main for Railway deployment

Set-Location "C:\Users\nickb\Projects\RTOWebLMS"

Write-Host "Current branch and status:"
git status --short
git log --oneline -3

Write-Host "`nSwitching to main branch:"
git checkout main

Write-Host "`nMerging master into main:"
git merge master --no-edit

Write-Host "`nPushing to origin main:"
git push origin main --force

Write-Host "`nDone! Railway should now deploy the latest commits with 3D forklift model."