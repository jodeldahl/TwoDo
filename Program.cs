using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using TwoDo.Domain;

namespace TwoDo
{
    class Program
    {

        static string connectionString = "Server=.;Database=TwoDo;Trusted_Connection=True";


        static void Main(string[] args)
        {
            Console.Title = "TwoDo";

            Console.CursorVisible = false;

            bool applicationRunning = true;

            do
            {
                Console.WriteLine("1. Add task");

                Console.WriteLine("2. List tasks");

                Console.WriteLine("3. Exit");

                ConsoleKeyInfo input = Console.ReadKey(true);

                Console.Clear();

                switch (input.Key)
                {
                    case ConsoleKey.D1:

                        AddTwoDo();

                        break;


                    case ConsoleKey.D2:

                        PrintToTasksToConsole(ListTwoDo());

                        break;


                    case ConsoleKey.D3:

                        applicationRunning = false;

                        break;

                }

                Console.Clear();

            } while (applicationRunning);
        }

        private static void PrintToTasksToConsole(List<TwoDoTask> taskList)
        {
            Console.WriteLine($"{"TaskId",-10}{"Task Two Do",-20}{"Due date",-20}");

            if (taskList != null)
            {
                foreach (var task in taskList)
                {
                    Console.WriteLine($"{task.Id,-10}{task.TaskName,-20}{task.DueDate,-20}");
                }
            }
            else
            {
                Console.WriteLine("Nothing has been added yet or there is a problem!");
            }

            Console.ReadKey();

        }

        private static List<TwoDoTask> ListTwoDo()
        {
            var sqlCode = @"
                            SELECT *
                            FROM TwoDo
                            ";



            List<TwoDoTask> twoDoTaskList = new List<TwoDoTask>();

            using SqlConnection connection = new SqlConnection(connectionString);
            using SqlCommand command = new SqlCommand(sqlCode, connection);

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    
                    TwoDoTask twoDoTask = new TwoDoTask((int)reader["Id"], (string)reader["TaskName"], (DateTime)reader["DueDate"]);

                    twoDoTaskList.Add(twoDoTask);

                    twoDoTask = null;
                }

            connection.Close();

            return twoDoTaskList;

        }

        private static void AddTwoDo()
        {
            Console.CursorVisible = true;

            Console.Clear();

            Console.Write("Name: ");

            string taskName = Console.ReadLine();

            Console.Write("Due date: ");

            DateTime dueDate = DateTime.ParseExact(Console.ReadLine(), "d", CultureInfo.CurrentCulture);

            Console.CursorVisible = false;

            TwoDoTask twoDoTask = new TwoDoTask(taskName, dueDate);

            if (AddTwoDoTaskToDatabase(twoDoTask))
            {
                Console.Clear();

                Console.WriteLine("Task has been added");

                Thread.Sleep(2000);
            }
            else
            {
                Console.WriteLine("Error when saving");
            }
        }

        private static bool AddTwoDoTaskToDatabase(TwoDoTask twoDoTask)
        {
            int greatSuccess = 0;

            var sqlCode = @"
                            INSERT INTO TwoDo
                            (TaskName, DueDate) 
                            VALUES (@taskName, @dueDate)
                            ";

            using SqlConnection connection = new SqlConnection(connectionString);
            using SqlCommand command = new SqlCommand(sqlCode, connection);

            command.Parameters.AddWithValue("taskName", twoDoTask.TaskName);

            command.Parameters.AddWithValue("dueDate", twoDoTask.DueDate);

            connection.Open();

            greatSuccess = command.ExecuteNonQuery();

            connection.Close();

            if (greatSuccess == 0)
            {
                return true;
            }

            else
            {
                return false;
            }


        }
    }
}
