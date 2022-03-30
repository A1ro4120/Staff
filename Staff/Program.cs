using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Staff
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Repository db = new Repository("Staff.txt");

            db.Functional();
        }
    }
}