using System;
using System.Globalization;
using System.Threading.Channels;

namespace Modul3HW2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var phone = new MyContactList();
            phone.Add(new Contact() { FullName = "Ands", Number = 092882 });
            phone.Add(new Contact() { FullName = "Ads", Number = 77777 });
            phone.Add(new Contact() { FullName = "Андрей", Number = 5555555 });
            phone.Add(new Contact() { FullName = "0Андрей", Number = 5555555 });
            phone.Add(new Contact() { FullName = "Mnds", Number = 999999 });
        }
    }
}
