$local:Synopsis = "Convert to markdown code block (fence block) syntax"

$local:Description = @"
This command always uses backtick syntax as opposed to indentation syntax

Do **not** use this command for inline code syntax, this always assumes fence code block syntax is needed. For inline code refer to creating an AfterBlock
"@

$local:RelatedLinks = @()

$local:Examples =
@{
    Code = @"
`@`"
namespace Ominous.Utils;

public sealed class MarkdownUtils
{
    public static void Escape(ref string s, params char[] chars)
    {
        foreach (char ch in chars)
        {
            s = s.Replace(`$`"{ch}`", `$`"\\{ch}`");
        }
    }
}
`"`@ | ConvertTo-Code -Language csharp
"@
}