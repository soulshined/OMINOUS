using System.Management.Automation;

namespace Ominous.Extensions;

internal static class ObjectExtensions
{
    internal static PSObject ToPSObject(this object o, bool shallow = false)
    {
        if (null == o) return PSObject.AsPSObject(null);
        var casted = (PSObject)(o is PSObject ? o : PSObject.AsPSObject(o));
        return shallow ? casted : casted.Resolve();
    }
}