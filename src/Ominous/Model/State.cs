using System.Collections.Generic;
using Ominous.Commands;
using Ominous.Constants;

namespace Ominous.Model;

internal record State
{
    private List<AfterBlock> _afterBlocks;
    private State _previous;
    private State _root;
    private ref State Previous => ref _previous;
    private ref State Root => ref _root;

    public ref List<AfterBlock> AfterBlocks => ref _afterBlocks;
    public readonly AbstractPSCmdlet Initiator;
    public readonly FlavorType Flavor;
    public TypeMappers TypeMapperService { get; } = new TypeMappers();

    internal State(AbstractPSCmdlet initiator, ref List<AfterBlock> afterBlocks)
    {
        Initiator = initiator;
        Flavor = Initiator.Preference.Flavor;
        _afterBlocks = afterBlocks;
        _root = null;
        var tms = initiator.SessionState.PSVariable.Get("OminousTypeMappers");
        if (null != tms)
        {
            TypeMapperService = tms.Value as TypeMappers;
        }
    }

    internal State(AbstractPSCmdlet initiator, ref List<AfterBlock> afterBlocks, ref State previous)
    {
        Initiator = initiator;
        Flavor = initiator.Preference.Flavor;
        _afterBlocks = afterBlocks;
        _previous = previous;
        _root = previous.Root;
        TypeMapperService = previous.TypeMapperService;
    }

    public bool HasPrevious => Previous != null;
    public bool HasRoot => Root != null;
}