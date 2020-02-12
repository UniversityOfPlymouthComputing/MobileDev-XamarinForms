using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace MasterDetail
{
    public class MasterDetail : MasterDetailPage
    {
        private Page _detailPage;
        public Page DetailPage
        {
            get
            {
                return _detailPage;
            }
            set
            {
                if (_detailPage?.GetType() == value.GetType()) return;
                _detailPage = value;
                this.Detail = new NavigationPage(_detailPage);
                DismissMaster();
            }
        }
        public MasterDetail()
        {
            // Master Page
            var master = new MasterPage();
            if (Device.RuntimePlatform == Device.iOS)
            {
                master.IconImageSource = ImageSource.FromFile("MenuIcon");
            }
            this.Master = master;
            this.MasterBehavior = MasterBehavior.Popover;

            // Detail Page
            DetailPage = new DetailPage_1();

            //Hook up events
            master.PageSelected += Master_PageSelected;
        }

        private void Master_PageSelected(object sender, MasterPageEvent e)
        {
            switch (e)
            {
                case MasterPageEvent.Reset_Data:
                    DismissMaster();
                    break;

                case MasterPageEvent.Show_Page_1:
                    DetailPage = new DetailPage_1();
                    break;

                case MasterPageEvent.Show_Page_2:
                    DetailPage = new DetailPage_2();
                    break;
            }
        }

        private void DismissMaster() 
        {
            try
            {
                IsPresented = false;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }
    }
}
