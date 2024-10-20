using TRLevelControl.Helpers;
using TRLevelControl.Model;
using TRXInjectionTool.Control;

namespace TRXInjectionTool.Types.TR1.Items;

public class TR1ColosseumItemBuilder : ItemBuilder
{
    public override void Build()
    {
        TR1Level colosseum = _control1.Read($@"Resources\{TR1LevelNames.COLOSSEUM}");
        InjectionData data = InjectionData.Create(InjectionType.ItemRotation);

        data.ItemEdits = new()
        {
            SetAngle(colosseum, 50, -16384),
        };

        InjectionIO.Export(data, @"Output\colosseum_itemrots.bin");
    }
}
