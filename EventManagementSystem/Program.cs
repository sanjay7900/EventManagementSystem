using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagementSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
        //    Customer customer = new Customer();
        //    Admin admin = new Admin();
        //    admin.AddEvent();
        //    // customer.BookEvent();
        //    //customer.showEvents();
        //    //customer.ShowMyEvent();
          PerFormAllFunctionlity perFormAllFunctionlity = new PerFormAllFunctionlity();
            perFormAllFunctionlity.mainPortal();
            Console.ReadLine(); 
        }
    }
}
