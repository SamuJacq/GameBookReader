
using GBReaderJacqueminS.Domains;
using GBReaderJacqueminS.Repositories;

namespace GBReaderJacqueminS.Presentations
{
    public class MainPresentation
    {

        private IMainView _mainView;
        private IDBRepository _storage;
        private IJSONRepository _file;
        private Reader _reader;

        public MainPresentation(IMainView mainView, IDBRepository storage, IJSONRepository file, Reader reader)
        {
            _mainView = mainView;
            _storage = storage;
            _file = file;
            _reader = reader;
        }

        public Reader Reader
        {
            get => _reader;
        }

        public void BeginRead(Book book)
        {
            book.AddPage(_storage.ReadPage(book.Isbn));
            for (int i = 0; i < book.Pages.Count; i++)
            {
                book.GetPage(i).ListChoice = _storage.ReadChoice(i + 1);
            }
            _reader.CurrentBook = book;
            _reader.Stat = LoadOneStat(book.Isbn);
            _mainView.GoTo("Read");
        }

        public void SaveGame(string isbn, int numPage, Stat stat)
        {
            _file.SaveGame(isbn, numPage, stat);
        }

        public int LoadGame(string isbn)
        {
            return _file.LoadGame(isbn);
        }

        public void DeleteSave(string isbn)
        {
            _file.DropSave(isbn);
        }

        public void ReturnHome(string goTo)
        {
            _mainView.GoTo(goTo);
        }

        public Stat LoadOneStat(string isbn)
        {
            return _file.LoadOneStat(isbn);
        }

        public List<Stat> LoadStat()
        {
            return _file.LoadAllStat(LoadBook());
        }

        public void SwitchStat(string goTo)
        {
            _mainView.GoTo(goTo);
        }

        public List<Book> LoadBook()
        {
            return _storage.ReadBook();
        }

    }
}
