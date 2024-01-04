using NoteApp.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Xamarin.Forms;

namespace XAMLUI.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public MainPageViewModel()
        {
            Notes = new ObservableCollection<NoteModel>();
            DeleteNoteCommand = new Command<NoteModel>((NoteModel NoteToBeRemoved) =>
            {
                Notes.Remove(NoteToBeRemoved);
            });
            SaveNoteCommand = new Command(() =>
            {
                Notes.Add(new NoteModel { Text = NoteText });
                NoteText = string.Empty;
            },
            () => !string.IsNullOrEmpty(NoteText));

            EraseNotesCommand = new Command(() => Notes.Clear());
        }

        public event PropertyChangedEventHandler PropertyChanged;

        string noteText;
        public string NoteText
        {
            get => noteText;
            set
            {
                noteText = value;
                PropertyChanged?.Invoke(this,
                    new PropertyChangedEventArgs(nameof(NoteText)));

                SaveNoteCommand.ChangeCanExecute();
            }
        }

        public ObservableCollection<NoteModel> Notes { get; }

        public Command SaveNoteCommand { get; }
        public Command EraseNotesCommand { get; }
        public Command DeleteNoteCommand { get; }
    }
}