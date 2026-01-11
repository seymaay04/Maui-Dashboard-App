using Odev3.Services;
using System.Collections.ObjectModel;

namespace Odev3;

public partial class HavaDurumuSayfasi : ContentPage
{
    private readonly HavaDurumuServisi _havaDurumuServisi;
    public ObservableCollection<SehirHavaDurumu> Sehirler { get; set; }

    public HavaDurumuSayfasi()
    {
        InitializeComponent();
        _havaDurumuServisi = new HavaDurumuServisi();
        Sehirler = new ObservableCollection<SehirHavaDurumu>();
        HavaDurumuListesi.ItemsSource = Sehirler;

        SehirleriYukle();
    }

    private async void SehirleriYukle()
    {
        var kayitliSehirler = await _havaDurumuServisi.KayitliSehirleriGetirAsync();
        Sehirler.Clear();
        foreach (var sehir in kayitliSehirler)
        {
            Sehirler.Add(sehir);
        }
    }

    private async void Ekle_clk(object gonderen, EventArgs e)
    {
        string girilenSehir = await DisplayPromptAsync("Þehir Ekle", "Þehir ismini giriniz (Örn: ANKARA):");

        if (!string.IsNullOrWhiteSpace(girilenSehir))
        {
            var yeniSehir = new SehirHavaDurumu { Isim = girilenSehir.ToUpper() };
            Sehirler.Add(yeniSehir);

            await _havaDurumuServisi.SehirleriKaydetAsync(Sehirler.ToList());
        }
    }

    private async void Sil_clk(object gonderen, EventArgs e)
    {
        var buton = gonderen as Button;

        var silinecekSehir = buton?.CommandParameter as SehirHavaDurumu;

        // Eðer buton null geldiyse iþlem yapma
        if (silinecekSehir == null) return;

        bool cevap = await DisplayAlertAsync("Silinsin mi?", $"{silinecekSehir.Isim} listeden silinsin mi?", "Evet", "Hayýr");

        if (cevap)
        {
            Sehirler.Remove(silinecekSehir);
            // Listeyi güncelledikten sonra dosyaya kaydet
            await _havaDurumuServisi.SehirleriKaydetAsync(Sehirler.ToList());
        }
    }

    private void Yenile_clk(object gonderen, EventArgs e)
    {
        var geciciListe = Sehirler.ToList();
        Sehirler.Clear();
        foreach (var oge in geciciListe) Sehirler.Add(oge);
    }
}