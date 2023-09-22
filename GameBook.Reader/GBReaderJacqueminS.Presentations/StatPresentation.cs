
using GBReaderJacqueminS.Domains;

namespace GBReaderJacqueminS.Presentations
{
    public class StatPresentation
    {

        private IStatView _statView;
        private MainPresentation _presentation;

        public StatPresentation(MainPresentation presentation, IStatView statView)
        {
            _statView = statView;
            _presentation = presentation;
            _statView.SwitchHome += ReturnHome;
            _statView.ListStat = ViewStat();
        }

        public IStatView StatView
        {
            set { _statView = value; }
        }

        public void ReturnHome(object? sender, string goTo)
        {
            _presentation.ReturnHome(goTo);
        }

        public List<Stat> ViewStat()
        {
            return _presentation.LoadStat();
        }
    }
}
