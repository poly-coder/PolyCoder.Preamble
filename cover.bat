call dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura
call reportgenerator -reports:**/coverage.cobertura.xml -targetdir:Report -reporttypes:HtmlInline_AzurePipelines;Cobertura
