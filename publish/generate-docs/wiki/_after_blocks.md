After Blocks are script blocks that are invoked before a value is output.

These scriptblocks can be used for 2 things:

1. Style content in a D.R.Y manner (via attributes or otherwise)
2. Alter content in-line to the styling

## Understanding

### Creating an After Block

- You create an After Block just like any normal variable

    ```powershell
    $BoldAfterBlock = {
        [Style.Bold()]
        parm()
    }
    $ItalicAfterBlock = {
        [Style.Italic()]
        param()
    }
    ```

- You can create an After Block with multiple styles:

    ```powershell
    $BoldItalicAfterBlock = {
        [Style.Bold()]
        [Style.Italic()]
        [Style.Center()]
        parm()
    }
    ```



### Style Content

  > [!IMPORTANT]
  > After blocks are cumulative (additive). Meaning if you use 2 after blocks and they both style the content bold you will get the following `****<content>****`


- You can then use these after blocks to style content:

    ```powershell
    @(@{ Name = "John" }, @{ Name = "Steve" }).Values | Mkdn $BoldAfterBlock
    ```

    **John**<br>
    **Steve**

    ```powershell
    @(@{ Name = "John" }, @{ Name = "Steve" }).Values | Mkdn $BoldItalicAfterBlock
    ```

    ***<center>John</center>***
    ***<center>Steve</center>***

- Or you can use multiple:

    ```powershell
    @(@{ Name = "John" }, @{ Name = "Steve" }).Values | Mkdn $BoldAfterBlock, $ItalicAfterBlock
    ```

    ***John***<br>
    ***Steve***

### Alter Content

In addition to styling, you can alter the value that is returned. Each after block is invoked with an argument that is the same value passed to it

In order for this to work, it is required to include a `return` statement using the `return` keyword

To demonstrate:

```powershell
$BoldUpperAfterBlock = {
    [Style.Bold()]
    param($v)

    return $v.ToString().ToUpper() + ": "
}

(@{ type = 'error' },@{ type = 'note' }).Values | Mkdn $BoldUpperAfterBlock
```

`**ERROR: **`<br>
`**NOTE: **`

## Control Flow

1. Input is recieved
2. A conversion result is created for the input (some functions wait until accumaltive records are processed (end block) while some simple commands process them immediately)
3. for each item in process:
    - if item is stringible:
        - for each after block in current scope:
            - duplicates attributes are removed from after block
            - order of precedence is ensured
            - after block is invoked
                - if value is changed apply to next attribute && apply to next set of after blocks
    elseif item can be converted (like a nested list):
    - else:

        Convert using native method, if needed, or repeat for values until something is stringible.

        For example, a table has a cell with contains an array of enum values, the table conversion method will call the `ConvertTo-List` conversion method which repeats #3 for that control flow. The same AfterBlocks from the top most command used is passed to all other conversion methods

> [!NOTE]
> The same After Blocks from the top most command are always passed to all other conversion methods
