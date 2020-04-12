#!/bin/bash 
dotnet build -c Release
dotnet pack -o bin/Release -c Release
