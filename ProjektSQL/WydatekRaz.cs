using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplikacja_do_zarzadzania_wydatkami
{
    public class WydatekRaz : Wydatek, ICloneable, IComparable<WydatekRaz>, IEquatable<WydatekRaz>
    {
        [Key]
        public int IdWydatek {  get; set; }

        public int IdKonta { get; set;  }
        public virtual Konto Konto { get; set; }

        public WydatekRaz() { }
        public WydatekRaz(decimal kwota, DateTime data, string kategoria) : base(kwota, data, kategoria)
        {
        }
        public WydatekRaz(decimal kwota, DateTime data, string kategoria, Uzytkownik uzytkownik, Konto konto) : base(kwota, data, kategoria, uzytkownik, konto)
        {
            IdKonta = konto.IdKonta;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public int CompareTo(WydatekRaz? other)
        {
            if (other == null)
                return 1;
            if (Kategoria.CompareTo(other.Kategoria) == 0)
                return -this.Kwota.CompareTo(other.Kwota);
            return Kategoria.CompareTo(other.Kategoria);
        }

        public bool Equals(WydatekRaz? other)
        {
            if (other == null)
                return false;

            // Sprawdź czy Kategoria nie jest null przed dostępem do niej
            if (this.Kategoria != null && other.Kategoria != null)
            {
                return this.Kategoria.Equals(other.Kategoria) && this.Kwota.Equals(other.Kwota);
            }

            return false;
        }

    }
}
