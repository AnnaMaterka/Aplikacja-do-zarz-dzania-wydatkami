using Aplikacja_do_zarzadzania_wydatkami;
namespace ProjektTestowy
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
           
        }

        Uzytkownik uz = new Uzytkownik();

        [Test]
        public void Test1()
        {
            // Sprawdzamy, czy u¿ytkownik generuje siê ze stanem pocz¹tkowym 0
            Assert.AreEqual(uz.StanGotowki, 0);
        }

        [Test]

        public void Test2()
        {
            // Sprawdzamy, czy dzia³a metoda DodajKonto()
            Konto k = new Konto("PKO", 2000, uz);
            uz.DodajKonto(k);
            Assert.IsTrue(uz.ListaKont.Contains(k));
        }

        [Test]

        public void Test3()
        {
            // Próbujemy wyp³aciæ wiêcej ni¿ jest na koncie
            Konto k = new Konto("PKO", 2000, uz);
            uz.DodajKonto(k);
            Assert.Throws<BrakSrodkow>(() => uz.WyplaczKonta(k, 4000, DateTime.Today, "Wydatki"));
        }

        [Test]

        public void Test4()
        {
            // Testujemy sumowanie wp³ywów w tygodniu
            Konto k = new Konto("PKO", 2000, uz);
            WplywRaz w1 = new WplywRaz(3000, DateTime.Today, "Pensja", uz, k);
            WplywRaz w2 = new WplywRaz(700, DateTime.Parse("24-01-2024"), "Sprzeda¿ mebli", uz, k);
            k.NowyWplywKonto(w1);
            k.NowyWplywKonto(w2);
            Assert.AreEqual(k.SumaWplywowTyg(true), 3700);
        }

        [Test]

        public void Test5()
        {
            WydatekRaz w1 = new WydatekRaz(100, DateTime.Parse("10-10-2023"), "Kieszonkowe");
            WydatekRaz w2 = new WydatekRaz(100, DateTime.Today, "Kieszonkowe");
            Assert.IsTrue(w1.Equals(w2));
        }
    }
}