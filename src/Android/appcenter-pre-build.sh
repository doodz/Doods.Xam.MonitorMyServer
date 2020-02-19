#!/usr/bin/env bash
echo "=============================================================================="
echo "Task         : For Xamarin Android or iOS, change the version name located in AndroidManifest.xml and Info.plist."
echo "Description  : :/"
echo "Author       : Doods"
echo "=============================================================================="


ANDROID_MANIFEST_FILE=$APPCENTER_SOURCE_DIRECTORY/src/Android/Properties/AndroidManifest.xml
INFO_PLIST_FILE=$APPCENTER_SOURCE_DIRECTORY/src/iOS/Info.plist

if [ -e "$ANDROID_MANIFEST_FILE" ]
then
    echo "Updating version name to ${APPCENTER_BRANCH}_${APPCENTER_BUILD_ID} in AndroidManifest.xml"
    sed -i '' 's/versionName="[0-9.]*"/versionName="'${APPCENTER_BRANCH}_${APPCENTER_BUILD_ID}'"/' $ANDROID_MANIFEST_FILE

    echo "File content:"
    cat $ANDROID_MANIFEST_FILE
fi


if [ -e "$INFO_PLIST_FILE" ]
then
    echo "Updating version name to ${APPCENTER_BRANCH}_${APPCENTER_BUILD_ID} in Info.plist"
    plutil -replace CFBundleShortVersionString -string $VERSION_NAME $INFO_PLIST_FILE

    echo "File content:"
    cat $INFO_PLIST_FILE
fi