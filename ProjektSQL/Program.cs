namespace Aplikacja_do_zarzadzania_wydatkami
{
    internal class Program
    {
        private static void Main()
        {
            //Konto k1 = new("Bank1", (decimal)100.00);
            //Console.Write(DateTime.Today.ToString("yyyyMM"));
            Uzytkownik u1 = new Uzytkownik(300);
            Konto k1 = new("Bank1", (decimal)100, u1);
            Konto k2 = new("Bank1", (decimal)200, u1);
            Konto k3 = new("Bank3", (decimal)150, u1);
            //u1.DodajKonto(k1);
            //u1.DodajKonto(k2);
            //u1.DodajKonto(k3);
            //u1.WplacnaKonto(k1, 200, DateTime.Now, "prezent");
            //u1.WplywGotowki(300);
            //u1.WplacnaKonto(k1, 100, new DateTime(2024, 1, 10), "prezent");
            //u1.WyplaczKonta(k2, 100, DateTime.Now, "ubrania");
            u1.ZapiszDoBazy();

        }
    }
}