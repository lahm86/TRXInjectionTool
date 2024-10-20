using TRXInjectionTool.Actions;
using TRXInjectionTool.Control;

namespace TRXInjectionTool.Types.TR1.Textures;

public class TR1Cut4TextureBuilder : TextureBuilder
{
    public override void Build()
    {
        InjectionData data = InjectionData.Create(InjectionType.TextureFix);
        data.RoomEdits.AddRange(CreateRefacings());
        data.RoomEdits.AddRange(CreateRotations());

        InjectionIO.Export(data, @"Output\cut4_textures.bin");
    }

    private static List<TRRoomTextureReface> CreateRefacings()
    {
        return new()
        {
            new()
            {
                RoomIndex = 9,
                FaceType = TRMeshFaceType.TexturedQuad,
                SourceRoom = 9,
                SourceFaceType = TRMeshFaceType.TexturedQuad,
                SourceIndex = 15,
                TargetIndex = 21
            }
        };
    }

    private static List<TRRoomTextureRotate> CreateRotations()
    {
        return new()
        {
            Rotate(15, TRMeshFaceType.TexturedTriangle, 0, 1),
            Rotate(16, TRMeshFaceType.TexturedTriangle, 7, 2),
            Rotate(16, TRMeshFaceType.TexturedQuad, 43, 1),
        };
    }
}
