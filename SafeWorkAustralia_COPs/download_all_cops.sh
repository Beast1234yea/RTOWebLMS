#!/bin/bash
# Safe Work Australia - Download All Relevant COPs
# Created: 2025-11-17
# Purpose: Download all Model Codes of Practice for RTO AI Tutor

# Set download directory
DOWNLOAD_DIR="$(cd "$(dirname "$0")" && pwd)"
echo "Downloading to: $DOWNLOAD_DIR"

# Create subdirectories
mkdir -p "$DOWNLOAD_DIR/Core_COPs"
mkdir -p "$DOWNLOAD_DIR/Specialized_COPs"
mkdir -p "$DOWNLOAD_DIR/Industry_Specific"

echo "=========================================="
echo "Safe Work Australia COP Download Script"
echo "=========================================="
echo ""

# Function to download with retry
download_file() {
    local url="$1"
    local output="$2"
    local description="$3"

    echo "Downloading: $description"
    echo "URL: $url"

    if wget -O "$output" "$url" 2>&1 | grep -q "200 OK\|saved"; then
        echo "✓ Downloaded: $output"
        echo ""
        return 0
    else
        echo "✗ Failed: $description"
        echo "  Please download manually from: $url"
        echo ""
        return 1
    fi
}

# CORE COPs (Priority 1)
echo "=== CORE CODES OF PRACTICE ==="
echo ""

download_file \
    "https://www.safeworkaustralia.gov.au/sites/default/files/2024-11/model_code_of_practice-how_to_manage_work_health_and_safety_risks-nov24.pdf" \
    "$DOWNLOAD_DIR/Core_COPs/01_How_to_Manage_WHS_Risks_Nov2024.pdf" \
    "How to Manage WHS Risks (Nov 2024)"

download_file \
    "https://www.safeworkaustralia.gov.au/sites/default/files/2023-12/dec_2023_-_code_of_practice_-_managing_the_risks_of_plant_in_the_workplace.pdf" \
    "$DOWNLOAD_DIR/Core_COPs/02_Managing_Risks_of_Plant_Dec2023.pdf" \
    "Managing Risks of Plant (Dec 2023)"

download_file \
    "https://www.safeworkaustralia.gov.au/sites/default/files/2022-10/Model%20Code%20of%20Practice%20-%20Managing%20the%20Risk%20of%20Falls%20at%20Workplaces%2021102022_0.pdf" \
    "$DOWNLOAD_DIR/Core_COPs/03_Managing_Risk_of_Falls_Oct2022.pdf" \
    "Managing Risk of Falls (Oct 2022)"

download_file \
    "https://www.safeworkaustralia.gov.au/sites/default/files/2024-11/model_code_of_practice-confined_spaces-nov24.pdf" \
    "$DOWNLOAD_DIR/Core_COPs/04_Confined_Spaces_Nov2024.pdf" \
    "Confined Spaces (Nov 2024)"

# SPECIALIZED COPs (Priority 2)
echo "=== SPECIALIZED CODES OF PRACTICE ==="
echo ""

# Note: Some URLs may require browser access or may be landing pages
# If download fails, the script will provide the URL for manual download

echo "Note: Some specialized COPs may require manual download from Safe Work Australia website"
echo "Visit: https://www.safeworkaustralia.gov.au/law-and-regulation/codes-practice"
echo ""

# INDUSTRY SPECIFIC COPs (Priority 3)
echo "=== INDUSTRY SPECIFIC CODES OF PRACTICE ==="
echo ""

download_file \
    "https://www.safeworkaustralia.gov.au/sites/default/files/2024-11/model_code_of_practice-managing_the_work_environment_and_facilities-nov24.pdf" \
    "$DOWNLOAD_DIR/Industry_Specific/11_Work_Environment_Facilities_Nov2024.pdf" \
    "Work Environment and Facilities (Nov 2024)"

download_file \
    "https://www.safeworkaustralia.gov.au/sites/default/files/2024-11/model_code_of_practice-safe_design_of_structures-nov24.pdf" \
    "$DOWNLOAD_DIR/Industry_Specific/15_Safe_Design_Structures_Nov2024.pdf" \
    "Safe Design of Structures (Nov 2024)"

download_file \
    "https://www.safeworkaustralia.gov.au/system/files/documents/1705/mcop-excavation-work-v3.pdf" \
    "$DOWNLOAD_DIR/Industry_Specific/14_Excavation_Work_2018.pdf" \
    "Excavation Work (2018)"

echo ""
echo "=========================================="
echo "Download Summary"
echo "=========================================="
echo ""
echo "✓ Core COPs should be downloaded to: Core_COPs/"
echo "✓ Specialized COPs: Specialized_COPs/ (some may require manual download)"
echo "✓ Industry Specific COPs: Industry_Specific/"
echo ""
echo "For any failed downloads, please visit:"
echo "https://www.safeworkaustralia.gov.au/law-and-regulation/codes-practice"
echo ""
echo "See SAFE_WORK_AUSTRALIA_COP_INDEX.md for complete list and direct links"
echo ""
