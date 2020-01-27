using System.Diagnostics;
using Xamarin.Forms;

namespace SimpleListView
{
    public class PlanetTemplateSelector : DataTemplateSelector
    {
        public ContentPage Page { get; set; }
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            DataTemplate template;
            ListView list = (ListView)container;

            if (item is SolPlanet p)
            {
                MenuItem m1 = new MenuItem
                {
                    Text = "Delete",
                    IsDestructive = true,
                };
                m1.SetBinding(MenuItem.CommandProperty, new Binding("DeleteCommand", source: Page.BindingContext));
                m1.SetBinding(MenuItem.CommandParameterProperty, new Binding("."));

                if (p.Name == "Earth")
                {
                    template = new DataTemplate(() =>
                    {
                        HomePlanetViewCell cell = new HomePlanetViewCell();
                        cell.IsEnabled = false;
                        return cell;
                    });
                }
                else
                {
                    template = new DataTemplate(() =>
                    {
                        PlanetViewCell cell = new PlanetViewCell();
                        cell.ContextActions.Add(m1);
                        return cell;
                    });
                }
            }
            else
            {
                template = new DataTemplate(typeof(TextCell));
            }

            return template;
        }
    }
}
