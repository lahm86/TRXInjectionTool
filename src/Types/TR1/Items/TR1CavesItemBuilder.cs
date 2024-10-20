using TRLevelControl.Helpers;
using TRLevelControl.Model;
using TRXInjectionTool.Control;

namespace TRXInjectionTool.Types.TR1.Items;

public class TR1CavesItemBuilder : ItemBuilder
{
    public override void Build()
    {
        TR1Level caves = _control1.Read($@"Resources\{TR1LevelNames.CAVES}");
        InjectionData data = InjectionData.Create(InjectionType.ItemRotation);

        data.ItemEdits = new()
        {
            SetAngle(caves, 54, -32768),
        };

        InjectionIO.Export(data, @"Output\caves_itemrots.bin");
    }
}
