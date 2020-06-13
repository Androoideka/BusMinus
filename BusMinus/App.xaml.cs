using System.Reflection;
using System.IO;
using Xamarin.Forms;

namespace BusProgram
{
    public partial class App : Application
	{
		public App ()
		{
            var assembly = typeof(App).GetTypeInfo().Assembly;
            var t = assembly.GetManifestResourceNames();
            Stream stream = assembly.GetManifestResourceStream("BusProgram.Droid.veze.txt");
            string[] text = new string[4];
            int i = 0;
            using (var reader = new System.IO.StreamReader(stream))
            {
                while (!reader.EndOfStream)
                {
                    text[i] = reader.ReadLine();
                    i++;
                }
            }
            BusSharp.BusSharp GSP = new BusSharp.BusSharp(text);
            MainPage = new NavigationPage(new MainPage(GSP));
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
