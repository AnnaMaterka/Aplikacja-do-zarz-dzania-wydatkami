using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplikacja_do_zarzadzania_wydatkami
{
    //public enum KategoriaWydatku { Spożywcze, Ubrania, Buty, Opłaty, Rozrywka, Elektronika, Uroda, DoDomu, Inne }
    public class WydatekRaz : Wydatek
    {
        private decimal kwota;
        private DateTime data;
        //private KategoriaWydatku kategoria;
        private string kategoria;

        //public WydatekRaz(decimal kwota, DateTime data, KategoriaWydatku kategoria) : base(kwota, data)
        //{
        //    this.kategoria = kategoria;
        //}
        //public KategoriaWydatku Kategoria { get => kategoria; set => kategoria = value; }
        public WydatekRaz(decimal kwota, DateTime data) : base(kwota, data)
        {
            Kategoria = "Inne";
        }
        public WydatekRaz(decimal kwota, DateTime data, string kategoria) : base(kwota, data)
        {
            this.kategoria = kategoria;
        }
        public string Kategoria { get => kategoria; set => kategoria = value; }
    }
}
