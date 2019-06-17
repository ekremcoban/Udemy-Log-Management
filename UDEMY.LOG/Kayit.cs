using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UDEMY.LOG
{
    public class Kayit
    {
        public Guid ID { get; set; }
        public DateTime TarihSaat { get; set; }
        public KayitTip Tip { get; set; }
        public string Namespace { get; set; }
        public string Class { get; set; }
        public string Metot { get; set; }
        public object Parametreler { get; set; }
        public Exception Ex { get; set; }
        public string Ex_Aciklama { get; set; }
        public KayitDurum Durum { get; set; }
        public string DurumAciklama { get; set; }
    }

    public enum KayitTip
    {
        Debug, Bilgilendirme, Hata
    }

    public enum KayitDurum
    {
        YeniKayit, Islemde, Duzenlendi, IptalEdildi
    }
}
