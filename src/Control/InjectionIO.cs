using TRLevelReader.Model;
using TRXInjectionTool.Actions;

namespace TRXInjectionTool.Control;

public static class InjectionIO
{
    public static readonly uint Magic = MakeTag('T', '1', 'M', 'J');
    public static readonly uint CurrentVersion = 8;

    public static void Export(InjectionData data, string file)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(Path.GetFullPath(file)));
        File.WriteAllBytes(file, Serialize(data));
    }

    private static uint MakeTag(char a, char b, char c, char d)
    {
        return (uint)(a | b << 8 | c << 16 | d << 24);
    }

    public static byte[] Serialize(InjectionData data)
    {
        using MemoryStream stream = new();
        using BinaryWriter writer = new(stream);

        List<byte> meshData = new();
        foreach (TRMesh mesh in data.Meshes)
            meshData.AddRange(mesh.Serialize());

        {
            // Header
            writer.Write(Magic);
            writer.Write(CurrentVersion);
            writer.Write((uint)data.InjectionType);
            writer.Write((uint)data.Images8.Count);
            writer.Write((uint)data.ObjectTextures.Count);
            writer.Write((uint)data.SpriteTextures.Count);
            writer.Write((uint)data.SpriteSequences.Count);
            writer.Write((uint)(meshData.Count / 2));
            writer.Write((uint)data.MeshPointers.Count);
            writer.Write((uint)data.AnimChanges.Count);
            writer.Write((uint)data.AnimDispatches.Count);
            writer.Write((uint)data.AnimCommands.Count);
            writer.Write((uint)(data.MeshTrees.Count * 4));
            writer.Write((uint)data.AnimFrames.Count);
            writer.Write((uint)data.Animations.Count);
            writer.Write((uint)data.Models.Count);
            writer.Write((uint)data.SFX.Count);
            uint sampleDataSize = 0;
            uint sampleCount = 0;
            foreach (TRSFXData sfx in data.SFX)
            {
                sampleDataSize += sfx.GetSampleDataSize();
                sampleCount += (uint)((sfx.Characteristics & 0xFC) >> 2);
            }
            writer.Write(sampleDataSize);
            writer.Write(sampleCount);
            writer.Write((uint)data.MeshEdits.Count);
            writer.Write((uint)data.TextureOverwrites.Count);
            writer.Write((uint)data.FloorEdits.Count);

            WriteRoomMeshData(writer, data); // Summary of room mesh size changes
            writer.Write((uint)data.RoomEdits.Count); // Actual fixes
            writer.Write((uint)data.VisPortalEdits.Count);
            writer.Write((uint)data.AnimRangeEdits.Count);
            writer.Write((uint)data.ItemEdits.Count);
        }

        foreach (TRColour col in data.Palette)
            writer.Write(col.Serialize());

        foreach (TRTexImage8 tex in data.Images8)
            writer.Write(tex.Serialize());

        foreach (TRObjectTexture tex in data.ObjectTextures)
            writer.Write(tex.Serialize());

        foreach (TRSpriteTexture spr in data.SpriteTextures)
            writer.Write(spr.Serialize());

        foreach (TRSpriteSequence spr in data.SpriteSequences)
            writer.Write(spr.Serialize());

        writer.Write(meshData.ToArray());

        foreach (uint ptr in data.MeshPointers)
            writer.Write(ptr);

        foreach (TRStateChange change in data.AnimChanges)
            writer.Write(change.Serialize());

        foreach (TRAnimDispatch dispatch in data.AnimDispatches)
            writer.Write(dispatch.Serialize());

        foreach (TRAnimCommand command in data.AnimCommands)
            writer.Write(command.Serialize());

        foreach (TRMeshTreeNode node in data.MeshTrees)
            writer.Write(node.Serialize());

        foreach (ushort frame in data.AnimFrames)
            writer.Write(frame);

        foreach (TRAnimation anim in data.Animations)
            writer.Write(anim.Serialize());

        foreach (TRModel model in data.Models)
            writer.Write(model.Serialize());

        foreach (TRSFXData sfx in data.SFX)
            writer.Write(sfx.Serialize());

        foreach (TRMeshEdit edit in data.MeshEdits)
            writer.Write(edit.Serialize());

        foreach (TRTextureOverwrite edit in data.TextureOverwrites)
            writer.Write(edit.Serialize());

        foreach (TRFloorDataEdit edit in data.FloorEdits)
            writer.Write(edit.Serialize());

        foreach (TRRoomTextureEdit edit in data.RoomEdits)
            writer.Write(edit.Serialize());

        foreach (TRVisPortalEdit edit in data.VisPortalEdits)
            writer.Write(edit.Serialize());

        foreach (TRAnimRangeEdit edit in data.AnimRangeEdits)
            writer.Write(edit.Serialize());

        foreach (TRItemEdit edit in data.ItemEdits)
            writer.Write(edit.Serialize());

        return stream.ToArray();
    }

    private static void WriteRoomMeshData(BinaryWriter writer, InjectionData data)
    {
        Dictionary<short, uint> sizes = new();
        foreach (TRRoomTextureEdit edit in data.RoomEdits)
        {
            uint size = edit.AdditionalMeshLength;
            if (size == 0)
                continue;

            if (!sizes.ContainsKey(edit.RoomIndex))
                sizes[edit.RoomIndex] = size;
            else
                sizes[edit.RoomIndex] += size;
        }

        writer.Write((uint)sizes.Count);
        foreach (short room in sizes.Keys)
        {
            writer.Write(room);
            writer.Write(sizes[room]);
        }
    }
}
