using System;
using System.Collections;
using System.Management.Automation;
using Ominous.Constants;

namespace Ominous.Model;

public record OminousPreference
{
    public FlavorType Flavor { get; set; } = FlavorType.Unspecified;
    public bool NoNewLine { get; set; } = false;
    public bool NoMappers { get; set; } = false;

    public OminousPreference() { }

    internal OminousPreference(PSVariable input)
    {
        if (null == input) return;
        if (input.Value.GetType() != typeof(Hashtable))
            return;

        Hashtable preference = (Hashtable)input.Value;
        if (preference.ContainsKey("Flavor"))
        {
            Enum.TryParse(preference["Flavor"].ToString(), true, out FlavorType flavor);
            Flavor = flavor;
        }

        NoNewLine = GetBool(preference, "NoNewLine");
        NoMappers = GetBool(preference, "NoMappers");
    }

    private bool GetBool(Hashtable ht, string key)
    {
        if (!ht.ContainsKey(key)) return false;

        var parsed = bool.TryParse(ht[key].ToString(), out bool keyOut);
        return parsed && keyOut;
    }

    public override string ToString() => $"{{ Flavor= {Flavor}, NoMappers= {NoMappers}, NoNewLine= {NoNewLine}  }}";
}