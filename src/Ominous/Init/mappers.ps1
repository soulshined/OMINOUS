$script:PSTypeNames = @{
    CmdletInfo          = 'Ominous.Runtime.Cmdlet'
    CmdletParameterInfo = 'Ominous.Runtime.Cmdlet.Parameter'
}

$global:OminousTypeMappers.AddType([System.Management.Automation.CmdletInfo], {
        param([System.Management.Automation.CmdletInfo]$Info)

        process {
            $local:Name = $Info.Name
            $local:Help = Get-Help $local:Name -ProgressAction Ignore -ErrorAction SilentlyContinue

            $local:Parameters = $local:Help.Syntax.syntaxItem[0].parameter | Sort-Object -Property Position | % {
                $local:Attributes = $Info.Parameters[$_.Name].Attributes | % {
                    if (@(
                            [Parameter].FullName,
                            [Alias].FullName
                        ) -contains $_.TypeId.ToString()) { return }
                    $_.GetType()
                }

                [PSCustomObject]@{
                    PSTypeName     = $script:PSTypeNames.CmdletParameterInfo
                    Name           = $_.Name
                    Type           = $_.Type
                    Attributes     = $local:Attributes
                    Aliases        = $_.Aliases -ieq 'none' ? @() : $_.Aliases
                    Description    = ($_.Description | % { $_.Text?.Trim() }) -join [System.Environment]::NewLine
                    Required       = $_.Required
                    PipelineInput  = $_.PipelineInput
                    ParameterValue = $_.ParameterValueGroup?.ParameterValue ?? ''
                }
            }

            [PSCustomObject]@{
                PSTypeName   = $script:PSTypeNames.CmdletParameterInfo
                Name         = $local:Name
                Aliases      = (Get-Alias -Definition $local:Name -ErrorAction SilentlyContinue) ?? @()
                Definition   = $Info.Definition
                HelpInfoUri  = $Info.HelpUri
                Details      = [ordered]@{
                    Name        = $Info.Name
                    Description = ($local:Help.Details.Description | % Text) -join [System.Environment]::NewLine
                    Verb        = $Info.Verb
                    Noun        = $Info.Noun
                }
                Synopsis     = $local:Help.Synopsis
                Parameters   = $local:Parameters ?? @()
                Category     = $Info.CommandType
                ModuleName   = $Info.ModuleName
                Description  = ($local:Help.Description | % Text) -join [System.Environment]::NewLine
                Notes        = ($local:Help.AlertSet | % Alert | % Text) -join [System.Environment]::NewLine
                RelatedLinks = $local:Help.RelatedLinks | % NavigationLink
                OutputTypes  = $Info.OutputType
                Examples     = $local:Help.Examples | % Example | % {
                    [ordered]@{
                        Title   = $_.Title
                        Code    = $_.Code
                        Remarks = ($_.Remarks | % Text) -join [System.Environment]::NewLine
                    }
                }
                Version      = $Info.Version
            }
        }
    }
)

$global:OminousTypeMappers.AddType($script:PSTypeNames.CmdletParameterInfo, {
        param([PSTypeName("Ominous.Runtime.CmdletInfo")]$CmdletInfo)

        process {
            $local:CodeBlock = { [Ominous.Attributes.Markdown.Style.Code()]param() }
            $local:CenterBlock = { [Ominous.Attributes.Markdown.Style.Center()]param() }

            $CmdletInfo.Aliases = ($CmdletInfo.Aliases | % { $_ | Mkdn $local:CodeBlock -NoNewLine }) -join ', '
            $CmdletInfo.Parameters | % {
                $local:Name = ""
                if ($_.Attributes) {
                    $local:Name += ($_.Attributes | % { "<sub>{0}</sub>" -f ("[{0}]" -f $_.FullName | Mkdn $CodeBlock -NoNewLine) }) -join "<br>"
                    $local:Name += "<br>"
                }

                $_.Name = $local:Name + ("[{0}] {1}" -f $_.Type.name, $_.Name | Mkdn $local:CodeBlock -NoNewLine)
                $_.Aliases = ($_.Aliases | Mkdn $local:CodeBlock -NoNewLine) -join ', '
                $_.Required = $_.Required -eq $true ? '&check;' : '&nbsp;' | Mkdn $local:CenterBlock
                $_.PipelineInput = $_.PipelineInput -eq $false ? '' : $_.PipelineInput | Mkdn $local:CenterBlock -NoNewLine

                foreach ($i in @('Type', 'Attributes')) {
                    $_.PSObject.Properties.Remove($i)
                }
            }

            $CmdletInfo.Details = [ordered]@{
                Name = $CmdletInfo.Details.Name
                Noun = $CmdletInfo.Details.Noun
                Verb = $CmdletInfo.Details.Verb
            }

            $local:Synopsis = $CmdletInfo.Synopsis
            [Ominous.Utils.MarkdownUtils]::Escape([ref]$local:Synopsis)
            $CmdletInfo.Synopsis = $local:Synopsis

            $local:Definition = $CmdletInfo.Definition.Trim()
            [Ominous.Utils.MarkdownUtils]::HtmlEncode([ref]$local:Definition)
            $CmdletInfo.Definition = $local:Definition

            $local:Description = $CmdletInfo.Description
            [Ominous.Utils.MarkdownUtils]::HtmlEncode([ref]$local:Description, "`n", "`t")
            $CmdletInfo.Description = $local:Description

            $local:Notes = $CmdletInfo.Notes
            [Ominous.Utils.MarkdownUtils]::Escape([ref]$local:Notes)
            [Ominous.Utils.MarkdownUtils]::HtmlEncode([ref]$local:Notes, "`n", "`t")
            $CmdletInfo.Notes = $local:Notes

            if ($CmdletInfo.HelpInfoUri) {
                $CmdletInfo.Name = "[{0}]({1})" -f $CmdletInfo.Name, $CmdletInfo.HelpInfoUri
                $CmdletInfo.PSObject.Properties.Remove("HelpInfoUri")
            }

            $CmdletInfo.RelatedLinks = $CmdletInfo.RelatedLinks | % {
                "[{0}]({1})" -f ($_.linkText ?? $_.uri), $_.uri
            }

            'Notes', 'Examples' | % { $CmdletInfo.PSObject.Properties.Remove($_) }

            $CmdletInfo
        }

    }
)