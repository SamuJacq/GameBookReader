using Avalonia.Controls;
using GBReaderJacqueminS.Avalonia;
using GBReaderJacqueminS.Presentations;
using System;

namespace GBReaderJacqueminS
{
    public partial class MainWindow : Window, IMainView
    {
        private ReadWindow _readWindow;
        private HomeWindow _homeWindow;
        private StatWindow _statWindow;
        public MainWindow()
        {
            InitializeComponent();
            _homeWindow = new HomeWindow();
            _readWindow = new ReadWindow();
            _statWindow = new StatWindow();
            HomeViewPanel.Children.Add(_homeWindow);
            ReadViewPanel.Children.Add(_readWindow);
            StatViewPanel.Children.Add(_statWindow);
            ErrorBD.IsVisible = false;
        }

        public void GoTo(string view)
        {
            if (view.Equals("Home"))
            {
                _homeWindow.IsVisible = true;
                _readWindow.IsVisible = false;
                _statWindow.IsVisible = false;
                _homeWindow.GoTo("");
            }
            else if (view.Equals("Read"))
            {
                _homeWindow.IsVisible = false;
                _readWindow.IsVisible = true;
                _statWindow.IsVisible = false;
                _readWindow.GoTo("");
            }
            else if (view.Equals("Stat"))
            {
                _homeWindow.IsVisible = false;
                _readWindow.IsVisible = false;
                _statWindow.IsVisible = true;
                _statWindow.GoTo("");
            }
            else
            {
                ErrorBD.IsVisible = true;
            }
        }

        public void InitPresentation(MainPresentation presentation)
        {
            new HomePresentation(presentation, _homeWindow, presentation.Reader);
            new ReadPresentation(presentation, _readWindow, presentation.Reader);
            new StatPresentation(presentation, _statWindow);
        }

    }
}
