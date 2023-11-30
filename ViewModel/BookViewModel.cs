using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfAppLibraryf.Model;
using System.Runtime.CompilerServices;
using WpfAppLibraryf.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using WpfAppLibraryf.View;

namespace WpfAppLibraryf.ViewModel
{
    public class BookViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Book> _books;
        private Book _selectedBook;
        private string _searchText;

        public ObservableCollection<Book> Books
        {
            get { return _books; }
            set
            {
                _books = value;
                OnPropertyChanged();
            }
        }

        public Book SelectedBook
        {
            get { return _selectedBook; }
            set
            {
                _selectedBook = value;
                OnPropertyChanged();
                EditCommand.RaiseCanExecuteChanged();
                DeleteCommand.RaiseCanExecuteChanged();
            }
        }

        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChanged();
            }
        }

        public DelegateCommand AddCommand { get; private set; }
        public DelegateCommand EditCommand { get; private set; }
        public DelegateCommand DeleteCommand { get; private set; }
        public DelegateCommand SearchCommand { get; private set; }

        public BookViewModel()
        {
            Books = new ObservableCollection<Book>();
            AddCommand = new DelegateCommand(AddBook);
            EditCommand = new DelegateCommand(EditBook, () => SelectedBook != null);
            DeleteCommand = new DelegateCommand(DeleteBook, () => SelectedBook != null);
            SearchCommand = new DelegateCommand(SearchBooks);
        }

        private void AddBook()
        {
            
            Book newBook = new Book
            {
                Title = "New Book",
                Author = "Unknown Author",
                Description = "Description of the new book."
            };

            Books.Add(newBook);
            SelectedBook = newBook;
        }
        private string _editTitle;
        private string _editAuthor;
        private string _editDescription;

        
        public string EditTitle
        {
            get { return _editTitle; }
            set { Set(ref _editTitle, value); }
        }

        public string EditAuthor
        {
            get { return _editAuthor; }
            set { Set(ref _editAuthor, value); }
        }

        public string EditDescription
        {
            get { return _editDescription; }
            set { Set(ref _editDescription, value); }
        }

        private void Set(ref string editDescription, string value)
        {
            
        }
        
        private void EditBook()
        {

            if (SelectedBook != null)
            {
                EditTitle = SelectedBook.Title;
                EditAuthor = SelectedBook.Author;
                EditDescription = SelectedBook.Description;

                
                var editBookWindow = new EditBookWindow();
                editBookWindow.ShowDialog();
            }
        }

        private void SaveEdit()
        {
            if (SelectedBook != null)
            {
                
                SelectedBook.Title = EditTitle;
                SelectedBook.Author = EditAuthor;
                SelectedBook.Description = EditDescription;

                
                Messenger.Default.Send(new NotificationMessage("CloseEditDialog"));
            }
        }

        private void CancelEdit()
        {
            
            Messenger.Default.Send(new NotificationMessage("CloseEditDialog"));
        }

        private void DeleteBook()
        {
           
            if (SelectedBook != null)
            {
                Books.Remove(SelectedBook);
                SelectedBook = null;
            }
        }

        private void SearchBooks()
        {
            
            if (!string.IsNullOrEmpty(SearchText))
            {
                var searchResults = Books.Where(book =>
                    book.Title.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                    book.Author.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                    book.Description.Contains(SearchText, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                Books.Clear();
                foreach (var result in searchResults)
                {
                    Books.Add(result);
                }
            }
            else
            {
               
            }
        }
       

        private void Set(ref bool isEditing, bool value)
        {
            
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}