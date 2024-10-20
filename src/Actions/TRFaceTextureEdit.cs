﻿using TRLevelControl;

namespace TRXInjectionTool.Actions;

public enum TRMeshFaceType
{
    TexturedQuad,
    TexturedTriangle,
    ColouredQuad,
    ColouredTriangle
}

public class TRFaceTextureEdit
{
    public uint ModelID { get; set; }
    public short MeshIndex { get; set; } // If < 0, indicates specific palette entry
    public TRMeshFaceType FaceType { get; set; }
    public short FaceIndex { get; set; }
    public List<short> TargetFaceIndices { get; set; } = new();

    public void Serialize(TRLevelWriter writer)
    {
        writer.Write(ModelID);
        writer.Write(MeshIndex);
        writer.Write((uint)FaceType);
        writer.Write(FaceIndex);
        writer.Write((uint)TargetFaceIndices.Count);
        writer.Write(TargetFaceIndices);
    }
}