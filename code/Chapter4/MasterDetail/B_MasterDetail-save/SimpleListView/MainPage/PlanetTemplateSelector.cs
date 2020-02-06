using System.Diagnostics;
using Xamarin.Forms;

namespace SimpleListView
{
    public class PlanetTemplateSelector : DataTemplateSelector
    {
        public ContentPage PageRef { get; set; }

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
                m1.SetBinding(MenuItem.CommandProperty, new Binding("DeleteCommand", source: PageRef.BindingContext));
                m1.SetBinding(MenuItem.CommandParameterProperty, new Binding("."));

                MenuItem m2 = new MenuItem
                {
                    Text = "Swap",
                    IsDestructive = false,
                };
                m2.SetBinding(MenuItem.CommandProperty, new Binding("SwapCommand", source: PageRef.BindingContext));
                m2.SetBinding(MenuItem.CommandParameterProperty, new Binding("."));

                MenuItem m3 = new MenuItem
                {
                    Text = "Edit",
                    IsDestructive = false,
                };
                m3.SetBinding(MenuItem.CommandProperty, new Binding("EditCommand", source: PageRef.BindingContext));
                m3.SetBinding(MenuItem.CommandParameterProperty, new Binding("."));


                if (p.Name == "Earth")
                {
                    //No item template for Earth - cannot delete or move
                    template = new DataTemplate(typeof(HomePlanetViewCell));
                }
                else
                {
                    template = new DataTemplate(() =>
                    {
                        PlanetViewCell cell = new PlanetViewCell();
                        cell.ContextActions.Add(m1);
                        cell.ContextActions.Add(m2);
                        cell.ContextActions.Add(m3);
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
