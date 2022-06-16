using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Collections;



namespace EventManagementSystem
{
    public class Admin
    {
        private static string connectionString=@"Data Source=DESKTOP-AMR2CQS\MSSQLSERVER01;Initial Catalog=EventManagementSystem;Integrated Security=True";
        private  SqlConnection connection=new SqlConnection(connectionString);
        public static int adminId;
        public void AddEvent()
        {
            try
            {
                addmoredata:
                Console.WriteLine("Add the Events --------------------");
                Console.WriteLine("=============================================================================");
                Console.WriteLine("Enter the Event name");
                string eventname = Console.ReadLine();
                Console.WriteLine("Enter the Event Cost  ");
                int EventCost = Convert.ToInt32(Console.ReadLine());
                List<string> equip = new List<string>();
                Console.WriteLine("Enter The All The EquipMent for the Event " + eventname.ToString());
            addmore:
                Console.WriteLine("enter the Equipment name");
                equip.Add(Console.ReadLine());
                Console.WriteLine();
                Console.WriteLine("Do you want to More");
                Console.WriteLine("Press 1");
                int check = Convert.ToInt32(Console.ReadLine());
                if (check == 1)
                {
                    goto addmore;
                }
                List<string> fooditem = new List<string>();
                Console.WriteLine("Enter The All The EquipMent for the Event " + eventname.ToString());
            additem:
                Console.WriteLine("enter the food item name");
                fooditem.Add(Console.ReadLine());
                Console.WriteLine();
                Console.WriteLine("Do you want to More");
                Console.WriteLine("Press 1");
                int check1 = Convert.ToInt32(Console.ReadLine());
                if (check1 == 1)
                {
                    goto additem;
                }

                connection.Open();
                string sql = "insert into EventsManagement values('"+eventname+"',"+EventCost+")";
                SqlCommand cmd = new SqlCommand(sql,connection);    
                cmd.ExecuteNonQuery();
                connection.Close();
                connection.Open();
                string sql2 = "select max(Eventid) from EventsManagement";
                 cmd = new SqlCommand(sql2,connection);
                SqlDataReader reader= cmd.ExecuteReader();
                DataTable dt = new DataTable(); 
                dt.Load(reader);

                int currentId =Convert.ToInt32(dt.Rows[0][0]);
                connection.Close();

                for(int i = 0; i < equip.Count; i++)
                {
                    connection.Open();
                    string sqlequip = "insert into Equipment values("+currentId+",'"+equip[i]+"')";
                    cmd=new SqlCommand(sqlequip,connection);
                    cmd.ExecuteNonQuery();
                    connection.Close(); 
                }
                for (int i = 0; i < fooditem.Count; i++)
                {
                    connection.Open();
                    string food = "insert into fooditemForEvent values("+currentId+",'"+fooditem[i]+"')";
                    cmd = new SqlCommand(food,connection);
                    cmd.ExecuteNonQuery();
                    connection.Close();

                }
                connection.Close();
                Console.WriteLine("added...");
                Console.WriteLine("Are you want to addmore press 1");
                int p=Convert.ToInt32(Console.ReadLine());
                if (p == 1)
                {
                    goto addmoredata;
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                connection.Close(); 
            }

        }
        public void ShowEvent()
        {
            try
            {
                connection.Open();
                string sql = "select * from EventsManagement";
                SqlCommand cmd = new SqlCommand(sql, connection);
                SqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        Console.Write(dt.Rows[i][j] + "  ");
                    }
                    Console.WriteLine();
                    Console.WriteLine("        Equipment            ");
                    string findequip = "select * from Equipment where eventid=" + dt.Rows[i][0]+"";
                    cmd = new SqlCommand(findequip, connection);
                    SqlDataReader reader1 = cmd.ExecuteReader();
                    DataTable dataTable1 = new DataTable();
                    dataTable1.Load(reader1);
                    for (int k = 0; k < dataTable1.Rows.Count; k++)
                    {

                        Console.WriteLine(k + "   " + dataTable1.Rows[k][1]);

                    }

                    string findfood = "select * form fooditemForEvent where Eventid=" + dt.Rows[i][0] + "";
                    Console.WriteLine("        food Item            ");
                    cmd = new SqlCommand(findequip, connection);
                    SqlDataReader reader2 = cmd.ExecuteReader();
                    DataTable dataTable2 = new DataTable();
                    dataTable1.Load(reader2);
                    for (int k = 0; k < dataTable2.Rows.Count; k++)
                    {

                        Console.WriteLine(k + "   " + dataTable2.Rows[k][1]);

                    }
                    Console.WriteLine();
                }
                connection.Close(); 

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                connection.Close ();    
            }

        }
        public void ChangeStatus()
        {
            try
            {
                Console.WriteLine("This Is all Pending Request");
                Console.WriteLine();
                connection.Open();
                string pen = "pending";
                string sql = "select * from BookEvent where Eventstatus='"+pen+"'";
                SqlCommand cmd = new SqlCommand(sql, connection);
                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                DataTable dataTable = new DataTable();
                dataTable.Load(sqlDataReader);
                for (int k = 0; k < dataTable.Columns.Count; k++)
                {
                    Console.Write(dataTable.Columns[k].ColumnName + "     ");
                }
                Console.WriteLine();
                for (int k = 0; k < dataTable.Rows.Count; k++)
                {
                    for (int j = 0; j < dataTable.Columns.Count; j++)
                    {
                        Console.Write(dataTable.Rows[k][j]+"     ");
                    }
                    Console.WriteLine();
                }
                connection.Close();

                Console.WriteLine();
            onemore:
                connection.Open ();
                Console.WriteLine("Enter the Event Id to Approved Their status");
                int id = Convert.ToInt32(Console.ReadLine());
                onetime:
                Console.WriteLine("Enter the reject/Approved");

                string sta = Console.ReadLine();
                sta=sta.Trim(); 
                if(sta.Equals("reject") || sta.Equals("Approved"))
                {
                    
                }
                else
                {
                    Console.WriteLine("choose Approved/reject");
                    goto onetime;
                }
                string sql2 = "update BookEvent set Eventstatus='"+sta+"' where BookId=" + id + "";
                cmd = new SqlCommand(sql2, connection);
                cmd.ExecuteNonQuery();
                Console.WriteLine();
                Console.WriteLine("Do you Want More To Be Approved\n Press 1");
                int oncemore = Convert.ToInt32(Console.ReadLine());
                if (oncemore == 1)
                {
                    goto onemore;
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                connection.Close();
            }







        }
        public void ShowallApproved()
        {
            try
            {
                Console.WriteLine("This Is all Approved Request");
                Console.WriteLine();
                connection.Open();
                string ap = "Approved";
                string sql = "select * from BookEvent where Eventstatus='"+ap+"'";
                SqlCommand cmd = new SqlCommand(sql, connection);
                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                DataTable dataTable = new DataTable();
                dataTable.Load(sqlDataReader);
                for (int k = 0; k < dataTable.Columns.Count; k++)
                {
                    Console.Write(dataTable.Columns[k].ColumnName + "     ");
                }
                Console.WriteLine();
                for (int k = 0; k < dataTable.Rows.Count; k++)
                {
                    for (int j = 0; j < dataTable.Columns.Count; j++)
                    {
                        Console.Write(dataTable.Rows[k][j]+"      ");
                    }
                    Console.WriteLine();
                }
                connection.Close ();


            }
            catch (Exception ex)
            {
                connection.Close(); 
                Console.WriteLine(ex.Message);
            }
        }
        public void ShowAllRejectRequests()
        {
            try
            {
                Console.WriteLine("This Is all rejects Request");
                Console.WriteLine();
                connection.Open();
                string rj = "reject";
                string sql = "select * from BookEvent where Eventstatus='"+rj+"'";
                SqlCommand cmd = new SqlCommand(sql, connection);
                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                DataTable dataTable = new DataTable();
                dataTable.Load(sqlDataReader);
                for (int k = 0; k < dataTable.Columns.Count; k++)
                {
                    Console.Write(dataTable.Columns[k].ColumnName + "     ");
                }
                Console.WriteLine();
                for (int k = 0; k < dataTable.Rows.Count; k++)
                {
                    for (int j = 0; j < dataTable.Columns.Count; j++)
                    {
                        Console.Write(dataTable.Rows[k][j]);
                    }
                    Console.WriteLine();
                }
                connection.Close();


            }
            catch (Exception ex)
            {
                connection.Close();
                Console.WriteLine(ex.Message);
            }
        }

    }
}
