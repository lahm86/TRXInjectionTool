﻿using TRXInjectionTool.Control;

namespace TRXInjectionTool.Types.TR1.FD;

public class TR1CavesFDBuilder : FDBuilder
{
    public override List<InjectionData> Build()
    {
        InjectionData data = InjectionData.Create(InjectionType.FDFix, "caves_fd");

        // Dart stairway, room 34
        for (ushort x = 1; x < 5; x++)
        {
            data.FloorEdits.Add(MakeMusicOneShot(34, x, 12));
        }

        return new() { data };
    }
}