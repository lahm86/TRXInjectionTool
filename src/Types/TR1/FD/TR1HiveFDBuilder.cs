using TRXInjectionTool.Control;

namespace TRXInjectionTool.Types.TR1.FD;

public class TR1HiveFDBuilder : FDBuilder
{
    public override void Build()
    {
        InjectionData data = InjectionData.Create(InjectionType.FDFix);
        data.FloorEdits = new()
        {
            MakeMusicOneShot(8, 12, 6),
            MakeMusicOneShot(18, 5, 11),
            MakeMusicOneShot(18, 6, 11),
            MakeMusicOneShot(18, 7, 11),
            MakeMusicOneShot(30, 6, 1),
            MakeMusicOneShot(30, 6, 2),
            MakeMusicOneShot(30, 6, 3),
            MakeMusicOneShot(31, 11, 4),
            MakeMusicOneShot(31, 11, 5),
            MakeMusicOneShot(31, 13, 3),
            MakeMusicOneShot(32, 12, 1),
            MakeMusicOneShot(32, 12, 2),
            MakeMusicOneShot(35, 2, 4),
        };

        InjectionIO.Export(data, @"Output\hive_fd.bin");
    }
}
