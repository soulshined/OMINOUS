$local:Attributes = [Style.Bold].Assembly.DefinedTypes | ? {
    [Ominous.Attributes.AbstractOrderedAttribute].IsAssignableFrom($_) -and
    @([Ominous.Attributes.Markdown.Style.StyleAttribute], [Ominous.Attributes.AbstractOrderedAttribute]) -notcontains $_
}  | % {
    $o = $_ -eq [Ominous.Attributes.Markdown.Style.ColorAttribute] ? $_::New("foobar") : $_::New()
    $Name = $_.Name

    @{
        Name              = $Name
        FullName          = $_.FullName
        IsStyleAttribute  = $_.FullName.Contains(".Style.")
        Prefix            = $o_.Prefix
        Suffix            = $o.Suffix
        TagName           = $o.TagName
        Description       = if (Test-Path (Join-Path $HelpRoot 'attributes' "about_${Name}.md")) {
            gc (Join-Path $HelpRoot 'attributes' "about_${Name}.md") -Raw
        }
        else { $null }
        IsExclusivelyHTML = $o.IsExclusivelyHTML
        Precedence        = ($_.GetCustomAttributes([Ominous.Attributes.OrderPrecedenceAttribute]) | % {
                if ($_.GetType() -ne [Ominous.Attributes.OrderPrecedenceAttribute]) { return }
                $Order = $_.Order

                $IsPrecedenceLast = $Order -ge [Ominous.Attributes.OrderPrecedenceAttribute]::PROCESS_LAST - $Order
                $IsPrecedenceLast ? "OrderPrecedenceAttribute.PROCESS_LAST - {0}" -f ([Ominous.Attributes.OrderPrecedenceAttribute]::PROCESS_LAST - $Order)  : "OrderPrecedenceAttribute.PROCESS_FIRST + $Order"
            })
    }
} | Sort-Object Name | % {
    $Name = $_.Name
    $Usage = $_.IsStyleAttribute ? $_.FullName.Replace("Ominous.Attributes.Markdown.", "") : $_.FullName
    $Usage = $Usage -replace 'Attribute', ''
    $Description = $_.Description
    $Precedence = $_.Precedence
    $HtmlDisclaimer = -not $_.IsExclusivelyHTML -eq $true ? '' : @"
`$`$`{\color`{red`}ℍ`}`$`$

> [!CAUTION]
> This attribute will always use html syntax. Ensure your markdown flavor of choice supports HTML rendering

"@

    @"

### $Name $HtmlDisclaimer

#### Precedence
``$Precedence``

#### Usage
``[$Usage()]``

#### Description
$Description
"@
}

(Get-Content (Join-Path $PSScriptRoot '_attributes.md') -Raw) `
    -replace '\{\{ attributes \}\}', $local:Attributes