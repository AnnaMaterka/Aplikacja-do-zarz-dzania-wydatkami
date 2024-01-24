using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplikacja_do_zarzadzania_wydatkami
{
    public abstract class Wplyw
    {
        private decimal kwota;
        private DateTime data;
        private Uzytkownik uzytkownik;
        private Konto konto;
        private string kategoria;
        public Wplyw() { }
        protected Wplyw(decimal kwota, DateTime data)
        {
            Kwota = kwota;
            Data = data;
        }

        protected Wplyw(decimal kwota, DateTime data, string kategoria) : this(kwota, data)
        {
            Kategoria = kategoria;
        }
        protected Wplyw(decimal kwota, DateTime data, string kategoria, Uzytkownik uzytkownik, Konto konto) : this(kwota, data, kategoria)
        {
            Uzytkownik = uzytkownik;
            Konto = konto;
        }

        public decimal Kwota { get => kwota; set => kwota = value; }
        public DateTime Data { get => data; set => data = value; }
        public Uzytkownik Uzytkownik { get => uzytkownik; set => uzytkownik = value; }
        public Konto Konto { get => konto; set => konto = value; }
        public string Kategoria { get => kategoria; set => kategoria = value; }

    }

}
