using System;
using System.Threading.Tasks;
using Prism.Navigation;
using Xamarin.Forms;

namespace NavigationExSample.Navigation
{
    public static class NavigationServiceExtentions
    {

        public static async Task NavigateAsync<T>(this INavigationService nav, NavigationParameters param = null, bool animated = true) where T : ContentPage
        {
            if (param == null) {
                param = new NavigationParameters();
            }
            await nav.NavigateAsync(typeof(T).Name, param, (bool?)false, animated);
        }

        public static async Task NavigateModalAsync<T>(this INavigationService nav, NavigationParameters param = null, bool animated = true) where T : ContentPage
        {
            if (param == null) {
                param = new NavigationParameters();
            }
            await nav.NavigateAsync(typeof(T).Name, param, (bool?)true, animated);
        }

        public static bool ChangeTab<T>(this INavigationService nav) where T : Page
        {

            var mainPage = (nav as MyPageNavigationService)?.MainPage;
            if (mainPage == null) {
                return false;
            }

            if (mainPage is TabbedPage) {
                var tabbed = mainPage as TabbedPage;
                return SearchTargetTab(tabbed, typeof(T));
            }
            else {
                return false;
            }

        }

        static bool SearchTargetTab(TabbedPage tabbed, Type target)
        {

            foreach (var child in tabbed.Children) {
                if (child.GetType() == target) {
                    tabbed.CurrentPage = child;
                    return true;
                }
                var nav = (child as NavigationPage);
                if (nav == null) {
                    continue;
                }

                if (nav.CurrentPage.GetType() == target) {
                    tabbed.CurrentPage = child;
                    return true;
                }
            }

            return false;
        }

    }
}
