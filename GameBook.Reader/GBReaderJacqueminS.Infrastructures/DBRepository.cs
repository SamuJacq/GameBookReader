
using GBReaderJacqueminS.Domains;
using GBReaderJacqueminS.Repositories;
using MySql.Data.MySqlClient;
using System.Data;

namespace GBReaderJacqueminS.Infrastructures
{
    public class DBRepository : IDBRepository, IDisposable
    {
        private readonly IDbConnection _con;
        private Dictionary<string, int> _idBook;
        // first int pour numPage, second int pour idPage
        private Dictionary<int, int> _idPages;

        public DBRepository(IDbConnection con)
        {
            this._con = con;
            _idPages = new Dictionary<int, int>();
            _idBook = new Dictionary<string, int>();
        }

        public List<Book> ReadBook()
        {
            var list = new List<DTOBook>();
            string select = "SELECT * FROM Book b JOIN Author a on b.idAuthor = a.idAuthor WHERE b.isPublier = 1";
            _idBook.Clear();
            try
            {
                using var selectCmd = _con.CreateCommand();
                selectCmd.CommandText = select;
                using IDataReader reader = selectCmd.ExecuteReader();
                while (reader.Read())
                {
                    var book = new DTOBook(
                        (string)reader["title"],
                        (string)reader["isbn"],
                        (string)reader["resume"],
                        (string)reader["name"]);
                    _idBook.Add((string)reader["isbn"], (int)reader["idBook"]);
                    list.Add(book);
                }
                return Mapping.ReadBookDB(list);
            }
            catch (MySqlException ex)
            {
                throw new BookException("selectionner des livres raté", ex);
            }
        }
        public List<Page> ReadPage(string isbn)
        {
            var list = new List<DTOPage>();
            string select = "SELECT * FROM Page WHERE idBook = @idBook";
            try
            {
                using var selectCmd = _con.CreateCommand();

                var nameParam = selectCmd.CreateParameter();
                nameParam.ParameterName = "@idBook";
                nameParam.Value = _idBook[isbn];
                nameParam.DbType = DbType.Int64;
                selectCmd.Parameters.Add(nameParam);

                selectCmd.CommandText = select;
                using IDataReader reader = selectCmd.ExecuteReader();
                _idPages.Clear();
                while (reader.Read())
                {
                    var page = new DTOPage((int)reader["numPage"], (string)reader["contain"]);
                    list.Add(page);
                    _idPages.Add((int)reader["numPage"], (int)reader["idPage"]);
                }
                return Mapping.ReadPageDB(list);
            }
            catch (MySqlException ex)
            {
                throw new PageException("selection des pages raté", ex);
            }

        }

        public List<Choice> ReadChoice(int numPage)
        {
            var list = new List<DTOChoice>();
            string select = "SELECT * FROM Choice WHERE idPage = @idPage";
            try
            {
                using var selectCmd = _con.CreateCommand();

                var nameParam = selectCmd.CreateParameter();
                nameParam.ParameterName = "@idPage";
                nameParam.Value = _idPages[numPage];
                nameParam.DbType = DbType.Int64;
                selectCmd.Parameters.Add(nameParam);

                selectCmd.CommandText = select;
                using IDataReader reader = selectCmd.ExecuteReader();
                while (reader.Read())
                {
                    var choice = new DTOChoice((string)reader["summary"], (int)reader["numPage"]);
                    list.Add(choice);
                }
                return Mapping.ReadChoiceDB(list);
            }
            catch (MySqlException ex)
            {
                throw new ChoiceException("selection des choix de la page a échoué", ex);
            }

        }

        public void Dispose()
        {
            _con.Close();
        }

    }
}
