
using GBReaderJacqueminS.Domains;
using GBReaderJacqueminS.Infrastructures;
using GBReaderJacqueminS.Repositories;
using MySqlX.XDevAPI.Common;
using Newtonsoft.Json;


namespace RepositoryTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ReadFileNotExists()
        {
            string pathDirectory = Path.Join("../../../resources");
            string pathFile = Path.Join("../../../resources", "NoExists.json");

            IJSONRepository repository = new JSONRepository(pathDirectory, pathFile);
            Stat stat = new Stat(DateTime.Now.ToString(), DateTime.Now.ToString());
            string isbn = "220001745X";
            try
            {
                repository.SaveGame(isbn, 1, stat);
                Assert.True(File.Exists(pathFile));
                var map = JsonConvert.DeserializeObject<Dictionary<string, DTOStat>>(File.ReadAllText(pathFile));
                Assert.That(map.Count, Is.EqualTo(1));
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
            File.Delete(pathFile);
        }

        [Test]
        public void ReadFileBadFormat()
        {
            string pathDirectory = Path.Join("../../../resources");
            string pathFile = Path.Join("../../../resources", "badFormat.json");

            IJSONRepository repository = new JSONRepository(pathDirectory, pathFile);
            List<Book> list = new List<Book>();
            list.Add(new Book("titre", "2200017131", "resume", "author"));
            list.Add(new Book("titre", "220001745X", "resume", "author"));
            try
            {
                repository.LoadAllStat(list);
                Assert.Fail("sa ne doit pas marché");
            }
            catch (SaveException e)
            {
                Assert.True(e.Message.Equals("selection des sauvegardes a échoué"));
            }
        }


        [Test]
        public void GetAllSaveInJson()
        {
            string pathDirectory = Path.Join("../../../resources");
            string pathFile = Path.Join("../../../resources", "getSave.json");

            IJSONRepository repository = new JSONRepository(pathDirectory, pathFile);
            List<Book> list = new List<Book>();
            list.Add(new Book("titre", "2200017131", "resume", "author"));
            list.Add(new Book("titre", "220001745X", "resume", "author"));
            list.Add(new Book("titre", "2200017467", "resume", "author"));
            try
            {
                var data = repository.LoadAllStat(list);
                Assert.That(data.Count, Is.EqualTo(3));
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [Test]
        public void GetNotSaveInJson()
        {
            string pathDirectory = Path.Join("../../../resources");
            string pathFile = Path.Join("../../../resources", "getNotSave.json");

            IJSONRepository repository = new JSONRepository(pathDirectory, pathFile);
            List<Book> list = new List<Book>();
            list.Add(new Book("titre", "2200017131", "resume", "author"));
            list.Add(new Book("titre", "220001745X", "resume", "author"));
            list.Add(new Book("titre", "2200017467", "resume", "author"));
            try
            {
                var data = repository.LoadAllStat(list);
                Assert.That(data.Count, Is.EqualTo(0));
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [Test]
        public void DropSaveInJson()
        {
            string pathDirectory = Path.Join("../../../resources");
            string pathFile = Path.Join("../../../resources", "dropSave.json");

            IJSONRepository repository = new JSONRepository(pathDirectory, pathFile);
            DTOStat drop = new DTOStat(3, new Stat(DateTime.Now.ToString(), DateTime.Now.ToString()));
            List<Stat> list = new List<Stat>();
            Dictionary<string, DTOStat> map = new Dictionary<string, DTOStat>();
            map.Add("2200017131", drop);

            try
            {
                repository.DropSave("2200017131");
                if (File.Exists(pathFile))
                {
                    map = JsonConvert.DeserializeObject<Dictionary<string, DTOStat>>(File.ReadAllText(pathFile));
                }
                if (pathFile != null)
                {
                    foreach (var dtoStat in map)
                    {
                        Stat stat = Mapping.ReadOneStat(dtoStat.Value);
                        list.Add(stat);
                    }
                }

                Assert.That(list.Count, Is.EqualTo(0));
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
            finally {
                using (StreamWriter file = File.CreateText(pathFile))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, map);
                }
            }
        }

        [Test]
        public void DropSaveNotExists()
        {
            string pathDirectory = Path.Join("../../../resources");
            string pathFile = Path.Join("../../../resources", "dropNotExists.json");

            IJSONRepository repository = new JSONRepository(pathDirectory, pathFile);
            DTOStat drop = new DTOStat(3, new Stat(DateTime.Now.ToString(), DateTime.Now.ToString()));
            List<Stat> list = new List<Stat>();
            Dictionary<string, DTOStat> map = new Dictionary<string, DTOStat>();
            try
            {
                repository.DropSave("2200017131");
                if (File.Exists(pathFile))
                {
                    map = JsonConvert.DeserializeObject<Dictionary<string, DTOStat>>(File.ReadAllText(pathFile));
                }
                if (pathFile != null)
                {
                    foreach (var dtoStat in map)
                    {
                        Stat stat = Mapping.ReadOneStat(dtoStat.Value);
                        list.Add(stat);
                    }
                }

                Assert.That(list.Count, Is.EqualTo(2));
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [Test]
        public void SaveStatNull()
        {
            string pathDirectory = Path.Join("../../../resources");
            string pathFile = Path.Join("../../../resources", "saveWithNull.json");

            IJSONRepository repository = new JSONRepository(pathDirectory, pathFile);
            DTOStat drop = new DTOStat(3, new Stat(DateTime.Now.ToString(), DateTime.Now.ToString()));
            List<Stat> list = new List<Stat>();
            Dictionary<string, DTOStat> map = new Dictionary<string, DTOStat>();
            try
            {
                repository.SaveGame("2200017131", 3, null);
                if (File.Exists(pathFile))
                {
                    map = JsonConvert.DeserializeObject<Dictionary<string, DTOStat>>(File.ReadAllText(pathFile));
                }
                if (pathFile != null)
                {
                    foreach (var dtoStat in map)
                    {
                        Stat stat = Mapping.ReadOneStat(dtoStat.Value);
                        list.Add(stat);
                    }
                }

                Assert.That(list.Count, Is.EqualTo(1));
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
            File.Delete(pathFile);
        }

        [Test]
        public void FindSave()
        {
            string pathDirectory = Path.Join("../../../resources");
            string pathFile = Path.Join("../../../resources", "findSave.json");

            IJSONRepository repository = new JSONRepository(pathDirectory, pathFile);
            DTOStat drop = new DTOStat(3, new Stat(DateTime.Now.ToString(), DateTime.Now.ToString()));
            int save;
            try
            {
                save = repository.LoadGame("2200017131");
                Assert.That(save, Is.EqualTo(3));
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [Test]
        public void NotFindSave()
        {
            string pathDirectory = Path.Join("../../../resources");
            string pathFile = Path.Join("../../../resources", "notFindSave.json");

            IJSONRepository repository = new JSONRepository(pathDirectory, pathFile);
            DTOStat drop = new DTOStat(3, new Stat(DateTime.Now.ToString(), DateTime.Now.ToString()));
            int save;
            try
            {
                save = repository.LoadGame("2200017131");
                Assert.That(save, Is.EqualTo(-1));
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [Test]
        public void findStat()
        {
            string pathDirectory = Path.Join("../../../resources");
            string pathFile = Path.Join("../../../resources", "findStat.json");

            IJSONRepository repository = new JSONRepository(pathDirectory, pathFile);
            Stat result = new Stat("21-12-22 08:45:28", "21-12-22 08:45:32");
            try
            {
                Stat save = repository.LoadOneStat("2200017131");
                Assert.That(save.DateStart, Is.EqualTo(result.DateStart));
                Assert.That(save.DateLastSession, Is.EqualTo(result.DateLastSession));
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [Test]
        public void NotFindStat()
        {
            string pathDirectory = Path.Join("../../../resources");
            string pathFile = Path.Join("../../../resources", "notFindSave.json");

            IJSONRepository repository = new JSONRepository(pathDirectory, pathFile);
            Stat result = new Stat(DateTime.Now.ToString(), DateTime.Now.ToString());
            try
            {
                Stat save = repository.LoadOneStat("2200017131");
                Assert.That(save.DateStart, Is.EqualTo(result.DateStart));
                Assert.That(save.DateLastSession, Is.EqualTo(result.DateLastSession));
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

    }
}