using Newtonsoft.Json;

namespace Odev3.Services
{
    public class KurModel
    {
        public string Isim { get; set; }
        public string Alis { get; set; }
        public string Satis { get; set; }
        public string Degisim { get; set; }
    }
    public class KurDetay
    {
        [JsonProperty("Alış")]
        public string Alis { get; set; }

        [JsonProperty("Satış")]
        public string Satis { get; set; }

        [JsonProperty("Değişim")]
        public string Degisim { get; set; }
    }

    public class Root
    {
        [JsonProperty("Update_Date")]
        public string Update_Date { get; set; }

        public KurDetay USD { get; set; }
        public KurDetay EUR { get; set; }
        public KurDetay GBP { get; set; }
        public KurDetay CHF { get; set; }
        public KurDetay CAD { get; set; }
        public KurDetay RUB { get; set; }
        public KurDetay AED { get; set; }
        public KurDetay AUD { get; set; }
        public KurDetay JPY { get; set; }

        [JsonProperty("gram-altin")]
        public KurDetay GramAltin { get; set; }

        [JsonProperty("ceyrek-altin")]
        public KurDetay CeyrekAltin { get; set; }

        [JsonProperty("yarim-altin")]
        public KurDetay YarimAltin { get; set; }

        [JsonProperty("tam-altin")]
        public KurDetay TamAltin { get; set; }

        [JsonProperty("cumhuriyet-altini")]
        public KurDetay CumhuriyetAltini { get; set; }

        [JsonProperty("ata-altin")]
        public KurDetay AtaAltin { get; set; }

        [JsonProperty("14-ayar-altin")]
        public KurDetay _14AyarAltin { get; set; }

        [JsonProperty("18-ayar-altin")]
        public KurDetay _18AyarAltin { get; set; }

        [JsonProperty("22-ayar-bilezik")]
        public KurDetay _22AyarBilezik { get; set; }

        [JsonProperty("gumus")]
        public KurDetay Gumus { get; set; }
    }

    internal static class KurServisi
    {
        public static async Task<(string tarih, List<KurModel> kurlar)> KurlariYukle()
        {
            string url = "https://finans.truncgil.com/today.json";

            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0");

            string jsonString = await client.GetStringAsync(url);

            // Veriyi sınıfa çeviriyoruz
            Root veri = JsonConvert.DeserializeObject<Root>(jsonString);

            // Gelen veriyi ListView'da gösterebilmek için Listeye aktarıyoruz
            List<KurModel> liste = new List<KurModel>();

            // Tek tek ekliyoruz
            Ekle(liste, "ABD DOLARI", veri.USD);
            Ekle(liste, "EURO", veri.EUR);
            Ekle(liste, "İNGİLİZ STERLİNİ", veri.GBP);
            Ekle(liste, "GRAM ALTIN", veri.GramAltin);
            Ekle(liste, "ÇEYREK ALTIN", veri.CeyrekAltin);
            Ekle(liste, "YARIM ALTIN", veri.YarimAltin);
            Ekle(liste, "TAM ALTIN", veri.TamAltin);
            Ekle(liste, "CUMHURİYET ALTINI", veri.CumhuriyetAltini);
            Ekle(liste, "ATA ALTIN", veri.AtaAltin);
            Ekle(liste, "GÜMÜŞ", veri.Gumus);
            Ekle(liste, "14 AYAR ALTIN", veri._14AyarAltin);
            Ekle(liste, "18 AYAR ALTIN", veri._18AyarAltin);
            Ekle(liste, "22 AYAR BİLEZİK", veri._22AyarBilezik);

            string tarih = veri.Update_Date ?? "Tarih Yok";
            return (tarih, liste);
        }

        private static void Ekle(List<KurModel> liste, string ad, KurDetay detay)
        {
            if (detay != null)
            {
                liste.Add(new KurModel
                {
                    Isim = ad,
                    Alis = detay.Alis,
                    Satis = detay.Satis,
                    Degisim = detay.Degisim
                });
            }
        }
    }
}