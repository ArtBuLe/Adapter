using Npgsql;
using System.Data;
using System.Data.Common;

namespace dot.net.Lesson2
{
    public class Result
    {
        const string connString =
                "Host=wotacoma.ru;" +
                "Port=5090;" +
                "Username=students;" +
                "Password=asd123qwe4ewq321dsa;" +
                "Database=9/3-RPO-24/1";
        static DbProviderFactory Factory = NpgsqlFactory.Instance;

        static void Add()
        {
            using var conn = Factory.CreateConnection();
            conn.ConnectionString = connString;
            conn.Open();

            Console.WriteLine("Write your nick : ");
            string nick = Console.ReadLine(); 
            Console.WriteLine("Write your first name : ");
            string fname = Console.ReadLine();
            Console.WriteLine("Write your last name : ");
            string lname = Console.ReadLine();


            using var command = conn.CreateCommand();
            command.CommandText =
                """
                INSERT INTO students_lesson13
                (nickname, first_name, last_name)
                VALUES ($1, $2, $3)
                """;
            var paramNick = command.CreateParameter();
            paramNick.Value = nick;
            command.Parameters.Add(paramNick); 

            var paramFName = command.CreateParameter();
            paramFName.Value = fname;
            command.Parameters.Add(paramFName);

            var paramLName = command.CreateParameter();
            paramLName.Value = lname;
            command.Parameters.Add(paramLName);
        }
        
        static void AddNew()
        {
            using var conn = Factory.CreateConnection();
            conn.ConnectionString = connString;
            conn.Open();

            Console.WriteLine("Write your nick : ");
            string nick = Console.ReadLine();
            Console.WriteLine("Write your first name : ");
            string fname = Console.ReadLine();
            Console.WriteLine("Write your last name : ");
            string lname = Console.ReadLine();

            using var command = conn.CreateCommand();
            command.CommandText =
                """
                INSERT INTO students_lesson13
                (nickname, first_name, last_name)
                VALUES ($1, $2, $3)
                """;
            command.Connection = conn;

            var paramNick = command.CreateParameter();
            paramNick.Value = nick;
            command.Parameters.Add(paramNick);

            var paramFName = command.CreateParameter();
            paramFName.Value = fname;
            command.Parameters.Add(paramFName);

            var paramLName = command.CreateParameter();
            paramLName.Value = lname;
            command.Parameters.Add(paramLName);

            command.ExecuteNonQuery();
            }

        static void GetAll()
        {
            using var conn = Factory.CreateConnection();
            conn.ConnectionString = connString;
            conn.Open();

            using var command = conn.CreateCommand();
            command.CommandText =
                """
                SELECT * FROM students_lesson13
                """;

            using var adapter = Factory.CreateDataAdapter();
            adapter.SelectCommand = command;

            DataTable table = new DataTable();
            adapter.Fill(table);

            foreach (DataRow row in table.Rows)
            {
                Console.WriteLine
                    (
                    $"#{row["Id"],-3} - {row["nickname"],-20} - {row["first_name"],-20} - {row["last_name"]}"
                    );
            }
        }

        static void Main()
        {
            while (true)
            {
                Console.WriteLine
                    (
                    """
                    Выберите действие :
                    1 - Добавить студента
                    2 - Получить и вывести всех
                    """
                    );
                var choose = Convert.ToInt32(Console.ReadLine());
                switch(choose)
                {
                    case 1: 
                        AddNew();
                        break;
                    case 2:
                        GetAll();
                        break;
                }

                Console.WriteLine("\nНажмите Enter для продолжения...");
                Console.ReadLine();
                Console.Clear();
            }
        }
    }
}