APPCENTER_USER=thibaultherviou
APP=Monitor-my-server-android
RELEASE_RESULT_FILE=SetReleaseResult.txt
APK_NAME=com.doods.monitormyserver.apk

build_url=https://appcenter.ms/users/$APPCENTER_USER/apps/$APP/build/branches/$APPCENTER_BRANCH/builds/$APPCENTER_BUILD_ID
curl_url=https://api.github.com/repos/doodz/$BUILD_REPOSITORY_NAME/statuses/$BUILD_SOURCEVERSION

github_set_status() {
    local status job_status
    local "${@}"

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
            \"tag_name\": \"${BUILD_REPOSITORY_NAME}_${APPCENTER_BRANCH}_${APPCENTER_BUILD_ID}\", 
            \"target_commitish\": \"$APPCENTER_BRANCH\",
            \"name\": \"${BUILD_REPOSITORY_NAME}_${APPCENTER_BRANCH}_${APPCENTER_BUILD_ID}\",
            \"body\": \"Description of the release\",
            \"draft\": false,
            \"prerelease\": false
        }" \
        -H "Authorization: token $GITHUB_TOKEN" \
        -H "Accept: application/vnd.github.v3.raw+json" \
        > $RELEASE_RESULT_FILE 
}

github_upload_release_asset()
{
    local uploadUrl=$1
    local filePath=$2
    local filename=$(basename "$filePath")
    local buildUrl="${uploadUrl}?name=${filename}"
    echo "My build url for asset $buildUrl"
    echo "My file path to upload $filePath"
    curl -X POST $buildUrl
        -H "Authorization: token $GITHUB_TOKEN" \
        -H "Content-Type: application/octet-stream"
        --data-binary @"$filePath"
}


github_find_asset_url()
{
    cat $RELEASE_RESULT_FILE  | grep "assets_url" | awk -F "\"assets_url\":" '{print $2}' | sed 's/",//'|  sed 's/"//'
}
