using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.ServiceLocation;

namespace Base.Prism
{
    public static class ViewModelLocatorSetup
    {
        static readonly List<string> _ViewSuffixes = new List<string>
        {
            "Page",
            "Window",
            "Control",
            "Dialog",
            "View"
        };


        public static void Setup()
        {
            ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver(Resolve);

            ViewModelLocationProvider.SetDefaultViewModelFactory(viewModelType => ServiceLocator.Current.GetInstance(viewModelType));
        }

        public static Type Resolve(Type viewType)
        {
            var fullViewName = viewType.FullName;

            var viewModelName = fullViewName.Replace(".Views.", ".ViewModels.");

            _ViewSuffixes.ForEach(suffix =>
            {
                if (viewModelName.EndsWith(suffix))
                    viewModelName = viewModelName.Substring(0, viewModelName.Length - suffix.Length);
            });

            viewModelName = $"{viewModelName}ViewModel, {viewType.Assembly.FullName}";

            return Type.GetType(viewModelName);
        }

    }
}
