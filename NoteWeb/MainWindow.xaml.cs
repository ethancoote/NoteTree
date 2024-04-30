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
using System.IO;

namespace NoteWeb
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public MainWindow()
        {
            // directory/file paths
            string dirPath = docPath + "/NoteTree";
            string notesPath = dirPath + "/Notes";
            string logPath = dirPath + "/notesLog.txt";

            // creating directories/files
            Directory.CreateDirectory(dirPath);
            Directory.CreateDirectory(notesPath);
            File.AppendAllText(logPath, "");
            
            InitializeComponent();
        }

        private void NewNote_Click(object sender, RoutedEventArgs e)
        {
            // getting logFile data for new note
            string logPath = docPath + "/NoteTree/notesLog.txt";
            int noteCount = File.ReadLines(logPath).Count();
            noteCount += 1;

            // this is formatted as 'NoteName~~NoteFileName.txt'
            // only the NoteName is displayed to the user
            File.AppendAllText(logPath, "Note" + noteCount.ToString() + "~~Note" + noteCount.ToString() + ".txt\n");

            // creating new note
            Note note = new Note(noteCount);

            note.Show();
        }

        private void MoveWindow_Hold(object sender, RoutedEventArgs e)
        {
            DragMove();
        }

        private void CloseWindow_Click(object sender, RoutedEventArgs e) 
        {
            Close();
        }
        private void MaximizeWindow_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
            }
            else 
            {
                WindowState = WindowState.Maximized;
            }
            
        }
        private void MinimizeWindow_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
    }
}