﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplikacja_do_zarzadzania_wydatkami
{
    public abstract class Wydatek
    {
        private decimal kwota;
        private DateTime data;
        private Kategoria kategoria;

        [ForeignKey("Kategoria")]
        public int IdKategorii { get; set; }
        public virtual Kategoria Kategorie { get; set; }
        public Wydatek(decimal kwota, DateTime data)
        {
            Kwota = kwota;
            Data = data;
        }


        protected Wydatek(decimal kwota, DateTime data, string kategoria) :this(kwota, data) 
        {
            Kategoria = Kategoria.SzukanieKategorii(kategoria);
        }

        public decimal Kwota { get => kwota; set => kwota = value; }
        public DateTime Data { get => data; set => data = value; }
        public Kategoria Kategoria { get => kategoria; set => kategoria = value; }
    }
}
