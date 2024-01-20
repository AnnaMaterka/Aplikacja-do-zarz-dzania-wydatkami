using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplikacja_do_zarzadzania_wydatkami
{
    public class Kategoria
    {
        [Key]
        public int IdKategorii { get; set; }
        
        string nazwaKategorii;
        public Kategoria(string nazwaKategorii)
        {
            NazwaKategorii = nazwaKategorii;
        }
        public string NazwaKategorii { get => nazwaKategorii; set => nazwaKategorii = value; }

        public int IdWplywu1 { get; set; }
        public virtual Wplyw Wplyw { get; set; }
        public int IdWydatek {  get; set; }
        public virtual WydatekRaz WydatekRaz { get; set; }
        public int IdWydatkuStalego { get; set; }
        public virtual WydatekStaly WydatekStaly { get; set; }

        //public string NazwaKategorii;
    }
}
