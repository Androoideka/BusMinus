using Xamarin.Forms;

namespace BusProgram
{
	public class Page1 : ContentPage
	{
		public Page1 (BusSharp.BusSharp GSP, string slkt, string slkt2)
		{
            var lst = new ListView
            {
                HasUnevenRows = true,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                ItemsSource = GSP.Ispis(slkt, slkt2)
            };
            Content = new StackLayout()
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                Children = { lst }
            };
        }
	}
}
