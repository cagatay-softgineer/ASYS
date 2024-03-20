#region Packages
#endregion

namespace NTP_P1
{
    public class BProduct
    {
        #region Class Variable Definition
        public int Id { get; set; }
        public string UrunAdı { get; set; }
        public double SatisFiyati { get; set; }
        public string UrunGrubu { get; set; }
        public string Tarih { get; set; }
        public int StokMiktari { get; set; }
        #endregion

        #region Class Methods
        public BProduct(int id, string urunAdi, double satisFiyati, string urunGrubu, string tarih, int stokMiktari)
        {
            Id = id;
            UrunAdı = urunAdi;
            SatisFiyati = satisFiyati;
            UrunGrubu = urunGrubu;
            Tarih = tarih;
            StokMiktari = stokMiktari;
        }

        public static BProduct FromTuple((int id, string urunAdi, double satisFiyati, string urunGrubu, string tarih, int stokMiktari) tuple)
        {
            return new BProduct(tuple.id, tuple.urunAdi, tuple.satisFiyati, tuple.urunGrubu, tuple.tarih, tuple.stokMiktari);
        }
        #endregion
    }
}
