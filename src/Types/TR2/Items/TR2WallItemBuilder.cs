﻿using TRLevelControl.Helpers;
using TRLevelControl.Model;
using TRXInjectionTool.Control;

namespace TRXInjectionTool.Types.TR2.Items;

public class TR2WallItemBuilder : ItemBuilder
{
    public override List<InjectionData> Build()
    {
        TR2Level wall = _control2.Read($"Resources/{TR2LevelNames.GW}");
        InjectionData data = InjectionData.Create(TRGameVersion.TR2, InjectionType.ItemRotation, "wall_itemrots");
        CreateDefaultTests(data, TR2LevelNames.GW);

        data.ItemPosEdits = new()
        {
            SetAngle(wall, 17, 16384),
            SetAngle(wall, 92, -32768),
        };

        return new() { data };
    }
}
