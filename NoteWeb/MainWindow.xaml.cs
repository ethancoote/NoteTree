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
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Diagnostics;

namespace NoteWeb
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        private ObservableCollection<string> notesCollection;
        private ObservableCollection<string> searchCollection;

        public MainWindow()
        {
            DataContext = this;
            // directory/file paths
            string dirPath = docPath + "/NoteTree";
            string notesPath = dirPath + "/Notes";
            string logPath = dirPath + "/notesLog.txt";

            // creating directories/files
            Directory.CreateDirectory(dirPath);
            Directory.CreateDirectory(notesPath);
            File.AppendAllText(logPath, "");

            notesCollection = new ObservableCollection<string>();
            searchCollection = new ObservableCollection<string>();
            InitializeCollection();
            InitializeComponent();
        }

        public ObservableCollection<string> NotesCollection
        {
            get { return notesCollection; }
            set { notesCollection = value; }
        }

        public ObservableCollection<string> SearchCollection
        {
            get { return searchCollection; }
            set { searchCollection = value; }
        }

        private void InitializeCollection()
        {
            // getting number of existing notes
            string logPath = docPath + "/NoteTree/notesLog.txt";
            int noteCount = File.ReadLines(logPath).Count();

            // adding the titles of each not to the notes collection
            int i;
            string currentNote;
            for (i = 1; i <= noteCount; i++) 
            {
                currentNote = GetNoteTitle(i);
                NotesCollection.Add(currentNote);
                SearchCollection.Add(currentNote);
            }
            //FilteredCollection = CollectionViewSource.GetDefaultView(NotesCollection);
        }

        private void SearchBarEventHandler(object sender, EventArgs e)
        {
            TextBox? textBox = sender as TextBox;
            if (textBox != null)
            {
                if (textBox.Text == "")
                {
                    SearchTextBlock.Text = "Search Notes...";
                }
                else
                {
                    SearchTextBlock.Text = "";
                }
                SearchCollection.Clear();
                foreach (string noteTitle in notesCollection)
                {
                    if (noteTitle.ToLower().Contains(textBox.Text.ToLower()))
                    {
                        SearchCollection.Add(noteTitle);
                    }
                }
            }
            
            
        }

        private string GetNoteTitle(int noteNum) 
        {
            // getting log file path
            string noteTitle;
            string logPath = docPath + "/NoteTree/notesLog.txt";

            // getting the note title
            string line = File.ReadLines(logPath).Skip(noteNum - 1).Take(1).First();
            string[] splitStr = line.Split("~~");
            noteTitle = splitStr[0];
            return noteTitle;
        }

        private int GetNoteNum(string noteTitle)
        {
            // getting log file path
            int noteNum = -1;
            string logPath = docPath + "/NoteTree/notesLog.txt";

            // getting note file name
            var lines = File.ReadLines(logPath);
            foreach (string line in lines)
            {
                if (line.Contains(noteTitle + "~~"))
                {
                    string[] splitStr = line.Split("~~Note");
                    string[] splitStr1 = splitStr[1].Split(".");
                    noteNum = Convert.ToInt32(splitStr1[0]);
                    break;
                }
            }

            return noteNum;
        }

        private void OpenNote(int noteNum) 
        {
            Note note = new Note(noteNum, 1);
            note.Show();
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
            Note note = new Note(noteCount, 0);

            note.Show();
        }

        private void NotesList_Click(object sender, RoutedEventArgs e)
        {
            string selectedItem = (string)NotesList.SelectedItem;
            int noteNum = GetNoteNum(selectedItem);
            if (noteNum == -1)
            {
                Trace.WriteLine("Error: Filename note found");
            }
            else 
            {
                OpenNote(noteNum);
            }
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