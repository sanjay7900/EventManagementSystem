using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace EventManagementSystem
{
    public class Customer
    {
        public static int userId;
        private static string connectionString = @"Data Source=DESKTOP-AMR2CQS\MSSQLSERVER01;Initial Catalog=EventManagementSystem;Integrated Security=True";
        private SqlConnection connection=new SqlConnection(connectionString);
        public void showEvents()
        {
            try
            {
               
                string sql = "select * from EventsManagement";
               SqlDataAdapter adapter = new SqlDataAdapter(sql,connection);
                DataTable dt = new DataTable();
                adapter.Fill(dt);


                
                
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        Console.Write(dt.Rows[i][j] + "  ");
                    }
                    Console.WriteLine();
                    Console.WriteLine("        Equipment            ");
                    string findequip = "select * from Equipment where eventid='" + dt.Rows[i][0]+"'";
                    adapter = new SqlDataAdapter (findequip, connection);
                    
                    DataTable dataTable1 = new DataTable();
                    adapter.Fill (dataTable1);
                    
                    for (int k = 0; k < dataTable1.Rows.Count; k++)
                    {

                        Console.WriteLine(k + "   " + dataTable1.Rows[k][1]);

                    }

                    string findfood = "select * form fooditemForEvent where Eventid=" + dt.Rows[i][0] + "";
                    adapter = new SqlDataAdapter(findequip, connection);
                    
                    DataTable dataTable2 = new DataTable();
                    adapter.Fill(dataTable2);
                    Console.WriteLine("        food item        ");
                    for (int k = 0; k < dataTable2.Rows.Count; k++)
                    {

                        Console.WriteLine(k + "   " + dataTable2.Rows[k][1]);

                    }
                    Console.WriteLine();
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
        }
        public void BookEvent()
        {
            tryagain:
            Console.WriteLine("Enter the Event Name To Book That Event");
            string eventbook=Console.ReadLine();
            string sql = "select * from EventsManagement where EventName='" + eventbook + "'";
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sql, connection);    
            DataTable dataTable = new DataTable();  
            sqlDataAdapter.Fill(dataTable);
            if (dataTable != null)
            {
                int eventid = Convert.ToInt32(dataTable.Rows[0][0]);
                Console.WriteLine(  " Enter the Date On Which Date You Want to book (mm/dd/yyyy) ");
                string date = Console.ReadLine();
                string status = "pending";
                string bookEvents = "insert into BookEvent values("+userId+","+eventid+",'"+date+"','"+status+"')";
                SqlDataAdapter sqlDataAdapter1=new SqlDataAdapter(bookEvents, connection);
                DataTable dataTable1=new DataTable();
                sqlDataAdapter1.Fill(dataTable1);   

            }
            else
            {
                Console.WriteLine("you Choose Wrong Event Name First Check All Avilable Event");
                goto tryagain;
            }
        }
        public void ShowMyEvent()
        {
            string findmysql = "select * from BookEvent where userId="+userId+"";
            SqlDataAdapter sql=new SqlDataAdapter(findmysql,connection);
            DataTable dataTable=new DataTable();    
            sql.Fill(dataTable);
            for (int k = 0; k < dataTable.Columns.Count; k++)
            {
                Console.Write(dataTable.Columns[k].ColumnName + "  ");
            }
            Console.WriteLine();
            for (int k = 0; k < dataTable.Rows.Count; k++)
            {
                for (int j = 0; j < dataTable.Columns.Count; j++)
                {
                    Console.Write(dataTable.Rows[k][j] + "      ");
                }
                Console.WriteLine();
            }
        }

    }
}
