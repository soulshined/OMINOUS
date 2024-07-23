using System;
using System.Reflection;

namespace Ominous.Attributes;

public abstract class AbstractOrderedAttribute : Attribute, IComparable<AbstractOrderedAttribute>
{
    public int CompareTo(AbstractOrderedAttribute other)
    {
        var precendence = GetType().GetCustomAttribute<OrderPrecedenceAttribute>()?.Order ?? OrderPrecedenceAttribute.PROCESS_LAST;
        var oprecendence = other?.GetType().GetCustomAttribute<OrderPrecedenceAttribute>()?.Order ?? OrderPrecedenceAttribute.PROCESS_LAST;
        return precendence.CompareTo(oprecendence);
    }
}