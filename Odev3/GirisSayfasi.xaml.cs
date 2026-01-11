using Odev3.Services;
namespace Odev3;

public partial class GirisSayfasi : ContentPage
{
    public GirisSayfasi()
    {
        InitializeComponent();
    }
    private async void Giris_clk(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(MailGiris.Text) || string.IsNullOrEmpty(SifreGiris.Text))
        {
            await DisplayAlertAsync("Uyarý", "Lütfen mail ve þifre giriniz.", "Tamam");
            return;
        }

        string errorMessage = "";

        // Firebase Servisini Çaðýr
        bool GirisDurumu = await FirebaseServices.Login(MailGiris.Text, SifreGiris.Text, ref errorMessage);

        if (GirisDurumu)
        {
            // Veritabanýndan kullanýcýnýn detaylarýný çek
            var kullaniciDetay = await FirebaseServices.KullaniciGetir(MailGiris.Text);

            App.AktifKullanici = kullaniciDetay;

            // Ana sayfaya geçiþ
            Application.Current.MainPage = new AppShell();
        }
        else
        {
            await DisplayAlertAsync("Hata", errorMessage, "Tamam");
        }
    }
    private async void KayitButon_clk(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new KayitSayfasi());
    }
}