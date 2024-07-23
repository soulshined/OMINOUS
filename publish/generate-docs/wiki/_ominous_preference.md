## Overivew

Preferences are mutable. Meaning you can define them before you import the module or anytime after and the latest values will always be aware

For example:

```powershell
foreach @($flavor in 'GitHub', 'Asciidoc')  {
    $global:OminousPreference.Flavor = $flavor

    'Foobar' | ConvertTo-Admonition
}

> [!NOTE]
> Foobar

> :spiral_notebook: Foobar

```

### Supported Properties

| Property | Description | Default
| - | :-: | :-:
| Flavor | Tells the ominous ecosystem your preference of flavor for the current state<br>Possible values: GitHub
| NoMappers | Ensure no type mappers are used by the ecosystem | `$false` |
| NoNewLine | Ensures no new lines appear at the end of block elements (this has no affect on inline elements (links, images etc)) | `$false`
