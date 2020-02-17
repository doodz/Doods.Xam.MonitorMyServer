
APPCENTER_USER=thibaultherviou
APP=Monitor-my-server-android

build_url=https://appcenter.ms/users/$APPCENTER_USER/apps/$APP/build/branches/$APPCENTER_BRANCH/builds/$APPCENTER_BUILD_ID
curl_url=https://api.github.com/repos/doodz/$BUILD_REPOSITORY_NAME/statuses/$BUILD_SOURCEVERSION

echo "build_url $build_url"
echo "curl_url $curl_url"
github_set_status() {
    local status job_status
    local "${@}"

echo "state: $status"
echo "target_url :  $build_url"
echo "description :  The build status is: $job_status!"
echo "context :  continuous-integration/appcenter"

    curl -X POST https://api.github.com/repos/doodz/$BUILD_REPOSITORY_NAME/statuses/$BUILD_SOURCEVERSION -d \
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

github_set_release(){
	curl -X POST https://api.github.com/repos/doodz/$BUILD_REPOSITORY_NAME/releases -d \
        "{
            \"tag_name\": \"$APPCENTER_BUILD_ID\", 
            \"target_commitish\": \"$APPCENTER_BRANCH\",
            \"name\": \"$APPCENTER_BUILD_ID\",
            \"body\": \"Description of the release\",
			\"body\":false,
			\"prerelease\":false
        }" \
        -H "Authorization: token $GITHUB_TOKEN" \
        -H "Accept: application/vnd.github.v3.raw+json"
}
