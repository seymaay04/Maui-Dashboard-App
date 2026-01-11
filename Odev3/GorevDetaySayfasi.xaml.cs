using Odev3.Models;
using Odev3.Services;

namespace Odev3;

public partial class GorevDetaySayfasi : ContentPage
{
    Gorev gelenGorev; // Düzenlenecek görev

    public GorevDetaySayfasi(Gorev gorev = null)
    {
        InitializeComponent();
        gelenGorev = gorev;

        // Düzenleme modunda bilgileride gör
        if (gelenGorev != null)
        {
            txtBaslik.Text = gelenGorev.Baslik;
            txtDetay.Text = gelenGorev.Detay;
        }
    }

    private async void Kaydet_Clicked(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtBaslik.Text))
        {
            await DisplayAlertAsync("Uyarý", "Lütfen bir görev baþlýðý giriniz.", "Tamam");
            return;
        }

        bool yeniKayitMi = (gelenGorev == null);

        if (yeniKayitMi)
        {
            gelenGorev = new Gorev();
            gelenGorev.YapildiMi = false; // Yeni görev yapýlmamýþ olarak baþlar
        }

        // Verileri al
        gelenGorev.Baslik = txtBaslik.Text;
        gelenGorev.Detay = txtDetay.Text;

        gelenGorev.Tarih = dpTarih.Date.Value.ToString("dd/MM/yyyy");
        gelenGorev.Saat = tpSaat.Time.Value.ToString(@"hh\:mm");

        // Veritabanýna gönder
        if (yeniKayitMi)
        {
            await FirebaseServices.GorevEkle(gelenGorev);
        }
        else
        {
            await FirebaseServices.GorevGuncelle(gelenGorev);
        }

        // Sayfayý kapat
        await Navigation.PopModalAsync();
    }

    private async void Iptal_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
}