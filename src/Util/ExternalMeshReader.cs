using TRImageControl;
using TRLevelControl.Model;

namespace TRXInjectionTool.Util;

public class ExternalMeshReader
{
    public string WorkingDir { get; set; } = Directory.GetCurrentDirectory();

    private List<Material> _materials;
    private List<UV> _uvs;

    public int Scale { get; set; } = 1;
    public bool FlipX { get; set; }
    public bool FlipY { get; set; }
    public bool FlipZ { get; set; }
    public Func<Material, List<UV>, ushort> TextureCallback { get; set; }

    public TRMesh Read(string filename)
    {
        WorkingDir = Path.GetDirectoryName(filename);
        using StreamReader reader = new(File.OpenRead(filename));
        return Read(reader);
    }

    public TRMesh Read(StreamReader reader)
    {
        _materials = new();
        _uvs = new();

        TRMesh mesh = new();
        Material currentMaterial = null;

        string line;
        while ((line = reader.ReadLine()) != null)
        {
            string[] parts = line.Split(' ');
            if (parts.Length == 0 || parts[0] == "#")
            {
                continue;
            }

            string action = parts[0].ToLower();
            IEnumerable<string> args = parts[1..];

            switch (action)
            {
                case "mtllib":
                    LoadMaterial(parts[1]);
                    break;
                case "v":
                    ReadVertex(mesh, args.Select(a => double.Parse(a)).ToList());
                    break;
                case "vn":
                    ReadNormal(mesh, args.Select(a => double.Parse(a)).ToList());
                    break;
                case "vt":
                    ReadUV(args.Select(a => double.Parse(a)).ToList());
                    break;
                case "usemtl":
                    currentMaterial = _materials.Find(m => m.Name == parts[1]);
                    break;
                case "f":
                    ReadFace(mesh, args.ToList(), currentMaterial);
                    break;
            }
        }

        mesh.SelfCalculateBounds();

        // Counts must match in TR, this isn't ideal.
        while (mesh.Normals.Count < mesh.Vertices.Count)
        {
            mesh.Normals.Add(mesh.Normals[^1]);
        }

        return mesh;
    }

    private void LoadMaterial(string filename)
    {
        using StreamReader reader = new(File.OpenRead(Path.Combine(WorkingDir, filename)));

        string name = null;
        string textureFile = null;
        string line;
        while ((line = reader.ReadLine()) != null)
        {
            string[] parts = line.Split(' ');
            if (parts.Length == 0 || parts[0] == "#")
            {
                continue;
            }

            switch (parts[0])
            {
                case "newmtl":
                    name = parts[1];
                    break;
                case "map_Kd":
                    textureFile = parts[1];
                    break;
            }

            if (name != null && textureFile != null)
            {
                if (!_materials.Any(m => m.Name == name))
                {
                    _materials.Add(new()
                    {
                        Name = name,
                        Image = new(Path.Combine(WorkingDir, textureFile)),
                    });
                }

                name = null;
                textureFile = null;
            }
        }        
    }

    private void ReadVertex(TRMesh mesh, List<double> coords)
    {
        TRVertex vertex = ReadPosition(coords);
        if (FindVertex(mesh, vertex) == -1)
        {
            mesh.Vertices.Add(vertex);
        }
    }

    private void ReadNormal(TRMesh mesh, List<double> coords)
    {
        TRVertex normal = ReadPosition(coords);
        mesh.Normals ??= new();
        mesh.Normals.Add(normal);
    }

    private void ReadUV(List<double> values)
    {
        if (values.Count != 2)
        {
            throw new IOException();
        }

        _uvs.Add(new()
        {
            U = values[0],
            V = values[1],
        });
    }

    private TRVertex ReadPosition(List<double> coords)
    {
        if (coords.Count != 3)
        {
            throw new IOException();
        }

        return new()
        {
            X = (short)(coords[0] * Scale * (FlipX ? -1 : 1)),
            Y = (short)(coords[1] * Scale * (FlipY ? -1 : 1)),
            Z = (short)(coords[2] * Scale * (FlipZ ? -1 : 1)),
        };
    }

    private static int FindVertex(TRMesh mesh, TRVertex vertex)
    {
        return mesh.Vertices.FindIndex(v => v.X == vertex.X && v.Y == vertex.Y && v.Z == vertex.Z);
    }

    private void ReadFace(TRMesh mesh, List<string> args, Material material)
    {
        if (args.Count < 3 || args.Count > 5)
        {
            throw new ArgumentException($"Only triangles and quads are supported, face has {args.Count} vertices");
        }

        if (args.Count == 5)
        {
            // Assume we need a quad and a triangle
            ReadFace(mesh, args.GetRange(0, 4), material);
            ReadFace(mesh, new() { args[3], args[4], args[0], }, material);
            return;
        }

        TRMeshFace face = new()
        {
            Vertices = new(),
            Type = (TRFaceType)args.Count,
        };

        List<UV> faceUVs = new();
        foreach (string arg in args)
        {
            List<int> parts = arg.Split('/').Select(a => int.Parse(a)).ToList();
            if (parts.Count < 2)
            {
                throw new IOException();
            }

            int vertex = parts[0];
            int uv = parts[1];

            if (vertex < 1 || vertex > mesh.Vertices.Count)
            {
                throw new ArgumentException($"{arg}: Bad vertex index {vertex}. {mesh.Vertices.Count} vertices previously parsed");
            }

            if (uv < 1 || uv > _uvs.Count)
            {
                throw new ArgumentException($"{arg}: Bad UV index {uv}. {_uvs.Count} UVs previously parsed");
            }

            face.Vertices.Add((ushort)(vertex - 1));
            faceUVs.Add(_uvs[uv - 1]);            
        }

        face.Texture = TextureCallback?.Invoke(material, faceUVs) ?? 0;

        if (face.Type == TRFaceType.Triangle)
        {
            mesh.TexturedTriangles.Add(face);
        }
        else
        {
            mesh.TexturedRectangles.Add(face);
        }
    }
}

public class Material
{
    public string Name { get; set; }
    public TRImage Image { get; set; }
}

public class UV
{
    public double U { get; set; }
    public double V { get; set; }
}
