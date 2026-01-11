using Odev3.Models;
using Odev3.Services;
using System.Runtime.CompilerServices;

namespace Odev3;

public partial class HaberlerSayfasi : ContentPage
{
    public HaberlerSayfasi()
    {
        InitializeComponent();
        // Sayfa açýlýnca manþetleri yükle
        KategoriYukle("https://www.trthaber.com/manset_articles.rss");
    }

    private async void KategoriYukle(string url)
    {
        yukleniyor.IsVisible = true;
        yukleniyor.IsRunning = true;
        haberListesi.ItemsSource = null; // Listeyi temizle

        // Servisten verileri çek
        var haberler = await HaberServisi.HaberleriGetir(url);

        haberListesi.ItemsSource = haberler;

        yukleniyor.IsRunning = false;
        yukleniyor.IsVisible = false;
    }

    private void Kategori_clk(object sender, EventArgs e)
    {
        var btn = sender as Button;
        string kategoriAdi = btn.Text;
        string secilenUrl = "";

        // TRT Haber RSS Linkleri
        switch (kategoriAdi)
        {
            case "Manþet": secilenUrl = "https://www.trthaber.com/manset_articles.rss"; break;
            case "Son Dakika": secilenUrl = "https://www.trthaber.com/sondakika_articles.rss"; break;
            case "Gündem": secilenUrl = "https://www.trthaber.com/gundem_articles.rss"; break;
            case "Ekonomi": secilenUrl = "https://www.trthaber.com/ekonomi_articles.rss"; break;
            case "Spor": secilenUrl = "https://www.trthaber.com/spor_articles.rss"; break;
            case "Bilim Tekno": secilenUrl = "https://www.trthaber.com/bilim_teknoloji_articles.rss"; break;
            default: secilenUrl = "https://www.trthaber.com/manset_articles.rss"; break;
        }

        // Veriyi Yükle
        KategoriYukle(secilenUrl);
    }

    // Listeden haber seçince
    private async void secilenHaber(object sender, SelectionChangedEventArgs e)
    {
        var secilenHaber = e.CurrentSelection.FirstOrDefault() as Haber;
        if (secilenHaber == null) return;

        // Detay sayfasýna git
        await Navigation.PushAsync(new HaberDetaySayfasi(secilenHaber));

        // Seçimi kaldýr
        haberListesi.SelectedItem = null;
    }
}