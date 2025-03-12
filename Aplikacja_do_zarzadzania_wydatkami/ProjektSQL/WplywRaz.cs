using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplikacja_do_zarzadzania_wydatkami
{
    public class WplywRaz :Wplyw, ICloneable, IComparable<WplywRaz>
    {

        [Key]
        public int IdWplywu { get; set; }

        public int IdKonta { get; set; }
        public virtual Konto Konto { get; set; }

        public WplywRaz() { }
        public WplywRaz(decimal kwota, DateTime data, string kategoria, Uzytkownik uzytkownik, Konto konto) : base(kwota, data, kategoria, uzytkownik, konto)
        {
            IdKonta = konto.IdKonta;
        }
       
        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public int CompareTo(WplywRaz? other)
        {
            if (other == null)
                return 1;
            if (this.Kategoria.CompareTo(other!.Kategoria) == 0) 
                return -this.Kwota.CompareTo(other.Kwota);
            return this.Kategoria.CompareTo(other.Kategoria);
        }

        public bool Equals(WplywRaz? other)
        {
            if (other == null)
                return false;

            if (this.Kategoria != null && other.Kategoria != null)
            {
                return this.Kategoria.Equals(other.Kategoria) && this.Kwota.Equals(other.Kwota);
            }

            // Jeśli jedna z Kategorii jest null, obiekty nie są równe
            return false;
        }
    }
}
