using Newtonsoft.Json;
using Odev3.Models;

namespace Odev3.Services
{
    public static class HaberServisi
    {
        public static async Task<List<Haber>> HaberleriGetir(string rssUrl)
        {
            string baseApi = "https://api.rss2json.com/v1/api.json?rss_url=";
            string tamUrl = baseApi + rssUrl;

            using HttpClient client = new HttpClient();
            try
            {
                var response = await client.GetStringAsync(tamUrl);
                var veri = JsonConvert.DeserializeObject<HaberRoot>(response);

                // Veri başarılı geldiyse listeyi dön, yoksa boş liste dön
                return veri?.items ?? new List<Haber>();
            }
            catch
            {
                return new List<Haber>();
            }
        }
    }
}