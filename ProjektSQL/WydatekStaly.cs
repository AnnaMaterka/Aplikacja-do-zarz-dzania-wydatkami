using ProjektSQL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplikacja_do_zarzadzania_wydatkami
{
    public enum Cykl {Tygodniowy, Miesięczny, Dwumiesięczny, Kwartalny, Półroczny, Roczny}
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
    public class WydatekStaly: Wydatek, IPonawialny
    {

        [Key]
        public int IdWydatkuStalego { get; set; }
        public int IdKonta { get; set; }
        public virtual Konto Konto { get; set; }

        private Cykl cyklWydatku;
        private bool oplaconyWBiezacymCyklu;
        private bool stalaKwota;
        private decimal kwota;
        private DateTime data;
        private Kategoria kategoria;

        public WydatekStaly(Cykl cyklWydatku, bool oplaconyWBiezacymCyklu, bool stalaKwota, decimal kwota, DateTime data, string kategoria) :base(kwota, data, kategoria)
        {
            this.cyklWydatku = cyklWydatku;
            this.oplaconyWBiezacymCyklu = oplaconyWBiezacymCyklu;
            this.stalaKwota = stalaKwota;
            this.kwota = stalaKwota?kwota:0;
        }

        public Cykl CyklWydatku { get => cyklWydatku; set => cyklWydatku = value; }
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
        public Kategoria Kategoria { get => kategoria; set => kategoria = value; }

        public void Ponow()
        {
            this.OplaconyWBiezacymCyklu = false;
            Calendar myCal = CultureInfo.InvariantCulture.Calendar;
            switch (this.CyklWydatku)
            {
                case Cykl.Tygodniowy: Data = myCal.AddWeeks(Data, 1); break;
                case Cykl.Miesięczny: Data = myCal.AddMonths(Data,1); break;
                case Cykl.Dwumiesięczny: Data = myCal.AddMonths(Data, 2); break;
                case Cykl.Kwartalny: Data = myCal.AddMonths(Data, 3); break;
                case Cykl.Półroczny: Data = myCal.AddMonths(Data, 6); break;
                case Cykl.Roczny: Data = myCal.AddYears(Data, 1); break;
            }
        }

    }
}
