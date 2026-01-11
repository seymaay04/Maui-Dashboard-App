using Newtonsoft.Json;

namespace Odev3.Models
{
    public class Haber
    {
        public string title { get; set; }
        public string pubDate { get; set; }
        public string link { get; set; }
        public string description { get; set; }
        public string author { get; set; }

        public Enclosure enclosure { get; set; }

        // Resim yolu
        public string ResimYolu => enclosure?.link ?? "haber_yok.png";
    }

    public class Enclosure
    {
        public string link { get; set; } // Resmin URL'si
    }

    public class HaberRoot
    {
        public string status { get; set; }
        public Feed feed { get; set; }
        public List<Haber> items { get; set; } // Haber Listesi
    }

    public class Feed
    {
        public string url { get; set; }
        public string title { get; set; }
    }
}