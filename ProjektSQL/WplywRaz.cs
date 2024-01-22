using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplikacja_do_zarzadzania_wydatkami
{
    public class WplywRaz :Wplyw, ICloneable, IComparable<WplywRaz>, IEquatable<WplywRaz>
    {

        [Key]
        public int IdWplywu { get; set; }

        public int IdKonta { get; set; }
        public int IdKategorii { get; set; }
        public virtual Konto Konto { get; set; }
        public virtual Kategoria Kategoria { get; set; }

        public WplywRaz() { }
        public WplywRaz(decimal kwota, DateTime data) : base(kwota, data)
        {
        }
        public WplywRaz(decimal kwota, DateTime data, Kategoria kategoria) : base(kwota, data, kategoria)
        {
        }
        public WplywRaz(decimal kwota, DateTime data, Kategoria kategoria, Konto konto) : base(kwota, data, kategoria)
        {
            Konto = konto;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        // tylko żeby nie wyrzucało błędu
        public int CompareTo(WplywRaz? other)
        {
            throw new NotImplementedException();
        }

        //public int CompareTo(WplywRaz? other)
        //{
        //    if (this.Kategoria.CompareTo(other!.Kategoria) == 0) { return -this.Kwota.CompareTo(other.Kwota); }
        //    return this.Kategoria.CompareTo(other.Kategoria);
        //}

        public bool Equals(WplywRaz? other)
        {
            if (Kategoria.Equals(other!.Kategoria) && base.Kwota.Equals(other.Kwota)) { return true; }
            return false;
        }
    }
}
