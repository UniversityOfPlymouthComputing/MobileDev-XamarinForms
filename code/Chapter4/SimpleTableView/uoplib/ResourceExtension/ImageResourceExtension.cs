using System;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace uoplib.ResourceExtension
{
    [ContentProperty(nameof(Source))]
    public class ImageResourceExtension : IMarkupExtension
    {
        public string Source { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Source == null) return null;

            // Do your translation lookup here, using whatever method you require
            //ImageSource imageSource = ImageSource.FromResource(Source, typeof(ImageResourceExtension).GetTypeInfo().Assembly);
            ImageSource imageSource = ImageSource.FromResource(Source, Assembly.GetCallingAssembly());
            return imageSource;
        }
    }
}
