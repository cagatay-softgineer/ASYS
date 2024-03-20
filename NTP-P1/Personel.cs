#region Packages
#endregion

namespace NTP_P1
{
    public class Personel
    {
        #region Class Variable Definition
        public int Id { get; set; }
        public string KullaniciAdi { get; set; }
        public string Sifre { get; set; }
        public string Mail { get; set; }
        public string GirisZamani { get; set; }
        public bool isRoot { get; set; }
        public bool isDefaultUser { get; set; }
        public bool isEmployee { get; set; }
        public string Ad { get; set; }
        public string Soyadi { get; set; }
        #endregion

        #region Class Methods
        public Personel(int id, string kullaniciAdi, string sifre, string mail, string girisZamani, bool root, bool defaultUser, bool employee, string ad, string soyad)
        {
            Id = id;
            KullaniciAdi = (kullaniciAdi);
            Sifre = (sifre);
            Mail = (mail);
            GirisZamani = girisZamani;
            isRoot = root;
            isDefaultUser = defaultUser;
            isEmployee = employee;
            Ad = ad;
            Soyadi = soyad;
        }

        public static Personel FromTuple((int id, string kullaniciAdi, string sifre, string mail, string girisZamani, bool root, bool defaultUser, bool employee, string ad, string soyad) tuple)
        {
            return new Personel(tuple.id, (tuple.kullaniciAdi), (tuple.sifre), (tuple.mail), tuple.girisZamani, tuple.root, tuple.defaultUser, tuple.employee, tuple.ad, tuple.soyad);
        }
        #endregion
    }
}
