Underline a markdown element

##### Examples

###### Example 1

```powershell
$UnderlineAfterBlock = {
    [Style.Underline()]
    param()
}

'This is pretty neat' | Mkdn $UnderlineAfterBlock
```

Produces:

```html
<u>This is pretty neat</u>
```

<u>This is pretty neat</u>
