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
    public class WydatekStaly
    {
        private CyklWydatku cyklWydatku;
        private bool OplaconyWBiezacymCyklu;
        private bool stalaKwota;
        private decimal kwota;
        private DateTime deadline;
        private KategoriaWydatkuSt kategoria;

        public WydatekStaly(CyklWydatku cyklWydatku, bool oplaconyWBiezacymCyklu, bool stalaKwota, decimal kwota, DateTime deadline, KategoriaWydatkuSt kategoria)
        {
            this.cyklWydatku = cyklWydatku;
            OplaconyWBiezacymCyklu = oplaconyWBiezacymCyklu;
            this.stalaKwota = stalaKwota;
            this.kwota = kwota;
            this.deadline = deadline;
            this.kategoria = kategoria;
        }

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

    }
}
