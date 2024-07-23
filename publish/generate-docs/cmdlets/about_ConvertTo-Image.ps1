$local:Synopsis = "Convert to markdown image syntax"

$local:Description = @"
Convert a item to a markdown image element

The syntax is as follows:

``![<alt?>](<src> "<title?>")``<br>
``<caption?>``

- Where if alt is not provided, the title is used for the alt
- Where if title is not provided, the url is used for the alt

This command supports creating clickable images by wrapping them in link syntax:

``[![<alt?>](<src> "<title?>")](<link>)``
"@

$local:RelatedLinks = @()

$local:Examples =
@{
    Code = @"
PS D:\> '/assets/images/foobar.png' | ConvertTo-Image -Alt 'Beautiful Sunset'

![Beautiful Sunset](/assets/images/foobar.png)
"@
},
@{
    Code = @"
PS D:\> '/assets/images/foobar.png' | ConvertTo-Image -Alt 'Beautiful Sunset' -Caption 'Beautiful Sunset'

![Beautiful Sunset](/assets/images/foobar.png)
*Beautiful Sunset*

"@
},
@{
    Code = @"
PS D:\> '/assets/images/foobar.png' | ConvertTo-Image -Alt 'Beautiful Sunset' -Caption 'Beautiful Sunset' -Title 'My Beautiful Sunset'

![Beautiful Sunset](/assets/images/foobar.png "My Beautiful Sunset")
*Beautiful Sunset*

"@
},
@{
    Code = @"
PS D:\> '/assets/images/foobar.png' | ConvertTo-Image -Alt 'Beautiful Sunset' -Link 'https://full.link.to.image.com/foobar.png'

[![Beautiful Sunset](/assets/images/foobar.png)](https://full.link.to.image.com/foobar.png)

"@
} ,
@{
    Code = @"
PS D:\> '/assets/images/foobar.png' | ConvertTo-Image -Alt 'Beautiful Sunset' -Link 'https://full.link.to.image.com/foobar.png' -Title 'My Beautiful Sunset'"

[![Beautiful Sunset](/assets/images/foobar.png "My Beautiful Sunset")](https://full.link.to.image.com/foobar.png)

"@
} ,
@{
    Code = @"
PS D:\> '/assets/images/foobar.png' | ConvertTo-Image -Alt 'Beautiful Sunset' -Caption 'Beautiful Sunset' -Link 'https://full.link.to.image.com/foobar.png' -Title 'My Beautiful Sunset'

[![Beautiful Sunset](/assets/images/foobar.png "My Beautiful Sunset")](https://full.link.to.image.com/foobar.png)
*Beautiful Sunset*

"@
}