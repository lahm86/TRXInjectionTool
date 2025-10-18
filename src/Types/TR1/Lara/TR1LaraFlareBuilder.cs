using TRDataControl;
using TRLevelControl.Helpers;
using TRLevelControl.Model;
using TRXInjectionTool.Control;

namespace TRXInjectionTool.Types.TR1.Lara;

public class TR1LaraFlareBuilder : InjectionBuilder
{
    public override string ID => "lara_flares";

    public override List<InjectionData> Build()
    {
        var caves = _control1.Read($"Resources/{TR1LevelNames.CAVES}");
        var lara = caves.Models[TR1Type.Lara];
        ResetLevel(caves, 1);
        caves.Models[TR1Type.Lara] = lara;

        new TR1DataImporter
        {
            Level = caves,
            DataFolder = "Resources/TR1/Lara",
            TypesToImport = [TR1Type.LaraFlareAnim_H],
        }.Import();
        caves.Models.Remove(TR1Type.Lara);

        _control1.Write(caves, "tmp.phd");
        var data = InjectionData.Create(caves, InjectionType.General, ID);

        var wall = _control2.Read($"Resources/{TR2LevelNames.GW}");
        var soundMap = new Dictionary<TR2SFX, short>
        {
            [TR2SFX.LaraFlareIgnite] = 257,
            [TR2SFX.LaraFlareBurn] = 258,
        };

        foreach (var (tr2Id, tr1Id) in soundMap)
        {
            var sfx = wall.SoundEffects[tr2Id];
            data.SFX.Add(new()
            {
                ID = tr1Id,
                Chance = sfx.Chance,
                Characteristics = 1 << 2,//sfx.GetFlags(),
                Volume = sfx.Volume,
                SampleOffset = sfx.SampleID,
            });
            data.SFX[^1].LoadSFX(TRGameVersion.TR2);
        }

        return [data];
    }
}
