using System;
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
        Śmieci,
        Czynsz,
        RataKredytu,
        Rozrywka,
        Inne
    }
    public class WydatekStaly : ICloneable
    {
        private CyklWydatku cyklWydatku;
        private bool OplaconyWBiezacymCyklu;
        private bool stalaKwota;
        private decimal kwota;
        private DateTime deadline;
        private KategoriaWydatkuSt kategoria;

        public CyklWydatku CyklWydatku { get => cyklWydatku; set => cyklWydatku = value; }
        public bool OplaconyWBiezacymCyklu1 { get => OplaconyWBiezacymCyklu; set => OplaconyWBiezacymCyklu = value; }
        public bool StalaKwota { get => stalaKwota; set => stalaKwota = value; }
        public decimal Kwota
        {
            get => kwota; set
            {
                if (StalaKwota) { kwota = value; }
            }
        }
        public DateTime Deadline { get => deadline; set => deadline = value; }
        public KategoriaWydatkuSt Kategoria { get => kategoria; set => kategoria = value; }

        public object Clone()
        {
            WydatekStaly x = (WydatekStaly)this.MemberwiseClone();
            x.OplaconyWBiezacymCyklu = false;
            return x;
        }

    }
}
