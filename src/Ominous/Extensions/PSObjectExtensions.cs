using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;

namespace Ominous.Extensions;

internal static class PSObjectExtensions
{
    internal static bool IsString(this PSObject o) => !o.IsNull() && o.BaseObject is string;

    internal static bool IsBool(this PSObject o) => !o.IsNull() && o.BaseObject is bool;

    internal static bool IsNull(this PSObject o) => o == null || o.BaseObject == null;

    internal static bool IsDictionary(this PSObject o) => !o.IsNull() && o.BaseObject is IDictionary;

    internal static bool IsIterable(this PSObject o) => !o.IsNull() && o.BaseObject is ICollection || o.BaseObject.GetType().IsArray;

    internal static bool IsAllSameAsFirstFoundType(this List<PSObject> o, params Type[] types)
    {
        if (o == null) return false;

        Type t = null;

        foreach (var i in o)
        {
            if (null == i) continue;
            if (null == t)
            {
                t = i.BaseObject.GetType();
                if (!types.Any(j => j.IsAssignableFrom(t)))
                    return false;
            }

            if (!t.IsAssignableFrom(i.BaseObject.GetType())) return false;
        }

        return o.Count > 0;
    }

    internal static PSObject Resolve(this PSObject o)
    {
        if (null == o) return PSObject.AsPSObject(null);
        if (null == o.BaseObject) return o;

        if (o.BaseObject is PSObject || o.BaseObject is PSCustomObject)
        {
            var d = new Dictionary<string, PSObject>();

            foreach (var i in o.Properties)
            {
                var value = i.Value;
                d.Add(i.Name, value == null ? string.Empty : value.ToPSObject());
            }

            return PSObject.AsPSObject(d);
        }
        else if (o.IsDictionary())
        {
            var d = new Dictionary<string, PSObject>();

            foreach (string k in ((IDictionary)o.BaseObject).Keys)
            {
                var value = ((IDictionary)o.BaseObject)[k];
                d.Add(k, value == null ? string.Empty : value.ToPSObject());
            }

            return PSObject.AsPSObject(d);
        }
        else if (o.IsIterable() && o.BaseObject is not string)
        {
            return PSObject.AsPSObject(((ICollection)o.BaseObject).Cast<object>().Select(i => i.ToPSObject()).ToList());
        }

        return o;
    }
}