using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Security.Principal;
using static System.Net.Mime.MediaTypeNames;

class Program
{
    static void Main()
    {
        Dictionary<int, string> idandname = new Dictionary<int, string>();
        Queue<string> request = new Queue<string>();
        Stack<string> tickets = new Stack<string>();
        HashSet<string> studentname = new HashSet<string>();
        List<string> error = new List<string>();

        error.Add("Login Issue");
        error.Add("Password Reset");
        error.Add("Course Access");

        while (true)
        {
            Console.WriteLine("1. Add Student");
            Console.WriteLine("2. Raise Help Request");
            Console.WriteLine("3. View Next Ticket");
            Console.WriteLine("4. Resolve Ticket");
            Console.WriteLine("5. Show All Students");
            Console.WriteLine("6. Show Resolved Tickets");
            Console.WriteLine("7. Exit");
            int choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.WriteLine("Enter StudentID");
                    int studentid = Convert.ToInt32(Console.ReadLine());
                    if (idandname.ContainsKey(studentid))
                    {
                        Console.WriteLine("This id already exists. Try a different one.");
                        break;
                    }

                    Console.WriteLine("Enter Student Name");
                    string name = Console.ReadLine();

                    if (studentname.Contains(name))
                    {
                        Console.WriteLine("❌ This student is already registered.");
                        break;
                    }

                    idandname.Add(studentid, name);
                    studentname.Add(name);
                    Console.WriteLine("✅ Student added successfully!");
                    break;

                case 2:
                    Console.WriteLine("Select an issue to raise");
                    for (int i = 0; i < error.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {error[i]}");
                    }
                    Console.WriteLine("Enter issue number");
                    int issuechoice = Convert.ToInt32(Console.ReadLine());

                    if (issuechoice < 1 || issuechoice > error.Count)
                    {
                        Console.WriteLine("Invalid issue selected");
                        break;
                    }
                    Console.WriteLine("Enter your Student ID");
                    int sid = Convert.ToInt32(Console.ReadLine());

                    if (!idandname.ContainsKey(sid))
                    {
                        Console.WriteLine("❌ Student not found. Please register first.");
                        break;
                    }
                    string studentName = idandname[sid];
                    string issuetype = error[issuechoice - 1];
                    string ticket = $"{studentName} - {issuetype}";

                    request.Enqueue(ticket);
                    tickets.Push(ticket);

                    Console.WriteLine("✅ Help request raised successfully!");
                    break;

                case 3:
                    if (request.Count == 0)
                    {
                        Console.WriteLine("❌ No tickets in the queue.");
                        break;
                    }
                    string nextticket = request.Peek();
                    Console.WriteLine("Next ticket: " + nextticket);
                    break;

                case 4:
                    if (request.Count == 0)
                    {
                        Console.WriteLine("❌ No tickets to resolve.");
                        break;
                    }
                    string resolvedfirst = request.Dequeue();
                    tickets.Push(resolvedfirst);

                    Console.WriteLine("✅ Ticket resolved: " + resolvedfirst);
                    break;

                case 5:
                    Console.WriteLine("All Registered Students");
                    foreach (var student in idandname)
                    {
                        Console.WriteLine($"Student ID : {student.Key}, Name: {student.Value}");
                    }
                    break;

                case 6:
                    Console.WriteLine("Resolved Tickets");
                    foreach (var resolvedticket in tickets)
                    {
                        Console.WriteLine(resolvedticket);
                    }
                    break;

                case 7:
                    Console.WriteLine("Exiting....... Goodbye!");
                    return;
            }
        }
    }
}
