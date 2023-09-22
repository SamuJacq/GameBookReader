namespace GBReaderJacqueminS.Domains
{
    public class Stat
    {

        private Book? _book;
        private string _dateStart;
        private string _dateLastSession;

        public Stat(string dateStart, string dateEnd)
        {
            _dateStart = string.IsNullOrEmpty(dateStart) ? "unknown date" : dateStart;
            _dateLastSession = string.IsNullOrEmpty(dateEnd) ? "unknown date" : dateEnd;
        }

        public Book? Book
        {
            get { return _book; }
            set { _book = value; }
        }

        public string DateStart
        {
            get { return _dateStart; }
            set { _dateStart = value; }
        }

        public string DateLastSession
        {
            get { return _dateLastSession; }
            set { _dateLastSession = value; }
        }
    }
}
