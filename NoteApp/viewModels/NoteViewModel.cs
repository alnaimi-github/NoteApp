using NoteApp.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace NoteApp.viewModels
{
    public partial class NoteViewModel : INotifyPropertyChanged
    {



        //fields
        private string _noteTitle;
        private string _noteDescription;
        private Note _selectedNote;

        //Get and Set for fields

        public string NoteTitle
        {
            get { return _noteTitle; }
            set
            {
                if (_noteTitle != value)
                {
                    _noteTitle = value;
                    OnPropertyChanged(nameof(NoteTitle));
                }
            }
        }

        public string NoteDescription
        {
            get { return _noteDescription; }
            set
            {
                if (_noteDescription != value)
                {
                    _noteDescription = value;
                    OnPropertyChanged(nameof(NoteDescription));
                }
            }
        }

        public Note SelectedNote
        {
            get { return _selectedNote; }
            set
            {
                if (_selectedNote != value)
                {
                    _selectedNote = value;
                    NoteTitle= _selectedNote.Title; NoteDescription= _selectedNote.Description;//set from List to UI
                    OnPropertyChanged(nameof(_selectedNote));
                }
            }
        }



        //Property
        public ObservableCollection<Note> NoteCollection { get; set; }
        public ICommand AddNoteCommand { get; }
        public ICommand EditNoteCommand { get; }
        public ICommand RemoveNoteCommand { get; }

        public NoteViewModel()
        {
            NoteCollection = new ObservableCollection<Note>();
            AddNoteCommand = new Command(AddNote);
            EditNoteCommand = new Command(EditNote);
            RemoveNoteCommand = new Command(DeleteNote);
        }

        private void EditNote(object obj)
        {
            // Edit  Note
            if (SelectedNote != null)
            {
                foreach (Note note in NoteCollection.ToList())
                {
                    if (note == SelectedNote)
                    {
                        // Set New Note
                        var Newnote = new Note
                        {
                            Id= note.Id,
                            Title = NoteTitle,
                            Description = NoteDescription
                        };
                        NoteCollection.Remove(note);
                         NoteCollection.Add(Newnote);
                         
                       
                       
                    }
                }
            }

        }

        private void DeleteNote(object obj)
        {
            // Delete  Note
            if (SelectedNote != null)
            {
                NoteCollection.Remove(SelectedNote);
                //Rest Value
                NoteTitle = string.Empty;
                NoteDescription = string.Empty;
            }


        }

        private void AddNote(object obj)
        {
            int newId = NoteCollection.Count > 0 ? NoteCollection.Max(p => p.Id) + 1 : 1;

            // Set New Note
            var note = new Note
            {
                Id = newId,
                Title = NoteTitle,
                Description = NoteDescription
            };
            NoteCollection.Add(note);
            //Rest Value
            NoteTitle = string.Empty;
            NoteDescription = string.Empty;
        }






        /// <summary>
        /// PropertyChanged
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(propertyName, new PropertyChangedEventArgs(propertyName));
        }
    }
}
