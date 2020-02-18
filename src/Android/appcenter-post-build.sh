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
    local url=github_find_asset_url
    local apkFile=find_first_Apk
    github_upload_release_asset uploadUrl=url filePath=apkFile
}

source github.sh

if [ "$AGENT_JOBSTATUS" != "Succeeded" ]; then
    github_set_status_fail
else
    set_github_processes
fi
