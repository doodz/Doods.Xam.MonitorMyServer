#!/usr/bin/env bash
echo "=============================================================================="
echo "Task         : For Xamarin Android or iOS, change the version name located in AndroidManifest.xml and Info.plist."
echo "Description  : appcenter-pre-clone.sh"
echo "Author       : Doods"
echo "=============================================================================="

ANDROID_MANIFEST_FILE=$APPCENTER_SOURCE_DIRECTORY/src/Android/Properties/AndroidManifest.xml
INFO_PLIST_FILE=$APPCENTER_SOURCE_DIRECTORY/src/iOS/Info.plist

echo ANDROID_MANIFEST_FILE
echo INFO_PLIST_FILE
