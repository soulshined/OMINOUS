$global:OminousTypeAccelerators = [PowerShell].Assembly.GetType("System.Management.Automation.TypeAccelerators")
$global:OminousTypeAccelerators::Add("MarkdownUtils", 'Ominous.Utils.MarkdownUtils')

foreach ($attr in @(
        'Style.Bold',
        'Style.Center',
        'Style.Center',
        'Style.Code',
        'Style.Color',
        'Style.Highlight',
        'Style.Indent',
        'Style.Italic',
        'Style.Strikethrough',
        'Style.Subscript',
        'Style.Superscript',
        'Style.Underline'
    )) {
    $global:OminousTypeAccelerators::Add($attr, "Ominous.Attributes.Markdown.${attr}Attribute")
}