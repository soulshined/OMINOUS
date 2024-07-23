$local:Synopsis = "Convert to a flavored markdown callout/admonition (i.e. note, warning, tip etc)"

$local:Description = @"
For flavorless markdown, a generic emoji will be used in lieu of captions by default

For flavorless markdown, you can use the ``-WithCaption`` switch to annotate in bold the admonition type for the admonition. See Example #2
"@

$local:RelatedLinks = @(
    , 'https://github.com/soulshined/OMINOUS/wiki/Ominous-Preference'
)

$local:Examples =
@{
    Code = @"
PS D:\> 'Daily Maintenance occurs at 1700 PST' | ConvertTo-Admonition"

> :spiral_notepad: Daily Maintenance occurs at 1700 PST
"@
},
@{
    Code = @"
PS D:\> 'Daily Maintenance occurs at 1700 PST' | ConvertTo-Admonition -WithCaption"

> :spiral_notepad: **NOTE**: Daily Maintenance occurs at 1700 PST
"@
},
@{
    Code = @"
`$global:OminousPreference = @{
    Flavor = 'GitHub'
}
PS D:\> 'Daily Maintenance occurs at 1700 PST' | ConvertTo-Admonition

> [!NOTE]
> Daily Maintenance occurs at 1700 PST
"@
},
@{
    Code = "PS D:\> 'Don't forget to log out every night!' | ConvertTo-Admonition -Type Warning"
},
@{
    Code = "PS D:\> 'Server restart in 30 mins' | ConvertTo-Admonition -Type Important"
},
@{
    Code = "PS D:\> 'You can skip ads after 5 seconds' | ConvertTo-Admonition -Type Tip"
}