using TRLevelControl.Model;
using TRXInjectionTool.Actions;
using TRXInjectionTool.Control;

namespace TRXInjectionTool.Types.TR1.FD;

public class TR1KhamoonFDBuilder : InjectionBuilder
{
    public override void Build()
    {
        {
            InjectionData data = InjectionData.Create(InjectionType.FDFix);
            data.FloorEdits = CreateTrapdoorTriggers();
            InjectionIO.Export(data, @"Output\khamoon_fd.bin");
        }

        {
            InjectionData data = InjectionData.Create(InjectionType.Item);
            data.FloorEdits = CreatePsxMummy();
            InjectionIO.Export(data, @"Output\khamoon_mummy.bin");
        }
    }

    private static List<TRFloorDataEdit> CreateTrapdoorTriggers()
    {
        // Convert the triggers in rooms 31 and 34 to dummy type.
        List<TRFloorDataEdit> edits = new()
        {
            new()
            {
                RoomIndex = 31,
                X = 2,
                Z = 5,
                Fixes = new()
                {
                    new FDTrigCreateFix
                    {
                        Entries = new()
                        {
                            new FDTriggerEntry
                            {
                                TrigType = FDTrigType.Dummy,
                                Mask = 31,
                                Actions = new()
                                {
                                    new() { Parameter = 33 },
                                },
                            },
                        },
                    },
                },
            },
            new()
            {
                RoomIndex = 31,
                X = 3,
                Z = 5,
                Fixes = new()
                {
                    new FDTrigCreateFix
                    {
                        Entries = new()
                        {
                            new FDTriggerEntry
                            {
                                TrigType = FDTrigType.Dummy,
                                Mask = 31,
                                Actions = new()
                                {
                                    new() { Parameter = 34 },
                                },
                            },
                        },
                    },
                },
            },

            new()
            {
                RoomIndex = 34,
                X = 1,
                Z = 7,
                Fixes = new()
                {
                    new FDTrigCreateFix
                    {
                        Entries = new()
                        {
                            new FDSlantEntry
                            {
                                XSlant = -2,
                                ZSlant = 2,
                            },
                            new FDTriggerEntry
                            {
                                TrigType = FDTrigType.Dummy,
                                Mask = 31,
                                Actions = new()
                                {
                                    new() { Parameter = 40 },
                                },
                            },
                        },
                    },
                },
            },
            new()
            {
                RoomIndex = 34,
                X = 2,
                Z = 7,
                Fixes = new()
                {
                    new FDTrigCreateFix
                    {
                        Entries = new()
                        {
                            new FDTriggerEntry
                            {
                                TrigType = FDTrigType.Dummy,
                                Mask = 31,
                                Actions = new()
                                {
                                    new() { Parameter = 42 },
                                },
                            },
                        },
                    },
                },
            },
            new()
            {
                RoomIndex = 34,
                X = 1,
                Z = 8,
                Fixes = new()
                {
                    new FDTrigCreateFix
                    {
                        Entries = new()
                        {
                            new FDSlantEntry
                            {
                                XSlant = -2,
                                ZSlant = 2,
                            },
                            new FDTriggerEntry
                            {
                                TrigType = FDTrigType.Dummy,
                                Mask = 31,
                                Actions = new()
                                {
                                    new() { Parameter = 39 },
                                },
                            },
                        },
                    },
                },
            },
            new()
            {
                RoomIndex = 34,
                X = 2,
                Z = 8,
                Fixes = new()
                {
                    new FDTrigCreateFix
                    {
                        Entries = new()
                        {
                            new FDSlantEntry
                            {
                                XSlant = -2,
                                ZSlant = 2,
                            },
                            new FDTriggerEntry
                            {
                                TrigType = FDTrigType.Dummy,
                                Mask = 31,
                                Actions = new()
                                {
                                    new() { Parameter = 41 },
                                },
                            },
                        },
                    },
                },
            },
        };

        return edits;
    }

    private static List<TRFloorDataEdit> CreatePsxMummy()
    {
        // Add the mummy from the PS version and trigger it.
        List<TRFloorDataEdit> edits = new()
        {
            new()
            {
                RoomIndex = 25,
                Fixes = new()
                {
                    new FDTrigItem
                    {
                        Item = new()
                        {
                            TypeID = TR1Type.NonShootingAtlantean_N,
                            Room = 25,
                            X = 43520,
                            Y = -4096,
                            Z = 34304,
                            Intensity = -1,
                            Flags = 256,
                        }
                    }
                },
            }
        };

        short[] trigRooms = new short[] { 20, 29 };
        foreach (short room in trigRooms)
        {
            edits.Add(new()
            {
                RoomIndex = room,
                X = 3,
                Z = 8,
                Fixes = new()
                {
                    new FDTrigCreateFix
                    {
                        Entries = new()
                        {
                            new FDTriggerEntry
                            {
                                Mask = 31,
                                Actions = new()
                                {
                                    new() { Parameter = 93 },
                                }
                            }
                        }
                    }
                }
            });
        }

        return edits;
    }
}
