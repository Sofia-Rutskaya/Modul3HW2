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
        private const string _ru = "абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
        private const string _en = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private SortedDictionary<string, SortedDictionary<string, List<Contact>>> _differentLanguageGroups;
        private string _language;
        private List<Contact> _contacts;

        public MyContactList()
        {
            _language = "en-US";
            _differentLanguageGroups = new SortedDictionary<string, SortedDictionary<string, List<Contact>>>();
            _differentLanguageGroups.Add(_language, new SortedDictionary<string, List<Contact>>());
        }

        public void Add(Contact contact)
        {
            var firstLatter = $"{contact.FullName[0]}";
            if (_differentLanguageGroups[_language].ContainsKey(firstLatter))
            {
                _differentLanguageGroups[_language][firstLatter].Add(contact);
            }
            else
            {
                NewGroup(firstLatter, contact, _language);
            }

            SortDictionaryList();
        }

        public void AddLanguage(string language)
        {
            _differentLanguageGroups.Add(language, new SortedDictionary<string, List<Contact>>());
        }

        public void ChangeLanguage(string language)
        {
            try
            {
                for (var j = GetLanguage(_language).Length - 1; j > 0; j--)
                {
                    if (_differentLanguageGroups[_language].ContainsKey($"{GetLanguage(_language)[j]}"))
                    {
                        for (var k = 0; k < _differentLanguageGroups[_language][$"{GetLanguage(_language)[j]}"].Count; k++)
                        {
                            OtherGroup(_differentLanguageGroups[_language][$"{GetLanguage(_language)[j]}"][k], language);
                        }
                    }
                }

                for (var i = 0; i < _differentLanguageGroups[_language]["#"].Count; i++)
                {
                    var firstLatter = $"{_differentLanguageGroups[_language]["#"][i].FullName[0]}";

                    if (CompareCulture(firstLatter))
                    {
                        NewGroup(firstLatter, _differentLanguageGroups[_language]["#"][i], language, false);
                    }
                }
            }
            catch
            {
            }

            try
            {
                for (var i = 0; i < _differentLanguageGroups[_language]["0-9"].Count(); i++)
                {
                    NumberGroup(_differentLanguageGroups[_language]["0-9"][i], language);
                }
            }
            catch
            {
            }

            _language = language;
        }

        public void PrintAllContacts()
        {
            for (var j = GetLanguage(_language).Length - 1; j > 0; j--)
            {
                if (_differentLanguageGroups[_language].ContainsKey($"{GetLanguage(_language)[j]}"))
                {
                    Console.WriteLine($"{GetLanguage(_language)[j]}:");
                    for (var k = 0; k < _differentLanguageGroups[_language][$"{GetLanguage(_language)[j]}"].Count; k++)
                    {
                        Console.WriteLine($"\t{_differentLanguageGroups[_language][$"{GetLanguage(_language)[j]}"][k].FullName}\t\t" +
                            $"{_differentLanguageGroups[_language][$"{GetLanguage(_language)[j]}"][k].Number}");
                    }
                }
            }

            try
            {
                Console.WriteLine("#:");
                for (var k = 0; k < _differentLanguageGroups[_language]["#"].Count; k++)
                {
                    Console.WriteLine($"\t{_differentLanguageGroups[_language]["#"][k].FullName}\t\t" +
                        $"{_differentLanguageGroups[_language]["#"][k].Number}");
                }
            }
            catch
            {
            }

            try
            {
                Console.WriteLine("0-9:");
                for (var k = 0; k < _differentLanguageGroups[_language]["0-9"].Count; k++)
                {
                    Console.WriteLine($"\t{_differentLanguageGroups[_language]["0-9"][k].FullName}\t\t" +
                        $"{_differentLanguageGroups[_language]["0-9"][k].Number}");
                }
            }
            catch
            {
            }
        }

        private string GetLanguage(string language)
        {
            switch (language)
            {
                case "ru-RU":
                    return _ru;
                case "en-US":
                    return _en;
                default:
                    return _en;
            }
        }

        private void NewGroup(string firstLatter, Contact contact, string language, bool needToAddInOtherGroup = true)
        {
            if (int.TryParse(firstLatter, out var number))
            {
                NumberGroup(contact, language);
                return;
            }
            else if (CompareCulture(firstLatter) && needToAddInOtherGroup)
            {
                OtherGroup(contact, language);
                return;
            }
            else
            {
                _differentLanguageGroups[language].Add(firstLatter, new List<Contact>() { contact });
            }
        }

        private void SortDictionaryList()
        {
            for (var j = 0; j < GetLanguage(_language).Length; j++)
            {
                if (_differentLanguageGroups[_language].ContainsKey($"{GetLanguage(_language)[j]}"))
                {
                    _contacts = _differentLanguageGroups[_language][$"{GetLanguage(_language)[j]}"];

                    _differentLanguageGroups[_language][$"{GetLanguage(_language)[j]}"].Sort(new SortContacts());
                }
            }
        }

        private void NumberGroup(Contact contact, string language)
        {
            try
            {
                _differentLanguageGroups[language].Add("0-9", new List<Contact>() { contact });
            }
            catch
            {
                try
                {
                    _differentLanguageGroups[language]["0-9"].Add(contact);
                }
                catch
                {
                    return;
                }
            }
        }

        private void OtherGroup(Contact contact, string language)
        {
            try
            {
                _differentLanguageGroups[language].Add("#", new List<Contact>() { contact });
            }
            catch
            {
                _differentLanguageGroups[language]["#"].Add(contact);
            }
        }

        private bool CompareCulture(string firstLatter)
        {
            switch (_language)
            {
                case "ru-RU":
                    for (var i = 0; i < _ru.Length; i++)
                    {
                        if (firstLatter.Equals($"{_ru[i]}", StringComparison.CurrentCulture))
                        {
                            return false;
                        }
                    }

                    break;
                case "en-US":
                    for (var i = 0; i < _en.Length; i++)
                    {
                        if (firstLatter.Equals($"{_en[i]}", StringComparison.CurrentCulture))
                        {
                            return false;
                        }
                    }

                    break;
            }

            return true;
        }
    }
}
