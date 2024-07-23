using System.Collections.Generic;

namespace Ominous.Model.List;

internal abstract class AbstractList<T> : List<T>
{
    internal AbstractList() : base() { }
    internal AbstractList(params T[] items) : base(items) { }
    internal AbstractList(int capacity) : base(capacity) { }
    internal AbstractList(IEnumerable<T> collection) : base(collection) { }
}