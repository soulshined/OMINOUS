$local:Synopsis = "Convert to markdown table syntax"

$local:Description = @"
This command inherently supports nested tables
This command also supports column definitions, though it's important to understand column definitions currently only exist for the top most table

You can define a column definition to change it's alignment property or label

When using column definitions, the first definition will always be applied to the first column header, the second will always belong to the second and so forth.
However, you can use dictionaries to create definitions, and these are matched by the Name property to the Column header value.
Anything not defined in a dictionary then proceeds to be applied in the same order as before

This command is a great candidate on why Type Mappers can be valuable. You can make more succinct controlled tables by creating a Type Mapper and that mapped object will always be used for every table object that supports it

Refer to [Type Mapper Wiki](https://github.com/soulshined/OMINOUS/wiki/Type-Mapper) for understanding
"@

$local:RelatedLinks = @(
    , 'https://github.com/soulshined/OMINOUS/wiki/Type-Mapper'
)

$local:Examples =
@{
    Code = @"
[ordered]@{
    Name = "Foobar";
    Age = 11;
    Bio = "[example](http://example.com)";
} | ConvertTo-Table

| Name | Age | Bio |
| - | - | - |
| Foobar | 11 | [example](http://example.com) |
"@
},
@{
    Code        = @"
[ordered]@{
    Name = "Foobar";
    Age = 11;
    Bio = "[example](http://example.com)";
    Siblings = @([ordered]@{
        Name = 'Jteve Sobs';
        Age = 99;
    },[ordered]@{
        Name = 'Sichard Rimmons';
        Age = 5;
    },[ordered]@{
        Name = 'Gill Bates';
        Age = 1995;
    })
} | ConvertTo-Table

| Name | Age | Bio | Siblings |
| - | - | - | - |
| Foobar | 11 | [example](http://example.com) | <table><thead><tr><th>Name</th><th>Age</th></tr></thead><tbody><tr><td>Jteve Sobs</td><td>99</td></tr><tr><td>Sichard Rimmons</td><td>5</td></tr><tr><td>Gill Bates</td><td>1995</td></tr></tbody></table> |

"@
    Description = "This example produces a nested table automatically for nested key value pairs"
},
@{
    Code        = @"
[ordered]@{
    Name = "Foobar";
    Age = 11;
    Bio = "[example](http://example.com)";
    Siblings = 'Jteve Sobs','Sichard Rimmons',[ordered]@{
        Name = 'Gill Bates';
        Age = 1995;
    }
} | ConvertTo-Table

| Name | Age | Bio | Siblings |
| - | - | - | - |
| Foobar | 11 | [example](http://example.com) | <ul><li>Jteve Sobs</li><li>Sichard Rimmons</li><li><ul><li>Name: Gill Bates</li><li>Age: 1995</li></ul></li></ul> |

"@
    Description = "This example demonstrates an iterable object with different data typed values"
},
@{
    Code = @"
[ordered]@{
    Name = "Foobazz";
    Age = 11;
    Bio = "[example](http://example.com)";
    Father = [ordered]@{
        Name = 'Jteve Sobs';
        Age = 99;
    }
} | Table -ColumnDefinitions Right,Center,Center,Center

| Name | Age | Bio | Father |
| -: | :-: | :-: | :-: |
| Foobazz | 11 | [example](http://example.com) | <table><thead><tr><th>Name</th><th>Age</th></tr></thead><tbody><tr><td>Jteve Sobs</td><td>99</td></tr></tbody></table> |

"@
},
@{
    Code        = @"
[ordered]@{
    Name = "Foobar";
    Age = 11;
    Bio = "[example](http://example.com)";
    Father = [ordered]@{
        Name = 'Jteve Sobs';
        Age = 99;
    }
} | Table -ColumnDefinitions @{ Name = "Age"; Alignment = "Center"}

| Name | Age | Bio | Father |
| - | :-: | - | - |
| Foobar | 11 | [example](http://example.com) | <table><thead><tr><th>Name</th><th>Age</th></tr></thead><tbody><tr><td>Jteve Sobs</td><td>99</td></tr></tbody></table> |

"@
    Description = "This example demonstrates setting a column definition by it's matching name (via dictionary)"
},
@{
    Code        = @"
[ordered]@{
    Name = "Foobazz";
    Age = 11;
    Bio = "[example](http://example.com)";
    Father = [ordered]@{
        Name = 'Jteve Sobs';
        Age = 99;
    }
} | Table -ColumnDefinitions @{ Name = "Bio"; Alignment = "Right"; Label = "Biography Link"},Center,Center,Center

| Name | Age | Biography Link | Father |
| :-: | :-: | -: | :-: |
| Foobazz | 11 | [example](http://example.com) | <table><thead><tr><th>Name</th><th>Age</th></tr></thead><tbody><tr><td>Jteve Sobs</td><td>99</td></tr></tbody></table> |

"@
    Description = "This example demonstrates how to re-label a given column header name"
}