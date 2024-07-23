function Remove-ContentSection([string]$Content, [string]$header) {

    $splits = $Content -split $header

    $level = $header.Trim().Split(" ", 2)[0]

    $afterLines = $splits[1] -split "`n"
    $length = 0

    for ($i = 0; $i -lt $afterLines.Count; $i++) {
        $line = $afterLines[$i]
        if ($line.Trim().StartsWith($level) -or $line.Trim().StartsWith($level.Substring(1))) {
            $length = ($afterLines[0..($i - 1)] -join "`n").ToString().Length
            break
        }
    }

    @{
        Before  = $splits[0].Trim()
        Content = $splits[1].Substring(0, $length)
        After   = $splits[1].Substring($length).Trim()
    }
}

$Module.ExportedCmdlets.Values | % {
    $CommandName = $_.Name
    $MnemonicAliases = (((Get-Alias -Definition $CommandName -ErrorAction SilentlyContinue) ?? @() | Sort-Object | % {
                '`' + $_ + '`'
            }) ?? "") -join ', '

    $Config = @{
        Command               = $CommandName
        AlphabeticParamsOrder = $false
        ExcludeDontShow       = $true
        Encoding              = [System.Text.Encoding]::UTF8
        UseFullTypeName       = $true
        OutputFolder          = (Join-Path $HelpRoot 'cmdlets')
        Force                 = $true
        NoMetadata            = $false
    }

    $File = New-MarkdownHelp @Config
    $Content = ($File | gc -Raw).ReplaceLineEndings("`n")

    $Removed = Remove-ContentSection $Content "### -ProgressAction"
    $Content = $Removed.Before + "`n" + $Removed.After

    $Removed = Remove-ContentSection $Content "### Example 1"
    $Content = $Removed.Before + "`n{{ Fill in the Examples }}`n`n" + $Removed.After

    $Removed = Remove-ContentSection $Content "## Syntax"
    $Syntax = ($Removed.Content -split "``````") -replace "`n", ""
    $Content = $Removed.Before + "`n## Syntax`n```````n" + $Syntax.Trim().Replace('[-ProgressAction <ActionPreference>] ', '') + "`n```````n" + $Removed.After

    $Removed = Remove-ContentSection $Content "## RELATED LINKS"
    $Content = $Removed.Before + "`n`n## RELATED LINKS`n`n"

    $local:Description, $local:Synopsis = $null
    $local:Examples, $local:RelatedLinks = @()

    if (Test-Path (Join-Path $HelpRoot 'cmdlets' "about_${CommandName}.ps1")) {
        . ((Join-Path $HelpRoot 'cmdlets' "about_${CommandName}.ps1"))
    }

    $local:Synopsis = ($local:Synopsis ? $local:Synopsis : "") + [System.Environment]::NewLine + [System.Environment]::NewLine + "Mnemonic Aliases: $MnemonicAliases"
    $local:RelatedLinks += @(
        'https://github.com/soulshined/OMINOUS/wiki/After-Blocks',
        'https://github.com/soulshined/OMINOUS/wiki/Attributes'
    )

    $Content += ($local:RelatedLinks | % {
            if ($_ -is [string]) {
                return "[{0}]({1})" -f $_, $_
            }
            else {
                return "[{0}]({1})" -f $_.Title, $_.Url
            }
        }) -join [System.Environment]::NewLine

    @{
        "{{ Fill in the Description }}" = $local:Description ?? ''
        "{{ Fill in the Synopsis }}"    = $local:Synopsis ?? ''
        "{{ Fill in the Examples }}"    = ($local:Examples | % -Begin { $i = 0 } -Process {
                @"
### Example {0}

``````powershell
{1}
``````

{2}
"@ -f (++$i), $_.Code, ($_.Description ?? '') }) -join "`n"
    }.GetEnumerator() | % {
        $Content = $Content.Replace($_.Key, $_.Value)
    }

    $Content | Out-File $File -Force
}

New-ExternalHelp -Path (Join-Path $HelpRoot 'cmdlets') -OutputPath $DLLHelpPath -Force