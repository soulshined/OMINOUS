---
external help file: Ominous.dll-Help.xml
Module Name: OMINOUS
online version: https://github.com/soulshined/OMINOUS/blob/master/publish/generate-docs/cmdlets/ConvertTo-List.md
schema: 2.0.0
---

# ConvertTo-List

## SYNOPSIS
Convert to markdown list syntax

Mnemonic Aliases: `List`, `OrderedList`, `TaskList`, `UnorderedList`
## Syntax
```
 ConvertTo-List [-InputObject] <PSObject> [-AfterBlock <AfterBlock[]>] [-NoNewLine] [-NoMappers] [<CommonParameters>] 
```
## DESCRIPTION
This command supports unordered lists, ordered lists, ordered list starting at specific number, and task lists

This command, additionally, inherently supports nested lists

This command has dynamic parameters

The default list type is unordered list

This command automatically escapes a trailing period for list value that has text of starting with a number (like a year)

Refer to examples for understanding

## EXAMPLES
### Example 1

```powershell
PS D:\> 'one','three','five','seven' | ConvertTo-List

- one
- three
- five
- seven

```


### Example 2

```powershell
PS D:\> 'one','1998. three','five','seven' | ConvertTo-List

- one
- 1998\. three
- five
- seven

```


### Example 3

```powershell
PS D:\> 'one','three',@('five','seven') | ConvertTo-List

- one
- three
  - five
  - seven

```


### Example 4

```powershell
PS D:\> 'one','three','five','seven' | OrderedList

1. one
2. three
3. five
4. seven

```


### Example 5

```powershell
PS D:\> @('one','three','five','seven') | OrderedList -Start 4

4. one
5. three
6. five
7. seven

```


### Example 6

```powershell
PS D:\> 'one','three',@('five','seven') | OrderedList

1. one
2. three
  1. five
  2. seven

```


### Example 7

```powershell
PS D:\> 'one','three',@('five','seven') | OrderedList -Start 4

4. one
5. three
  1. five
  2. seven

```


### Example 8

```powershell
PS D:\> 'one','three','five','seven' | TaskList

- [ ] one
- [ ] three
- [ ] five
- [ ] seven

```


### Example 9

```powershell
PS D:\> 'one','three',@('five','seven') | TaskList

- [ ] one
- [ ] three
  - [ ] five
  - [ ] seven

```


### Example 10

```powershell
PS D:\> [ordered]@{ Value = 'one'; Bone = 19; Freed = $false },'three','five','seven' | TaskList

- [ ] one
- [ ] three
- [ ] five
- [ ] seven

```

When a dictionary is provided, a key with 'Value' will always take precedence
### Example 11

```powershell
PS D:\> 'one',@('three', $true),'five','seven' | TaskList

- [ ] one
- [x] three
- [ ] five
- [ ] seven

```


### Example 12

```powershell
PS D:\> [ordered]@{ Value = 'one'; Checked = $true },'three','five','seven' | TaskList

- [x] one
- [ ] three
- [ ] five
- [ ] seven

```


### Example 13

```powershell
PS D:\> [ordered]@{Bone='tail';Checked=$	rue;Nested=[ordered]@{A='b';B='a';}},'three','five','seven' | TaskList

- [ ] Bone: tail
- [ ] Checked: True
- [ ] Nested
  - [ ] A: b
  - [ ] B: a
- [ ] three
- [ ] five
- [ ] seven

```

There is no 'Value' property in the dictionary therefore it's converted to a normal key-value pair list

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
