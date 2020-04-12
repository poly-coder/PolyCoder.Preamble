#!/bin/bash 
bash ./pack.sh

PACKAGE_ID=$(xmllint --xpath '/Project/PropertyGroup[1]/PackageId/text()' Preamble.fsproj)
PACKAGE_VERSION=$(xmllint --xpath '/Project/PropertyGroup[1]/Version/text()' Preamble.fsproj)

echo "Publishing $PACKAGE_ID version $PACKAGE_VERSION ..."

dotnet nuget push "bin/Release/$PACKAGE_ID.$PACKAGE_VERSION.nupkg" -s locals
