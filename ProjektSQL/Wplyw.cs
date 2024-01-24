using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplikacja_do_zarzadzania_wydatkami
{
    //public abstract class Wplyw
    //{
    //    private decimal kwota;
    //    private DateTime data;
    //    private Kategoria kategoria;

    //    [Key]
    //    public int IdWplywu { get; set; }

    //    [ForeignKey("Kategoria")]
    //    public int IdKategorii { get; set; }
    //    public virtual Kategoria Kategorie { get; set; }
    //    protected Wplyw(decimal kwota, DateTime data)
    //    {
    //        Kwota = kwota;
    //        Data = data;
    //    }
    //    protected Wplyw(decimal kwota, DateTime data, string kategoria) : this(kwota, data)
    //    {
    //        Kategoria = Kategoria.SzukanieKategorii(kategoria);
    //    }

    //    public decimal Kwota { get => kwota; set => kwota = value; }
    //    public DateTime Data { get => data; set => data = value; }
    //    public Kategoria Kategoria { get => kategoria; set => kategoria = value; }

    //}
    public abstract class Wplyw
    {
        private decimal kwota;
        private DateTime data;
        private Kategoria kategoria;
        private Uzytkownik uzytkownik;
        private Konto konto;

        //[Key]
        //public int IdWplywu { get; set; }

        //[ForeignKey("Kategorie")]
        //public int IdKategorii { get; set; }
        //public virtual Kategoria Kategorie { get; set; }
        public Wplyw() { }
        protected Wplyw(decimal kwota, DateTime data)
        {
            Kwota = kwota;
            Data = data;
        }

        protected Wplyw(decimal kwota, DateTime data, Kategoria kategoria) : this(kwota, data)
        {
            Kategoria = kategoria;
        }
        protected Wplyw(decimal kwota, DateTime data, Kategoria kategoria, Uzytkownik uzytkownik, Konto konto) : this(kwota, data, kategoria)
        {
            Uzytkownik = uzytkownik;
            Konto = konto;
        }

        public decimal Kwota { get => kwota; set => kwota = value; }
        public DateTime Data { get => data; set => data = value; }
        public Uzytkownik Uzytkownik { get => uzytkownik; set => uzytkownik = value; }
        public Konto Konto { get => konto; set => konto = value; }

        // Prywatna właściwość kategorii
        private Kategoria Kategoria { get => kategoria; set => kategoria = value; }


        // Metoda dostępu do kategorii, jeśli to konieczne
        //public Kategoria PobierzKategorie() => Kategoria.SzukanieKategorii(Kategoria.NazwaKategorii);
    }

}
