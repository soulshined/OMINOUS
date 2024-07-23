---
external help file: Ominous.dll-Help.xml
Module Name: OMINOUS
online version: https://github.com/soulshined/OMINOUS/blob/master/publish/generate-docs/cmdlets/ConvertTo-Blockquote.md
schema: 2.0.0
---

# ConvertTo-Blockquote

## SYNOPSIS
Convert to markdown blockquote syntax

Mnemonic Aliases: `Blockquote`, `Quote`
## Syntax
```
 ConvertTo-Blockquote [-InputObject] <PSObject> [-AfterBlock <AfterBlock[]>] [-NoNewLine] [-NoMappers] [<CommonParameters>] 
```
## DESCRIPTION
This command converts value[s] to a markdown blockquote. If the value is an array of values it gets collected into 1 single blockquote, separated by an empty line in the blockquote.

When using an array as one of the values in the initial set of values (a nested array), the nested array becomes a nested blockquote inherently. See Example #2

## EXAMPLES
### Example 1

```powershell
PS D:\> 'This is a blockquote','','This is another line in the blockquote' | ConvertTo-Blockquote

> This is a blockquote
>
> This is another line in the blockquote
```


### Example 2

```powershell
PS D:\> '**NOTE** This is first note',"",@('This is note 1.a', " ", 'This is note 1.b', @('This is note 1.b.i','This is note 1.b.ii','This is note 1.b.iii')) | Quote

> **NOTE** This is first note
>
>> This is note 1.a
>>
>> This is note 1.b
>>> This is note 1.b.i
>>> This is note 1.b.ii
>>> This is note 1.b.iii

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

### -InputObject
object to convert to markdown

```yaml
Type: System.Management.Automation.PSObject
Parameter Sets: (All)
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByPropertyName, ByValue)
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
### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### System.Management.Automation.PSObject

## OUTPUTS

### System.String

## NOTES

## RELATED LINKS

[https://github.com/soulshined/OMINOUS/wiki/After-Blocks](https://github.com/soulshined/OMINOUS/wiki/After-Blocks)
[https://github.com/soulshined/OMINOUS/wiki/Attributes](https://github.com/soulshined/OMINOUS/wiki/Attributes)
