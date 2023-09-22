
using GBReaderJacqueminS.Domains;
using GBReaderJacqueminS.Repositories;
using Newtonsoft.Json;

namespace GBReaderJacqueminS.Infrastructures
{
    public class JSONRepository : IJSONRepository
    {

        private string _pathDirectory;
        private string _pathFile;
        private Dictionary<string, DTOStat> _mapSave = new Dictionary<string, DTOStat>();

        public JSONRepository(string pathDirectory, string pathFile) {
            _pathDirectory = pathDirectory;
            _pathFile = pathFile;
        }

        private void CreateDossier()
        {
            if (!Directory.Exists(_pathDirectory))
            {
                try
                {
                    Directory.CreateDirectory(_pathDirectory);
                }
                catch (Exception ex)
                {
                    throw new SaveException("la création du dossier de sauvegarde n'a pas pu être effectuer", ex);
                }
            }

        }

        public void SaveGame(string isbn, int numPage, Stat stat)
        {
            DTOStat dtoStat = new DTOStat(numPage, stat);
            try
            {
                CreateDossier();
                if (File.Exists(_pathFile))
                {
                    var save = JsonConvert.DeserializeObject<Dictionary<string, DTOStat>>(File.ReadAllText(_pathFile));
                    if (save != null)
                    {
                        _mapSave = save;
                    }
                }
                else
                {
                    using (File.Create(_pathFile)) { }
                }
                if (_mapSave.ContainsKey(isbn))
                {
                    _mapSave.Remove(isbn);
                }
                _mapSave.Add(isbn, dtoStat);
                using (StreamWriter file = File.CreateText(_pathFile))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, _mapSave);
                }
            }
            catch (IOException ex)
            {
                throw new SaveException("la sauvegarde n'a pas pu être effectuer", ex);
            }


        }

        public int LoadGame(string isbn)
        {
            try
            {
                if (File.Exists(this._pathFile))
                {
                    _mapSave = JsonConvert.DeserializeObject<Dictionary<string, DTOStat>>(File.ReadAllText(this._pathFile));
                    if (_mapSave == null || !_mapSave.ContainsKey(isbn))
                    {
                        return -1;
                    }
                    DTOStat stat = _mapSave[isbn];
                    return stat.NumPage;
                }
                else
                {
                    return -1;
                }
            }
            catch (IOException ex)
            {
                throw new SaveException("sélection de la sauvegarde a échoué", ex);
            }


        }

        public void DropSave(string isbn)
        {
            try
            {
                if (File.Exists(this._pathFile))
                {
                    _mapSave = JsonConvert.DeserializeObject<Dictionary<string, DTOStat>>(File.ReadAllText(this._pathFile));
                    if (_mapSave != null)
                    {
                        _mapSave.Remove(isbn);
                        using (StreamWriter file = File.CreateText(_pathFile))
                        {
                            JsonSerializer serializer = new JsonSerializer();
                            serializer.Serialize(file, _mapSave);
                        }
                    }
                }
            }
            catch (IOException ex)
            {
                throw new SaveException("la suppression de la sauvegarde n'a pas pu s'effectuer", ex);
            }

        }

        public Stat LoadOneStat(string isbn)
        {
            try
            {
                if (File.Exists(_pathFile))
                {
                    _mapSave = JsonConvert.DeserializeObject<Dictionary<string, DTOStat>>(File.ReadAllText(_pathFile));
                    if (_mapSave == null || !_mapSave.ContainsKey(isbn))
                    {
                        return new Stat(DateTime.Now.ToString(), DateTime.Now.ToString());
                    }
                    DTOStat stat = _mapSave[isbn];
                    return Mapping.ReadOneStat(stat);
                }
                else
                {
                    return new Stat(DateTime.Now.ToString(), DateTime.Now.ToString());
                }
            }
            catch (IOException ex)
            {
                throw new SaveException("selection de la sauvegarde a échoué", ex);
            }

        }

        public List<Stat> LoadAllStat(List<Book> listBook)
        {
            try
            {
                List<Stat> statList = new List<Stat>();
                if (File.Exists(_pathFile))
                {
                    _mapSave = JsonConvert.DeserializeObject<Dictionary<string, DTOStat>>(File.ReadAllText(_pathFile));
                }
                if (_mapSave != null)
                {
                    foreach (var dtoStat in _mapSave)
                    {
                        Stat stat = Mapping.ReadOneStat(dtoStat.Value);
                        foreach (var book in listBook)
                        {
                            if (dtoStat.Key.Equals(book.Isbn))
                            {
                                stat.Book = book;
                            }
                        }
                        statList.Add(stat);
                    }
                }

                return statList;

            }
            catch (JsonSerializationException ex)
            {
                throw new SaveException("selection des sauvegardes a échoué", ex);
            }

        }

    }
}
