using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using TwoDo.Data;
using TwoDo.Domain;
using Microsoft.EntityFrameworkCore;

namespace TwoDo
{
    class Program
    {
        static TwoDoContext context = new TwoDoContext();

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

            Console.WriteLine("--------------------------------------------------------------");

            if (taskList != null)
            {
                foreach (var task in taskList)
                {
                    Console.WriteLine($"{task.Id,-10}{task.TaskName,-20}{task.DueDate,-20}");
                }

                Console.WriteLine();

                Console.WriteLine("Press any key to return to Main menu");

            }
            else
            {
                Console.Clear();

                Console.WriteLine("Nothing has been added yet or there is a problem!");

                Console.WriteLine("Press any key to continue");
            }

            Console.ReadKey(true);

        }

        private static List<TwoDoTask> ListTwoDo()
        {
            List<TwoDoTask> twoDoTasks = new List<TwoDoTask>(context.TwoDoTask);

            return twoDoTasks;
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
            context.TwoDoTask.Add(twoDoTask);

            int greatSuccess = 0;

            greatSuccess = context.SaveChanges();

            if (greatSuccess == 1)
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
