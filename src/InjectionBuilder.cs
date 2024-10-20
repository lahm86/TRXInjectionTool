using TRDataControl;
using TRLevelControl;
using TRLevelControl.Model;

namespace TRXInjectionTool;

public abstract class InjectionBuilder
{
    protected static readonly TR1LevelControl _control1 = new();
    protected static readonly TR2LevelControl _control2 = new();

    public abstract void Build();

    protected static void ResetLevel(TR1Level level, uint texturePageCount = 0)
    {
        level.Images8.Clear();
        for (int i = 0; i < texturePageCount; i++)
        {
            level.Images8.Add(new() { Pixels = new byte[256 * 256] });
        }

        level.AnimatedTextures.Clear();
        level.ObjectTextures.Clear();
        level.Sprites.Clear();
        level.Models.Clear();
        level.SoundEffects.Clear();
        level.SoundSources.Clear();
        level.Rooms.Clear();
        level.StaticMeshes.Clear();
        level.Boxes.Clear();
        level.Entities.Clear();
        level.Cameras.Clear();

        for (int i = 0; i < 256; i++)
        {
            TRColour c = level.Palette[i];
            c.Red = c.Green = c.Blue = 0;
        }
    }

    protected static void CreateModelLevel(TR1Level level, params TR1Type[] types)
    {
        // Remove everything from the level bar the data related to the provided types.
        // Slight hack to export what we want, then re-import it into an empty level.
        TR1DataExporter exporter = new();
        foreach (TR1Type type in types)
        {
            exporter.Export(level, type);
        }

        ResetLevel(level, 1);
        TR1DataImporter importer = new()
        {
            Level = level,
            TypesToImport = new(types),
        };
        importer.Import();
    }
}
