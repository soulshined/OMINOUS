param([string] $ReleaseType)

$DebugPreference = 'Continue'
$ErrorActionPreference = 'Stop'

dotnet test (Join-Path $PSScriptRoot '..')

$Date = Get-Date
$ReleaseTypes = 'Prerelease', 'Major', 'Minor', 'Patch'
$DefaultReleaseType = 3
$ModulePath = Join-Path $PSScriptRoot '..' "src" "Ominous" "OMINOUS.psd1"

for ($i = 0; $i -lt $ReleaseTypes.Count; $i++) {
    if ($ReleaseTypes[$i] -ieq $ReleaseType) {
        $DefaultReleaseType = $i
        break
    }
}

$Prerelease = $DefaultReleaseType -eq 0 ? ('update' + $Date.Day + $Date.Month + $Date.Year)  : $null

$Choice = $ReleaseType ? $DefaultReleaseType : $Host.UI.PromptForChoice('Update Manifest', 'Release Type', $ReleaseTypes, $DefaultReleaseType)

$NewVersion = switch ($Choice) {
    1 { [version]::new(($Version.Major + 1), 0, 0) }
    2 { [version]::new($Version.Major, ($Version.Minor + 1), 0) }
    3 { [version]::new($Version.Major, $Version.Minor, ($Version.Build + 1)) }
    Default { $Version }
}

$Dll = [System.Reflection.Assembly]::Load([IO.File]::ReadAllBytes((Join-Path $PSScriptRoot '..' 'src' 'Ominous' 'bin' 'Release' 'Ominous.dll' -Resolve)))
$Version = $Dll.GetName().Version
"DLL version: {0} => $NewVersion" -f $Version | Write-Debug

if ($ReleaseType -ieq 'Prerelease') {
    dotnet build (Join-Path $PSScriptRoot '..' 'src' 'Ominous' 'Ominous.csproj') --configuration Release /p:Version=$NewVersion /p:VersionSuffix=$Prerelease
}
else {
    dotnet build (Join-Path $PSScriptRoot '..' 'src' 'Ominous' 'Ominous.csproj') --configuration Release /p:Version=$NewVersion
}

$ManifestData = @{
    Path              = $ModulePath
    GUID              = 'f90f3879-126c-4338-a7d5-5649b5775b49'
    Author            = 'David Freer'
    Copyright         = '(c) David Freer. All rights reserved.'
    RootModule        = 'bin\Release\Ominous.dll'
    RequiredModules   = @('Init\Init.psm1')
    ScriptsToProcess  = @('accelerators.ps1')
    PowerShellVersion = '7.4.0'
    Description       = 'OMINOUS is a markdown generator for PowerShell. Use the commands to write markdown in a fluent manner or mnemonically invoking conventions, like H1, H2, Table'
    IconUri           = 'https://raw.githubusercontent.com/soulshined/OMINOUS/blob/master/ominous.png'
    Prerelease        = $Prerelease ?? ''
    ModuleVersion     = $NewVersion
    ProjectUri        = 'https://github.com/soulshined/OMINOUS'
    Tags              = @('Markdown', 'Flavor', 'Callout', 'Admonition')
    HelpInfoUri       = 'https://github.com/soulshined/OMINOUS/wiki'
    ReleaseNotes      = 'https://github.com/soulshined/OMINOUS/CHANGELOG.md'
}

$ManifestData

Update-ModuleManifest @ManifestData

(Get-Content $ModulePath -Raw) | % {
    $Content = $_
    'FunctionsToExport', 'CmdletsToExport', 'AliasesToExport' | % {
        $Content = $Content -replace "$_ = @()", '# $0'
    }
    $Content
} | Invoke-Formatter | Out-File $ModulePath -Force

. (Join-Path $PSScriptRoot 'generate-docs' entry.ps1)

# git checkout "release/$NewVersion"
# git add --all
# git commit -m "[RELEASE] $NewVersion"
# git push origin "release/$NewVersion"