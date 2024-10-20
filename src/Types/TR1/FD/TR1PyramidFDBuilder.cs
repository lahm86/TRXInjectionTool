using TRLevelControl.Model;
using TRXInjectionTool.Actions;
using TRXInjectionTool.Control;

namespace TRXInjectionTool.Types.TR1.FD;

public class TR1PyramidFDBuilder : FDBuilder
{
    public override void Build()
    {
        InjectionData data = InjectionData.Create(InjectionType.FDFix);
        data.FloorEdits = new()
        {
            MakeMusicOneShot(36, 4, 5),
            CreateSecretTriggerFix(),
        };

        InjectionIO.Export(data, @"Output\pyramid_fd.bin");
    }

    private static TRFloorDataEdit CreateSecretTriggerFix()
    {
        // Fix the index on the final secret trigger.
        return new()
        {
            RoomIndex = 64,
            X = 2,
            Z = 1,
            Fixes = new()
            {
                new FDTrigParamFix
                {
                    ActionType = FDTrigAction.SecretFound,
                    OldParam = 0,
                    NewParam = 2,
                },
            },
        };
    }
}
