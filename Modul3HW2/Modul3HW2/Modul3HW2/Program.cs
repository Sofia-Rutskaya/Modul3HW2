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
            phone.Add(new Contact() { FullName = "Mery", Number = 092882 });
            phone.Add(new Contact() { FullName = "Mia", Number = 262736 });
            phone.Add(new Contact() { FullName = "Meggy", Number = 262736 });
            phone.Add(new Contact() { FullName = "Mystery", Number = 262736 });
            phone.Add(new Contact() { FullName = "Meast", Number = 262736 });
            phone.Add(new Contact() { FullName = "Meazot", Number = 262736 });
            phone.Add(new Contact() { FullName = "Margaret", Number = 77777 });
            phone.Add(new Contact() { FullName = "Андрей", Number = 5555555 });
            phone.Add(new Contact() { FullName = "Яна", Number = 95468905 });
            phone.Add(new Contact() { FullName = "0Андрей", Number = 5555555 });
            phone.Add(new Contact() { FullName = "Miss Helen", Number = 999999 });
            phone.Add(new Contact() { FullName = "Den", Number = 2362151 });
            phone.AddLanguage("ru-RU");
            phone.ChangeLanguage("ru-RU");
            phone.Add(new Contact() { FullName = "Ася", Number = 4737372 });
            phone.Add(new Contact() { FullName = "Олег", Number = 4588727 });
            phone.ChangeLanguage("en-US");
            phone.Add(new Contact() { FullName = "Fortune", Number = 77777 });
        }
    }
}
