using TRXInjectionTool.Control;

namespace TRXInjectionTool.Types.TR1.FD;

public class TR1CatFDBuilder : FDBuilder
{
    public override void Build()
    {
        InjectionData data = InjectionData.Create(InjectionType.FDFix);
        data.FloorEdits = new()
        {
            MakeMusicOneShot(14, 1, 1),
            MakeMusicOneShot(14, 1, 2),
            MakeMusicOneShot(98, 3, 5),
            MakeMusicOneShot(100, 3, 2),
            MakeMusicOneShot(100, 3, 3),
            MakeMusicOneShot(100, 3, 4),
        };

        InjectionIO.Export(data, @"Output\cat_fd.bin");
    }
}
