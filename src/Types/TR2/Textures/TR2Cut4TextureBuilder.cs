﻿using TRImageControl.Packing;
using TRLevelControl.Helpers;
using TRLevelControl.Model;
using TRXInjectionTool.Control;

namespace TRXInjectionTool.Types.TR2.Textures;

public class TR2Cut4TextureBuilder : TextureBuilder
{
    public override List<InjectionData> Build()
    {
        InjectionData data = CreateBaseData();
        return new() { data };
    }

    private static InjectionData CreateBaseData()
    {
        // Fix Lara's hips appearing on Bartoli's back.
        TR2Level cut4 = _control2.Read($"Resources/{TR2LevelNames.XIAN_CUT}");

        TRModel bartoliSwap = cut4.Models[TR2Type.CutsceneActor1];
        bartoliSwap.Meshes[18] = cut4.Models[TR2Type.CutsceneActor5].Meshes[18].Clone();

        TR2TexturePacker packer = new(cut4);
        var regions = packer.GetMeshRegions(bartoliSwap.Meshes).Values.SelectMany(v => v);
        var n = regions.SelectMany(r => r.Segments.Select(s => s.Index));
        List<TRObjectTexture> originalInfos = new(cut4.ObjectTextures);
        
        ResetLevel(cut4, 1);

        packer = new(cut4);
        packer.AddRectangles(regions);
        packer.Pack(true);

        cut4.Models[TR2Type.CutsceneActor1] = bartoliSwap;
        cut4.ObjectTextures.AddRange(regions.SelectMany(r => r.Segments.Select(s => s.Texture as TRObjectTexture)));
        bartoliSwap.Meshes.SelectMany(m => m.TexturedFaces)
            .ToList()
            .ForEach(f =>
            {
                TRObjectTexture t = originalInfos[f.Texture];
                f.Texture = (ushort)cut4.ObjectTextures.IndexOf(t);
            });

        _control2.Write(cut4, MakeOutputPath(TRGameVersion.TR2, "Debug/cut4.tr2"));

        InjectionData data = InjectionData.Create(cut4, InjectionType.TextureFix, "cut4_textures");
        CreateDefaultTests(data, TR2LevelNames.XIAN_CUT);
        return data;
    }
}
