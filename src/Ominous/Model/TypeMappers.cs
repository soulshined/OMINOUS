using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Management.Automation;
using Ominous.Extensions;

namespace Ominous.Model;

public class TypeMappers
{
    internal Dictionary<object, ScriptBlock> Types { get; } = new Dictionary<object, ScriptBlock>();

    public TypeMappers() { }

    public void AddType(Type receiver, ScriptBlock sb) => Types.Add(receiver, sb);
    public void AddType(string psTypeName, ScriptBlock sb) => Types.Add(new PSTypeName(psTypeName), sb);

    internal bool CanMap(PSObject o)
    {
        if (o.IsNull()) return false;

        var mapper = GetMapper(o) != null;

        return mapper;
    }

    internal Collection<PSObject> Map(PSObject o)
    {
        if (o.IsNull()) return new();

        var sb = GetMapper(o);

        object arg = o;

        if (o.BaseObject is not PSCustomObject)
        {
            arg = o.BaseObject;
        }

        var result = sb == null ? new() : sb.Invoke(arg);

        return result;
    }

    private ScriptBlock GetMapper(PSObject o)
    {
        if (Types.ContainsKey(o.BaseObject.GetType()))
            return Types[o.BaseObject.GetType()];

        foreach (object key in Types.Keys)
        {
            if (key is PSTypeName pstn && o.TypeNames.Contains(pstn.Name))
            {
                return Types[key];
            }
            else if (key is Type && (key as Type).IsAssignableFrom(o.BaseObject.GetType()))
            {
                return Types[key];
            }
        }

        return null;
    }
}