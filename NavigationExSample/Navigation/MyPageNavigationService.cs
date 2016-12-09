using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Prism.Common;
using Prism.Logging;
using Prism.Navigation;
using Prism.Unity.Navigation;
using Xamarin.Forms;

namespace NavigationExSample.Navigation
{
    public class MyPageNavigationService : UnityPageNavigationService
    {
        public IUnityContainer Container { get; private set; }
        IApplicationProvider _app;

        public Page MainPage {
            get {
                return _app.MainPage;
            }
        }

        public MyPageNavigationService(IUnityContainer container, IApplicationProvider applicationProvider, ILoggerFacade logger)
            : base(container, applicationProvider, logger)
        {
            _app = applicationProvider;
            Container = container;
        }

        public TabbedPage CreateMainTabbedPage(string tabbedName, IEnumerable<NavigationPage> children)
        {
            var tabbedPage = CreatePageFromSegment(tabbedName) as TabbedPage;

            tabbedPage.Behaviors.Add(new TabbedPageOverNavigationPageActiveAwareBehavior());

            foreach (var c in children) {
                tabbedPage.Children.Add(c);
            }

            return tabbedPage;
        }

        public async Task<NavigationPage> CreateNavigationPage(string navName, string pageName, NavigationParameters parameters = null)
        {

            var naviPage = CreatePageFromSegment(navName) as NavigationPage;
            var contentPage = CreatePageFromSegment(pageName);

            if (parameters == null) {
                parameters = new NavigationParameters();
            }

            PageUtilities.OnNavigatingTo(contentPage, parameters);

            await naviPage.PushAsync(contentPage, false);

            PageUtilities.OnNavigatedTo(contentPage, parameters);

            return naviPage;
        }
    }
}
