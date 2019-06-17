using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UDEMY.LOG
{
    public class Islemler
    {
        // C:\Users\ecoban\source\repos\UDEMY\AYARLAR\XYZProjesi\Genel.json
        static string UygulamaYolu = @"C:\Users\ecoban\source\repos\UDEMY\AYARLAR\";
        ProjeAyar Ayar;
        List<Kayit> kayitlar;

        public Islemler(string ProjeAdi)
        {
            Ayar = new ProjeAyar();
            Ayar.ProjeAdi = ProjeAdi;
            ProjeAyarlariGetir();
            kayitlar = new List<Kayit>();
        }

        public bool YeniKayit(Kayit kayit)
        {
            bool kayitDurum = false;

            if(Ayar != null && Ayar.KlasorKontrol && Ayar.YetkiKontrol)
            {
                string klasorAdi = DateTime.Now.ToString("yyyyMMdd");
                string tamYol = Ayar.KayitYolu + klasorAdi;

                if(!Directory.Exists(tamYol))
                {
                    Directory.CreateDirectory(tamYol);
                }

                if(File.Exists(tamYol + @"\Kayitlar.json"))
                {
                    string kayitlarJsonText = File.ReadAllText(tamYol + @"\Kayitlar.json");
                    kayitlar = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Kayit>>(kayitlarJsonText);
                    kayitlar.Add(kayit);
                }
                else
                {
                    kayitlar.Add(kayit);
                }

                File.WriteAllText(tamYol + @"\Kayitlar.json", Newtonsoft.Json.JsonConvert.SerializeObject(kayitlar));
                kayitDurum = true;
            }
            else
            {
                kayitDurum = false;
            }

            
            return kayitDurum;
        }

        public bool KayitDuzenle(Guid id, KayitDurum kDurum, string kdAciklama, string dosyaAdi)
        {
            bool kayitDurum = true;

            if(Ayar != null && Ayar.KlasorKontrol && Ayar.YetkiKontrol)
            {
                string tamYol = Ayar.KayitYolu + dosyaAdi + @"\Kayitlar.json";
                if(File.Exists(tamYol))
                {
                    string jsonText = File.ReadAllText(tamYol);
                    List<Kayit> kayitlar = Newtonsoft.Json.JsonConvert
                        .DeserializeObject<List<Kayit>>(jsonText);
                    int index = kayitlar.FindIndex(i => i.ID == id);
                    if(index > -1)
                    {
                        kayitlar[index].Durum = kDurum;
                        kayitlar[index].DurumAciklama = kdAciklama;

                        jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(kayitlar);
                        File.WriteAllText(tamYol, jsonText);
                    }
                }
            }
            else
            {
                kayitDurum = false;
            }

            return kayitDurum;
        }

        #region Ayar İşlemleri
        private void ProjeAyarlariGetir()
        {
            string TamYol = $"{UygulamaYolu}{Ayar.ProjeAdi}\\Genel.json";
            if(File.Exists(TamYol))
            {
                string ProjeJson = File.ReadAllText(TamYol);
                Ayar = Newtonsoft.Json.JsonConvert.DeserializeObject<ProjeAyar>(ProjeJson);
                Ayar.KlasorKontrol = true;
                Ayar.YetkiKontrol = YetkiKontrol(Ayar.KayitYolu);
            }
            else
            {
                Ayar.KlasorKontrol = false;
                Ayar.YetkiKontrol = false;
            }
        }
       
        private bool YetkiKontrol(string TamURL)
        {
            bool kontrol = false;
            int dosyaAdet = Directory.GetFiles(TamURL).Length;
            if(dosyaAdet <= 0)
            {
                FileStream fs = File.Create(TamURL + "test.txt");
                if(File.Exists(TamURL + "test.txt"))
                {
                    fs.Close();
                    File.Delete(TamURL + "test.txt");
                    kontrol = true;
                }
            }
            return kontrol;
        }
        #endregion

    }
}
