using TRLevelControl.Helpers;
using TRLevelControl.Model;
using TRXInjectionTool.Control;

namespace TRXInjectionTool.Types.TR1.Items;

public class TR1VilcabambaItemBuilder : ItemBuilder
{
    public override void Build()
    {
        TR1Level vilcabamba = _control1.Read($@"Resources\{TR1LevelNames.VILCABAMBA}");
        InjectionData data = InjectionData.Create(InjectionType.ItemRotation);

        data.ItemEdits = new()
        {
            SetAngle(vilcabamba, 4, -16384),
        };

        InjectionIO.Export(data, @"Output\vilcabamba_itemrots.bin");
    }
}
