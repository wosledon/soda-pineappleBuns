using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Soda.PineappleBunsWin.Pages;
using Soda.PineappleBunsWin.Services;
using Soda.PineappleBunsWin.Shared;

namespace Soda.PineappleBunsWin;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        var shortCutBoxService = App.Current.GetService<ShortCutBoxService>();
        
        shortCutBoxService.CreateShortCutBox(new ShortCutBox("test"));
    }
}