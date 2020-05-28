#!/usr/bin/env bash
echo "=============================================================================="
echo "Task         : For Xamarin Android or iOS, change the version name located in AndroidManifest.xml and Info.plist."
echo "Description  : pre build scrip for updating ndroidManifest.xml and Info.plist with appcenter build_id and appcenter_branch"
echo "Author       : Doods"
echo "=============================================================================="

echo "##[warning][Pre-Build Action] - Lets do some Pre build transformations..."
# Declare local script variables
SCRIPT_ERROR=0
# Define the files to manipulate
ANDROID_MANIFEST_FILE=$APPCENTER_SOURCE_DIRECTORY/src/Android/Properties/AndroidManifest.xml
INFO_PLIST_FILE=$APPCENTER_SOURCE_DIRECTORY/src/iOS/Info.plist
ANDROID_MAINACTIVITY_FILE=${APPCENTER_SOURCE_DIRECTORY}/src/Android/MainActivity.cs
echo "##[warning][Pre-Build Action] - Checking if all files and environment variables are available..."
if [ -z "${APP_DISPLAY_NAME}" ]
then
    echo "##[error][Pre-Build Action] - APP_DISPLAY_NAME variable needs to be defined in App Center!!!"
    let "SCRIPT_ERROR += 1"
    else
    echo "##[warning][Pre-Build Action] - APP_DISPLAY_NAME variable - oK!"
fi

if [ -e "${INFO_PLIST_FILE}" ]
then
    echo "##[warning][Pre-Build Action] - Info.plist file found - oK!"
else
    echo "##[error][Pre-Build Action] - Info.plist file not found!"
    # let "SCRIPT_ERROR += 1"
fi

if [ -e "${ANDROID_MAINACTIVITY_FILE}" ]
then
    echo "##[warning][Pre-Build Action] - MainActivity file found - oK!"
else
    echo "##[error][Pre-Build Action] - MainActivity file not found!"
    let "SCRIPT_ERROR += 1"
fi


if [ ${SCRIPT_ERROR} -gt 0 ]
then
    echo "##[error][Pre-Build Action] - There are ${SCRIPT_ERROR} errors."
    echo "##[error][Pre-Build Action] - Fix them and try again..."
    exit 1 # this will kill the build
    # exit # this will exit this script, but continues building
    else
    echo ""
fi

echo "##[warning][Pre-Build Action] - There are ${SCRIPT_ERROR} errors."
echo "##[warning][Pre-Build Action] - Now everything is checked, lets change the app display name on iOS and Android..."

######################## Changes on Android
if [ -e "$ANDROID_MANIFEST_FILE" ]
then
    echo "##[command][Pre-Build Action] - Changing the App display name on Android to: ${APP_DISPLAY_NAME} "
    sed -i '' "s/Label=\"[-a-zA-Z0-9_ ]*\"/Label=\"${APP_DISPLAY_NAME}\"/" ${ANDROID_MAINACTIVITY_FILE}

    echo "##[command][Pre-Build Action] - Changing the version name on Android to: ${APPCENTER_BRANCH}_${APPCENTER_BUILD_ID} "    
    sed -i '' 's/versionName=".*"/versionName="'${APPCENTER_BRANCH}_${APPCENTER_BUILD_ID}'"/' $ANDROID_MANIFEST_FILE

    if [ -z "$PACKAGE_NAME" ]
        then
         echo "No PACKAGE_NAME key found"  
        else
           
            echo "Updating package name to ${PACKAGE_NAME}"      
            #sed -i '' 's/package="com.doods.monitormyserver"/package="'${PACKAGE_NAME}'"/' $ANDROID_MANIFEST_FILE
            sed -i '' 's/package="[^"]*"/package="'$PACKAGE_NAME'"/' $ANDROID_MANIFEST_FILE
    fi

     if [ -z "$PACKAGE_ICON" ]
        then
         echo "No PACKAGE_ICON key found"  
        else
           
            echo "Updating package name to ${PACKAGE_ICON}"      
            #sed -i '' 's/package="com.doods.monitormyserver"/package="'${PACKAGE_NAME}'"/' $ANDROID_MANIFEST_FILE
            sed -i '' 's/android:icon="[^"]*"/android:icon="@drawable\/'$PACKAGE_ICON'"/' $ANDROID_MANIFEST_FILE
    fi
    echo "##[section][Pre-Build Action] - MainActivity.cs File content:"
    cat ${ANDROID_MANIFEST_FILE}
    echo "##[section][Pre-Build Action] - MainActivity.cs EOF"
fi
if [ -e "${ANDROID_MAINACTIVITY_FILE}" ]
then
    echo "##[command][Pre-Build Action] - Changing the App display name on Android to: ${APP_DISPLAY_NAME} "
    sed -i '' "s/Label = \"[-a-zA-Z0-9_ ]*\"/Label = \"${APP_DISPLAY_NAME}\"/" ${ANDROID_MAINACTIVITY_FILE}


    if [ -z "$PACKAGE_ICON" ]
        then
         echo "No PACKAGE_ICON key found"  
        else
           
            echo "Updating package name to ${PACKAGE_ICON}"      
            #sed -i '' 's/package="com.doods.monitormyserver"/package="'${PACKAGE_NAME}'"/' $ANDROID_MANIFEST_FILE
            sed -i '' 's/Icon="[^"]*"/Icon="@drawable\/'$PACKAGE_ICON'"/' $ANDROID_MANIFEST_FILE
    fi
    echo "##[section][Pre-Build Action] - MainActivity.cs File content:"
    cat ${ANDROID_MAINACTIVITY_FILE}
    echo "##[section][Pre-Build Action] - MainActivity.cs EOF"
fi

######################## Changes on iOS
#if [ -e "$INFO_PLIST_FILE" ]
#then
#     echo "##[command][Pre-Build Action] - Changing the App display name on iOS to: $APP_DISPLAY_NAME "
#    plutil -replace CFBundleShortVersionString -string $VERSION_NAME $INFO_PLIST_FILE
#
#    echo "##[section][Pre-Build Action] - Info.plist File content:"
#    cat $INFO_PLIST_FILE
#    echo "##[section][Pre-Build Action] - Info.plist EOF"
#fi
