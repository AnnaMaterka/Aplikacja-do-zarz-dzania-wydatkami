﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplikacja_do_zarzadzania_wydatkami
{
    public enum CyklWydatku {Tygodniowy, Miesięczny, Dwumiesięczny, Kwartalny, Półroczny, Roczny}
    public enum KategoriaWydatkuSt
    {
        PodatekDochodowy,
        PodatekodNier,
        Prąd,
        Woda,
        Gaz,
        Internet,
        Telewizja,
        Abonament,
        Śmieci,
        Czynsz,
        RataKredytu,
        Rozrywka,
        Inne
    }
    public class WydatekStaly: Wydatek
    {
        private CyklWydatku cyklWydatku;
        private bool oplaconyWBiezacymCyklu;
        private bool stalaKwota;
        private decimal kwota;
        private DateTime data;
        private KategoriaWydatkuSt kategoria;

        public WydatekStaly(CyklWydatku cyklWydatku, bool oplaconyWBiezacymCyklu, bool stalaKwota, decimal kwota, DateTime data, KategoriaWydatkuSt kategoria) :base(kwota, data)
        {
            this.cyklWydatku = cyklWydatku;
            this.oplaconyWBiezacymCyklu = oplaconyWBiezacymCyklu;
            this.stalaKwota = stalaKwota;
            this.kwota = stalaKwota?kwota:0;
            this.kategoria = kategoria;
        }

        public CyklWydatku CyklWydatku { get => cyklWydatku; set => cyklWydatku = value; }
        public bool OplaconyWBiezacymCyklu { get => oplaconyWBiezacymCyklu; set => oplaconyWBiezacymCyklu = value; }
        public bool StalaKwota { get => stalaKwota; set => stalaKwota = value; }
        public decimal Kwota
        {
            get => kwota; set
            {
                if (StalaKwota) { kwota = value; }
            }
        }
        public DateTime Data { get => data; set => data = value; }
        public KategoriaWydatkuSt Kategoria { get => kategoria; set => kategoria = value; }

    }
}
