using Aplikacja_do_zarzadzania_wydatkami;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Microsoft.VisualBasic;
using System.Windows;

namespace ProjektSQL
{
    public class Sesja
    {
        [Key]
        public int Id { get; set; }
        public int IdUzytkownika { get; set; }
        public bool Zalogowany { get; set; }

        //[ForeignKey(nameof(IdUzytkownika))]
        //public Uzytkownik Uzytkownik { get; set; }

    }
}
