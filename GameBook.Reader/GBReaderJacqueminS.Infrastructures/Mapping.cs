

using GBReaderJacqueminS.Domains;

namespace GBReaderJacqueminS.Infrastructures
{
    public class Mapping
    {

        public static List<Book> ReadBookDB(List<DTOBook> listDTO)
        {
            var list = new List<Book>();
            foreach (var item in listDTO)
            {
                list.Add(new Book(item.Title, item.Isbn, item.Resume, item.Author));
            }
            return list;
        }

        public static Book ReadOneBook(DTOBook book)
        {
            return new Book(book.Title, book.Isbn, book.Resume, book.Author);
        }

        public static List<Page> ReadPageDB(List<DTOPage> listDTO)
        {
            var list = new List<Page>();
            foreach (var item in listDTO)
            {
                list.Insert(item.NumPage - 1, new Page(item.Contain));
            }
            return list;
        }

        public static List<Choice> ReadChoiceDB(List<DTOChoice> listDTO)
        {
            var list = new List<Choice>();
            foreach (var item in listDTO)
            {
                list.Add(new Choice(item.Summary, item.NumPage));
            }
            return list;
        }

        public static Stat ReadOneStat(DTOStat dtoStat)
        {
            return new Stat(dtoStat.DateStart, dtoStat.DateLastSession);
        }

        public static List<Stat> ReadAllStat(List<DTOStat> listDto)
        {
            List<Stat> list = new List<Stat>();
            foreach (var stat in listDto)
            {
                list.Add(ReadOneStat(stat));
            }
            return list;
        }

    }
}
