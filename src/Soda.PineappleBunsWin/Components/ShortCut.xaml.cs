using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using IWshRuntimeLibrary;
using System.Drawing;
using Soda.PineappleBunsWin.Extensions;
using Color = System.Windows.Media.Color;

namespace Soda.PineappleBunsWin.Components
{
    /// <summary>
    /// ShortCut.xaml 的交互逻辑
    /// </summary>
    public partial class ShortCut : UserControl
    {
        private readonly IWshShortcut _shortcut;

        public ShortCut(IWshShortcut shortcut)
        {
            _shortcut = shortcut;
            InitializeComponent();

            string iconPath = shortcut.TargetPath; // Get the target path of the shortcut

            // Extract the icon from the target file
            Icon? icon = Icon.ExtractAssociatedIcon(iconPath);

            // Convert the icon to a BitmapSource
            BitmapSource bitmapSource = Imaging.CreateBitmapSourceFromHIcon(
                icon.Handle,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());

            // Set the source of the Image control
            ShortCutIcon.Source = bitmapSource;

            ShortCutText.Text = shortcut.GetShortCutName();
        }

        private void ShortCut_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void ShortCut_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void ShortCut_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // 打开快捷方式对应的程序
            try
            {
                System.Diagnostics.Process.Start(_shortcut.TargetPath);
            }
            catch (Exception ex)
            {
                MessageBoxResult result = MessageBox.Show("无法打开该程序，是否删除该快捷方式？", "错误", MessageBoxButton.YesNo, MessageBoxImage.Error);
                if (result == MessageBoxResult.Yes)
                {
                    // 删除快捷方式
                    string shortcutPath = _shortcut.FullName;
                    //System.IO.File.Delete(shortcutPath);
                }
            }
        }

        // 鼠标移入时背景显示一个边框，背景变为亚克力色
        private void ShortCut_OnMouseEnter(object sender, MouseEventArgs e)
        {
            ShortCutBorder.BorderBrush = new SolidColorBrush(Colors.LightGray);
            ShortCutBorder.Background = new SolidColorBrush(Color.FromArgb(80, 0xF0, 0xF8, 0xFF));
        }

        // 鼠标移出时背景恢复
        private void ShortCut_OnMouseLeave(object sender, MouseEventArgs e)
        {
            ShortCutBorder.BorderBrush = new SolidColorBrush(Colors.Transparent);
            ShortCutBorder.Background = new SolidColorBrush(Colors.Transparent);
        }

    }
}
