using Odev3.Models;

namespace Odev3;

public partial class HaberDetaySayfasi : ContentPage
{
    Haber secilenHaber;

    public HaberDetaySayfasi(Haber haber)
    {
        InitializeComponent();
        secilenHaber = haber;

        // Linki yükle
        if (secilenHaber != null)
        {
            webViewHaber.Source = secilenHaber.link;
        }
    }
    private async void Paylas_clk(object sender, EventArgs e)
    {
        if (secilenHaber == null) return;

        await Share.Default.RequestAsync(new ShareTextRequest
        {
            Uri = secilenHaber.link,
            Title = secilenHaber.title
        });
    }
}