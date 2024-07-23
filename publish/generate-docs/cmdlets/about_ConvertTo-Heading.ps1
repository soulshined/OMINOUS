$local:Synopsis = "Convert to markdown header syntax"

$local:Description = @"
Create a heading element and optionally provide a custom id (anchor)
"@

$local:RelatedLinks = @()

$local:Examples =
@{
    Code = @"
PS D:\> 'Hello World' | ConvertTo-Heading"

# Hello World

"@
},
@{
    Code = @"
PS D:\> 'Hello World' | ConvertTo-Heading -Level 3

### Hello World

"@
},
@{
    Code = @"
PS D:\> 'Hello World' | H2 -Id 'greeting'

## Hello World {#greeting}

"@
}