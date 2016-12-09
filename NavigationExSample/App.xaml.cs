using Prism.Unity;
using NavigationExSample.Views;
using System.Reflection;
using System.Linq;
using Microsoft.Practices.ObjectBuilder2;
using Xamarin.Forms;
using System;
using Prism.Navigation;
using NavigationExSample.Navigation;
using Microsoft.Practices.Unity;
using System.Collections.Generic;

namespace NavigationExSample
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            var nav = (MyPageNavigationService)NavigationService;

            MainPage = nav.CreateMainTabbedPage(nameof(MyTabbed), new List<NavigationPage> {
                    await nav.CreateNavigationPage(nameof(NaviA),nameof(FirstPage)),
                    await nav.CreateNavigationPage(nameof(NaviB),nameof(SecondPage)),
                });

        }

        protected override void RegisterTypes()
        {
            this.GetType().GetTypeInfo().Assembly.DefinedTypes
                          .Where(t => t.Namespace.EndsWith(".Views", System.StringComparison.Ordinal))
                          .ForEach(t => {
                              Container.RegisterTypeForNavigation(t.AsType(), t.Name);
                          });
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            Container.RegisterType<INavigationService, MyPageNavigationService>("MyPageNavigationService");
        }

        protected override INavigationService CreateNavigationService()
        {
            return Container.Resolve<INavigationService>("MyPageNavigationService");
        }
    }
}

