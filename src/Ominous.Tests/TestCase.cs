using System.Text.RegularExpressions;
using Ominous.Tests.Extensions;

namespace Ominous.Tests;

public record struct TestCaseGivenConfiguration(bool IsDisabled, string Arguments, string Type);

public partial class TestCase
{

    public string Path { get; }
    public string FileName { get; }
    public string DirectoryName { get; }
    public string Given { get; }
    public string Expected { get; }
    public TestCaseGivenConfiguration GivenConfig { get; } = new TestCaseGivenConfiguration();
    private static readonly HashSet<string> GivenConfigOptions = new(){
        "ignore",
        "args",
        "type"
    };
    private static readonly char[] ConfigDelimiters = new char[] { ':' };

    public TestCase(string path)
    {
        Path = path;
        var content = File.ReadAllText(path);
        FileName = System.IO.Path.GetFileName(Path);
        DirectoryName = Directory.GetParent(path)?.Name ?? "cases";

        var givenConfig = new TestCaseGivenConfiguration(false, "", "command");

        var splits = content.Split("=== Expect", 2);

        if (splits.Length != 2)
        {
            throw new Exception($"Given clause & expected markdown must be provided for test cases @ {Path}");
        }

        Given = splits[0].Trim();
        Expected = EOL().Split(splits[1].TrimStart()).Join();

        if (string.IsNullOrEmpty(Given) || !Given.StartsWith("=== Given") && !Given.StartsWith("=== Given ==="))
        {
            throw new Exception($"Given clause must be provided @ {Path}");
        }

        var givenSplits = EOL().Split(Given.Trim());
        var metadataEndingMatchResult = givenSplits[1..].Find(i => i.Trim().Equals("==="));
        var containsMetadata = metadataEndingMatchResult != null && metadataEndingMatchResult.Index > -1;

        for (int i = 1; i < givenSplits.Length; i++)
        {
            string line = givenSplits[i];
            if (containsMetadata && line.Trim().Equals("==="))
            {
                Given = givenSplits[(i + 1)..].Join().Trim();
                break;
            }

            var lineSplits = line.Split(ConfigDelimiters, 2);
            if (lineSplits.Length == 2)
            {
                string identifier = lineSplits[0].ToLower().Trim();
                if (!GivenConfigOptions.Contains(identifier))
                    throw new Exception($"Unknown test case given configuration option: {lineSplits[0]}");

                switch (identifier)
                {
                    case "ignore":
                        givenConfig.IsDisabled = bool.Parse(lineSplits[1].Trim());
                        break;
                    case "args":
                        givenConfig.Arguments = lineSplits[1].Trim();
                        break;
                    case "type":
                        givenConfig.Type = lineSplits[1].Trim();
                        break;
                }
            }
            else
            {
                Given = givenSplits[i..].Join().Trim();
                break;
            }
        }

        if (string.IsNullOrWhiteSpace(Expected))
        {
            throw new Exception($"Expected markdown must be provided @ {Path}");
        }

        GivenConfig = givenConfig;
    }

    [GeneratedRegex("\r?\n")]
    private static partial Regex EOL();
}