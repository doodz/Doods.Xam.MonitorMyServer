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
echo "=============================================================================="
echo "Task         : Update app.config"
echo "Description  : inject keys"
echo "Author       : Doods"
echo "=============================================================================="
MY_CONFIG=$APPCENTER_SOURCE_DIRECTORY/src/Android/Assets/App.config
echo MobileCenterKey__
sed -i 's/MobileCenterKey__/'$MobileCenter_Key'/' $MY_CONFIG
echo RewardedVideoKey__
sed -i 's/RewardedVideoKey__/'$RewardedVideo_Key'/' $MY_CONFIG
echo AdsKey__
sed -i 's/AdsKey__/'$Ads_Key'/' $MY_CONFIG
echo AppAdsKey__
sed -i 's/AppAdsKey__/'$AppAds_Key'/' $MY_CONFIG

if [ -z "$ModeOmvOnly_Key" ]
    then
        echo "ModeOmvOnlyKey__ evoided in App.config default False"      
    else
		sed -i 's/ModeOmvOnlyKey__/'$ModeOmvOnly_Key'/' $MY_CONFIG
fi
cat $MY_CONFIG