using System.Diagnostics;
using Xamarin.Forms;

namespace SimpleListView
{
    public class PlanetTemplateSelector : DataTemplateSelector
    {
        public ContentPage PageRef { get; set; }
        private DataTemplate _earth = null;
        public DataTemplate Earth
        {
            get
            {
                if (_earth == null)
                {
                    _earth = new DataTemplate(typeof(HomePlanetViewCell));
                }
                return _earth;
            }
        }
        private DataTemplate _other = null;
        public DataTemplate Other
        {
            get
            {
                if (_other == null)
                {
                    _other = new DataTemplate(() =>
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

                        PlanetViewCell cell = new PlanetViewCell();
                        cell.ContextActions.Add(m1);
                        cell.ContextActions.Add(m2);
                        cell.ContextActions.Add(m3);
                        return cell;
                    });
                }
                return _other;
            }
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            ListView list = (ListView)container;

            if (item is SolPlanet p)
            {
                if (p.Name == "Earth")
                {
                    return Earth;
                }
                else
                {
                    return Other;
                }
            }
            else
            {
                return new DataTemplate(typeof(TextCell));
            }
        }
    }
}
