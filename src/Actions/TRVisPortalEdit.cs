using System.Diagnostics;
using TRLevelControl;
using TRLevelControl.Model;

namespace TRXInjectionTool.Actions;

public class TRVisPortalEdit
{
    public short BaseRoom { get; set; }
    public short LinkRoom { get; set; }
    public ushort PortalIndex { get; set; }
    public List<TRVertex> VertexChanges { get; set; }

    public byte[] Serialize()
    {
        Debug.Assert(VertexChanges.Count == 4);
        using MemoryStream stream = new();
        using TRLevelWriter writer = new(stream);
        writer.Write(BaseRoom);
        writer.Write(LinkRoom);
        writer.Write(PortalIndex);
        writer.Write(VertexChanges);

        return stream.ToArray();
    }
}
