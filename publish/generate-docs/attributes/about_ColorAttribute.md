Colors a markdown element

##### Examples

###### Example 1

```powershell
$RedColorAfterBlock = {
    [Style.Color('red')]
    param()
}

'Alert: 7 days until' | Mkdn $RedColorAfterBlock
```

Produces:

```html
<span style="color: red">Alert: 7 days until</span>
```

<span style="color: red">Alert: 7 days until</span>

###### Example 2

```powershell
$BlueColorAfterBlock = {
    [Style.Color('#00f')]
    param()
}

'Note: 7 days until' | Mkdn $BlueColorAfterBlock
```

Produces:

```html
<span style="color: #00f">Note: 7 days until</span>
```

<span style="color: #00f">Note: 7 days until</span>
