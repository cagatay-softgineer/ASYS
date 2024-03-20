#region Packages
#endregion

namespace NTP_P1
{
    public class Product
    {
        #region Class Variable Definition
        public int Id { get; set; }
        public double SatisMiktari { get; set; }
        public double SatisFiyati { get; set; }
        public double AlisFiyati { get; set; }
        public string Tarih { get; set; }
        public double StokMiktari { get; set; }
        #endregion

        #region Class Methods
        public Product(int id, double satisMiktari, double satisFiyati, double alisFiyati, string tarih, double stokMiktari)
        {
            Id = id;
            SatisMiktari = satisMiktari;
            SatisFiyati = satisFiyati;
            AlisFiyati = alisFiyati;
            Tarih = tarih;
            StokMiktari = stokMiktari;
        }

        // Static method to create a Product from a tuple
        public static Product FromTuple((int Id, double SatisFiyati, double SatisMiktari, double AlisFiyati, string Tarih, double StokMiktari) tuple)
        {
            return new Product(tuple.Id, tuple.SatisMiktari, tuple.SatisFiyati, tuple.AlisFiyati, tuple.Tarih, tuple.StokMiktari);
        }
        #endregion
    }
}