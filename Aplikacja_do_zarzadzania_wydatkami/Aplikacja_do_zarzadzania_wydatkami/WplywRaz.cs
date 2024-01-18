using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplikacja_do_zarzadzania_wydatkami
{
    internal class WplywRaz :Wplyw, ICloneable, IComparable<WplywRaz>, IEquatable<WplywRaz>
    {
        private string kategoria;
        public WplywRaz(decimal kwota, DateTime data) : base(kwota, data)
        {
            Kategoria = "Inne";
        }
        public WplywRaz(decimal kwota, DateTime data, string kategoria) : base(kwota, data)
        {
            Kategoria = kategoria;
        }
        public string Kategoria { get => kategoria; set => kategoria = value; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public int CompareTo(WplywRaz? other)
        {
            if (this.Kategoria.CompareTo(other!.Kategoria) == 0) { return -this.Kwota.CompareTo(other.Kwota); }
            return this.Kategoria.CompareTo(other.Kategoria);
        }

        public bool Equals(WplywRaz? other)
        {
            if (this.Kategoria.Equals(other!.Kategoria) && this.Kwota.Equals(other.Kwota)) { return true; }
            return false;
        }
    }
}
