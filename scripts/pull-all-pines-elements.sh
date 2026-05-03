#!/usr/bin/env bash
set -e

DEST="Components/Pines"
mkdir -p "$DEST"

echo "→ Pulling Pines /elements/ folder..."

# Clean any old temp folder
rm -rf /tmp/pines-temp 2>/dev/null || true

# Shallow clone (fast)
git clone --depth 1 --single-branch https://github.com/thedevdojo/pines.git /tmp/pines-temp

# Copy only the elements folder
cp -r /tmp/pines-temp/elements/* "$DEST/" 2>/dev/null || true

# Cleanup
rm -rf /tmp/pines-temp

echo "✅ Done! Pines components pulled into $DEST/"
echo "Files found:"
ls -1 "$DEST" | head -15
