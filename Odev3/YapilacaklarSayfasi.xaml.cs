using Odev3.Models;
using Odev3.Services;

namespace Odev3;

public partial class YapilacaklarSayfasi : ContentPage
{
    public YapilacaklarSayfasi()
    {
        InitializeComponent();
    }

    // Sayfa her ekrana geldiðinde listeyi güncelle
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await ListeyiGuncelle();
    }

    // Veritabanýndan verileri çek
    private async Task ListeyiGuncelle()
    {
        var gorevListesi = await FirebaseServices.GorevleriGetir();
        cvGorevler.ItemsSource = gorevListesi;
    }

    // Görev ekle
    private async void Ekle_clk(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new GorevDetaySayfasi(null));
    }

    // Yenile
    private async void Yenile_clk(object sender, EventArgs e)
    {
        await ListeyiGuncelle();
    }

    // Görevi düzenle
    private async void Duzenle_clk(object sender, EventArgs e)
    {
        var buton = sender as Button;
        var secilenGorev = buton.CommandParameter as Gorev;

        if (secilenGorev != null)
        {
            // Seçilen görevi GorevDetay sayfasýna gönderiyoruz
            await Navigation.PushModalAsync(new GorevDetaySayfasi(secilenGorev));
        }
    }

    // Görevi sil
    private async void Sil_clk(object sender, EventArgs e)
    {
        var buton = sender as Button;
        var silinecekGorev = buton.CommandParameter as Gorev;

        if (silinecekGorev != null)
        {
            // Kullanýcýdan onay al
            bool eminMi = await DisplayAlertAsync("Silinsin mi?", "Bu görevi silmek istediðinize emin misiniz?", "Evet", "Hayýr");

            if (eminMi)
            {
                await FirebaseServices.GorevSil(silinecekGorev.Id);
                await ListeyiGuncelle(); // Listeyi yenileki silindiði görünsün
            }
        }
    }

    // Checkbox deðiþince çalýþýr
    private async void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        var checkbox = sender as CheckBox;
        var guncelGorev = checkbox.BindingContext as Gorev;

        if (guncelGorev != null)
        {
            // Durumu güncelle ve veritabanýna kaydet
            guncelGorev.YapildiMi = e.Value;
            await FirebaseServices.GorevGuncelle(guncelGorev);
        }
    }
}