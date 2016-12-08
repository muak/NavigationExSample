using Prism.Unity;
using NavigationExSample.Views;
using System.Reflection;
using System.Linq;
using Microsoft.Practices.ObjectBuilder2;
using Xamarin.Forms;
using System;

namespace NavigationExSample
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override void OnInitialized()
        {
            InitializeComponent();

            var root = new MyTabbed();
            var naviA = new NaviA { Title = "Tab1" };
            var naviB = new NaviB { Title = "Tab2" };
            naviA.PushAsync(new FirstPage(), false);
            naviB.PushAsync(new SecondPage(), false);
            root.Children.Add(naviA);
            root.Children.Add(naviB);
            MainPage = root;

        }

        protected override void RegisterTypes()
        {
            this.GetType().GetTypeInfo().Assembly.DefinedTypes
                          .Where(t => t.Namespace.EndsWith(".Views", System.StringComparison.Ordinal))
                          .ForEach(t => {
                              Container.RegisterTypeForNavigation(t.AsType(), t.Name);
                          });
        }
    }
}

