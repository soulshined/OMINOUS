---
external help file: Ominous.dll-Help.xml
Module Name: OMINOUS
online version: https://github.com/soulshined/OMINOUS/blob/master/publish/generate-docs/cmdlets/Convert-WithAfterBlock.md
schema: 2.0.0
---

# Convert-WithAfterBlock

## SYNOPSIS
Convert to markdown with only after blocks

Mnemonic Aliases: `AfterBlock`, `Block`, `Markdown`, `Mkdn`
## Syntax
```
 Convert-WithAfterBlock -Value <String> [[-AfterBlock] <AfterBlock[]>] [-NoNewLine] [-NoMappers] [<CommonParameters>] 
```
## DESCRIPTION
This function converts a nullable string value to markdown using only after block syntax.
These can be used to do things like inline code blocks, indent, center or color specific text.

## EXAMPLES
### Example 1

```powershell
$CodeBlock = {
    [Style.Code()]
    param()
}
PS D:\> 'Foobar' | Mkdn $CodeBlock
```

An after block is a scriptblock with OMINOUS attributes specified that the content provided to it will be converted to.
In this example, it converts Foobar to \`Foobar\`
### Example 2

```powershell
$BoldItalicBlock = {
    [Style.Bold()]
    [Style.Italic()]
    param()
}
$CenterBlock = {
    [Style.Center()]
    param()
}
PS D:\> 'Foobar' | Mkdn $BoldItalicBlock, $CenterBlock
```



## PARAMETERS

### -AfterBlock
An after block is a scriptblock with ominous attributes specified that the content provided to it will be converted to

```yaml
Type: Ominous.Model.AfterBlock[]
Parameter Sets: (All)
Aliases: After

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
Position: Named
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
