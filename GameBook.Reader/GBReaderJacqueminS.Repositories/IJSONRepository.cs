
using GBReaderJacqueminS.Domains;

namespace GBReaderJacqueminS.Repositories
{
    public interface IJSONRepository
    {

        public void SaveGame(string isbn, int numPage, Stat stat);

        public int LoadGame(string isbn);

        public void DropSave(string isbn);

        public Stat LoadOneStat(string isbn);

        public List<Stat> LoadAllStat(List<Book> listBook);

    }
}
