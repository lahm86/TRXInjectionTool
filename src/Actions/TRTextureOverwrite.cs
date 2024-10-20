namespace TRXInjectionTool.Actions;

public class TRTextureOverwrite
{
    public ushort Page { get; set; }
    public byte X { get; set; }
    public byte Y { get; set; }
    public ushort Width { get; set; }
    public ushort Height { get; set; }
    public byte[] Data { get; set; }

    public byte[] Serialize()
    {
        using MemoryStream stream = new();
        using BinaryWriter writer = new(stream);
        writer.Write(Page);
        writer.Write(X);
        writer.Write(Y);
        writer.Write(Width);
        writer.Write(Height);
        writer.Write(Data);

        return stream.ToArray();
    }
}
