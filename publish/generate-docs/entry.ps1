$DebugPreference = 'Continue'
$VerbosePreference = 'Continue'
$ErrorActionPreference = 'Stop'

$global:OminousPreference = @{
    Flavor = 'GitHub'
}
$WorkspaceRoot = Join-Path $PSScriptRoot '../../'
$WikiRoot = Join-Path $WorkspaceRoot 'wiki'
$ModuleRoot = Join-Path $WorkspaceRoot 'src' 'Ominous'
$HelpRoot = Join-Path $WorkspaceRoot 'publish' 'generate-docs'
$DLLHelpPath = Join-Path $ModuleRoot 'en-us' 'Ominous.dll-Help.xml'

$TemplateVariables = @{
    'FlavorTypes' = 'GitHub'
    'svc.url'     = 'https://github.com/soulshined/OMINOUS/blob/master'
    'wiki.url'    = 'https://github.com/soulshined/OMINOUS/wiki'
}

# 1. Clean
Remove-Item $DLLHelpPath -Force -ErrorAction SilentlyContinue
Remove-Module Ominous -Force -ErrorAction SilentlyContinue

Get-ChildItem . -Filter *.md -Recurse | % {
    $Content = Get-Content $_ -Raw

    $TemplateVariables.GetEnumerator() | % {
        $Key = $_.Key
        $Content = $Content -replace "{{[ ]*$Key[ ]*}}", $_.Value
    }

    $Content | Out-File $_ -Force -NoNewline
}

# 2. Generate things
foreach ($doc in @(
        'platyps',
        'readme'
    )) {
    $Module = Import-Module (Join-Path $ModuleRoot 'OMINOUS.psd1') -PassThru
    . (Join-Path $PSScriptRoot "_${doc}.ps1")
    Remove-Module Ominous -Force -ErrorAction SilentlyContinue
}

# 3. Individual Wiki Things
Get-ChildItem (Join-Path $PSScriptRoot 'wiki') -File | % {
    if (@('.md', '.ps1') -inotcontains $_.Extension) { return }
    $BaseName = $_.BaseName
    $WikiFileName = ($_.BaseName -split '_' | ? { $_ } | % {
            $_[0].ToString().ToUpper() + $_.Substring(1)
        }) -join '-'

    if ($_.Extension -ieq '.md') {
        $ps1 = Join-Path $PSScriptRoot 'wiki' "${BaseName}.ps1"
        if (Test-Path $ps1) {
            return
        }

        $Content = Get-Content $_ -Raw
    }
    else {
        $Content = . $_
    }

    $TemplateVariables.GetEnumerator() | % {
        $Key = $_.Key
        $Content = $Content -replace "{{ $Key }}", $_.Value
    }

    $Content = $Content.Trim() + [System.Environment]::NewLine
    $Content | Out-File (Join-Path $WikiRoot "$WikiFileName.md") -Force
}