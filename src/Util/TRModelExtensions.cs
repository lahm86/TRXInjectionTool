using TRLevelControl.Model;

namespace TRXInjectionTool.Util;

public static class TRModelExtensions
{
    public static TRBoundingBox GetBounds(this TRMesh mesh)
    {
        return new()
        {
            MinX = mesh.Vertices.Min(v => v.X),
            MaxX = mesh.Vertices.Max(v => v.X),
            MinY = mesh.Vertices.Min(v => v.Y),
            MaxY = mesh.Vertices.Max(v => v.Y),
            MinZ = mesh.Vertices.Min(v => v.Z),
            MaxZ = mesh.Vertices.Max(v => v.Z),
        };
    }

    public static void SelfCalculateBounds(this TRMesh mesh)
    {
        TRBoundingBox box = mesh.GetBounds();

        mesh.Centre = new()
        {
            X = (short)((box.MinX + box.MaxX) / 2),
            Y = (short)((box.MinY + box.MaxY) / 2),
            Z = (short)((box.MinZ + box.MaxZ) / 2),
        };

        int xs = Math.Abs(box.MaxX - box.MinX);
        int ys = Math.Abs(box.MaxY - box.MinY);
        int zs = Math.Abs(box.MaxZ - box.MinZ);

        double inner = Math.Max(xs, Math.Max(ys, zs)) / 2d;
        double outer = Math.Sqrt(Math.Pow(xs, 2) + Math.Pow(ys, 2) + Math.Pow(zs, 2)) / 2d;
        mesh.CollRadius = (int)Math.Ceiling((inner + outer) / 2d);
    }

    public static bool IsEquivalent(this TRObjectTexture texture1, TRObjectTexture texture2)
    {
        if (texture1.Vertices.Count != texture2.Vertices.Count)
        {
            return false;
        }

        for (int i = 0; i < texture1.Vertices.Count; i++)
        {
            TRObjectTextureVert vert1 = texture1.Vertices[i];
            TRObjectTextureVert vert2 = texture2.Vertices[i];
            if (vert1.U != vert2.U || vert1.V != vert2.V)
            {
                return false;
            }
        }

        return true;
    }
}
