using TRLevelControl.Helpers;
using TRLevelControl.Model;
using TRXInjectionTool.Control;

namespace TRXInjectionTool.Types.TR1.Items;

public class TR1MidasItemBuilder : ItemBuilder
{
    public override void Build()
    {
        TR1Level midas = _control1.Read($@"Resources\{TR1LevelNames.MIDAS}");
        InjectionData data = InjectionData.Create(InjectionType.ItemRotation);

        data.ItemEdits = new()
        {
            SetAngle(midas, 131, -32768),
        };

        InjectionIO.Export(data, @"Output\midas_itemrots.bin");
    }
}
