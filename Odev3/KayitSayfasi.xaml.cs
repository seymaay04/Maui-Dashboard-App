using Odev3.Models;
using Odev3.Services;

namespace Odev3;

public partial class KayitSayfasi : ContentPage
{
    Kullanici yeniKullanici = new Kullanici();

    public KayitSayfasi()
    {
        InitializeComponent();
        this.BindingContext = yeniKullanici;
    }

    private async void Kaydol_clk(object sender, EventArgs e)
    {
        // Boþ mu kontrol
        if (string.IsNullOrEmpty(AdGiris.Text) ||
            string.IsNullOrEmpty(SoyadGiris.Text) ||
            string.IsNullOrEmpty(NoGiris.Text) ||
            string.IsNullOrEmpty(MailGiris.Text) ||
            string.IsNullOrEmpty(SifreGiris.Text))
        {
            await DisplayAlertAsync("Uyarý", "Lütfen tüm alanlarý eksiksiz doldurunuz.", "Tamam");
            return;
        }

        yeniKullanici.Ad = AdGiris.Text;
        yeniKullanici.Soyad = SoyadGiris.Text;
        yeniKullanici.OgrenciNo = NoGiris.Text;
        yeniKullanici.Email = MailGiris.Text;
        yeniKullanici.Sifre = SifreGiris.Text;


        string hataMesaji = "";
        bool kayitBasarili = await FirebaseServices.Register(yeniKullanici.Email, yeniKullanici.Sifre, ref hataMesaji);

        if (kayitBasarili)
        {
            // Detaylarý veritabanýna kaydet
            bool dbKayit = await FirebaseServices.KullaniciKaydet(yeniKullanici);

            if (dbKayit)
            {
                await DisplayAlertAsync("Tebrikler", "Kaydýnýz baþarýyla oluþturuldu!", "Tamam");

                // Baþarýlý olunca Giriþ sayfasýna geri git
                await Navigation.PopModalAsync();
            }
            else
            {
                await DisplayAlertAsync("Hata", "Giriþ kaydý yapýldý ama veritabaný hatasý oluþtu.", "Tamam");
            }
        }
        else
        {
            await DisplayAlertAsync("Kayýt Baþarýsýz", hataMesaji, "Tamam");
        }
    }

    private async void ResimSec_Clicked(object sender, EventArgs e)
    {
        var res = await DisplayActionSheetAsync("Resim Ekle", "Ýptal", null, "Kamera", "Galeri");

        if (res == "Ýptal" || res == null) return;

        FileResult photo = null;

        try
        {
            if (res == "Kamera")
            {
                if (MediaPicker.IsCaptureSupported)
                    photo = await MediaPicker.Default.CapturePhotoAsync();
                else
                    await DisplayAlertAsync("HATA", "Kamera desteklenmiyor", "OK");
            }
            else if (res == "Galeri")
            {
                var results = await MediaPicker.Default.PickPhotosAsync();
                photo = results?.FirstOrDefault();
            }

            if (photo != null)
            {
                using var stream = await photo.OpenReadAsync();
                byte[] imageBytes = new byte[stream.Length];
                await stream.ReadAsync(imageBytes, 0, (int)stream.Length);

                string base64String = Convert.ToBase64String(imageBytes);

                // DÜZELTME:
                yeniKullanici.Resim = $"kisi_{yeniKullanici.Id}.jpg"; // Sadece isim
                yeniKullanici.ResimData = base64String;             // Gerçek veri

                // Ekranda göstermek için geçici yolu kullan
                ProfilResmiGoster.Source = ImageSource.FromFile(photo.FullPath);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Hata", $"Resim iþlenirken bir sorun oluþtu: {ex.Message}", "Tamam");
        }
    }

    private async void Giris_clk(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new GirisSayfasi());
    }
}