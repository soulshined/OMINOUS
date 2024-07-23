$local:Synopsis = "Convert to markdown blockquote syntax"

$local:Description = @"
This command converts value[s] to a markdown blockquote. If the value is an array of values it gets collected into 1 single blockquote, separated by an empty line in the blockquote.

When using an array as one of the values in the initial set of values (a nested array), the nested array becomes a nested blockquote inherently. See Example #2
"@

$local:RelatedLinks = @()

$local:Examples =
@{
    Code = @"
PS D:\> 'This is a blockquote','','This is another line in the blockquote' | ConvertTo-Blockquote

> This is a blockquote
>
> This is another line in the blockquote
"@
},
@{
    Code = @"
PS D:\> '**NOTE** This is first note',"",@('This is note 1.a', " ", 'This is note 1.b', @('This is note 1.b.i','This is note 1.b.ii','This is note 1.b.iii')) | Quote

> **NOTE** This is first note
>
>> This is note 1.a
>>
>> This is note 1.b
>>> This is note 1.b.i
>>> This is note 1.b.ii
>>> This is note 1.b.iii

"@
}