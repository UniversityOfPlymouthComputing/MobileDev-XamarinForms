using System.Diagnostics;
using Xamarin.Forms;

namespace SimpleListView
{
    public class PlanetTemplateSelector : DataTemplateSelector
    {
        //public ContentPage PageRef { get; set; }

        //Set through bindings
        public DataTemplate EarthTemplate { get; set; }
        public DataTemplate OtherplanetTemplate { get; set; }

        //Selector
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            //ListView list = (ListView)container;

            if (item is SolPlanet p)
            {
                if (p.Name == "Earth")
                {
                    return EarthTemplate;
                }
                else
                {
                    return OtherplanetTemplate;
                }
            }
            else
            {
                return new DataTemplate(typeof(TextCell));
            }
        }
    }
}
