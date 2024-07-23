---
external help file: Ominous.dll-Help.xml
Module Name: OMINOUS
online version: https://github.com/soulshined/OMINOUS/blob/master/publish/generate-docs/cmdlets/ConvertTo-Code.md
schema: 2.0.0
---

# ConvertTo-Code

## SYNOPSIS
Convert to markdown code block (fence block) syntax

Mnemonic Aliases: `Code`
## Syntax
```
 ConvertTo-Code [-Language <String>] [-Value] <String> [-AfterBlock <AfterBlock[]>] [-NoNewLine] [-NoMappers] [<CommonParameters>] 
```
## DESCRIPTION
This command always uses backtick syntax as opposed to indentation syntax

Do **not** use this command for inline code syntax, this always assumes fence code block syntax is needed. For inline code refer to creating an AfterBlock

## EXAMPLES
### Example 1

```powershell
@"
namespace Ominous.Utils;

public sealed class MarkdownUtils
{
    public static void Escape(ref string s, params char[] chars)
    {
        foreach (char ch in chars)
        {
            s = s.Replace($"{ch}", $"\\{ch}");
        }
    }
}
"@ | ConvertTo-Code -Language csharp
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

### -Language
Specify the language/syntax for highlighting

```yaml
Type: System.String
Parameter Sets: (All)
Aliases: Syntax

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
