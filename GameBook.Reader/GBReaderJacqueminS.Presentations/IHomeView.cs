

using GBReaderJacqueminS.Domains;

namespace GBReaderJacqueminS.Presentations
{
    public interface IHomeView : IMainView
    {
        public List<Book> ListBook { get; set; }

        event EventHandler<List<Book>> LoadBook;

        event EventHandler<Book> BeginRead;

        event EventHandler<string> SwitchStat;
    }
}
