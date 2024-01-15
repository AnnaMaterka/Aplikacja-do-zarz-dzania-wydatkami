using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplikacja_do_zarzadzania_wydatkami
{
    public class Konto
    {
        private string nazwaBanku;
        private decimal stanKonta;

        public Konto(string nazwaBanku, decimal stanKonta)
        {
            this.NazwaBanku = nazwaBanku;
            this.StanKonta = stanKonta;
        }

        public string NazwaBanku { get => nazwaBanku; init => nazwaBanku = value; }
        public decimal StanKonta { get => stanKonta; set => stanKonta = value; }

    }
}
