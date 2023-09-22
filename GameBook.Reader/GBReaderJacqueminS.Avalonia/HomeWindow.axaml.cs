using Avalonia.Controls;
using Avalonia.Interactivity;
using GBReaderJacqueminS.Domains;
using GBReaderJacqueminS.Presentations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GBReaderJacqueminS.Avalonia
{
    public partial class HomeWindow : UserControl, IHomeView
    {
        private readonly List<Book> _listBook = new();

        public event EventHandler<Book>? BeginRead;
        public event EventHandler<string>? SwitchStat;
        public event EventHandler<List<Book>>? LoadBook;

        public HomeWindow()
        {
            InitializeComponent();
        }

        private void On_Click_Resume(object? sender, RoutedEventArgs args)
        {
            var book = ((Button)sender).Parent as StackPanel;
            var temp = book.FindControl<TextBlock>("Resume");
            temp.IsVisible = !temp.IsVisible;
        }

        private void On_Click_Lire(object? sender, RoutedEventArgs args)
        {
            var book = ((Button)sender).Parent as StackPanel;
            var isbn = book.FindControl<TextBlock>("ISBN").Text;
            for (int i = 0; i < _listBook.Count; i++)
            {
                if (isbn.Equals(_listBook[i].Isbn))
                {
                    BeginRead?.Invoke(sender, _listBook[i]);
                    break;
                }
            }

        }

        private void Start_Recherche(object? sender, RoutedEventArgs args)
        {
            string recherche = InputRechercher.Text ?? "";
            var book = new List<Book>();
            foreach (var item in _listBook)
            {
                if (CheckFilter(recherche, item))
                {
                    book.Add(item);
                }
            }
            if (book.Count == 0)
            {
                NoResult.IsVisible = true;
                ShowBook.IsVisible = false;
            }
            else
            {
                NoResult.IsVisible = false;
                ShowListBook(book);
            }

        }

        private bool CheckFilter(string recherche, Book item)
        {
            if (filtreTitle.IsSelected)
            {
                return item.Title.Contains(recherche);
            }
            else if (filtreISBN.IsSelected)
            {
                return item.Isbn.Contains(recherche);
            }
            return item.Title.Contains(recherche) || item.Isbn.Contains(recherche) || recherche == "";
        }

        private void Watch_Stat(object? sender, RoutedEventArgs args) => SwitchStat?.Invoke(sender, "Stat");

        public void ShowListBook(List<Book> books)
        {
            if (_listBook == null || _listBook.Count == 0)
            {
                NoBook.IsVisible = true;
                ShowBook.IsVisible = false;
            }
            else
            {
                NoBook.IsVisible = false;
                ShowBook.IsVisible = true;
                DescriptionBookPanel.Children.Clear();
                foreach (var item in books)
                {
                    if (item == books.First())
                    {
                        ShowDescriptionBook(item, true);
                    }
                    else
                    {
                        ShowDescriptionBook(item, false);
                    }

                }
            }
        }

        private void ShowDescriptionBook(Book book, bool showResume)
        {
            DescriptionBook showBook = new();
            showBook.SetBook(book);
            showBook.Resume.IsVisible = showResume;
            showBook.boutonResume.Click += On_Click_Resume;
            showBook.lecture.Click += On_Click_Lire;
            DescriptionBookPanel.Children.Add(showBook);
        }

        public List<Book> ListBook
        {
            get => _listBook;
            set => _listBook.AddRange(value);
        }

        public void GoTo(string view) => ShowListBook(_listBook);

    }
}
