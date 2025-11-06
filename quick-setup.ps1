#!/usr/bin/env pwsh
# Quick setup script to create test lesson with 3D forklift model

Write-Host "🚛 RTOWebLMS - Quick Database Setup with 3D Forklift Model" -ForegroundColor Cyan
Write-Host "=========================================================" -ForegroundColor Cyan

Set-Location "C:\Users\nickb\Projects\RTOWebLMS"

# Check if app is running locally
$process = Get-Process -Name "RTOWebLMS" -ErrorAction SilentlyContinue
if ($process) {
    Write-Host "ℹ️  Stopping existing RTOWebLMS process..." -ForegroundColor Yellow
    Stop-Process -Name "RTOWebLMS" -Force
    Start-Sleep -Seconds 2
}

Write-Host "📦 Building application..." -ForegroundColor Green
dotnet build --configuration Release

if ($LASTEXITCODE -eq 0) {
    Write-Host "✅ Build successful!" -ForegroundColor Green
    
    Write-Host " Starting application..." -ForegroundColor Green
    Write-Host "📍 Access at: http://localhost:8080" -ForegroundColor Cyan
    Write-Host "🎯 3D Model Test: http://localhost:8080/test-3d.html" -ForegroundColor Cyan
    Write-Host "" 
    Write-Host "Press Ctrl+C to stop the application" -ForegroundColor Yellow
    
    # Start the app
    dotnet run
} else {
    Write-Host "❌ Build failed. Please check the errors above." -ForegroundColor Red
    exit 1
}