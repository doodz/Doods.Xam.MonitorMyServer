#!/usr/bin/env bash
echo "=============================================================================="
echo "Task         : Post build script"
echo "Description  : Use github.sd"
echo "Author       : Doods"
echo "=============================================================================="


find_first_Apk()
{
    ls $APPCENTER_OUTPUT_DIRECTORY/*.apk| sort -n | head -1
}

set_github_processes()
{
    echo "=============================================================================="
    echo "Task         : Report build status next to github commit."
    echo "Description  : Use github.sd"
    echo "Author       : Doods"
    echo "=============================================================================="
    github_set_status_success


    if [ -z "$CREATE_RELEASE_GITHUB" ]
    then
        echo "Create release in github evoided"      
    else


        echo "=============================================================================="
        echo "Task         : Create release to github commit."
        echo "Description  : Use github.sd"
        echo "Author       : Doods"
        echo "=============================================================================="
        github_set_release
        echo "Looking for the url to send a release ..."
        local url=$(github_find_upload_url)
        echo "found $url"

        echo "Looking for the apk to send ..."
        local apkFile=$(find_first_Apk)
        echo "found $apkFile"

        echo "Upload release asset ..."
        github_upload_release_asset "$url" "$apkFile"
    fi
    echo "done!"

}

# Install jq if needed
if ! brew list jq > /dev/null; then
  brew install jq
fi

source github.sh

if [ "$AGENT_JOBSTATUS" != "Succeeded" ]; then
    github_set_status_fail
else
    set_github_processes
fi