using System.IO;
using Soda.PineappleBunsWin.Core;

namespace Soda.PineappleBunsWin.Helpers;

public class FileHelper
{
    // 读取安装文件夹下所有的lnk文件
    public static List<string> GetLnkFiles(string? folderPath = null)
    {
        folderPath ??= GetBaseDirectory();

        var files = Directory.GetFiles(folderPath, "*.lnk", SearchOption.AllDirectories);
        return files.ToList();
    }

    // 获取目录下的所有文件夹
    public static List<string> GetDirectories()
    {
        var folderPath = Path.Combine(GetBaseDirectory(), AppSettings.ShortCutPath);

        var directories = Directory.GetDirectories(folderPath);
        return directories.ToList();
    }

    public static string GetBaseDirectory()
    {
        return AppDomain.CurrentDomain.BaseDirectory;
    }
}