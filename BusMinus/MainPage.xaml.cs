using Xamarin.Forms;

namespace BusProgram
{
	public partial class MainPage : ContentPage
	{
        BusSharp.BusSharp GSP;
        public MainPage(BusSharp.BusSharp p)
		{
			InitializeComponent();
            GSP = p;
            var lst = new ListView
            {
                RowHeight = 50,
                ItemsSource = GSP.Prikaz()
            };
            lst.ItemSelected += OnSelectionAsync;
            Content = new StackLayout()
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                Children = { lst }
            };
        }

        async void OnSelectionAsync(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
            }
            await Navigation.PushAsync(new Page2(GSP, e.SelectedItem.ToString()));
        }
    }
}
