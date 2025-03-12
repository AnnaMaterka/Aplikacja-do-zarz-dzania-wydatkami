using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplikacja_do_zarzadzania_wydatkami
{
    public class Oszczednosc: Wydatek
    {
        private string cel; //lista celów do zrobienia

        [Key]
        public int IdOszczednosci { get; set; }
        //[ForeignKey("Konto")]
        public int IdKonta { get; set; }
        public virtual Konto Konto { get; set; }

        public Oszczednosc() { }
        public Oszczednosc(decimal kwota, DateTime data, string cel):base(kwota, data)
        {
            this.cel = cel;
        }
        public string Cel { get => cel; set => cel = value; }
    }
}
