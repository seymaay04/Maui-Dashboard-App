using Firebase.Auth;
using Firebase.Auth.Providers;
using Firebase.Database;
using Firebase.Database.Query;
using Odev3.Models;

namespace Odev3.Services
{
    internal static class FirebaseServices
    {
        static string projectId = "odev3-906fe";
        static string apiKey = "AIzaSyDv89K9zQXOYyuF3Cl9cPW7t6leVAmaEHU";
        static string authDomain = "odev3-906fe.firebaseapp.com";
        static string storeBucked = $"odev3-906fe.firebasestorage.app";

        static FirebaseAuthConfig config = new FirebaseAuthConfig()
        {
            ApiKey = apiKey,
            AuthDomain = authDomain,
            Providers = new FirebaseAuthProvider[] { new EmailProvider() }
        };

        const string ConnectionString = "https://odev3-906fe-default-rtdb.firebaseio.com/";
        public static FirebaseClient firebaseClient = new FirebaseClient(ConnectionString);

        // Kayıt ol
        public static Task<bool> Register(string email, string password, ref string message)// Kütüphane 3 parametre beklediği için 'username' yerine 'email'i tekrar verdik.
        {
            message = "";
            try
            {
                var client = new FirebaseAuthClient(config);


                var res = client.CreateUserWithEmailAndPasswordAsync(email, password, email);

                return Task.FromResult(res.Result.User != null ? true : false);
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return Task.FromResult(false);
        }

        // Giriş yap
        public static Task<bool> Login(string email, string password, ref string message)
        {
            message = "";
            try
            {
                var client = new FirebaseAuthClient(config);
                var res = client.SignInWithEmailAndPasswordAsync(email, password);

                return Task.FromResult(res.Result?.User != null ? true : false);
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return Task.FromResult(false);
        }

        public static async Task<bool> KullaniciKaydet(Odev3.Models.Kullanici k)
        {
            try
            {
                // Kullanicilar klasöre kayıt ekle
                await firebaseClient.Child("Kullanicilar").PostAsync(k);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static async Task<Odev3.Models.Kullanici> KullaniciGetir(string email)
        {
            try
            {
                // Tüm kullanıcıları geti ve maili eşleşeni bul
                var tumKullanicilar = await firebaseClient.Child("Kullanicilar").OnceAsync<Odev3.Models.Kullanici>();

                var bulunan = tumKullanicilar.FirstOrDefault(x => x.Object.Email == email);

                return bulunan?.Object; // Bulursa nesneyi, bulamazsa null döner
            }
            catch
            {
                return null;
            }
        }


    //TO-DO LİST İÇİN

    // Görev ekleme
        public static async Task<bool> GorevEkle(Gorev yeniGorev)
        {
            try
            {
                await firebaseClient.Child("Gorevler").PostAsync(yeniGorev);
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Görevleri listeleme
        public static async Task<List<Gorev>> GorevleriGetir()
        {
            try
            {
                var gelenVeriler = await firebaseClient.Child("Gorevler").OnceAsync<Gorev>();

                return gelenVeriler.Select(oge => new Gorev
                {
                    Id = oge.Key,
                    Baslik = oge.Object.Baslik,
                    Detay = oge.Object.Detay,
                    Tarih = oge.Object.Tarih,
                    Saat = oge.Object.Saat,
                    YapildiMi = oge.Object.YapildiMi
                }).ToList();
            }
            catch
            {
                return new List<Gorev>(); // Hata olursa boş liste dön
            }
        }

        // Görev güncelleme
        public static async Task<bool> GorevGuncelle(Gorev guncellenecekGorev)
        {
            try
            {
                await firebaseClient
                    .Child("Gorevler")
                    .Child(guncellenecekGorev.Id)
                    .PutAsync(guncellenecekGorev);
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Görev silme
        public static async Task<bool> GorevSil(string silinecekId)
        {
            try
            {
                await firebaseClient
                    .Child("Gorevler")
                    .Child(silinecekId)
                    .DeleteAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}