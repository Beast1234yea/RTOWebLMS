# Safe Work Australia - Download All Relevant COPs (PowerShell)
# Created: 2025-11-17
# Purpose: Download all Model Codes of Practice for RTO AI Tutor
# Run in PowerShell with: .\download_all_cops.ps1

# Set download directory to current script location
$DOWNLOAD_DIR = Split-Path -Parent $MyInvocation.MyCommand.Path
Write-Host "Downloading to: $DOWNLOAD_DIR"

# Create subdirectories
New-Item -ItemType Directory -Force -Path "$DOWNLOAD_DIR\Core_COPs" | Out-Null
New-Item -ItemType Directory -Force -Path "$DOWNLOAD_DIR\Specialized_COPs" | Out-Null
New-Item -ItemType Directory -Force -Path "$DOWNLOAD_DIR\Industry_Specific" | Out-Null

Write-Host "=========================================="
Write-Host "Safe Work Australia COP Download Script"
Write-Host "=========================================="
Write-Host ""

# Function to download files
function Download-COP {
    param(
        [string]$Url,
        [string]$Output,
        [string]$Description
    )

    Write-Host "Downloading: $Description" -ForegroundColor Cyan
    Write-Host "URL: $Url" -ForegroundColor Gray

    try {
        # Use .NET WebClient for download (more reliable than Invoke-WebRequest for large files)
        $webClient = New-Object System.Net.WebClient
        $webClient.DownloadFile($Url, $Output)
        Write-Host "✓ Downloaded: $Output" -ForegroundColor Green
        Write-Host ""
        return $true
    }
    catch {
        Write-Host "✗ Failed: $Description" -ForegroundColor Red
        Write-Host "  Error: $_" -ForegroundColor Red
        Write-Host "  Please download manually from: $Url" -ForegroundColor Yellow
        Write-Host ""
        return $false
    }
}

# Track success/failure
$successCount = 0
$failCount = 0

# CORE COPs (Priority 1)
Write-Host "=== CORE CODES OF PRACTICE ===" -ForegroundColor Yellow
Write-Host ""

if (Download-COP `
    -Url "https://www.safeworkaustralia.gov.au/sites/default/files/2024-11/model_code_of_practice-how_to_manage_work_health_and_safety_risks-nov24.pdf" `
    -Output "$DOWNLOAD_DIR\Core_COPs\01_How_to_Manage_WHS_Risks_Nov2024.pdf" `
    -Description "How to Manage WHS Risks (Nov 2024)") { $successCount++ } else { $failCount++ }

if (Download-COP `
    -Url "https://www.safeworkaustralia.gov.au/sites/default/files/2023-12/dec_2023_-_code_of_practice_-_managing_the_risks_of_plant_in_the_workplace.pdf" `
    -Output "$DOWNLOAD_DIR\Core_COPs\02_Managing_Risks_of_Plant_Dec2023.pdf" `
    -Description "Managing Risks of Plant (Dec 2023)") { $successCount++ } else { $failCount++ }

if (Download-COP `
    -Url "https://www.safeworkaustralia.gov.au/sites/default/files/2022-10/Model Code of Practice - Managing the Risk of Falls at Workplaces 21102022_0.pdf" `
    -Output "$DOWNLOAD_DIR\Core_COPs\03_Managing_Risk_of_Falls_Oct2022.pdf" `
    -Description "Managing Risk of Falls (Oct 2022)") { $successCount++ } else { $failCount++ }

if (Download-COP `
    -Url "https://www.safeworkaustralia.gov.au/sites/default/files/2024-11/model_code_of_practice-confined_spaces-nov24.pdf" `
    -Output "$DOWNLOAD_DIR\Core_COPs\04_Confined_Spaces_Nov2024.pdf" `
    -Description "Confined Spaces (Nov 2024)") { $successCount++ } else { $failCount++ }

# INDUSTRY SPECIFIC COPs (Priority 3)
Write-Host "=== INDUSTRY SPECIFIC CODES OF PRACTICE ===" -ForegroundColor Yellow
Write-Host ""

if (Download-COP `
    -Url "https://www.safeworkaustralia.gov.au/sites/default/files/2024-11/model_code_of_practice-managing_the_work_environment_and_facilities-nov24.pdf" `
    -Output "$DOWNLOAD_DIR\Industry_Specific\11_Work_Environment_Facilities_Nov2024.pdf" `
    -Description "Work Environment and Facilities (Nov 2024)") { $successCount++ } else { $failCount++ }

if (Download-COP `
    -Url "https://www.safeworkaustralia.gov.au/sites/default/files/2024-11/model_code_of_practice-safe_design_of_structures-nov24.pdf" `
    -Output "$DOWNLOAD_DIR\Industry_Specific\15_Safe_Design_Structures_Nov2024.pdf" `
    -Description "Safe Design of Structures (Nov 2024)") { $successCount++ } else { $failCount++ }

if (Download-COP `
    -Url "https://www.safeworkaustralia.gov.au/system/files/documents/1705/mcop-excavation-work-v3.pdf" `
    -Output "$DOWNLOAD_DIR\Industry_Specific\14_Excavation_Work_2018.pdf" `
    -Description "Excavation Work (2018)") { $successCount++ } else { $failCount++ }

# SPECIALIZED COPs - These may require browser download
Write-Host "=== ADDITIONAL SPECIALIZED COPs ===" -ForegroundColor Yellow
Write-Host ""
Write-Host "The following COPs may require manual browser download:" -ForegroundColor Yellow
Write-Host "  - First Aid in the Workplace (2019)"
Write-Host "  - Hazardous Manual Tasks (2018)"
Write-Host "  - Managing Risks of Hazardous Chemicals (2022)"
Write-Host "  - Managing Noise and Preventing Hearing Loss (2024)"
Write-Host "  - Managing Electrical Risks (2022)"
Write-Host "  - Construction Work (2021)"
Write-Host "  - And others listed in SAFE_WORK_AUSTRALIA_COP_INDEX.md"
Write-Host ""
Write-Host "Please visit: https://www.safeworkaustralia.gov.au/law-and-regulation/codes-practice" -ForegroundColor Cyan
Write-Host ""

# Summary
Write-Host "=========================================="
Write-Host "Download Summary"
Write-Host "=========================================="
Write-Host ""
Write-Host "✓ Successfully downloaded: $successCount files" -ForegroundColor Green
Write-Host "✗ Failed downloads: $failCount files" -ForegroundColor $(if ($failCount -gt 0) { 'Yellow' } else { 'Green' })
Write-Host ""
Write-Host "Downloaded files are organized in:" -ForegroundColor Cyan
Write-Host "  Core_COPs/           - Essential codes for all training"
Write-Host "  Specialized_COPs/    - Specific hazard/industry codes"
Write-Host "  Industry_Specific/   - Construction, excavation, etc."
Write-Host ""
Write-Host "For complete list of all 18 relevant COPs with direct links, see:" -ForegroundColor Cyan
Write-Host "  SAFE_WORK_AUSTRALIA_COP_INDEX.md"
Write-Host ""

if ($failCount -gt 0) {
    Write-Host "For failed downloads, please:" -ForegroundColor Yellow
    Write-Host "  1. Visit https://www.safeworkaustralia.gov.au/law-and-regulation/codes-practice"
    Write-Host "  2. Search for the specific COP by name"
    Write-Host "  3. Download manually to the appropriate subfolder"
    Write-Host "  4. Or check SAFE_WORK_AUSTRALIA_COP_INDEX.md for direct PDF links"
    Write-Host ""
}

Write-Host "Press any key to continue..." -ForegroundColor Gray
$null = $Host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown")
