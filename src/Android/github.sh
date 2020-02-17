
APPCENTER_USER=thibaultherviou
APP=Monitor-my-server-android

build_url=https://appcenter.ms/users/$APPCENTER_USER/apps/$APP/build/branches/$APPCENTER_BRANCH/builds/$APPCENTER_BUILD_ID
curl_url=https://api.github.com/repos/$USER/$BUILD_REPOSITORY_NAME/statuses/$BUILD_SOURCEVERSION

echo "build_url $build_url"
echo "curl_url $curl_url"
github_set_status() {
    local status job_status
    local "${@}"

    curl -X POST https://api.github.com/repos/$USER/$BUILD_REPOSITORY_NAME/statuses/$BUILD_SOURCEVERSION -d \
        "{
            \"state\": \"$status\", 
            \"target_url\": \"$build_url\",
            \"description\": \"The build status is: $job_status!\",
            \"context\": \"continuous-integration/appcenter\"
        }" \
        -H "Authorization: token $GITHUB_TOKEN" \
        -H "Accept: application/vnd.github.v3.raw+json"
}

github_set_status_pending() {
    github_set_status status="pending" job_status="In progress"
}

github_set_status_success() {
    github_set_status status="success" job_status="$AGENT_JOBSTATUS"
}

github_set_status_fail() {
    github_set_status status="failure" job_status="$AGENT_JOBSTATUS"
}

