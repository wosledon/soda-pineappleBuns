using System.ComponentModel;

namespace Soda.PineappleBunsWin.Shared;

public enum ShortCutMode
{
    /// <summary>
    /// 详细列表
    /// </summary>
    [Description("详细列表")]
    Detail,
    /// <summary>
    /// 大图标
    /// </summary>
    [Description("大图标")]
    Big,
    /// <summary>
    /// 小图标
    /// </summary>
    [Description("小图标")]
    Small
}