---
external help file: Ominous.dll-Help.xml
Module Name: OMINOUS
online version: https://github.com/soulshined/OMINOUS/blob/master/publish/generate-docs/cmdlets/ConvertTo-Link.md
schema: 2.0.0
---

# ConvertTo-Link

## SYNOPSIS
Convert to markdown link syntax

Mnemonic Aliases: `Link`
## Syntax
```
 ConvertTo-Link [-Value] <String> [-AfterBlock <AfterBlock[]>] [-NoNewLine] [-NoMappers] [[-Title] <String>] [-NewTab] [<CommonParameters>] 
```
## DESCRIPTION
Create a link and optionally set the link title

You can coerce the link to open a new browser tab `-NewTab`, but mind that that switch explicitly outputs html

## EXAMPLES
### Example 1

```powershell
PS D:\> 'http://example.com'

http://example.com
```


### Example 2

```powershell
PS D:\> 'http://example.com' -Title foobar

[foobar](http://example.com)
```


### Example 3

```powershell
PS D:\> 'http://example.com' -NewTab

<a href="http://example.com" target="_blank">http://example.com</a>
```


### Example 4

```powershell
PS D:\> 'http://example.com' -Title foobar -NewTab

<a href="http://example.com" target="_blank">foobar</a>
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

### -NewTab
Coerces a link to be an explicit HTML \<a\> tag with a _blank target attr value

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

### -Title
Display title of link (optional)

```yaml
Type: System.String
Parameter Sets: (All)
Aliases:

Required: False
Position: 1
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
### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### System.String

## OUTPUTS

### System.String

## NOTES

## RELATED LINKS

[https://github.com/soulshined/OMINOUS/wiki/After-Blocks](https://github.com/soulshined/OMINOUS/wiki/After-Blocks)
[https://github.com/soulshined/OMINOUS/wiki/Attributes](https://github.com/soulshined/OMINOUS/wiki/Attributes)
