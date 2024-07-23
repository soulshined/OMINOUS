---
external help file: Ominous.dll-Help.xml
Module Name: OMINOUS
online version: https://github.com/soulshined/OMINOUS/blob/master/publish/generate-docs/cmdlets/ConvertTo-Admonition.md
schema: 2.0.0
---

# ConvertTo-Admonition

## SYNOPSIS
Convert to a flavored markdown callout/admonition (i.e. note, warning, tip etc)

Mnemonic Aliases: `Admonition`, `Alert`, `Caution`, `Important`, `Tip`, `Warning`
## Syntax
```
 ConvertTo-Admonition [-Type <AdmonitionType>] [-WithCaption] [-Value] <String> [-AfterBlock <AfterBlock[]>] [-NoNewLine] [-NoMappers] [<CommonParameters>] 
```
## DESCRIPTION
For flavorless markdown, a generic emoji will be used in lieu of captions by default

For flavorless markdown, you can use the `-WithCaption` switch to annotate in bold the admonition type for the admonition. See Example #2

## EXAMPLES
### Example 1

```powershell
PS D:\> 'Daily Maintenance occurs at 1700 PST' | ConvertTo-Admonition"

> :spiral_notepad: Daily Maintenance occurs at 1700 PST
```


### Example 2

```powershell
PS D:\> 'Daily Maintenance occurs at 1700 PST' | ConvertTo-Admonition -WithCaption"

> :spiral_notepad: **NOTE**: Daily Maintenance occurs at 1700 PST
```


### Example 3

```powershell
$global:OminousPreference = @{
    Flavor = 'GitHub'
}
PS D:\> 'Daily Maintenance occurs at 1700 PST' | ConvertTo-Admonition

> [!NOTE]
> Daily Maintenance occurs at 1700 PST
```


### Example 4

```powershell
PS D:\> 'Don't forget to log out every night!' | ConvertTo-Admonition -Type Warning
```


### Example 5

```powershell
PS D:\> 'Server restart in 30 mins' | ConvertTo-Admonition -Type Important
```


### Example 6

```powershell
PS D:\> 'You can skip ads after 5 seconds' | ConvertTo-Admonition -Type Tip
```



## PARAMETERS

### -AfterBlock
An after block is a scriptblock with ominous attributes declared of which the content provided to it will be styled by

```yaml
Type: Ominous.Model.AfterBlock[]
Parameter Sets: (All)
Aliases: After

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -NoMappers
Coerce conversion to not pass input objects to type mappers, even if they are defined

```yaml
Type: System.Management.Automation.SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -NoNewLine
Prevent trailing line sequences in converted output

```yaml
Type: System.Management.Automation.SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Type
Synonymous to a 'callout' type (i.e.
tip, note, warning etc)

```yaml
Type: Ominous.Constants.AdmonitionType
Parameter Sets: (All)
Aliases:
Accepted values: CAUTION, NOTE, IMPORTANT, TIP, WARNING

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Value
Nullable string to convert to markdown

```yaml
Type: System.String
Parameter Sets: (All)
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByPropertyName, ByValue)
Accept wildcard characters: False
```

### -WithCaption
For flavorless markdown, use this to add a caption to the callout

```yaml
Type: System.Management.Automation.SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```
### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### System.String

## OUTPUTS

### System.String

## NOTES

## RELATED LINKS

[https://github.com/soulshined/OMINOUS/wiki/Ominous-Preference](https://github.com/soulshined/OMINOUS/wiki/Ominous-Preference)
[https://github.com/soulshined/OMINOUS/wiki/After-Blocks](https://github.com/soulshined/OMINOUS/wiki/After-Blocks)
[https://github.com/soulshined/OMINOUS/wiki/Attributes](https://github.com/soulshined/OMINOUS/wiki/Attributes)
