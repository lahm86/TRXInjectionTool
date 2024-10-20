using TRLevelControl.Model;

namespace TRXInjectionTool.Actions;

public class TRItemEdit
{
    public short Index { get; set; }
    public TR1Entity Item { get; set; }

    public byte[] Serialize()
    {
        using MemoryStream stream = new();
        using BinaryWriter writer = new(stream);
        writer.Write(Index);
        writer.Write(Item.Angle);
        writer.Write(Item.X);
        writer.Write(Item.Y);
        writer.Write(Item.Z);
        writer.Write(Item.Room);
        writer.Write((uint)Item.TypeID);
        writer.Write(Item.Flags);

        return stream.ToArray();
    }
}
