using System;

namespace Ominous.Attributes;

[AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
public sealed class OrderPrecedenceAttribute : Attribute, IComparable<OrderPrecedenceAttribute>, IComparable<int>
{
    public const uint PROCESS_FIRST = 0;
    public const uint PROCESS_LAST = uint.MaxValue;

    public readonly uint Order = PROCESS_LAST;

    public OrderPrecedenceAttribute() { }

    public OrderPrecedenceAttribute(uint order)
    {
        Order = order;
    }

    public int CompareTo(OrderPrecedenceAttribute other) => Order.CompareTo(other.Order);

    public int CompareTo(int other) => Order.CompareTo(other);
}