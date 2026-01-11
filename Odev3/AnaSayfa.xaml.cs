namespace Odev3
{
    public partial class AnaSayfa : ContentPage
    {
        public AnaSayfa()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (App.AktifKullanici != null)
            {
                AdSoyadLabel.Text = $"{App.AktifKullanici.Ad} {App.AktifKullanici.Soyad}";
                NumaraLabel.Text = App.AktifKullanici.OgrenciNo;

                // ResimData kontrol ediliyor
                if (!string.IsNullOrEmpty(App.AktifKullanici.ResimData))
                {
                    try
                    {
                        byte[] resimByteDizisi = Convert.FromBase64String(App.AktifKullanici.ResimData);
                        Profil.Source = ImageSource.FromStream(() => new MemoryStream(resimByteDizisi));
                    }
                    catch
                    {
                        Profil.Source = "user.png";
                    }
                }
                else
                {
                    Profil.Source = "user.png";
                }
            }
        }
    }
}