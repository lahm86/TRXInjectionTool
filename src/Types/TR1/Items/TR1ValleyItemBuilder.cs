﻿using TRLevelControl.Helpers;
using TRLevelControl.Model;
using TRXInjectionTool.Control;

namespace TRXInjectionTool.Types.TR1.Items;

public class TR1ValleyItemBuilder : ItemBuilder
{
    public override List<InjectionData> Build()
    {
        TR1Level valley = _control1.Read($"Resources/{TR1LevelNames.VALLEY}");
        InjectionData data = InjectionData.Create(TRGameVersion.TR1, InjectionType.ItemRotation, "valley_itemrots");
        CreateDefaultTests(data, TR1LevelNames.VALLEY);

        data.ItemPosEdits.Add(SetAngle(valley, 14, -16384));
        data.ItemPosEdits.Add(SetAngle(valley, 40, -16384));
        data.ItemPosEdits.Add(SetAngle(valley, 58, -32768));
        data.ItemPosEdits.Add(SetAngle(valley, 59, -32768));

        return new() { data };
    }
}
