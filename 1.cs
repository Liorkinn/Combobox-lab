using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Data.Common;
using System.Data;

namespace ZarplataLab
{
    class database
    {
        

        MySqlConnection Connection;
        MySqlConnectionStringBuilder Connect = new MySqlConnectionStringBuilder();

        public database(string server, string user, string pass, string database)
        {
            Connect.Server = server;
            Connect.UserID = user;
            Connect.Password = pass;
            Connect.Port = 3306;
            Connect.Database = database;
            Connect.CharacterSet = "utf8";
            Connection = new MySqlConnection(Connect.ConnectionString);
        }

        public DataTable getTableInfo(string query)
        {
            Connection.Open();          
            MySqlCommand queryExecute = new MySqlCommand(query, Connection);
            DataTable ass = new DataTable();
            ass.Load(queryExecute.ExecuteReader());
            Connection.Close();
            return ass;

        }
        public long download(string name, string surname, string otch)
        {

            MySqlCommand command = Connection.CreateCommand();
            command.CommandText = "INSERT INTO info(name, surname, otch) VALUES(?name, ?surname, ?otch)";
            command.Parameters.Add("?name", MySqlDbType.VarChar).Value = name;
            command.Parameters.Add("?surname", MySqlDbType.VarChar).Value = surname;
            command.Parameters.Add("?otch", MySqlDbType.VarChar).Value = otch;
            try
            {
                Connection.Open();
                command.ExecuteNonQuery();
                Form1 form = new Form1();
                form.zagruzka();
                return command.LastInsertedId;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Connection.Close();
            }
            return -1;
        }

        public long download2(string status, string money, int fio_id)
        {

            MySqlCommand command = Connection.CreateCommand();
            command.CommandText = "INSERT INTO `zarplatalab`.`zarplata`(status, money, fio_id) VALUES(?status, ?money, ?fio_id)";

            command.Parameters.Add("?status", MySqlDbType.VarChar).Value = status;
            command.Parameters.Add("?money", MySqlDbType.VarChar).Value = money;
            command.Parameters.Add("?fio_id", MySqlDbType.Int32).Value = fio_id;
            try
            {
                Connection.Open();
                command.ExecuteNonQuery();
                Form1 form = new Form1();
                form.zagruzka();
                return command.LastInsertedId;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Connection.Close();
            }
            return -1;
        }


        public long delete(int id)
        {          
            MySqlCommand command = Connection.CreateCommand();
            command.CommandText = "DELETE FROM info where id = ?id";
            command.Parameters.Add("?id", MySqlDbType.Int32).Value = id;          
            try
            {
                Connection.Open();
                command.ExecuteNonQuery();
                Form1 form = new Form1();
                form.zagruzka();
                return command.LastInsertedId;

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {
                Connection.Close();
            }
            return -1;

        }
    }
}
