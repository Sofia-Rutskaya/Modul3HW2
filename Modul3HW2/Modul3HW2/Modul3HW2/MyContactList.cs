using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Threading.Channels;

namespace Modul3HW2
{
    public class MyContactList
    {
        private List<Contact> _contacts;
        private Dictionary<string, List<Contact>> _phoneGroup;

        public MyContactList()
        {
            _contacts = new List<Contact>();
            _phoneGroup = new Dictionary<string, List<Contact>>();
        }

        public void Add(Contact contact)
        {
            var firstLatter = $"{contact.FullName[0]}";
            if (_phoneGroup.ContainsKey(firstLatter))
            {
                _phoneGroup[firstLatter].Add(contact);
                Console.WriteLine("Exist");
            }
            else
            {
                NewGroup(firstLatter, contact);
                Console.WriteLine("New Group");
            }
        }

        private void NewGroup(string firstLatter, Contact contact)
        {
            if (int.TryParse(firstLatter, out var number))
            {
                NumberGroup(contact);
            }
            else
            {
                _phoneGroup.Add(firstLatter, new List<Contact>() { contact });
            }
        }

        private void NumberGroup(Contact contact)
        {
            try
            {
                _phoneGroup.Add("0-9", new List<Contact>() { contact });
            }
            catch
            {
                _phoneGroup["0-9"].Add(contact);
            }
        }
    }
}
