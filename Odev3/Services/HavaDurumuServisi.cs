using System.Text.Json;

namespace Odev3.Services
{
    public class SehirHavaDurumu
    {
        public string Isim { get; set; }

        private string TurkceKarakterDegistir(string metin)
        {
            if (string.IsNullOrEmpty(metin)) return metin;

            return metin.ToUpper()
                .Replace("Ç", "C")
                .Replace("Ğ", "G")
                .Replace("İ", "I")
                .Replace("Ö", "O")
                .Replace("Ş", "S")
                .Replace("Ü", "U");
        }

        // Sadece bugünügösteren link
        public string BugunUrl => $"https://www.mgm.gov.tr/sunum/tahmin-klasik-5070.aspx?m={TurkceKarakterDegistir(Isim)}&basla=1&bitir=1&rC=111&rZ=fff";

        // 5 günlük tahmini gösteren link
        public string BesGunUrl => $"https://www.mgm.gov.tr/sunum/tahmin-klasik-5070.aspx?m={TurkceKarakterDegistir(Isim)}&basla=1&bitir=5&rC=111&rZ=fff";
    }

    public class HavaDurumuServisi
    {
        private const string DosyaAdi = "kayitli_sehirler.json";

        // Kayıtlı şehirleri dosyadan okuma
        public async Task<List<SehirHavaDurumu>> KayitliSehirleriGetirAsync()
        {
            string dosyaYolu = Path.Combine(FileSystem.AppDataDirectory, DosyaAdi);

            if (!File.Exists(dosyaYolu))
                return new List<SehirHavaDurumu>();

            try
            {
                string jsonVerisi = await File.ReadAllTextAsync(dosyaYolu);
                return JsonSerializer.Deserialize<List<SehirHavaDurumu>>(jsonVerisi) ?? new List<SehirHavaDurumu>();
            }
            catch
            {
                return new List<SehirHavaDurumu>();
            }
        }

        // Şehir listesini dosyaya kaydetme
        public async Task SehirleriKaydetAsync(List<SehirHavaDurumu> sehirler)
        {
            string dosyaYolu = Path.Combine(FileSystem.AppDataDirectory, DosyaAdi);
            string jsonVerisi = JsonSerializer.Serialize(sehirler);
            await File.WriteAllTextAsync(dosyaYolu, jsonVerisi);
        }
    }
}
