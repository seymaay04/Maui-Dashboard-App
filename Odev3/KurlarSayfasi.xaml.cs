using Odev3.Services;

namespace Odev3;

public partial class KurlarSayfasi : ContentPage
{
    public KurlarSayfasi()
    {
        InitializeComponent();
        RunVerileriGetir(); // Ýlk açýlýþta çalýþýr
    }

    private async void RunVerileriGetir()
    {
        await VerileriGetir();
    }

    // Butona basýnca çalýþýr
    private async void Yenile_Clicked(object sender, EventArgs e)
    {
        await VerileriGetir();
    }

    private async Task VerileriGetir()
    {
        try
        {
            yuklemeGostergesi.IsVisible = true;
            yuklemeGostergesi.IsRunning = true;
            btnYenile.IsEnabled = false;
            btnYenile.Text = "Yükleniyor...";

            // Listeyi anlýk temizle
            KurlarList.ItemsSource = null;

            // Veriyi çek
            var sonuc = await KurServisi.KurlariYukle();

            string suAnkiSaat = DateTime.Now.ToString("HH:mm:ss");

            lblDate.Text = $"Veri Tarihi: {sonuc.tarih}\nSon Kontrol: {suAnkiSaat}";

            KurlarList.ItemsSource = sonuc.kurlar;
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Hata", "Veri çekilemedi: " + ex.Message, "Tamam");
        }
        finally
        {
            yuklemeGostergesi.IsRunning = false;
            yuklemeGostergesi.IsVisible = false;
            btnYenile.IsEnabled = true;
            btnYenile.Text = "Listeyi Yenile";
        }
    }
}