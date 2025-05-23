﻿using TRLevelControl.Helpers;
using TRLevelControl.Model;
using TRXInjectionTool.Control;

namespace TRXInjectionTool.Types.TR1.Items;

public class TR1StrongholdItemBuilder : ItemBuilder
{
    public override List<InjectionData> Build()
    {
        TR1Level cat = _control1.Read($"Resources/{TR1LevelNames.STRONGHOLD}");
        InjectionData data = InjectionData.Create(TRGameVersion.TR1, InjectionType.ItemRotation, "stronghold_itemrots");
        CreateDefaultTests(data, TR1LevelNames.STRONGHOLD);

        data.ItemEdits = new()
        {
            SetAngle(cat, 10, -32768),
            SetAngle(cat, 78, -32768),
            SetAngle(cat, 159, -32768),
        };

        return new() { data };
    }
}
