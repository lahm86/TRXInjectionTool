using TRLevelControl.Helpers;
using TRLevelControl.Model;
using TRXInjectionTool.Control;

namespace TRXInjectionTool.Types.TR1.Misc;

public class TR1PDABuilder : InjectionBuilder
{
    public override void Build()
    {
        TR1Level caves = _control1.Read($@"Resources\{TR1LevelNames.CAVES}");
        CreateModelLevel(caves, TR1Type.Map_M_U);

        InjectionData data = InjectionData.Create(caves, InjectionType.General);
        InjectionIO.Export(data, @"Output\pda_model.bin");
    }
}
