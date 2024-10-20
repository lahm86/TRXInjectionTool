using TRXInjectionTool.Control;

namespace TRXInjectionTool.Types.TR1.FD;

public class TR1AtlantisFDBuilder : FDBuilder
{
    public override void Build()
    {
        InjectionData data = InjectionData.Create(InjectionType.FDFix);
        data.FloorEdits = new()
        {
            MakeMusicOneShot(59, 1, 1),
        };

        InjectionIO.Export(data, @"Output\atlantis_fd.bin");
    }
}
