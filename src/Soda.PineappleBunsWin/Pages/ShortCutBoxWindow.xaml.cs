using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
using System.Windows.Shapes;
using Soda.PineappleBunsWin.Components;
using Soda.PineappleBunsWin.Core;
using Soda.PineappleBunsWin.Services;
using Soda.PineappleBunsWin.Shared;

namespace Soda.PineappleBunsWin.Pages
{
    /// <summary>
    /// ShortCutBoxWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ShortCutBoxWindow : Window, IRefreshWindow
    {
        public string Id { get; }

        private readonly ShortCutBox _box;

        public ShortCutBox box;

        public ShortCutBoxWindow(ShortCutBox box)
        {
            Id = box.Name;

            _box = box ?? throw new ArgumentNullException(nameof(box));

            Left = _box.Left;
            Top = _box.Top;

            Width = _box.Width;
            Height = _box.Height;

            Refresh();

            InitializeComponent();

            PinToDesktopMenuItem.IsChecked = box.IsPin;

            var binding = new Binding("Name")
            {
                Source = box,
                Mode = BindingMode.TwoWay
            };

            ShortCutBoxTitle.SetBinding(Label.ContentProperty, binding);

            //var listBinding = new Binding("ShortCuts")
            //{
            //    Source = box
            //};

            //ShortCutList.SetBinding(ItemsControl.ItemsSourceProperty, listBinding);

        }

        public void Update(ShortCutBox box)
        {
            throw new NotImplementedException();
        }

        public void Refresh()
        {
            //throw new NotImplementedException();
        }

        #region 亚克力效果

        //protected override void OnSourceInitialized(EventArgs e)
        //{
        //    base.OnSourceInitialized(e);

        //    var windowHelper = new WindowInteropHelper(this);

        //    var accent = new AccentPolicy();
        //    accent.AccentState = AccentState.ACCENT_ENABLE_ACRYLICBLURBEHIND;
        //    accent.GradientColor = (0 << 24) | (0x990000); // ARGB color code

        //    var accentStructSize = Marshal.SizeOf(accent);

        //    var accentPtr = Marshal.AllocHGlobal(accentStructSize);
        //    Marshal.StructureToPtr(accent, accentPtr, false);

        //    var data = new WindowCompositionAttributeData();
        //    data.Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY;
        //    data.SizeOfData = accentStructSize;
        //    data.Data = accentPtr;

        //    SetWindowCompositionAttribute(windowHelper.Handle, ref data);

        //    Marshal.FreeHGlobal(accentPtr);
        //}


        public enum AccentState
        {
            ACCENT_DISABLED = 0,
            ACCENT_ENABLE_GRADIENT = 1,
            ACCENT_ENABLE_TRANSPARENTGRADIENT = 2,
            ACCENT_ENABLE_BLURBEHIND = 3,
            ACCENT_ENABLE_ACRYLICBLURBEHIND = 4,
            ACCENT_INVALID_STATE = 5
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct AccentPolicy
        {
            public AccentState AccentState;
            public int AccentFlags;
            public int GradientColor;
            public int AnimationId;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct WindowCompositionAttributeData
        {
            public WindowCompositionAttribute Attribute;
            public IntPtr Data;
            public int SizeOfData;
        }

        public enum WindowCompositionAttribute
        {
            // ...
            WCA_ACCENT_POLICY = 19
            // ...
        }

        [DllImport("user32.dll")]
        public static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);

        #endregion

        private void ShortCutBoxWindow_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!_box.IsPin)
                DragMove();
        }

        private void ShortCutBoxWindow_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void PinToDesktop_Click(object sender, RoutedEventArgs e)
        {
            if (!_box.IsPin)
            {
                _box.IsPin = true;
                PinToDesktopMenuItem.IsChecked = true;
            }
            else
            {
                _box.IsPin = false;
                PinToDesktopMenuItem.IsChecked = false;
            }
        }

        private void AddWindow_Click(object sender, RoutedEventArgs e)
        {
            // 创建新的窗口
            var newBox = new ShortCutBox(Guid.NewGuid().ToString())
            {
                // 设置新窗口的位置
                Left = this.Left,
                Top = this.Top + this.Height
            };

            var shortCutBoxService = App.Current.GetService<ShortCutBoxService>();

            shortCutBoxService.CreateShortCutBox(newBox);
        }

        private void ShortCutBoxWindow_OnLocationChanged(object? sender, EventArgs e)
        {
            var shortCutBoxService = App.Current.GetService<ShortCutBoxService>();
            shortCutBoxService.CheckForSnap(this);
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            var shortCutBoxService = App.Current.GetService<ShortCutBoxService>();

            shortCutBoxService.RemoveShortCutBox(Id);
        }

        private void ShortCutBoxWindow_OnDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var files = (string[]?)e.Data.GetData(DataFormats.FileDrop) ?? Array.Empty<string>();
                foreach (var file in files)
                {
                    if (files.Length > 0)
                    {
                        var shortCutBoxService = App.Current.GetService<ShortCutBoxService>();

                        var shortCut = shortCutBoxService.CreateShortCut(file);
                        _box.ShortCuts.Add(shortCut);
                        ShortCutList.Children.Add(new ShortCut(shortCut)
                        {
                            Margin = new Thickness(5)
                        });
                    }
                }
            }
        }
    }
}
