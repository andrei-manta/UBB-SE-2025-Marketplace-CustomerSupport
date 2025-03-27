using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;


namespace ClientChat.Data
{
    /*
       To get connection string from sql managment system

    !!! To select a specific database look at "initial catalog" property
     
     select
    'Integrated Security=True;TrustServerCertificate=True;data source=' + @@servername +
    ';initial catalog=' + db_name() +
    case type_desc
        when 'WINDOWS_LOGIN' 
            then ';trusted_connection=true'
        else
            ';user id=' + suser_name() + ';password=<<YourPassword>>'
    end
    as ConnectionString
    from sys.server_principals
    where name = suser_name() 
     
     */



    /*
    Simple script to initialize the database
     
    DROP TABLE Conversations;
    DROP TABLE  Messages;

    SELECT * FROM Conversations;
    SELECT * FROM Messages;

    CREATE TABLE Conversations (
        id INT PRIMARY KEY IDENTITY(1,1),
        user1 INT NOT NULL,
        user2 INT NOT NULL
    );

    CREATE TABLE Messages (
        id INT PRIMARY KEY IDENTITY(1,1),
        conversationId INT NOT NULL,
	    creator INT NOT NULL,
	    timestamp BIGINT NOT NULL,
        contentType NVARCHAR(50) NOT NULL,
        content NVARCHAR(MAX) NOT NULL,
    );
     
     */
    public class Database
    {
        public static Database database;

        private string connectionString;
        private SqlConnection databaseConnection;

        public Database(string connString)
        {
            connectionString = connString;
        }

        public void Close()
        {
            try
            {
                this.databaseConnection.Close();
                this.databaseConnection.Dispose();
            }
            catch
            {

            }
        }

        public bool Connect()
        {
            databaseConnection = new SqlConnection(connectionString);

            try
            {
                databaseConnection.Open();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
                return false;
            }


            return true;
        }

        public List<Dictionary<string, object>> Get(string query)
        {
            return Get(query, [], []);
        }

        public List<Dictionary<string, object>> Get(string query, string[] args, object[] values)
        {
            List<Dictionary<string, object>> resultList = new List<Dictionary<string, object>>();

            SqlCommand command = new SqlCommand(query, databaseConnection);
            for (int i = 0; i < args.Length; i++)
            {
                command.Parameters.AddWithValue(args[i], values[i]);
            }

            using (SqlDataReader reader = command.ExecuteReader())
            {
                var schemaTable = reader.GetSchemaTable();
                List<string> columnNames = new List<string>();
                foreach (DataRow row in schemaTable.Rows)
                {
                    columnNames.Add(row["ColumnName"].ToString());
                }

                while (reader.Read())
                {
                    Dictionary<string, object> row = new Dictionary<string, object>();

                    foreach (string columnName in columnNames)
                    {
                        int ordinal = reader.GetOrdinal(columnName);
                        if (!reader.IsDBNull(ordinal))
                        {
                            row[columnName] = reader.GetValue(ordinal);
                        }
                        else
                        {
                            row[columnName] = null;
                        }
                    }

                    resultList.Add(row);
                }
            }

            return resultList;
        }





        public int Execute(string query)
        {
            return Execute(query, [], []);
        }

        public int Execute(string query, string[] args, object[] values)
        {

            SqlCommand command = new SqlCommand(query, databaseConnection);
            for (int i = 0; i < args.Length; i++)
            {
                command.Parameters.AddWithValue(args[i], values[i]);
            }

            int affected = command.ExecuteNonQuery();
            return affected;
        }




        public List<T> ConvertToObject<T>(List<Dictionary<string, object>> dictList) where T : new()
        {
            List<T> result = new List<T>();

            foreach (var dict in dictList)
            {
                T obj = new T();
                Type type = typeof(T);


                FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);

                foreach (var field in fields)
                {
                    if (dict.ContainsKey(field.Name))
                    {

                        if (dict[field.Name] != null)
                        {

                            try
                            {
                                var value = Convert.ChangeType(dict[field.Name], field.FieldType);
                                field.SetValue(obj, value);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error converting {field.Name}: {ex.Message}");
                            }
                        }
                    }
                }

                result.Add(obj);
            }

            return result;
        }



        public T ConvertToObject<T>(Dictionary<string, object> dict) where T : new()
        {

            T obj = new T();
            Type type = typeof(T);


            FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);

            foreach (var field in fields)
            {
                if (dict.ContainsKey(field.Name))
                {

                    if (dict[field.Name] != null)
                    {

                        try
                        {
                            var value = Convert.ChangeType(dict[field.Name], field.FieldType);
                            field.SetValue(obj, value);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error converting {field.Name}: {ex.Message}");
                        }
                    }
                }
            }

            return obj;
        }



    }

}
