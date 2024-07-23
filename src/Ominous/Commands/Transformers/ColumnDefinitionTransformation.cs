using System;
using System.Collections;
using System.Collections.Generic;
using System.Management.Automation;
using Ominous.Constants;
using Ominous.Model.Table;

namespace Ominous.Commands.Transformers;

internal sealed class ColumnDefinitionTransformationAttribute : ArgumentTransformationAttribute
{
    public override object Transform(EngineIntrinsics engineIntrinsics, object inputData)
    {
        if (inputData.GetType().IsArray)
        {
            List<ColumnDefinition> defs = new();
            foreach (var i in (object[])inputData)
            {
                if (typeof(IDictionary).IsAssignableFrom(i.GetType()))
                {
                    var dict = (IDictionary)i;
                    var dictAlignment = dict.Contains("Alignment") ? dict["Alignment"] : "Left";
                    Enum.TryParse(dictAlignment.ToString(), true, out AlignmentType alignment);
                    var name = dict.Contains("Name") ? dict["Name"].ToString() : "";
                    var label = dict.Contains("Label") ? dict["Label"].ToString() : name;
                    defs.Add(new ColumnDefinition(alignment, name, label));
                }
                else if (i is string s)
                {
                    defs.Add(new ColumnDefinition(s));
                }
                else if (i is AlignmentType a)
                {
                    defs.Add(new ColumnDefinition(a, "", ""));
                }
                else if (i is ColumnDefinition cdef)
                {
                    defs.Add(cdef);
                }
            }

            return defs;
        }
        else if (typeof(IDictionary).IsAssignableFrom(inputData.GetType()))
        {
            var dict = (IDictionary)inputData;
            var inputAlignment = (string)(dict.Contains("Alignment") ? dict["Alignment"] : "Left");
            Enum.TryParse(inputAlignment, true, out AlignmentType alignment);
            var name = dict.Contains("Name") ? dict["Name"].ToString() : "";
            var label = dict.Contains("Label") ? dict["Label"].ToString() : name;

            return new List<ColumnDefinition>(new ColumnDefinition[] { new(alignment, name, label) });
        }

        return inputData;
    }
}