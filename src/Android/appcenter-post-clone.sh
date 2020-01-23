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
echo "print working directory :"
pwd
echo "clone https://github.com/doodz/Doods.Frameworks.Std.git"

git clone https://github.com/doodz/Doods.Frameworks.Std.git --progress

echo "Directories ?"
ls -la