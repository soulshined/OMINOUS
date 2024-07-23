using System;
using System.Text;
using Ominous.Constants;

namespace Ominous.Model.Table;

public sealed class ColumnDefinition
{
    public AlignmentType Alignment { get; } = AlignmentType.Left;
    public string Name { get; }
    public string Label { get; }

    internal bool IsExclusivelyPositional { get; } = false;

    public string SubHeader
    {
        get
        {
            StringBuilder s = new();
            switch (Alignment)
            {
                case AlignmentType.Center:
                    s.Append(":");
                    break;
            }

            s.Append("-");

            switch (Alignment)
            {
                case AlignmentType.Center:
                case AlignmentType.Right:
                    s.Append(":");
                    break;
            }

            return s.ToString();
        }
    }

    private ColumnDefinition()
    {
        throw new NotSupportedException();
    }
    public ColumnDefinition(string alignment) : this((AlignmentType)Enum.Parse(typeof(AlignmentType), alignment, true), "")
    {
        IsExclusivelyPositional = true;
    }
    public ColumnDefinition(AlignmentType alignment, string name) : this(alignment, name, name) { }
    public ColumnDefinition(AlignmentType alignment, string name, string label)
    {
        Alignment = alignment;
        Name = name.Trim();
        Label = string.IsNullOrWhiteSpace(label) ? Name : label.Trim();
    }

    public override bool Equals(object obj)
    {
        if (null == obj) return false;
        if (typeof(ColumnDefinition) != obj.GetType()) return false;

        var other = (ColumnDefinition)obj;

        return Alignment == other.Alignment &&
            Name.Equals(other.Name, StringComparison.CurrentCultureIgnoreCase) &&
            Label.Equals(other.Label, StringComparison.CurrentCultureIgnoreCase) &&
            IsExclusivelyPositional == other.IsExclusivelyPositional;
    }

    public override int GetHashCode() => (Alignment, Name, Label, IsExclusivelyPositional).GetHashCode();
}