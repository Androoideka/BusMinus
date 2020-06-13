using Xamarin.Forms;

namespace BusProgram
{
	public class Page2 : ContentPage
	{
        BusSharp.BusSharp GSP;
        string selected;
        public Page2(BusSharp.BusSharp p, string slkt)
        {
            GSP = p;
            selected = slkt;
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
            await Navigation.PushAsync(new Page1(GSP, selected, e.SelectedItem.ToString()));
        }
    }
}
