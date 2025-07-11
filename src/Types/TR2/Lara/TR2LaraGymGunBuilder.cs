﻿using TRDataControl;
using TRImageControl;
using TRLevelControl.Helpers;
using TRLevelControl.Model;
using TRXInjectionTool.Control;

namespace TRXInjectionTool.Types.TR2.Lara;

public class TR2LaraGymGunBuilder : InjectionBuilder
{
    public override string ID => "lara_gym_guns";

    public override List<InjectionData> Build()
    {
        TR2Level gym = Createlevel();
        InjectionData data = InjectionData.Create(gym, InjectionType.General, ID);
        return new() { data };
    }

    private static TR2Level Createlevel()
    {
        TR2Level level = _control2.Read($"Resources/{TR2LevelNames.VENICE}");
        var basePalette = level.Palette.Select(c => c.ToTR1Color()).ToList();
        TR2DataImporter importer = new()
        {
            Level = level,
            DataFolder = "Resources/TR2/Objects",
            TypesToImport = new()
            {
                TR2Type.LaraPistolAnim_H_Assault,
                TR2Type.LaraAutoAnim_H_Assault,
                TR2Type.LaraUziAnim_H_Assault,
            },
        };
        importer.Import();

        var gunTypes = new[]
        {
            TR2Type.Pistols_M_H, TR2Type.Autos_M_H, TR2Type.Uzi_M_H, TR2Type.Shotgun_M_H,
            TR2Type.M16_M_H, TR2Type.GrenadeLauncher_M_H, TR2Type.Harpoon_M_H,
            TR2Type.LaraPistolAnim_H, TR2Type.LaraAutoAnim_H, TR2Type.LaraUziAnim_H, TR2Type.LaraShotgunAnim_H,
            TR2Type.LaraM16Anim_H, TR2Type.LaraGrenadeAnim_H, TR2Type.LaraHarpoonAnim_H,
            TR2Type.Gunflare_H, TR2Type.M16Gunflare_H,
        };

        var glassSFX = level.SoundEffects[TR2SFX.GlassBreak];
        CreateModelLevel(level, gunTypes);

        TR2Level gym = _control2.Read($"Resources/{TR2LevelNames.ASSAULT}");
        level.SoundEffects[TR2SFX.GlassBreak] = glassSFX;
        foreach (var sfx in gym.SoundEffects.Keys)
        {
            level.SoundEffects.Remove(sfx);
        }

        TR2GunUtils.ConvertFlatFaces(level, basePalette);
        GenerateImages8(level, gym.Palette.Select(c => c.ToTR1Color()).ToList());

        return level;
    }
}
