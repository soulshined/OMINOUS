---
external help file: Ominous.dll-Help.xml
Module Name: OMINOUS
online version: https://github.com/soulshined/OMINOUS/blob/master/publish/generate-docs/cmdlets/ConvertTo-Image.md
schema: 2.0.0
---

# ConvertTo-Image

## SYNOPSIS
Convert to markdown image syntax

Mnemonic Aliases: `Image`
## Syntax
```
 ConvertTo-Image [-Title <String>] [-Link <String>] [-Alt <String>] [-Caption <String>] [-Value] <String> [-AfterBlock <AfterBlock[]>] [-NoNewLine] [-NoMappers] [<CommonParameters>] 
```
## DESCRIPTION
Convert a item to a markdown image element

The syntax is as follows:

`![<alt?>](<src> "<title?>")`<br>
`<caption?>`

- Where if alt is not provided, the title is used for the alt
- Where if title is not provided, the url is used for the alt

This command supports creating clickable images by wrapping them in link syntax:

`[![<alt?>](<src> "<title?>")](<link>)`

## EXAMPLES
### Example 1

```powershell
PS D:\> '/assets/images/foobar.png' | ConvertTo-Image -Alt 'Beautiful Sunset'

![Beautiful Sunset](/assets/images/foobar.png)
```


### Example 2

```powershell
PS D:\> '/assets/images/foobar.png' | ConvertTo-Image -Alt 'Beautiful Sunset' -Caption 'Beautiful Sunset'

![Beautiful Sunset](/assets/images/foobar.png)
*Beautiful Sunset*

```


### Example 3

```powershell
PS D:\> '/assets/images/foobar.png' | ConvertTo-Image -Alt 'Beautiful Sunset' -Caption 'Beautiful Sunset' -Title 'My Beautiful Sunset'

![Beautiful Sunset](/assets/images/foobar.png "My Beautiful Sunset")
*Beautiful Sunset*

```


### Example 4

```powershell
PS D:\> '/assets/images/foobar.png' | ConvertTo-Image -Alt 'Beautiful Sunset' -Link 'https://full.link.to.image.com/foobar.png'

[![Beautiful Sunset](/assets/images/foobar.png)](https://full.link.to.image.com/foobar.png)

```


### Example 5

```powershell
PS D:\> '/assets/images/foobar.png' | ConvertTo-Image -Alt 'Beautiful Sunset' -Link 'https://full.link.to.image.com/foobar.png' -Title 'My Beautiful Sunset'"

[![Beautiful Sunset](/assets/images/foobar.png "My Beautiful Sunset")](https://full.link.to.image.com/foobar.png)

```


### Example 6

```powershell
PS D:\> '/assets/images/foobar.png' | ConvertTo-Image -Alt 'Beautiful Sunset' -Caption 'Beautiful Sunset' -Link 'https://full.link.to.image.com/foobar.png' -Title 'My Beautiful Sunset'

[![Beautiful Sunset](/assets/images/foobar.png "My Beautiful Sunset")](https://full.link.to.image.com/foobar.png)
*Beautiful Sunset*

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

### -Alt
The alt of an image link (defaults to title first, then url): \`!\[\<alt?\>\](\<url\> "\<title?\>")\`

```yaml
Type: System.String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Caption
The caption applied under a link (optional)

```yaml
Type: System.String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Link
The url of an image link: \`!\[\<alt?\>\](\<url\> "\<title?\>")\`

```yaml
Type: System.String
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
The title of an image link (optional): \`!\[\<alt?\>\](\<url\> "\<title?\>")\`

```yaml
Type: System.String
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
