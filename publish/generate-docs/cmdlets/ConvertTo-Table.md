---
external help file: Ominous.dll-Help.xml
Module Name: OMINOUS
online version: https://github.com/soulshined/OMINOUS/blob/master/publish/generate-docs/cmdlets/ConvertTo-Table.md
schema: 2.0.0
---

# ConvertTo-Table

## SYNOPSIS
Convert to markdown table syntax

Mnemonic Aliases: `Table`
## Syntax
```
 ConvertTo-Table [[-ColumnDefinitions] <System.Collections.Generic.List`1[Ominous.Model.Table.ColumnDefinition]>] [-InputObject] <PSObject> [-AfterBlock <AfterBlock[]>] [-NoNewLine] [-NoMappers] [<CommonParameters>] 
```
## DESCRIPTION
This command inherently supports nested tables
This command also supports column definitions, though it's important to understand column definitions currently only exist for the top most table

You can define a column definition to change it's alignment property or label

When using column definitions, the first definition will always be applied to the first column header, the second will always belong to the second and so forth.
However, you can use dictionaries to create definitions, and these are matched by the Name property to the Column header value.
Anything not defined in a dictionary then proceeds to be applied in the same order as before

This command is a great candidate on why Type Mappers can be valuable. You can make more succinct controlled tables by creating a Type Mapper and that mapped object will always be used for every table object that supports it

Refer to [Type Mapper Wiki](https://github.com/soulshined/OMINOUS/wiki/Type-Mapper) for understanding

## EXAMPLES
### Example 1

```powershell
[ordered]@{
    Name = "Foobar";
    Age = 11;
    Bio = "[example](http://example.com)";
} | ConvertTo-Table

| Name | Age | Bio |
| - | - | - |
| Foobar | 11 | [example](http://example.com) |
```


### Example 2

```powershell
[ordered]@{
    Name = "Foobar";
    Age = 11;
    Bio = "[example](http://example.com)";
    Siblings = @([ordered]@{
        Name = 'Jteve Sobs';
        Age = 99;
    },[ordered]@{
        Name = 'Sichard Rimmons';
        Age = 5;
    },[ordered]@{
        Name = 'Gill Bates';
        Age = 1995;
    })
} | ConvertTo-Table

| Name | Age | Bio | Siblings |
| - | - | - | - |
| Foobar | 11 | [example](http://example.com) | <table><thead><tr><th>Name</th><th>Age</th></tr></thead><tbody><tr><td>Jteve Sobs</td><td>99</td></tr><tr><td>Sichard Rimmons</td><td>5</td></tr><tr><td>Gill Bates</td><td>1995</td></tr></tbody></table> |

```

This example produces a nested table automatically for nested key value pairs
### Example 3

```powershell
[ordered]@{
    Name = "Foobar";
    Age = 11;
    Bio = "[example](http://example.com)";
    Siblings = 'Jteve Sobs','Sichard Rimmons',[ordered]@{
        Name = 'Gill Bates';
        Age = 1995;
    }
} | ConvertTo-Table

| Name | Age | Bio | Siblings |
| - | - | - | - |
| Foobar | 11 | [example](http://example.com) | <ul><li>Jteve Sobs</li><li>Sichard Rimmons</li><li><ul><li>Name: Gill Bates</li><li>Age: 1995</li></ul></li></ul> |

```

This example demonstrates an iterable object with different data typed values
### Example 4

```powershell
[ordered]@{
    Name = "Foobazz";
    Age = 11;
    Bio = "[example](http://example.com)";
    Father = [ordered]@{
        Name = 'Jteve Sobs';
        Age = 99;
    }
} | Table -ColumnDefinitions Right,Center,Center,Center

| Name | Age | Bio | Father |
| -: | :-: | :-: | :-: |
| Foobazz | 11 | [example](http://example.com) | <table><thead><tr><th>Name</th><th>Age</th></tr></thead><tbody><tr><td>Jteve Sobs</td><td>99</td></tr></tbody></table> |

```


### Example 5

```powershell
[ordered]@{
    Name = "Foobar";
    Age = 11;
    Bio = "[example](http://example.com)";
    Father = [ordered]@{
        Name = 'Jteve Sobs';
        Age = 99;
    }
} | Table -ColumnDefinitions @{ Name = "Age"; Alignment = "Center"}

| Name | Age | Bio | Father |
| - | :-: | - | - |
| Foobar | 11 | [example](http://example.com) | <table><thead><tr><th>Name</th><th>Age</th></tr></thead><tbody><tr><td>Jteve Sobs</td><td>99</td></tr></tbody></table> |

```

This example demonstrates setting a column definition by it's matching name (via dictionary)
### Example 6

```powershell
[ordered]@{
    Name = "Foobazz";
    Age = 11;
    Bio = "[example](http://example.com)";
    Father = [ordered]@{
        Name = 'Jteve Sobs';
        Age = 99;
    }
} | Table -ColumnDefinitions @{ Name = "Bio"; Alignment = "Right"; Label = "Biography Link"},Center,Center,Center

| Name | Age | Biography Link | Father |
| :-: | :-: | -: | :-: |
| Foobazz | 11 | [example](http://example.com) | <table><thead><tr><th>Name</th><th>Age</th></tr></thead><tbody><tr><td>Jteve Sobs</td><td>99</td></tr></tbody></table> |

```

This example demonstrates how to re-label a given column header name

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

### -ColumnDefinitions
Definitions of column alignment/labeling

```yaml
Type: System.Collections.Generic.List`1[Ominous.Model.Table.ColumnDefinition]
Parameter Sets: (All)
Aliases:

Required: False
Position: 1
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

[https://github.com/soulshined/OMINOUS/wiki/Type-Mapper](https://github.com/soulshined/OMINOUS/wiki/Type-Mapper)
[https://github.com/soulshined/OMINOUS/wiki/After-Blocks](https://github.com/soulshined/OMINOUS/wiki/After-Blocks)
[https://github.com/soulshined/OMINOUS/wiki/Attributes](https://github.com/soulshined/OMINOUS/wiki/Attributes)
