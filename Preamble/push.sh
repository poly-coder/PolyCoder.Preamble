#!/bin/bash 
dotnet pack -o bin/Release -c Release

PACKAGE_ID=$(xmllint --xpath '/Project/PropertyGroup[1]/PackageId/text()' Preamble.fsproj)
PACKAGE_VERSION=$(xmllint --xpath '/Project/PropertyGroup[1]/Version/text()' Preamble.fsproj)

echo "Publishing $PACKAGE_ID version $PACKAGE_VERSION ..."

dotnet nuget push "bin/Release/$PACKAGE_ID.$PACKAGE_VERSION.nupkg" -s https://api.nuget.org/v3/index.json -k "$1"
