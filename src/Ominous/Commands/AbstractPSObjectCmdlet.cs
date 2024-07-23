using System.Collections;
using System.Collections.Generic;
using System.Management.Automation;
using Ominous.Extensions;
using Ominous.Model;

namespace Ominous.Commands;

public abstract class AbstractPSObjectCmdlet : AbstractPSCmdlet
{
    [Parameter(ValueFromPipeline = true, Mandatory = true, Position = 0, ValueFromPipelineByPropertyName = true, HelpMessage = "object to convert to markdown")]
    public PSObject InputObject { get; set; }
    protected List<PSObject> Items { get; private set; } = new List<PSObject>();

    protected override void ProcessRecord()
    {
        if (MyInvocation.ExpectingInput)
        {
            TryAddMapped(InputObject);
        }
        else
        {
            if (!InputObject.IsDictionary() && InputObject.IsIterable())
            {
                foreach (var i in (IEnumerable)InputObject.BaseObject)
                {
                    TryAddMapped(i);
                }
            }
            else
            {
                TryAddMapped(InputObject);
            }
        }
    }

    private bool CanMap(PSObject o) =>
        o.BaseObject is CmdletInfo || !NoMappers.IsPresent && State.TypeMapperService.CanMap(o);

    private void TryAddMapped(object o)
    {
        var pso = ObjectExtensions.ToPSObject(o);
        if (CanMap(pso))
        {
            var resolved = State.TypeMapperService.Map(pso);
            if (resolved.Count > 0)
            {
                foreach (var item in resolved)
                {
                    Items.Add(PSObjectExtensions.Resolve(item));
                }
            }
        }
        else
        {
            Items.Add(pso);
        }
    }

    private void TryAddMapped(PSObject o)
    {
        if (CanMap(o))
        {
            var resolved = State.TypeMapperService.Map(o);
            if (resolved.Count > 0)
            {
                foreach (var item in resolved)
                {
                    if (CanMap(item))
                    {
                        resolved = State.TypeMapperService.Map(item);
                        if (resolved.Count > 0)
                        {
                            foreach (var mapped in resolved)
                            {
                                Items.Add(PSObjectExtensions.Resolve(mapped));
                            }
                        }
                    }
                    else
                    {
                        Items.Add(PSObjectExtensions.Resolve(item));
                    }
                }
            }
        }
        else
        {
            Items.Add(PSObjectExtensions.Resolve(InputObject));
        }
    }

    protected override void EndProcessing()
    {
        base.EndProcessing();

        if (Items.Count == 0)
        {
            StopProcessing();
        }
    }
}