using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace StudentDBLayer
{
    public class Student : IStudent
    {
        //Global initialization
        private readonly MySqlConnection sqlConnection;
        private MySqlCommand sqlCommand;
        readonly string connectionString = ConfigurationManager.ConnectionStrings["mysqlConnection"].ConnectionString;        
        public string sqlQuery = "";
        public Student()
        {
            sqlConnection = new MySqlConnection(connectionString);
        }
        public int SaveStudent(string studentNumber, string firstName, string lastName, string collegeName)
        {
            int successcode = 0;
            try
            {                
                sqlQuery = "SELECT 1 FROM Student WHERE StudentNumber=" + studentNumber;
                sqlConnection.Open();
                sqlCommand = new MySqlCommand(sqlQuery, sqlConnection);
                MySqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                if (!sqlDataReader.HasRows)
                {
                    sqlDataReader.Close();
                    sqlQuery = "INSERT INTO Student (StudentNumber, FirstName, LastName, CollegeName) ";
                    sqlQuery += "VALUES('" + studentNumber + "', '" + firstName + "', '" + lastName + "', '" + collegeName + "')";
                    sqlCommand = new MySqlCommand(sqlQuery, sqlConnection);
                    sqlCommand.ExecuteNonQuery();
                    successcode = 1;
                }
                else
                {
                    sqlDataReader.Close();
                    sqlQuery = "Update Student SET FirstName='" + firstName + "', LastName='" + lastName + "', CollegeName='" + collegeName + "' WHERE  StudentNumber='" + studentNumber + "'";                    
                    sqlCommand = new MySqlCommand(sqlQuery, sqlConnection);
                    sqlCommand.ExecuteNonQuery();
                    successcode = 2;
                }
                sqlConnection.Close();
            }
            catch(Exception ex)
            {
                if (sqlConnection.State == ConnectionState.Open) sqlConnection.Close();
            }
            return successcode;
        }

        public DataTable GetStudent(string StudentNumber)
        {
            DataTable dataTable = new DataTable();
            try
            {
                sqlQuery = "SELECT * FROM Student WHERE StudentNumber='" + StudentNumber + "';";

                sqlConnection.Open();
                sqlCommand = new MySqlCommand(sqlQuery, sqlConnection);
                sqlCommand.CommandType = CommandType.Text;
                MySqlDataAdapter sda = new MySqlDataAdapter(sqlCommand);
                sda.Fill(dataTable);
                sqlConnection.Close();
            }
            catch(Exception ex)
            {
                if (sqlConnection.State == ConnectionState.Open) sqlConnection.Close();
            }
            return dataTable;
        }
    }
}

