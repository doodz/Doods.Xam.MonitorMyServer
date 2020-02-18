#!/usr/bin/env bash
echo "=============================================================================="
echo "Task         : Report build status next to github commit."
echo "Description  : Use github.sd"
echo "Author       : Doods"
echo "=============================================================================="


find_first_Apk()
{
    ls $APPCENTER_OUTPUT_DIRECTORY/.apk| sort -n | head -1

}

set_github_processes()
{
    github_set_status_success
    github_set_release
    echo "Looking for the url to send a release"
    local url=$(github_find_asset_url)
    echo "found $url"

    echo "Looking for the apk to send"
    local apkFile=$(find_first_Apk)
    echo "found $apkFile"

    echo "Upload release asset"
    github_upload_release_asset uploadUrl=url filePath=apkFile
    echo "done!"
}

source github.sh

if [ "$AGENT_JOBSTATUS" != "Succeeded" ]; then
    github_set_status_fail
else
    set_github_processes
fi
