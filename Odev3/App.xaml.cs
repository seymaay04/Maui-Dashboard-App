using Odev3.Models;

namespace Odev3
{
    public partial class App : Application
    {
        public static Kullanici AktifKullanici;

        public App()
        {
            InitializeComponent();

            // Başlangıç teması
            bool koyuTemaOlsunMu = Preferences.Get("koyu_tema", false);
            if (koyuTemaOlsunMu)
            {
                Application.Current.UserAppTheme = AppTheme.Dark;
            }
            else
            {
                Application.Current.UserAppTheme = AppTheme.Light;
            }
            // --------------------------------------
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new GirisSayfasi());
        }
    }
}