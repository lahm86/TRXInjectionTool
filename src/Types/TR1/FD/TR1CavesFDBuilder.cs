using TRXInjectionTool.Control;

namespace TRXInjectionTool.Types.TR1.FD;

public class TR1CavesFDBuilder : FDBuilder
{
    public override void Build()
    {
        InjectionData data = InjectionData.Create(InjectionType.FDFix);

        // Dart stairway, room 34
        for (ushort x = 1; x < 5; x++)
        {
            data.FloorEdits.Add(MakeMusicOneShot(34, x, 12));
        }

        InjectionIO.Export(data, @"Output\caves_fd.bin");
    }
}
