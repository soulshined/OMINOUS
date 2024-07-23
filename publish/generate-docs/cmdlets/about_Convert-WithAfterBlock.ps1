$local:Synopsis = "Convert to markdown with only after blocks"

$local:Description = @"
This function converts a nullable string value to markdown using only after block syntax.
These can be used to do things like inline code blocks, indent, center or color specific text.
"@

$local:RelatedLinks = @()

$local:Examples =
@{
    Description = "An after block is a scriptblock with OMINOUS attributes specified that the content provided to it will be converted to.
In this example, it converts Foobar to \``Foobar\``"
    Code        = @"
`$CodeBlock = {
    [Style.Code()]
    param()
}
PS D:\> 'Foobar' | Mkdn `$CodeBlock
"@
},
@{
    Code = @"
`$BoldItalicBlock = {
    [Style.Bold()]
    [Style.Italic()]
    param()
}
`$CenterBlock = {
    [Style.Center()]
    param()
}
PS D:\> 'Foobar' | Mkdn `$BoldItalicBlock, `$CenterBlock
"@
}