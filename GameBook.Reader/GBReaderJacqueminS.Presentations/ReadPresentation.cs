
using GBReaderJacqueminS.Domains;

namespace GBReaderJacqueminS.Presentations
{
    public class ReadPresentation
    {
        private IReadView _readView;
        private MainPresentation _presentation;
        private Reader _reader;
        private Stat _statGame;

        public ReadPresentation(MainPresentation presentation, IReadView readView, Reader reader)
        {
            _readView = readView;
            _presentation = presentation;
            _reader = reader;
            _readView.SwitchHome += ReturnHome;
            _readView.SaveStat += SaveGame;
            _readView.DeleteStat += DeleteSave;
            _readView.BeginRead += BeginRead;
            _readView.SwitchPage += SwitchPage;
            _statGame = new Stat(DateTime.Now.ToString(), DateTime.Now.ToString());
        }

        public Stat StatGame
        {
            set => _statGame = value;
        }

        public void BeginRead(object? sender, string isbn)
        {
            _statGame = _presentation.LoadOneStat(isbn);
            if (_statGame == null)
            {
                _statGame = new Stat(DateTime.Now.ToString(), DateTime.Now.ToString());
            }
            _readView.CurrentBook = _reader.CurrentBook;
            _readView.NumPageCurrent = LoadPage(_reader.CurrentBook.Isbn);
            _readView.CurrentPage = GetPageCurrent(LoadPage(_reader.CurrentBook.Isbn));
        }

        private Page GetPageCurrent(int index)
        {
            _reader.NumPage = index;
            return _reader.CurrentBook.GetPage(index - 1);
        }

        private int LoadPage(string isbn)
        {
            int numPage = _presentation.LoadGame(isbn);
            return numPage == -1 ? 1 : numPage;
        }

        public void SaveGame(object? sender, string isbn)
        {
            _statGame.DateLastSession = DateTime.Now.ToString();
            _presentation.SaveGame(isbn, _reader.NumPage, _statGame);
        }

        public void SwitchPage(object? sender, int index)
        {
            _readView.CurrentPage = GetPageCurrent(index);
        }

        public void DeleteSave(object? sender, string isbn)
        {
            _presentation.DeleteSave(isbn);
        }
        public void ReturnHome(object? sender, string goTo)
        {
            _presentation.ReturnHome(goTo);
        }

    }
}
