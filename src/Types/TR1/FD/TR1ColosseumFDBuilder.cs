using TRLevelControl.Model;
using TRXInjectionTool.Actions;
using TRXInjectionTool.Control;

namespace TRXInjectionTool.Types.TR1.FD;

public class TR1ColosseumFDBuilder : InjectionBuilder
{
    public override void Build()
    {
        InjectionData data = InjectionData.Create(InjectionType.FDFix);
        data.FloorEdits = CreateBatTriggerFixes();

        InjectionIO.Export(data, @"Output\colosseum_fd.bin");
    }

    private static List<TRFloorDataEdit> CreateBatTriggerFixes()
    {
        // Replace the duplicated parameter for item 74 with 73 in two triggers.
        FDTrigParamFix fix = new()
        {
            ActionType = FDTrigAction.Object,
            OldParam = 74,
            NewParam = 73,
        };

        return new()
        {
            new()
            {
                RoomIndex = 70,
                X = 7,
                Z = 1,
                Fixes = new() { fix },
            },
            new()
            {
                RoomIndex = 72,
                X = 2,
                Z = 12,
                Fixes = new() { fix },
            },
        };
    }
}
