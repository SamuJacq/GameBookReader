
using GBReaderJacqueminS.Domains;

namespace GBReaderJacqueminS.Repositories
{
    public interface IDBRepository
    {
        public List<Book> ReadBook();

        public List<Page> ReadPage(string isbn);

        public List<Choice> ReadChoice(int numPage);
    }
}
