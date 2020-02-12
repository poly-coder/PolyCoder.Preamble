dotnet pack -o bin/Release -c Release
dotnet nuget push "bin\Release\PolyCoder.Preamble.%1.nupkg" -s https://api.nuget.org/v3/index.json -k "%2"
