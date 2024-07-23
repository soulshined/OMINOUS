using System.Management.Automation;
using System.Management.Automation.Runspaces;

namespace Ominous.Tests;

public class PSCmdletRunner
{

    private static readonly string MODULE_PATH = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "../../../Ominous/OMINOUS.psd1"));
    private static readonly InitialSessionState Session = InitialSessionState.CreateDefault();

    static PSCmdletRunner()
    {
        Session.ImportPSModule(new string[] { MODULE_PATH });
    }

    public static RunnerResult InvokeCommand(string command, Dictionary<string, object>? parameters)
    {
        var pwsh = GetInstance();
        pwsh.AddCommand(command);

        if (null != parameters)
            pwsh.AddParameters(parameters);

        return new RunnerResult(pwsh);
    }

    public static RunnerResult InvokeCommand(string command, params object[] args)
    {
        var pwsh = GetInstance();
        pwsh.AddCommand(command);
        foreach (var arg in args)
            pwsh.AddArgument(arg);

        return new RunnerResult(pwsh);
    }

    public static RunnerResult InvokeExpression(string expression)
    {
        var pwsh = GetInstance();
        pwsh.AddCommand("Invoke-Expression").AddArgument(expression);
        return new RunnerResult(pwsh);
    }

    private static PowerShell GetInstance()
    {
        var rs = RunspaceFactory.CreateRunspace(Session);
        rs.Open();
        var pwsh = PowerShell.Create();
        pwsh.Runspace = rs;
        return pwsh;
    }

    public class RunnerResult
    {
        public bool HadErrors { get; }
        public PSInvocationStateInfo InvocationStateInfo { get; }

        public List<PSObject> Output { get; } = new();
        public List<InformationRecord> InformationStream { get; } = new();
        public List<DebugRecord> DebugStream { get; } = new();
        public List<WarningRecord> WarningStream { get; } = new();
        public List<ProgressRecord> ProgressStream { get; } = new();
        public List<ErrorRecord> ErrorStream { get; } = new();
        public List<VerboseRecord> VerboseStream { get; } = new();

        public bool IsSuccess
        {
            get
            {
                return !HadErrors && InvocationStateInfo.State == PSInvocationState.Completed;
            }
        }


        internal RunnerResult(PowerShell pwsh)
        {
            pwsh.InvocationStateChanged += (sender, args) =>
            {
                if (null != args && sender is PowerShell thisps)
                {
                    var state = args.InvocationStateInfo;
                    thisps.Streams.Information.ToList().ForEach(i => InformationStream.Add(i));
                    thisps.Streams.Debug.ToList().ForEach(d => DebugStream.Add(d));
                    thisps.Streams.Warning.ToList().ForEach(w => WarningStream.Add(w));
                    thisps.Streams.Error.ToList().ForEach(e => ErrorStream.Add(e));
                    thisps.Streams.Progress.ToList().ForEach(p => ProgressStream.Add(p));
                    thisps.Streams.Verbose.ToList().ForEach(v => VerboseStream.Add(v));
                }
            };
            try
            {
                Output = pwsh.Invoke().ToList();
            }
            catch (RuntimeException e)
            {
                ErrorStream.Add(e.ErrorRecord);
            }
            finally
            {
                HadErrors = pwsh.HadErrors;
                InvocationStateInfo = pwsh.InvocationStateInfo;
            }
        }

    }

}