using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace EventManagementSystem
{
    public class PerFormAllFunctionlity
    {
        private static string conn = @"Data Source=DESKTOP-AMR2CQS\MSSQLSERVER01;Initial Catalog=EventManagementSystem;Integrated Security=True";
        private  SqlConnection conn2 = new SqlConnection(conn);
        public void Customermenu()
        {
            Console.WriteLine("******************************************************");
            Console.WriteLine("*      .  To Show All Event list Press :1             *");
            Console.WriteLine("*      .  To Show My Event list  Press :2             *");
            Console.WriteLine("*      .  To Book a Event        press :3             *");
            Console.WriteLine("*      .  Exit                   press :4             *");
            Console.WriteLine("******************************************************");

        }
        public void AdminMenu()
        {
            Console.WriteLine("*************************************************************************");
            Console.WriteLine("*      .  To Show All Event list                   Press :1             *");
            Console.WriteLine("*      .  To Show All Approved requests Event list Press :2             *");
            Console.WriteLine("*      .  To Approved/reject a request a Event     Press :3             *");
            Console.WriteLine("*      .  To Add A Event                           Press :4             *");
            Console.WriteLine("*      .  To Show All Reject Requests              Press :5             *");
            Console.WriteLine("*      .  Exit                                     Press :6             *");
            Console.WriteLine("*************************************************************************");
        }
        public void MainMenu()
        {
            Console.WriteLine("*************************************************************");
            Console.WriteLine("* Login As Admin                  Press :1                  *");
            Console.WriteLine("* Login As Customer               Press :2                  *");
            Console.WriteLine("* Create An Account As Customers  Press :3                   *");
            Console.WriteLine("* Exit                            Press :4                  *");
            Console.WriteLine("*************************************************************");
        }


        public void AdminPortal()
        {
            Admin admin=new Admin();
        label1:
            AdminMenu();
            Console.Write("                                                ");
            int switch_on=Convert.ToInt32(Console.ReadLine());  

            switch (switch_on)
            {
                case 1:
                    admin.ShowEvent();
                    goto label1;
                case 2: 
                    admin.ShowallApproved();    
                    goto label1;
                case 3:
                    admin.ChangeStatus();
                    goto label1;
                case 4:
                    admin.AddEvent();
                    goto label1;
                case 5:
                    admin.ShowAllRejectRequests();  
                    goto label1;
                case 6:
                    Environment.Exit(0);
                    break;
               default:
                    Console.WriteLine("Wrong Choise Try Again");
                    goto label1;
            }
        }
        public void CustomerPortal()
        {
            Customer customer=new Customer();
        label2:
            Customermenu();
            Console.Write("                                       ");
            int switch_on=Convert.ToInt32(Console.ReadLine()); 

            switch (switch_on)
            {
                case 1:
                    customer.showEvents();  
                    goto label2;
                case 2:
                    customer.ShowMyEvent();
                    goto label2;
                case 3:
                    customer.BookEvent();
                    goto label2;
                case 4:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Wrong Choise Try Again");
                    goto label2;
            }

        }
        public void mainPortal()
        {
        label3:
            MainMenu();
            Console.Write("                              ");
            int switch_on = Convert.ToInt32(Console.ReadLine());
            switch (switch_on)
            {
                case 1:
                    if (checkAdmin())
                    {
                        AdminPortal();

                    }
                    else
                    {
                        Console.WriteLine("You Are Not a Registered admin");
                        //Console.WriteLine(" Choose 4 Option And Create an Account");

                    }
                   
                    goto label3;
                case 2:
                    if (checkCustomer())
                    {

                        CustomerPortal();
                    }
                    else
                    {
                        Console.WriteLine("You Are Not a Registered Customers");
                        Console.WriteLine(" Choose 4 Option And Create an Account");

                    }
                    
                    goto label3;

                case 3:
                    CreateAnAccount();  
                    goto label3;
                case 4:
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("wrong Choise ");
                    goto label3;
            }
        }

        private void CreateAnAccount()
        {
            try
            {
                Console.WriteLine("Enter the Email Id ");
                string email = Console.ReadLine();
                Console.WriteLine("Enter the User Name ");
                string name = Console.ReadLine();
                Console.WriteLine("Enter your Address ");
                string usertype = "customers";
                string address = Console.ReadLine();
                string sql = "insert into users values('" + email + "','" + name + "','" + usertype + "','" + address + "')";
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sql, conn);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                Console.WriteLine("Account Created Successfully");


            }
            catch (Exception ex)
            {
                Console.WriteLine("Account not  Created ");
                Console.WriteLine("try Later");
            }

           
        }
        private bool checkCustomer()
        {
            Console.WriteLine("Enter Your Email Id");
            string email = Console.ReadLine();
            string type = "customers";
            string sql = "select * from users where usermail='"+email+"'  and usertype='"+type+"'";
            SqlDataAdapter sqlDataAdapter   =new SqlDataAdapter(sql, conn);
            DataTable dataTable=new DataTable();
            sqlDataAdapter.Fill(dataTable);
            if(dataTable.Rows.Count > 0)
            {
               Customer.userId = Convert.ToInt32(dataTable.Rows[0][0]);
                return true;
            }
            return false;
        }
        private bool checkAdmin()
        {
            Console.WriteLine("Enter Your Email Id");
            string email = Console.ReadLine();
            string type = "admin";
            string sql = "select * from users where usermail='" + email + "' and usertype='" + type + "'";
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sql, conn);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            if (dataTable.Rows.Count > 0)
            {
                return true;
            }
            return false;
        }



    }
}
