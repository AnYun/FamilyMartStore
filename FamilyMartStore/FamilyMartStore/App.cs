using FamilyMartStore.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace FamilyMartStore
{
    public class App : Application
    {


        public App()
        {
            MainPage = new MyListViewPage("全家店家列表");
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
