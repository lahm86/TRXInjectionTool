using TRLevelControl;
using TRLevelControl.Model;

namespace TRXInjectionTool.Actions;

public class TRVertexEdit
{
    public short Index { get; set; }
    public TRVertex Change { get; set; }

    public byte[] Serialize()
    {
        using MemoryStream stream = new();
        using TRLevelWriter writer = new(stream);
        writer.Write(Index);
        writer.Write(Change);

        return stream.ToArray();
    }
}
