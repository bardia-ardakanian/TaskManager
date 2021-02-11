using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager
{
    class Program
    {
        static void Main(string[] args)
        {
            Admin admin = new Admin();
            Handler handler = new Handler(admin);

            while (true)
            {
                Console.Write("$ ");
                String input = Console.ReadLine();

                handler.Process(input);
            }
        }
    }
}
