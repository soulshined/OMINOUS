$local:Synopsis = "Convert to a collapsible/details explicit html element"

$local:Description = @"
This command converts a value to a collapsible html element (``<details>``)

> [!NOTE]
> This command always outputs html
"@

$local:RelatedLinks = @(
    , 'https://developer.mozilla.org/en-US/docs/Web/HTML/Element/details'
)

$local:Examples =
@{
    Code = @"
PS D:\> 'This is the content' | ConvertTo-Details

<details>
<summary>Details</summary>

This is the content
</details>
"@
},
@{
    Code = @"
PS D:\> 'This is the content' | ConvertTo-Details -Summary 'Click to reveal content'

<details>
<summary>Click to reveal content</summary>

This is the content
</details>

"@
}