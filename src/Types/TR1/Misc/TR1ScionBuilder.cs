using TRLevelControl;
using TRLevelControl.Model;
using TRXInjectionTool.Control;

namespace TRXInjectionTool.Types.TR1.Misc;

public class TR1ScionBuilder : InjectionBuilder
{
    public override void Build()
    {
        InjectionData data = InjectionData.Create(InjectionType.General);
        data.MeshEdits.Add(new()
        {
            ModelID = (uint)TR1Type.ScionPiece3_S_P,
            MeshIndex = 0,
            CollRadius = TRConsts.Step1 - 63,
        });

        InjectionIO.Export(data, @"Output\scion_collision.bin");
    }
}
