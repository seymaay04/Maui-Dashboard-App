namespace Odev3.Models
{
    public class Kullanici
    {
        private string id;
        public string Id {
            get {
                if (id == null)
                    id = Guid.NewGuid().ToString();
                return id;
            }
            set => id = value; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Email { get; set; }
        public string Sifre { get; set; }
        public string OgrenciNo { get; set; }

        public string TamAd()
        {
            return $"{Ad} {Soyad}";
        }
        public string Resim { get; set; }
        public string ResimData { get; set; }
    }
}