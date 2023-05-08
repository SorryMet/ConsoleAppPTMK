using System;
using System.Configuration;
using System.Data.SqlClient;

namespace ConsoleAppPTMK
{
    internal class Program
    {
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["PersonDB"].ConnectionString;

        private static SqlConnection sqlConnection = null;



        static void AutoInsert1000000(string connectionString)
        {
            Random random = new Random();
            for (int i = 1; i <= 100; i++)
            {
                string fullName = GeneratorForTask4.GenerateFullName(random);
                DateTime dateOfBirth = GeneratorForTask4.GenerateDateWithoutTime(random);
                string gender = GeneratorForTask4.GenerateGender(random);

                using (SqlCommand insert = new SqlCommand(
                    "INSERT INTO PERSON (FullName, DateOfBirth, Gender) VALUES (@FullName, @DateOfBirth, @Gender)",
                    sqlConnection))
                {
                    insert.Parameters.AddWithValue("@FullName", fullName);
                    insert.Parameters.AddWithValue("@DateOfBirth", dateOfBirth);
                    insert.Parameters.AddWithValue("@Gender", gender);
                    insert.ExecuteNonQuery();
                }
            }
            Console.WriteLine("1000000 записей добавлены в таблицу PERSON");
        }
        static void SelectUnique(string connectionString)
        {
            //Не очень понел ,как именно надо вывести , потому что групировать ФИО+дата выдает ошибку
            // Без групировки наверно можно было SELECT DISTINCT FullName, DateOfBirth, Gender, DATEDIFF(year, DateOfBirth, GETDATE()) AS Age FROM Person ORDER BY FullName;
            using (SqlCommand select = new SqlCommand(
                "SELECT FullName, DateOfBirth, Gender, DATEDIFF(year, DateOfBirth, GETDATE()) AS Age " +
                "FROM Person GROUP BY FullName, DateOfBirth, Gender ORDER BY FullName;",
                sqlConnection))
            {
                using (SqlDataReader reader = select.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string fullName = reader.GetString(0);
                        DateTime dateOfBirth = reader.GetDateTime(1).Date;
                        string withoutTimeDate = dateOfBirth.ToString("dd.MM.yyyy");
                        string gender = reader.GetString(2);
                        int age = reader.GetInt32(3);

                        Console.WriteLine("{0} {1} {2} {3}", fullName, withoutTimeDate, gender, age);
                    }
                }
                Console.WriteLine("Вывод произведен");
            }

        }
        static void InsertData(string connectionString, string fullName, string date, string gender)
        {
            using (SqlCommand insert = new SqlCommand(
                "INSERT INTO PERSON(FullName,DateOfBirth,Gender) VALUES (@FullName,@DateOfBirth,@Gender)",
                sqlConnection))
            {
                insert.Parameters.AddWithValue("@FullName", fullName);
                insert.Parameters.AddWithValue("@DateOfBirth", date);
                insert.Parameters.AddWithValue("@Gender", gender);

                insert.ExecuteNonQuery();
                Console.WriteLine("Запись вставлена");
            }
        }
        static void CreateTable(string connectionString)
        {
            using (SqlCommand create = new SqlCommand(
                "CREATE TABLE PERSON(ID INT IDENTITY(1,1)PRIMARY KEY, FullName VARCHAR(1000)," + "DateOfBirth DATE, Gender VARCHAR(20))",
                sqlConnection))
            {
                create.ExecuteNonQuery();
                Console.WriteLine("Таблица создана");
            }
        }
        static void Main(string[] args)
        {
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            switch (args[0])
            {
                case "1":
                    CreateTable(connectionString);
                    break;

                case "2":
                    string fullName = args[1];
                    string date = args[2];
                    string gender = args[3];
                    InsertData(connectionString, fullName, date, gender);
                    break;

                case "3":
                    SelectUnique(connectionString);
                    Console.ReadLine();
                    break;

                case "4":
                    AutoInsert1000000(connectionString);
                    Console.ReadLine();
                    break;
            }
        }
    }
}
