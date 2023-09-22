using Avalonia.Controls;
using GBReaderJacqueminS.Domains;

namespace GBReaderJacqueminS
{
    public partial class DescriptionBook : UserControl
    {
        public DescriptionBook()
        {
            InitializeComponent();
        }

        public void SetBook(Book book)
        {
            Title.Text = book.Title;
            ISBN.Text = book.Isbn;
            Author.Text = book.Author;
            Resume.Text = book.Resume;
        }

    }
}
