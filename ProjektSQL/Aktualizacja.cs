using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Aplikacja_do_zarzadzania_wydatkami
{
    public class Aktualizacja : INotifyPropertyChanged
    {
        //Zdarzenie to informuje o zmianie wartości właściwości obiektu. 
        public event PropertyChangedEventHandler PropertyChanged;

        // OnPropertyChanged to metoda, która jest wywoływana, gdy wartość właściwości ulega zmianie, aby poinformować o tym zdarzeniu.
        // CallerMemberName to atrybut, który umożliwia automatyczne przekazywanie nazwy właściwości, która ją wywołała.Dzięki temu nie trzeba
        // ręcznie podawać nazwy właściwości przy wywołaniu tej metody. Nazwa właściwości jest przekazywana jako argument do zdarzenia PropertyChanged.

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            // ?-sprawdzenie czy PropertyChanged jest null
            // this - element który się zmienił
            // proprertyName - nazwa właściwości
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
