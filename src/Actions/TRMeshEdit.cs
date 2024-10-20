using TRLevelControl;
using TRLevelControl.Model;

namespace TRXInjectionTool.Actions;

public class TRMeshEdit
{
    public uint ModelID { get; set; }
    public short MeshIndex { get; set; }
    public TRVertex Centre { get; set; } = new();
    public int CollRadius { get; set; }
    public List<TRFaceTextureEdit> FaceEdits { get; set; } = new();
    public List<TRVertexEdit> VertexEdits { get; set; } = new();

    public byte[] Serialize()
    {
        using MemoryStream stream = new();
        using TRLevelWriter writer = new(stream);
        writer.Write(ModelID);
        writer.Write(MeshIndex);
        writer.Write(Centre);
        writer.Write(CollRadius);
        writer.Write((uint)FaceEdits.Count);
        foreach (TRFaceTextureEdit data in FaceEdits)
        {
            writer.Write(data.Serialize());
        }

        writer.Write((uint)VertexEdits.Count);
        foreach (TRVertexEdit data in VertexEdits)
        {
            writer.Write(data.Serialize());
        }

        return stream.ToArray();
    }
}
