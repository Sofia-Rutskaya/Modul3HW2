using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modul3HW2
{
    public class SortContacts : IComparer<Contact>
    {
        public int Compare(Contact x, Contact y)
        {
            int size;
            if (x.FullName.Length > y.FullName.Length)
            {
                size = x.FullName.Length;
            }
            else
            {
                size = y.FullName.Length;
            }

            for (var i = 0; i < size; i++)
            {
                if (x.FullName[i] > y.FullName[i])
                {
                    var a = x;
                    x = y;
                    y = a;
                    return 1;
                }
                else if (x.FullName[i] < y.FullName[i])
                {
                    return -1;
                }
            }

            return 0;
        }
    }
}
