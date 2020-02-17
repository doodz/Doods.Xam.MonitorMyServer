#!/usr/bin/env bash
echo "=============================================================================="
echo "Task         : Report build status next to github commit."
echo "Description  : Add other repo"
echo "Author       : Doods"
echo "=============================================================================="

echo "list apk"
ls -la $APPCENTER_OUTPUT_DIRECTORY/*.apk

source github.sh

if [ "$AGENT_JOBSTATUS" != "Succeeded" ]; then
    github_set_status_fail
else
    github_set_status_success
    github_set_release
fi
