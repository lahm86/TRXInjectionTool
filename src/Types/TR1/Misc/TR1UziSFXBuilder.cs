using TRLevelControl.Model;
using TRXInjectionTool.Control;

namespace TRXInjectionTool.Types.TR1.Misc;

public class TR1UziSFXBuilder : InjectionBuilder
{
    public override void Build()
    {
        TR1Level caves = _control1.Read(@"Resources\LEVEL1.PHD");
        TR1SoundEffect fx = caves.SoundEffects[TR1SFX.LaraUziFire];
        fx.Samples = new()
        {
            File.ReadAllBytes(@"Resources\TR1\Uzis\0.wav"),
        };
        ResetLevel(caves);

        InjectionData data = InjectionData.Create(caves, InjectionType.PSUziSFX);
        InjectionIO.Export(data, @"Output\uzi_sfx.bin");
    }
}
