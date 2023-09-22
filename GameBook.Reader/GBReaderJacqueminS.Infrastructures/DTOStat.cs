

using GBReaderJacqueminS.Domains;

namespace GBReaderJacqueminS.Infrastructures
{
    public record DTOStat
    {
        private int _numPage;
        private string _dateStart;
        private string _dateLastSession;

        public DTOStat(int numPage, Stat stat)
        {
            _numPage = numPage;
            _dateStart = stat == null ? DateTime.Now.ToString(): stat.DateStart;
            _dateLastSession = stat == null ? DateTime.Now.ToString() : stat.DateLastSession;
        }

        public int NumPage
        {
            get => _numPage;
            set => _numPage = value;
        }

        public string DateStart
        {
            get => _dateStart;
            set => _dateStart = value;
        }

        public string DateLastSession
        {
            get => _dateLastSession;
            set => _dateLastSession = value;
        }
    }
}
