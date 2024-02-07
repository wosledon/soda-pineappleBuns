using System.IO;
using IWshRuntimeLibrary;

namespace Soda.PineappleBunsWin.Extensions;

public static class ShortCutExtensions
{
    public static string GetShortCutName(this IWshShortcut shortcut)
    {
        return Path.GetFileNameWithoutExtension(shortcut.FullName);
    }
}