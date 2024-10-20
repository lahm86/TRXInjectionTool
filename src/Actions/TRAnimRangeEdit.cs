namespace TRXInjectionTool.Actions;

public class TRAnimRangeEdit
{
    public uint ModelID { get; set; }
    public ushort AnimIndex { get; set; }
    public List<TRAnimRange> Ranges { get; set; }

    public byte[] Serialize()
    {
        using MemoryStream stream = new();
        using BinaryWriter writer = new(stream);
        writer.Write(ModelID);
        writer.Write(AnimIndex);
        writer.Write((uint)Ranges.Count);
        foreach (TRAnimRange range in Ranges)
        {
            writer.Write(range.Serialize());
        }

        return stream.ToArray();
    }
}

public class TRAnimRange
{
    public short ChangeOffset { get; set; }
    public short RangeOffset { get; set; }
    public short Low { get; set; }
    public short High { get; set; }

    public byte[] Serialize()
    {
        using MemoryStream stream = new();
        using BinaryWriter writer = new(stream);
        writer.Write(ChangeOffset);
        writer.Write(RangeOffset);
        writer.Write(Low);
        writer.Write(High);

        return stream.ToArray();
    }
}
