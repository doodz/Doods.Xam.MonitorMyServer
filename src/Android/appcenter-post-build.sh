#!/usr/bin/env bash
echo "=============================================================================="
echo "Task         : Report build status next to github commit."
echo "Description  : Add other repo"
echo "Author       : Doods"
echo "=============================================================================="

source github.sh
echo "debug ==>"

echo "USER $USER"
echo "APPCENTER_BRANCH $APPCENTER_BRANCH"
echo "APPCENTER_BUILD_ID $APPCENTER_BUILD_ID"
echo "GITHUB_TOKEN $GITHUB_TOKEN"

echo "<== debug"

if [ "$AGENT_JOBSTATUS" != "Succeeded" ]; then
    github_set_status_fail
else
    github_set_status_success
    github_set_release
fi
