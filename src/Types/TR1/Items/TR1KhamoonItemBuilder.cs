﻿using TRLevelControl.Helpers;
using TRLevelControl.Model;
using TRXInjectionTool.Control;

namespace TRXInjectionTool.Types.TR1.Items;

public class TR1KhamoonItemBuilder : ItemBuilder
{
    public override List<InjectionData> Build()
    {
        var level = _control1.Read($"Resources/{TR1LevelNames.KHAMOON}");
        

        return new()
        {
            CreateItemRots(level),
            FixMeshPositions(level),
        };
    }

    private static InjectionData CreateItemRots(TR1Level level)
    {
        var data = InjectionData.Create(TRGameVersion.TR1, InjectionType.ItemRotation, "khamoon_itemrots");
        CreateDefaultTests(data, TR1LevelNames.KHAMOON);

        data.ItemPosEdits.Add(SetAngle(level, 64, 16384));

        return data;
    }

    private static InjectionData FixMeshPositions(TR1Level level)
    {
        var data = InjectionData.Create(TRGameVersion.TR1, InjectionType.General, "khamoon_meshfixes");
        CreateDefaultTests(data, TR1LevelNames.KHAMOON);

        data.StaticMeshEdits.Add(FixEgyptPillar(level));

        return data;
    }
}
