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
        [Key]
        public int IdWplywu { get; set; }

        [ForeignKey("Kategorie")]
        public int IdKategorii { get; set; }

        public virtual Kategoria Kategorie { get; set; }

        public decimal Kwota { get; set; }

        public DateTime Data { get; set; }

        // Konstruktor domyślny dla Entity Framework
        protected Wplyw() { }

        // Konstruktor dla kwoty i daty
        protected Wplyw(decimal kwota, DateTime data)
        {
            Kwota = kwota;
            Data = data;
        }

        // Konstruktor dla kwoty, daty i nazwy kategorii
        protected Wplyw(decimal kwota, DateTime data, string kategoria) : this(kwota, data)
        {
            Kategorie = Kategoria.SzukanieKategorii(kategoria);
        }
    }
}
