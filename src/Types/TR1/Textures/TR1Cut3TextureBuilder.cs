using TRXInjectionTool.Actions;
using TRXInjectionTool.Control;

namespace TRXInjectionTool.Types.TR1.Textures;

public class TR1Cut3TextureBuilder : TextureBuilder
{
    public override void Build()
    {
        InjectionData data = InjectionData.Create(InjectionType.TextureFix);
        data.RoomEdits.AddRange(CreateRotations());

        InjectionIO.Export(data, @"Output\cut3_textures.bin");
    }

    private static List<TRRoomTextureRotate> CreateRotations()
    {
        return new()
        {
            Rotate(21, TRMeshFaceType.TexturedTriangle, 7, 2),
            Rotate(6, TRMeshFaceType.TexturedTriangle, 7, 2),
        };
    }
}
