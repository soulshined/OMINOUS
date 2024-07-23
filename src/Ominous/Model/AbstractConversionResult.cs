using System;
using Ominous.Constants;

namespace Ominous.Model;

internal abstract class AbstractConversionResult : IMkdnConvertible
{
    internal readonly uint Depth = 0;
    private State _state;
    protected ref State State => ref _state;

    private AbstractConversionResult() => throw new NotSupportedException();

    protected AbstractConversionResult(uint depth, ref State state)
    {
        Depth = depth;
        _state = state;
    }

    public abstract string ToMarkdown(FlavorType flavor);
    public abstract string ToHtml(FlavorType flavor);

    protected string ExecAfterBlocks(string s, bool isHTML = false)
    {
        foreach (var afb in State.AfterBlocks)
        {
            s = afb.Apply(s);
            afb.Style(ref s, isHTML);
        }

        return s;
    }

    protected string EOL => State.Initiator.NoNewLine ? "" : Environment.NewLine;

}