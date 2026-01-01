using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace AvaloniaSilkExample
{
    public partial class MainWindow : Window
    {
        public static string info="";
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        //private void InitializeComponent()
        //{
        //    AvaloniaXamlLoader.Load(this);
        //}

        private void Button_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            txt.Text = info;

        }
    }
}