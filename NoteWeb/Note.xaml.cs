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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;
using System.Diagnostics;

namespace NoteWeb
{
    /// <summary>
    /// Interaction logic for Note.xaml
    /// </summary>
    public partial class Note : Window
    {
        private readonly string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        private int noteCount;
        public Note(int noteCount)
        {
            this.noteCount = noteCount;
            InitializeComponent();
            createNewNote();
        }

        private void createNewNote()
        {
            string newNotePath = docPath + "/NoteTree/Notes/Note" + noteCount.ToString() + ".txt";
            File.AppendAllText(newNotePath, "");
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
            Trace.WriteLine("hello");
        }

        private void SaveNote_Click(object sender, RoutedEventArgs e)
        {
            Trace.WriteLine("Path=" + docPath);
            string notePath = docPath + "/NoteTree/Notes/Note" + noteCount.ToString() + ".txt";
            File.WriteAllText(notePath, FileTextBox.Text);
            //File.WriteAllText(Path.Combine(docPath, "notes/testFile.txt"), FileTextBox.Text);
        }
    }
}
