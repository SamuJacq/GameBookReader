using GBReaderJacqueminS.Domains;

namespace GBReaderJacqueminS.Presentations
{
    public class HomePresentation
    {
        private IHomeView _homeView;
        private MainPresentation _mainPresentation;
        private Reader _reader;

        public HomePresentation(MainPresentation mainPresentation, IHomeView homeView, Reader reader)
        {
            _mainPresentation = mainPresentation;
            _homeView = homeView;
            _reader = reader;
            _homeView.ListBook = LoadBook();
            _homeView.BeginRead += BeginRead;
            _homeView.SwitchStat += SwitchStat;
        }

        public void BeginRead(object? sender, Book book)
        {
            _reader.CurrentBook = book;
            _mainPresentation.BeginRead(book);
        }

        public void SwitchStat(object? sender, string goTo) => _mainPresentation.SwitchStat(goTo);

        private List<Book> LoadBook() => _mainPresentation.LoadBook();
    }
}
