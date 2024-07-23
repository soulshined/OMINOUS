using System;
using System.Management.Automation;

namespace Ominous.Model.List;

internal readonly struct ListItem
{
    internal readonly PSObject PSObject;
    internal readonly bool IsChecked;

    public ListItem() => throw new NotSupportedException();

    internal ListItem(PSObject value, bool isChecked = false)
    {
        PSObject = value;
        IsChecked = isChecked;
    }
}