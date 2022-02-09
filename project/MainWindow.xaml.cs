using System.Windows;
using project.Editor;
using project.Game;

namespace project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonGame_OnClick(object sender, RoutedEventArgs e)
        {
            new GameWindow().Show();
        }

        private void ButtonEditor_OnClick(object sender, RoutedEventArgs e)
        {
            new EditorWindow().Show();
        }

        private void ButtonExit_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}