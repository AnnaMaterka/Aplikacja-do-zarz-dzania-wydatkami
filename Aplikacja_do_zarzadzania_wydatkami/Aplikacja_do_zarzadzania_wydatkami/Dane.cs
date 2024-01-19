using System;
using System.Text;
using System.Threading.Task;
using System.IO;

public class Dane
{
	public Dane()
	{
        // konfiguracja dzięki której możemy połączyć się z bazą danych
        // Data Source=nazwa naszego serwera
        // Integrated Security=True - łączymy się za pomocą konta domenowego a nie za pomocą login i hasło?
        private string conString = "Data Source=ACERVERO\\SQLEXPRESS; Initial Catalog=DaneAplikacjaDoZarzadzaniaWydatkami, Integrated Security=True;";
	
        private void ModyfikacjaDanych(string zapytanie)
        {
        using (SqlConnection sCon = new SqlConnection(conString))
        {

        }
        }
    }
}
