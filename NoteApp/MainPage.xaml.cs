namespace NoteApp
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
            Container.Content=new NoteApp.Views.NoteView();
        }

       
    }

}
