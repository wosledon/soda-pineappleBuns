using System.IO;
using System.Text.Json;
using IWshRuntimeLibrary;
using Soda.PineappleBunsWin.Core;
using File = System.IO.File;

namespace Soda.PineappleBunsWin.Shared;

public class ShortCutBox(string name)
{
    public string Name { get; set; } = name;

    public ShortCutMode Mode { get; set; } = ShortCutMode.Small;

    public List<IWshShortcut> ShortCuts { get; set; } = new List<IWshShortcut>();

    public int Width { get; set; } = 300;
    public int Height { get; set; } = 280;

    public double Left { get; set; }
    public double Top { get; set; }

    public bool IsPin { get; set; }
    public string BackgroundColor { get; set; } = "#FF000000";

    public void Save()
    {
        var json = JsonSerializer.Serialize(this);
        File.WriteAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AppSettings.ShortCutPath, Name, $"{Name}.json"), json);
    }

    public static ShortCutBox? Load(string name)
    {
        var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AppSettings.ShortCutPath, name, $"{name}.json");
        if (!File.Exists(path))
        {
            return null;
        }

        var json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<ShortCutBox>(json);
    }

    public void ChangeLocation(double left, double top)
    {
        Left = left;
        Top = top;
    }

    public void ChangeSize(int width, int height)
    {
        Width = width;
        Height = height;
    }
}