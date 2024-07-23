---
external help file: Ominous.dll-Help.xml
Module Name: OMINOUS
online version: https://github.com/soulshined/OMINOUS/blob/master/publish/generate-docs/cmdlets/ConvertTo-Heading.md
schema: 2.0.0
---

# ConvertTo-Heading

## SYNOPSIS
Convert to markdown header syntax

Mnemonic Aliases: `H1`, `H2`, `H3`, `H4`, `H5`, `H6`, `Header`, `Heading`
## Syntax
```
 ConvertTo-Heading [[-Level] <Int32>] [[-Id] <String>] [-Value] <String> [-AfterBlock <AfterBlock[]>] [-NoNewLine] [-NoMappers] [<CommonParameters>] 
```
## DESCRIPTION
Create a heading element and optionally provide a custom id (anchor)

## EXAMPLES
### Example 1

```powershell
PS D:\> 'Hello World' | ConvertTo-Heading"

# Hello World

```


### Example 2

```powershell
PS D:\> 'Hello World' | ConvertTo-Heading -Level 3

### Hello World

```


### Example 3

```powershell
PS D:\> 'Hello World' | H2 -Id 'greeting'

## Hello World {#greeting}

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

### -Id
Specify a specific anchor name

```yaml
Type: System.String
Parameter Sets: (All)
Aliases:

Required: False
Position: 2
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Level
The header level (1-6)

```yaml
Type: System.Int32
Parameter Sets: (All)
Aliases:

Required: False
Position: 1
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
