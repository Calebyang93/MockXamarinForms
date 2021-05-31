using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XFAApp1.Services;
using XFAApp1.Views;

namespace XFAApp1
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            // this registers the data store by performing dependency injection from service asset folder 
            DependencyService.Register<MockDataStore>();

            // this registers the dictionary page by performing dependency injection from service asset folder 
            DependencyService.Register<DictionaryService>();

            //// renders the main page on a android phone /ios phone.
            //MainPage = new MainPage();
            //// renders the content page on a android phone/ios phone. 
            //MainPage = new ContentPage();
            MainPage = new AppShell();

        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
