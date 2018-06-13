using Prism;
using Prism.Ioc;
using TruthOrDareUI.ViewModels;
using TruthOrDareUI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Prism.DryIoc;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace TruthOrDareUI
{
    public partial class App : PrismApplication
    {
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            GlobalConfig.PrepareGameConfig();

            await NavigationService.NavigateAsync("NavigationPage/ChallengePage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage>();
            containerRegistry.RegisterForNavigation<AboutPage>();
            containerRegistry.RegisterForNavigation<SettingsPage>();
            containerRegistry.RegisterForNavigation<AddMembersPage>();
            containerRegistry.RegisterForNavigation<GamePage>();
            containerRegistry.RegisterForNavigation<ChallengePage>();
        }
    }
}
