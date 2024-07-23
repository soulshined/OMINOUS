$DebugPreference = 'Continue'
$VerbosePreference = 'Continue'
$ErrorActionPreference = 'Stop'

$Root = Join-Path $PSScriptRoot '../' 'src' 'Ominous'
# $Module = Import-Module (Join-Path $Root 'OMINOUS.psd1') -PassThru

Publish-Module -Path $Root -NuGetApiKey $Env:NuGetApiKey

