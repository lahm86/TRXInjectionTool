using System.Drawing;
using TRImageControl;
using TRImageControl.Packing;
using TRLevelControl.Helpers;
using TRLevelControl.Model;
using TRXInjectionTool.Control;
using TRXInjectionTool.Util;

namespace TRXInjectionTool.Types.TR1.Misc;

public class TR1FontBuilder : InjectionBuilder
{
    private static readonly Dictionary<TR1Type, string> _data = new()
    {
        [TR1Type.FontGraphics_S_H] = "Resources/TR1/Font/font_glyph_info.json",
        [TR1Type.Unused03] = "Resources/TR1/Font/frame_glyph_info.json",
    };

    private readonly List<BuildData> _buildData;
    private readonly Dictionary<string, TRImage> _imageCache;

    public override string ID => "font";

    public TR1FontBuilder()
    {
        _buildData = new();
        _imageCache = new();

        foreach (var (type, srcPath) in _data)
        {
            BuildData data = new()
            {
                ID = type,
                Glyphs = DeserializeFile<List<GlyphDef>>(srcPath),
                Sequence = new(),
            };
            data.Glyphs.Sort((g1, g2) => g1.mesh_num.CompareTo(g2.mesh_num));
            _buildData.Add(data);
        }
    }

    public TRImage GetImage(GlyphDef glyph)
    {
        string path = Path.Combine("Resources/TR1/Font", glyph.filename);
        if (!_imageCache.ContainsKey(path))
        {
            _imageCache[path] = new(path);
        }

        TRImage image = _imageCache[path];
        if (!_imageCache.ContainsKey(glyph.filename))
        {
            _imageCache[glyph.filename] = image;
        }

        Rectangle bounds = new(glyph.x, glyph.y, glyph.w, glyph.h);
        return image.Export(bounds);
    }

    public override List<InjectionData> Build()
    {
        List<InjectionData> result = new();
        foreach (BuildData buildData in _buildData)
        {
            TR1Level caves = _control1.Read($"Resources/{TR1LevelNames.CAVES}");
            ResetLevel(caves, 1);
            List<TRTextileRegion> regions = new();

            foreach (GlyphDef glyph in buildData.Glyphs)
            {
                TRSpriteTexture texture = new()
                {
                    Bounds = new(glyph.x, glyph.y, glyph.w, glyph.h),
                    Alignment = new()
                    {
                        Left = glyph.l,
                        Top = glyph.t,
                        Right = glyph.r,
                        Bottom = glyph.b,
                    },
                };

                buildData.Sequence.Textures.Add(texture);

                regions.Add(new()
                {
                    Bounds = texture.Bounds,
                    Image = GetImage(glyph),
                    Segments = new()
                    {
                        new()
                        {
                            Index = glyph.mesh_num,
                            Texture = texture,
                        },
                    },
                });
            }

            TR1TexturePacker packer = new(caves);
            packer.AddRectangles(regions);
            packer.Pack(true);

            caves.Sprites[buildData.ID] = buildData.Sequence;
            _control1.Write(caves, MakeOutputPath(TRGameVersion.TR1, $"Debug/{ID}.phd"));
            result.Add(InjectionData.Create(caves, InjectionType.General, buildData.ID.ToString())); // TODO: fix output name
        }

        return result;
    }

    private class BuildData
    {
        public TR1Type ID { get; set; }
        public TRSpriteSequence Sequence { get; set; }
        public List<GlyphDef> Glyphs { get; set; }
    }
}
