

using GBReaderJacqueminS.Domains;

namespace GBReaderJacqueminS.Presentations
{
    public interface IReadView : IMainView
    {
        Book? CurrentBook { get; set; }
        Page CurrentPage { get; set; }
        int NumPageCurrent { get; set; }

        event EventHandler<string> SwitchHome;

        event EventHandler<string> SaveStat;

        event EventHandler<string> DeleteStat;

        event EventHandler<string> BeginRead;

        event EventHandler<int> SwitchPage;
    }
}
