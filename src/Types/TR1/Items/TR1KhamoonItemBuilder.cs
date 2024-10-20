using TRLevelControl.Helpers;
using TRLevelControl.Model;
using TRXInjectionTool.Control;

namespace TRXInjectionTool.Types.TR1.Items;

public class TR1KhamoonItemBuilder : ItemBuilder
{
    public override void Build()
    {
        TR1Level khamoon = _control1.Read($@"Resources\{TR1LevelNames.KHAMOON}");
        InjectionData data = InjectionData.Create(InjectionType.ItemRotation);

        data.ItemEdits = new()
        {
            SetAngle(khamoon, 33, 16384),
            SetAngle(khamoon, 34, -16384),
        };

        InjectionIO.Export(data, @"Output\khamoon_itemrots.bin");
    }
}
