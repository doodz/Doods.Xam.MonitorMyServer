#!/usr/bin/env bash


# if find $APPCENTER_SOURCE_DIRECTORY -name '*.UITest.csproj';
# then
	# echo "Building UI test projects:"
	# find $APPCENTER_SOURCE_DIRECTORY -name '*.UITest.csproj' -exec msbuild {} \;
# else
	# echo "Can't find UI test project"
	# exit 9999
# fi
# Example: Clone a required repository
echo "=============================================================================="
echo "Task         : Clone a required repository"
echo "Description  : Add other repo"
echo "Author       : Doods"
echo "=============================================================================="
echo "print working directory :"
pwd
echo "Current APPCENTER_SOURCE_DIRECTORY is $APPCENTER_SOURCE_DIRECTORY"
cd $APPCENTER_SOURCE_DIRECTORY
# [command]/bin/bash /Users/runner/runners/2.164.8/scripts/android-xamarin-postprocess.sh /Users/runner/runners/2.164.8/work/1/a/build/*.apk
echo "APPCENTER_OUTPUT_DIRECTORY is $APPCENTER_OUTPUT_DIRECTORY"

# mkdir Doods.Frameworks.Std
# #cd Doods.Frameworks.Std
# echo "print working directory :"
# pwd
# echo "clone https://github.com/doodz/Doods.Frameworks.Std.git"

# git clone https://github.com/doodz/Doods.Frameworks.Std.git --progress

# echo "Directories ?"
# ls -la
# git submodule update
echo "=============================================================================="
echo "Task         : Update app.config"
echo "Description  : inject keys"
echo "Author       : Doods"
echo "=============================================================================="
MY_CONFIG=$APPCENTER_SOURCE_DIRECTORY/src/Android/Assets/App.config
seb -i 's/MobileCenterKey__/'$MobileCenter_Key'/' $MY_CONFIG
seb -i 's/RewardedVideoKey__/'$RewardedVideo_Key'/' $MY_CONFIG
seb -i 's/AdsKey__/'$Ads_Key'/' $MY_CONFIG
seb -i 's/AppAdsKey__/'$AppAds_Key'/' $MY_CONFIG

if [ -z "$ModeOmvOnly_Key" ]
    then
        echo "ModeOmvOnlyKey__ evoided in App.config default False"      
    else
		seb -i 's/ModeOmvOnlyKey__/'$ModeOmvOnly_Key'/' $MY_CONFIG
fi
cat MY_CONFIG