##  What does it do?

In short, it maps from 1 object to another *before* being converted to markdown

## Why

For obvious reasons we don't want to add any kind of powershell type converter because this impacts all processes not just ones scoped to ominous.

Therefore, by registering type mappers, you can ensure that they are only used by Ominous and for very specific Ominous reasons.

Out-of-box, there is 1 type mapper provided to demonstrate. Review the [`mappers.ps1`](https://github.com/soulshined/OMINOUS/blob/master/src/Ominous/mappers.ps1) file for demonstration

## Control flow

1. Input is recieved
2. A conversion result is created for the input (some functions wait until accumaltive records are processed (end block) while some simple commands process them immediately)
3. for each item in process:
    - if item has defined type mapper and can map:
        - map item
        - if mapped item can be mapped
            map item<br>
          else: add item

      else: add item
