#!/usr/bin/env bash
# ================================================================
# razorhat-clean-rider-cache.sh
# Nukes ONLY Rider/JetBrains Source Generator temp cache.
# 100% safe — does NOT touch your obj/, bin/, or real build outputs.
# ================================================================

echo "🔥 RazorHAT → Nuking Rider's stale SourceGeneratedDocuments cache..."

# Target the exact temp folder Rider uses (works for any user ID)
find /tmp -maxdepth 1 -name "JetBrainsPerUserTemp-*" -type d 2>/dev/null | while read -r tempdir; do
    cache_path="$tempdir/SourceGeneratedDocuments"
    if [ -d "$cache_path" ]; then
        echo "   🗑️  Clearing → $cache_path"
        rm -rf "$cache_path"/*
    fi
done

echo "✅ Rider temp cache obliterated."
echo "   (Your real generated files in obj/Generated are untouched)"
echo ""
echo "Pro tip: Run this anytime Rider is showing stale <badge_44> nonsense."
