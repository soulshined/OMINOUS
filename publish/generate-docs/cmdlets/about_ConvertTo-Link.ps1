$local:Synopsis = "Convert to markdown link syntax"

$local:Description = @"
Create a link and optionally set the link title

You can coerce the link to open a new browser tab ``-NewTab``, but mind that that switch explicitly outputs html
"@

$local:RelatedLinks = @()

$local:Examples =
@{
    Code = @"
PS D:\> 'http://example.com'

http://example.com
"@
},
@{
    Code = @"
PS D:\> 'http://example.com' -Title foobar

[foobar](http://example.com)
"@
},
@{
    Code = @"
PS D:\> 'http://example.com' -NewTab

<a href="http://example.com" target="_blank">http://example.com</a>
"@
},
@{
    Code = @"
PS D:\> 'http://example.com' -Title foobar -NewTab

<a href="http://example.com" target="_blank">foobar</a>
"@
}
