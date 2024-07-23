#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace Ominous.Constants;
#pragma warning restore IDE0130 // Namespace does not match folder structure

public static class Nouns
{
    public const string Admonition = "Admonition";
    public const string AfterBlock = "AfterBlock";
    public const string Blockquote = "Blockquote";
    public const string Code = "Code";
    public const string Details = "Details";
    public const string Heading = "Heading";
    public const string Image = "Image";
    public const string Link = "Link";
    public const string List = "List";
    public const string Table = "Table";
}

public enum FlavorType
{

    Unspecified,
    Github

}

public enum AdmonitionType
{
    CAUTION,
    NOTE,
    IMPORTANT,
    TIP,
    WARNING
}

public enum AlignmentType
{
    Left,
    Right,
    Center
}

public enum ListType
{
    Unordered,
    Ordered,
    Task
}