using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UDEMY.TEST
{
    class Program
    {
        static void Main(string[] args)
        {
            UDEMY.LOG.Islemler islemler = new LOG.Islemler("XYZProjesi");
            //islemler.YeniKayit(new LOG.Kayit()
            //{
            //    ID = Guid.NewGuid(),
            //    TarihSaat = DateTime.Now,
            //    Namespace = "UDEMY.TEST",
            //    Class = "Program",
            //    Metot = "Main",
            //    Tip = LOG.KayitTip.Debug,
            //    Durum = LOG.KayitDurum.YeniKayit
            //});
            islemler.KayitDuzenle(Guid.Parse("{5b893fc6-b335-4f1f-b08f-17b7a46300b0}"),
                LOG.KayitDurum.Islemde, "Abe kaynana", "20190616");
        }
    }
}
