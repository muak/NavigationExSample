using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using NavigationExSample.Navigation;
using NavigationExSample.Views;
using Prism.Navigation;
using Prism.Unity;
using Xamarin.Forms;

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

