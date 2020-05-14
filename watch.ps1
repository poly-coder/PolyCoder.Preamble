### This script is unfinished

# Setup watch for coverage change to generate new reports
$watcher = New-Object System.IO.FileSystemWatcher
$watcher.IncludeSubdirectories = $true
$watcher.Path = "."
$watcher.Filter = "coverage.cobertura.xml"
$watcher.EnableRaisingEvents = $true
$watchAction = {
  Write-Host "File change detected"
}
Register-ObjectEvent $watcher -EventName "Changed" -Action $watchAction

dotnet watch -p Preamble.Tests test /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura

