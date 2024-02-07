using Soda.PineappleBunsWin.Shared;

namespace Soda.PineappleBunsWin.Helpers;

public class ShortCutHelper
{

    /// <summary>
    /// 创建快捷方式
    /// </summary>
    /// <param name="targetPath">目标路径</param>
    /// <param name="shortcutPath">快捷方式路径</param>
    /// <param name="description">描述</param>
    /// <param name="iconPath">图标路径</param>
    /// <param name="args">参数（可选）</param>
    public static void CreateShortCut(string targetPath, string shortcutPath, string description, string iconPath, string args = "")
    {
        var shell = GetShell();
        var shortcut = shell.CreateShortcut(shortcutPath);
        shortcut.TargetPath = targetPath;
        shortcut.Description = description;
        shortcut.IconLocation = iconPath;
        shortcut.Arguments = args;
        shortcut.Save();
    }

    /// <summary>
    /// 获取快捷方式的详细信息
    /// </summary>
    /// <param name="linkFilePath">快捷方式文件路径</param>
    /// <returns>快捷方式的详细信息</returns>
    public static ShortCutDetail GetShortCutDetails(string linkFilePath)
    {
        var shell = GetShell();
        var shortcut = shell.CreateShortcut(linkFilePath);
        return new ShortCutDetail
        {
            Description = shortcut.Description,
            TargetPath = shortcut.TargetPath,
            IconLocation = shortcut.IconLocation,
            Arguments = shortcut.Arguments
        };
    }

    private static dynamic GetShell()
    {
        var shellType = Type.GetTypeFromProgID("WScript.Shell") ?? throw new NullReferenceException("Can not find shell.");

        dynamic shell = Activator.CreateInstance(shellType) ?? throw new InvalidOperationException();

        return shell;
    }
}