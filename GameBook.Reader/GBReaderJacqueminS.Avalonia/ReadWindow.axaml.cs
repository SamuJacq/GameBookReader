using Avalonia.Controls;
using Avalonia.Interactivity;
using GBReaderJacqueminS.Domains;
using GBReaderJacqueminS.Presentations;
using System;
using System.Collections.Generic;

namespace GBReaderJacqueminS.Avalonia
{
    public partial class ReadWindow : UserControl, IReadView
    {

        private Book _currentBook;
        private Page _currentPage;
        private int _numPageCurrent;
        public ReadWindow()
        {
            InitializeComponent();
            _numPageCurrent = 1;
        }

        public event EventHandler<string>? SwitchHome;
        public event EventHandler<string>? SaveStat;
        public event EventHandler<string>? DeleteStat;
        public event EventHandler<string>? BeginRead;
        public event EventHandler<int>? SwitchPage;

        private void StartRead()
        {
            BeginRead?.Invoke(null, "");
            Title.Text = _currentBook.Title;
            Isbn.Text = _currentBook.Isbn;
            Restart.IsVisible = false;
            Restart.Click += Restart_Book;
            SetPageView(_numPageCurrent);
        }

        public Page CurrentPage
        {
            get => _currentPage;
            set => _currentPage = value;
        }

        public Book CurrentBook
        {
            get => _currentBook;
            set => _currentBook = value;
        }

        public int NumPageCurrent
        {
            get => _numPageCurrent;
            set => _numPageCurrent = value;
        }

        private void Next_Page(object? sender, RoutedEventArgs args)
        {
            var choice = (string)((Button)sender).Content;
            var numPage = choice[choice.Length - 1];
            SetPageView((int)Char.GetNumericValue(numPage));
        }
        private void Return_Home(object? sender, RoutedEventArgs args) => SwitchHome?.Invoke(sender, "Home");

        private void Restart_Book(object? sender, RoutedEventArgs args)
        {
            Restart.IsVisible = false;
            SetPageView(1);
        }

        private void SetPageView(int numPage)
        {
            ListBouton.Children.Clear();
            SwitchPage?.Invoke(null, numPage);
            if (_currentPage != null)
            {
                TextBlock text = new TextBlock() { Text = _currentPage.Contain };
                ListBouton.Children.Add(text);
                if (_currentPage.ListChoice.Count == 0)
                {
                    text = new TextBlock() { Text = "histoire terminé" };
                    DeleteSession(null, Isbn.Text);
                    Restart.IsVisible = true;
                    ListBouton.Children.Add(text);
                }
                else
                {
                    SaveSession(null, Isbn.Text);
                    SetButtons(_currentPage.ListChoice);
                }
            }

        }

        private void SaveSession(object? sender, string isbn) => SaveStat?.Invoke(sender, isbn);

        private void DeleteSession(object? sender, string isbn) => DeleteStat?.Invoke(sender, isbn);

        private void SetButtons(List<Choice> listChoice)
        {
            foreach (var choice in listChoice)
            {
                TextBlock text = new TextBlock() { Text = choice.Summary };
                Button button = new Button()
                {
                    Content = "aller à la page " + choice.NumPage,
                    Name = "" + choice.NumPage
                };
                button.Click += Next_Page;
                ListBouton.Children.Add(text);
                ListBouton.Children.Add(button);
            }
        }

        public void GoTo(string view) => StartRead();
    }
}