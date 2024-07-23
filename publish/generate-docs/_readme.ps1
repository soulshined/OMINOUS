$Readme = H2 "Usage"

$Readme += H3 "PowerShell Gallery"

$Readme += @'
```powershell
Install-Module -Name OMINOUS
```

https://www.powershellgallery.com/packages/OMINOUS
'@

$Readme += H2 "Features"

$Readme += @"
- mnemonic invoking conventions

  This particular module encourages mnemonic naming conventions. For example, instead of calling

  ``````powershell
  ConvertTo-Heading 'Hello World' -Level 3
  ``````

  you can simply call the command like the following:

  ``````powershell
  H3 'Hello World'
  ``````

  The same is true for Table, List, UnorderedList, TaskList etc

- Attribute based styling logic
- Markdown Flavor support
- Automatic nested object[s] conversion
- Type Mapping:  map a type to another type before converting to markdown
- Preferences/Configuration

"@

$Readme += H2 "Commands"

$Readme += $Module.ExportedCmdlets.Values | Table -ColumnDefinitions Center, Center, Center

$Readme | Out-File (Join-Path $WorkspaceRoot README.md)