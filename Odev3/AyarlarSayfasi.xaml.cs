
namespace Odev3
{
    public partial class AyarlarSayfasi : ContentPage
    {
        public AyarlarSayfasi()
        {
            InitializeComponent();

            // Sayfa açýldýðýnda kayýtlý ayarý kontrol et
            if (Preferences.ContainsKey("koyu_tema"))
            {
                bool koyuMu = Preferences.Get("koyu_tema", false);
                Tema.IsToggled = koyuMu;
            }
        }

        private void Tema_Degisti(object gonderen, ToggledEventArgs e)
        {
            // Switch açýksa koyu, kapalýysa açýk tema
            if (e.Value)
            {
                Application.Current.UserAppTheme = AppTheme.Dark;
                Preferences.Set("koyu_tema", true);
            }
            else
            {
                Application.Current.UserAppTheme = AppTheme.Light;
                Preferences.Set("koyu_tema", false);
            }
        }
    }
}