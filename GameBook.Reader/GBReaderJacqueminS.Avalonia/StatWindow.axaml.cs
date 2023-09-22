using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Layout;
using GBReaderJacqueminS.Domains;
using GBReaderJacqueminS.Presentations;
using System;
using System.Collections.Generic;

namespace GBReaderJacqueminS.Avalonia
{
    public partial class StatWindow : UserControl, IStatView
    {

        private List<Stat> _listStat = new List<Stat>();

        public event EventHandler<string>? SwitchHome;
        public event EventHandler<List<Stat>>? LoadStat;

        public StatWindow()
        {
            InitializeComponent();
        }

        public List<Stat> ListStat
        {
            get { return _listStat; }
            set { _listStat.AddRange(value); }

        }

        public void GoTo(string view)
        {
            BookInLecture.Text = "nombre de livre en lecture : " + _listStat.Count;
            StatList.Children.Clear();
            foreach (var stat in _listStat)
            {
                WrapPanel wrapPanel = new WrapPanel();
                TextBlock titleLivre = new TextBlock();
                TextBlock dateStart = new TextBlock();
                TextBlock dateLastSession = new TextBlock();
                titleLivre.Text = "livre numéro : " + stat.Book.Title;
                dateStart.Text = "date du début de la lecture : " + stat.DateStart;
                dateLastSession.Text = "date de la dernière session : " + stat.DateLastSession;
                wrapPanel.Children.Add(titleLivre);
                wrapPanel.Children.Add(dateStart);
                wrapPanel.Children.Add(dateLastSession);
                wrapPanel.Orientation = Orientation.Vertical;
                StatList.Children.Add(wrapPanel);
            }
        }

        private void Return_Home(object? sender, RoutedEventArgs args) => SwitchHome?.Invoke(sender, "Home");


    }
}
