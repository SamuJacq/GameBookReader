

using GBReaderJacqueminS.Domains;

namespace GBReaderJacqueminS.Presentations
{
    public interface IStatView : IMainView
    {
        List<Stat> ListStat { get; set; }

        event EventHandler<string> SwitchHome;

        event EventHandler<List<Stat>> LoadStat;
    }
}
