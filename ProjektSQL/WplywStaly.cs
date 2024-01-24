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
    public class WplywStaly : Wplyw, IPonawialny
    {
        private Cykl cyklWplywu;
        [Key]
        public int IdWydatekStaly { get; set; }

        public int IdKonta { get; set; }
        //public int IdKategorii { get; set; }
        public virtual Konto Konto { get; set; }
        public virtual string Kategoria { get; set; }

        public Cykl CyklWplywu { get => cyklWplywu; set => cyklWplywu = value; }

        public WplywStaly() { }
        public WplywStaly(decimal kwota, DateTime data, Cykl cyklWplywu) : base(kwota, data)
        {
            this.CyklWplywu = cyklWplywu;
        }
        public WplywStaly(decimal kwota, DateTime data, string kategoria, Uzytkownik uzytkownik, Konto konto, Cykl cyklWplywu) : base(kwota, data, kategoria, uzytkownik, konto)
        {
            IdKonta = konto.IdKonta;
            CyklWplywu = cyklWplywu;
        }
        public void Ponow()
        {
            Calendar myCal = CultureInfo.InvariantCulture.Calendar;
            switch (this.CyklWplywu)
            {
                case Cykl.Tygodniowy: Data = myCal.AddWeeks(Data, 1); break;
                case Cykl.Miesięczny: Data = myCal.AddMonths(Data, 1); break;
                case Cykl.Dwumiesięczny: Data = myCal.AddMonths(Data, 2); break;
                case Cykl.Kwartalny: Data = myCal.AddMonths(Data, 3); break;
                case Cykl.Półroczny: Data = myCal.AddMonths(Data, 6); break;
                case Cykl.Roczny: Data = myCal.AddYears(Data, 1); break;
            }
        }

    }
}
