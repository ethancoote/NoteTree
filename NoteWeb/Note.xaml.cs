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
        public Note(int noteCount, int isNew)
        {
            this.noteCount = noteCount;
            InitializeComponent();
            if (isNew == 0) // note is new
            {
                CreateNewNote();
            }
            else if (isNew == 1) // note already exists
            {
                LoadNote();
            }
            
        }

        private void CreateNewNote()
        {
            string newNotePath = docPath + "/NoteTree/Notes/Note" + noteCount.ToString() + ".txt";
            File.AppendAllText(newNotePath, "");
        }

        private void LoadNote()
        {
            string newNotePath = docPath + "/NoteTree/Notes/Note" + noteCount.ToString() + ".txt";
            var noteFileText = File.ReadAllText(newNotePath);
            FileTextBox.Text = noteFileText;
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

        private void SaveNote_Click(object sender, RoutedEventArgs e)
        {
            SaveNote();
        }

        private void CommandBindingSave_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        { 
            e.CanExecute = true;
        }

        private void CommandBindingSave_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SaveNote();
        }

        private void SaveNote()
        {
            string notePath = docPath + "/NoteTree/Notes/Note" + noteCount.ToString() + ".txt";
            File.WriteAllText(notePath, FileTextBox.Text);

        }
    }
}
