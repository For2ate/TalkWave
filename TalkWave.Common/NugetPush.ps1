$projectPath = ".\TalkWave.Common\TalkWave.Common.csproj"       
$nugetSource = "https://api.nuget.org/v3/index.json"
$outputDir = ".\TalkWave.Common\Release"

$packageVersion = Read-Host -Prompt "Enter the package version (e.g. 1.0.0)"

$nugetApiKey = Read-Host -Prompt "Enter your NuGet API key" 

Write-Host "Building the project..." -ForegroundColor Green
dotnet clean $projectPath -c Release
dotnet build $projectPath -c Release -p:Version=$packageVersion

Write-Host "Creating NuGet package..." -ForegroundColor Green
dotnet pack $projectPath -c Release -o $outputDir -p:Version=$packageVersion

$nugetPackage = Get-ChildItem -Path $outputDir -Filter *.nupkg | Sort-Object LastWriteTime -Descending | Select-Object -First 1

if ($nugetPackage) {

    Write-Host "NuGet package found: $($nugetPackage.FullName)" -ForegroundColor Green
    Write-Host "Publishing NuGet package..." -ForegroundColor Green
    dotnet nuget push $nugetPackage.FullName --api-key $nugetApiKey --source $nugetSource

} else {

    Write-Host "No NuGet package found in the output directory." -ForegroundColor Red

}

Read-Host