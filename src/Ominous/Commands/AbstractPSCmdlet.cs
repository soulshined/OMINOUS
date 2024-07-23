using System;
using System.Collections.Generic;
using System.Management.Automation;
using Ominous.Commands.Transformers;
using Ominous.Model;

namespace Ominous.Commands;

[CmdletBinding]
public abstract class AbstractPSCmdlet : PSCmdlet
{
    [Parameter(HelpMessage = "An after block is a scriptblock with ominous attributes declared of which the content provided to it will be styled by")]
    [Alias("After")]
    [AfterBlockTransformation]
    public virtual AfterBlock[] AfterBlock { get; set; }

    [Parameter(HelpMessage = "Prevent trailing line sequences in converted output")]
    public SwitchParameter NoNewLine { get; set; }

    [Parameter(HelpMessage = "Coerce conversion to not pass input objects to type mappers, even if they are defined")]
    public SwitchParameter NoMappers { get; set; }

    private List<AfterBlock> _afterBlocks;
    protected ref List<AfterBlock> AfterBlocksByReference => ref _afterBlocks;

    internal OminousPreference Preference { get; private set; }
    private State _state;
    internal ref State State => ref _state;

    protected override void BeginProcessing()
    {
        base.BeginProcessing();
        _afterBlocks = new List<AfterBlock>(AfterBlock ?? Array.Empty<AfterBlock>());
        Preference = new(SessionState.PSVariable.Get("OminousPreference"));
        base.WriteVerbose($"[ominous+Preference]: Global Preferences => {Preference}");

        State = new State(this, ref AfterBlocksByReference);

        if (!Preference.NoNewLine && NoNewLine.IsPresent)
        {
            NoNewLine = new SwitchParameter(true);
            Preference.NoNewLine = true;
        }

        if (!Preference.NoMappers && NoMappers.IsPresent)
        {
            NoMappers = new SwitchParameter(true);
            Preference.NoMappers = true;
        }

        WriteVerbose("Preferences => {0}", Preference);
    }

    private string Log(string s) => $"[ominous+{MyInvocation.MyCommand.Name}]: {s}";

    protected void WriteVerbose(string format, params object[] args) => base.WriteVerbose(string.Format(Log(format), args));

    protected void WriteDebug(string format, params object[] args) => base.WriteDebug(string.Format(Log(format), args));
}