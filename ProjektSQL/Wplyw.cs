using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplikacja_do_zarzadzania_wydatkami
{
    public abstract class Wplyw
    {
        private decimal kwota;
        private DateTime data;
        private Kategoria kategoria;

        [Key]
        public int IdWplywu { get; set; }

        public virtual Kategoria Kategorie { get; set; }
        protected Wplyw(decimal kwota, DateTime data)
        {
            Kwota = kwota;
            Data = data;
        }
        protected Wplyw(decimal kwota, DateTime data, Kategoria kategoria) : this(kwota, data)
        {
            Kategoria = kategoria;
        }

        public decimal Kwota { get => kwota; set => kwota = value; }
        public DateTime Data { get => data; set => data = value; }
        public Kategoria Kategoria { get => kategoria; set => kategoria = value; }

    }
}
