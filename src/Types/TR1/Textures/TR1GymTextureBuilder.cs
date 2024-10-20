using TRLevelControl.Helpers;
using TRLevelControl.Model;
using TRXInjectionTool.Actions;
using TRXInjectionTool.Control;

namespace TRXInjectionTool.Types.TR1.Textures;

public class TR1GymTextureBuilder : TextureBuilder
{
    public override void Build()
    {
        TR1Level gym = _control1.Read($@"Resources\{TR1LevelNames.ASSAULT}");
        InjectionData data = InjectionData.Create(InjectionType.TextureFix);
        data.RoomEdits.AddRange(CreateRefacings(gym));
        data.RoomEdits.AddRange(CreateRotations());

        InjectionIO.Export(data, @"Output\gym_textures.bin");
    }

    private static List<TRRoomTextureReface> CreateRefacings(TR1Level level)
    {
        return new()
        {
            Reface(level, 9, TRMeshFaceType.TexturedTriangle, TRMeshFaceType.TexturedQuad, 197, 0),
            Reface(level, 9, TRMeshFaceType.TexturedTriangle, TRMeshFaceType.TexturedQuad, 197, 1),
        };
    }

    private static List<TRRoomTextureRotate> CreateRotations()
    {
        return new()
        {
            Rotate(9, TRMeshFaceType.TexturedTriangle, 1, 1),
        };
    }
}
