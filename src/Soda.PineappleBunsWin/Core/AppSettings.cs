namespace Soda.PineappleBunsWin.Core;

public class AppSettings
{
    public const string ConfigFileName = "config.json";

    public const string ShortCutPath = "shortcuts";

    public const string ShortCutConfigFileName = "shortcuts.json";

    public const int WindowSnap = 50;
    public const int WindowSnapMargin = 10;

    public bool IsFirstRun { get; set; } = true;
    public bool IsAutoStart { get; set; } = true;

    public bool IsRound { get; set; } = true;
    public int RoundRadius { get; set; } = 10;

    public bool IsTransparent { get; set; }
    public int Opacity { get; set; } = 100;

    public string BackgroundColor { get; set; } = "#FF000000";
}