using System.Diagnostics.CodeAnalysis;
using TRImageControl.Packing;
using TRLevelControl;
using TRLevelControl.Helpers;
using TRLevelControl.Model;
using TRXInjectionTool.Control;
using TRXInjectionTool.Util;

namespace TRXInjectionTool.Types.TR2.Misc;

public class TR2SecretsBuilder : InjectionBuilder
{
    public override string ID => "secrets";

    private List<TRTextileRegion> _textureRegions;
    private List<TRObjectTexture> _texInfos;
    private Queue<TRObjectTexture> _tempTexQueue;

    public override List<InjectionData> Build()
    {
        TR2Level wall = _control2.Read($"Resources/{TR2LevelNames.GW}");
        {
            // Temp hack for enough tex info space
            TR2TexturePacker temp = new(wall);
            List<TR2Type> models = new() { TR2Type.TigerOrSnowLeopard, TR2Type.Crow, TR2Type.TRex };
            _tempTexQueue = new(temp.GetMeshRegions(models.SelectMany(m => wall.Models[m].Meshes))
                .Values.SelectMany(r => r)
                .SelectMany(r => r.Segments)
                .Select(s => s.Texture as TRObjectTexture));
        }

        _textureRegions = new();
        _texInfos = wall.ObjectTextures;

        List<ModelDef> modelDefs = DeserializeFile<List<ModelDef>>("Resources/TR2/Secrets/model_info.json");
        int i = 0;
        foreach (ModelDef modelDef in modelDefs)
        {
            if (modelDef.disabled)
            {
                continue;
            }

            wall.Models[(TR2Type)modelDef.tr_id] = CreateModel(modelDef);

            {
                // Temporary to ensure sprite equivalent is present
                TR2Type spriteType = (TR2Type)(modelDef.tr_id - 4);
                wall.Sprites[spriteType] = wall.Sprites[TR2Type.Key1_S_P].Clone();
                wall.Entities.Add(new()
                {
                    X = 35328 + (i / 3) * TRConsts.Step4,
                    Z = 33280 + (i % 3) * TRConsts.Step4,
                    TypeID = spriteType,
                    Intensity1 = -1,
                    Intensity2 = -1,
                });
                i++;
            }
        }

        {
            TR2Entity lara = wall.Entities.Find(e => e.TypeID == TR2Type.Lara);
            lara.X = 35328;
            lara.Y = 0;
            lara.Z = 32256;
            lara.Room = 0;
        }

        TR2TexturePacker packer = new(wall);
        packer.AddRectangles(_textureRegions);
        packer.Pack(true);

        _control2.Write(wall, MakeOutputPath(TRGameVersion.TR2, $"Debug/{TR2LevelNames.GW}"));

        // TODO: actually generate an injection
        return new() { };
    }

    private ushort CreateTexture(Material material, List<UV> uvs)
    {
        TRObjectTexture texInfo = new() { Vertices = new() };
        foreach (UV uv in uvs)
        {
            ushort u = (ushort)(uv.U * material.Image.Width);
            ushort v = (ushort)(uv.V * material.Image.Height);
            texInfo.Vertices.Add(new(u, v));
        }

        if (uvs.Count == 3)
        {
            texInfo.Vertices.Add(new() { U = 0, V = 0 });
        }

        TRTextileRegion region = _textureRegions.Find(r => r.Image == material.Image);
        if (region == null)
        {
            TRObjectTexture wrapperInfo = new(new(0, 0, material.Image.Width, material.Image.Height));
            region = new(new() { Texture = wrapperInfo }, material.Image);
            _texInfos.Add(wrapperInfo);
            _textureRegions.Add(region);
        }

        TRTextileSegment segment = region.Segments.Find(s => texInfo.IsEquivalent(s.Texture as TRObjectTexture));
        if (segment == null)
        {
            // Temp!
            int index;
            if (_tempTexQueue.Count > 0)
            {
                index = _texInfos.IndexOf(_tempTexQueue.Dequeue());
                _texInfos[index] = texInfo;
            }
            else
            {
                index = _texInfos.Count;
                _texInfos.Add(texInfo);
            }

            segment = new()
            {
                Index = index,
                Texture = texInfo,
            };
            region.AddTexture(segment);
        }

        return (ushort)segment.Index;
    }

    private TRModel CreateModel(ModelDef def)
    {
        ExternalMeshReader reader = new()
        {
            Scale = def.scale,
            FlipX = def.flip_x,
            FlipY = def.flip_y,
            FlipZ = def.flip_z,
            TextureCallback = CreateTexture,
        };

        TRMesh mesh = reader.Read(def.obj_file);

        TRModel model = new();        
        model.Meshes.Add(mesh);
        model.Animations.Add(new()
        {
            Accel = new(),
            Changes = new(),
            Commands = new(),
            FrameRate = 1,
            Frames = new()
            {
                new()
                {
                    Bounds = mesh.GetBounds(),
                    Rotations = new()
                    {
                        new(),
                    }
                }
            },
            Speed = new(),
        });

        return model;
    }
}

[SuppressMessage("Style", "IDE1006:Naming Styles")]
public class ModelDef
{
    public bool disabled { get; set; }
    public string obj_file { get; set; }
    public int tr_id { get; set; }
    public int scale { get; set; }
    public bool flip_x { get; set; }
    public bool flip_y { get; set; }
    public bool flip_z { get; set; }
}
