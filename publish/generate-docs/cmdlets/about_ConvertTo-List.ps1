$local:Synopsis = "Convert to markdown list syntax"

$local:Description = @"
This command supports unordered lists, ordered lists, ordered list starting at specific number, and task lists

This command, additionally, inherently supports nested lists

This command has dynamic parameters

The default list type is unordered list

This command automatically escapes a trailing period for list value that has text of starting with a number (like a year)

Refer to examples for understanding
"@

$local:RelatedLinks = @()

$local:Examples =
@{
    Code = @"
PS D:\> 'one','three','five','seven' | ConvertTo-List

- one
- three
- five
- seven

"@
},
@{
    Code = @"
PS D:\> 'one','1998. three','five','seven' | ConvertTo-List

- one
- 1998\. three
- five
- seven

"@
},
@{
    Code = @"
PS D:\> 'one','three',@('five','seven') | ConvertTo-List

- one
- three
  - five
  - seven

"@
},
@{
    Code = @"
PS D:\> 'one','three','five','seven' | OrderedList

1. one
2. three
3. five
4. seven

"@
},
@{
    Code = @"
PS D:\> @('one','three','five','seven') | OrderedList -Start 4

4. one
5. three
6. five
7. seven

"@
},
@{
    Code = @"
PS D:\> 'one','three',@('five','seven') | OrderedList

1. one
2. three
  1. five
  2. seven

"@
},
@{
    Code = @"
PS D:\> 'one','three',@('five','seven') | OrderedList -Start 4

4. one
5. three
  1. five
  2. seven

"@
},
@{
    Code = @"
PS D:\> 'one','three','five','seven' | TaskList

- [ ] one
- [ ] three
- [ ] five
- [ ] seven

"@
},
@{
    Code = @"
PS D:\> 'one','three',@('five','seven') | TaskList

- [ ] one
- [ ] three
  - [ ] five
  - [ ] seven

"@
},
@{
    Code        = @"
PS D:\> [ordered]@{ Value = 'one'; Bone = 19; Freed = `$false },'three','five','seven' | TaskList

- [ ] one
- [ ] three
- [ ] five
- [ ] seven

"@
    Description = "When a dictionary is provided, a key with 'Value' will always take precedence"
},
@{
    Code = @"
PS D:\> 'one',@('three', `$true),'five','seven' | TaskList

- [ ] one
- [x] three
- [ ] five
- [ ] seven

"@
},
@{
    Code = @"
PS D:\> [ordered]@{ Value = 'one'; Checked = `$true },'three','five','seven' | TaskList

- [x] one
- [ ] three
- [ ] five
- [ ] seven

"@
},
@{
    Code        = @"
PS D:\> [ordered]@{Bone='tail';Checked=$`true;Nested=[ordered]@{A='b';B='a';}},'three','five','seven' | TaskList

- [ ] Bone: tail
- [ ] Checked: True
- [ ] Nested
  - [ ] A: b
  - [ ] B: a
- [ ] three
- [ ] five
- [ ] seven

"@
    Description = "There is no 'Value' property in the dictionary therefore it's converted to a normal key-value pair list"
}