using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ListViewWithoutBinding
{
    [DesignTimeVisible(true)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            string sentence = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas elementum ullamcorper turpis, vel semper magna pretium cursus. Vestibulum in tempor dolor. Quisque lacinia fringilla dui vel viverra. Curabitur accumsan pretium.";
            var src = sentence.Split(" ").ToArray<string>();
            MyList.ItemsSource = src;
        }
    }
}
