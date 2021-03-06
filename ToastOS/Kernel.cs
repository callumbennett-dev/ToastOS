﻿/*
ToastOS Source Code
Made by Callum Bennett (callumbennett-dev on GitHub)
Made using COSMOS C#
Project from March 2021
*/

using System;
using Cosmos.HAL;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;

namespace ToastOS
{
    class Global
    {
        public static int adminState = 0;
    }
    public class Kernel : Sys.Kernel
    {

        //Variables
        public int adminState = 0;

        protected override void BeforeRun()
        {
            Console.Clear();
            Console.WriteLine("ToastOS User Console");
            Global.adminState = 0;
        }

        protected override void Run()
        {
            Console.Write("> ");
            string input = Console.ReadLine();
            //To figure out what to call
            switch (input)
            {
                case "about":
                    about();
                    break;
                case "admin":
                    adminLogin(0);
                    break;
                case "clear":
                    clear();
                    break;
                    break;
                case "calculator":
                    calculator(0);
                    break;
                case "calculator 2":
                    calculator(1);
                    break;
                case "circle area":
                    area(0);
                    break;
                case "rect area":
                    area(1);
                    break;
                case "triangle area":
                    area(2);
                    break;
                case "logout":
                    adminLogin(2);
                    break;
                default:
                    oops();
                    break;
            }
        }

        private static void oops() //This will trigger when a command not listed is entered
        {
            Console.WriteLine("Bad command, try again");
        }

        private static void about() //Tells the user about the System
        {
            Console.WriteLine("ToastOS 0.8");
            Console.WriteLine("Developed by Callum Bennett");
            Console.WriteLine("Software made using COSMOS C# user kit");
        }

        public static void adminLogin(int called) //This will tell if the user is logged in as an admin or not
        {
            //Adminstate
            //0 = not signed in 
            //1 = Signed in

            //called
            //0 = start login
            //1 = check admin state
            //2 = logout
            if (called == 0)
            {
                //Login
                Console.WriteLine();
                Console.WriteLine("username: ");
                string user = Console.ReadLine();
                Console.WriteLine("password: ");
                string pass = Console.ReadLine();
                if (user == "administrator")
                {
                    if (pass == "administrator") //Set as another global variable, maybe add method to change this value later
                    {
                        Global.adminState = 1;
                        Console.WriteLine("Logon Successful");
                    }
                } else
                {
                    Console.WriteLine("login unsuccessful");
                }

            } else if (called == 1)
            {
                //Check if the user is admin or not for the Clear Command
                if (Global.adminState == 0)
                {
                    //Not signed in 
                    Console.WriteLine("ToastOS User Console");
                } else if (Global.adminState == 1)
                {
                    //Signed in
                    Console.WriteLine("ToastOS Administrator Console"); 
                }
            } else if (called == 2)
            {
                Global.adminState = 0;
                clear();
            }
        }

        private static void clear() //Clears the Console and calls adminLogin with an input value of 1
        {
            Console.Clear();
            adminLogin(1);
        }

        private static void calculator(int input)
        {
            if (input == 0) //For two numbers with +, -, * and /
            {
                int result = 0;
                Console.WriteLine();
                Console.Write("Enter the equation > ");
                var numInput = Console.ReadLine();
                var a = numInput.Split(' ');
                int v0 = int.Parse(a[0]);
                int v1 = int.Parse(a[2]);
                if (a[1].Contains("+"))
                {
                    result = v0 + v1;
                } else if (a[1].Contains("-"))
                {
                    result = v0 - v1;
                } else if (a[1].Contains("*"))
                {
                    result = v0 * v1;
                } else if (a[1].Contains("/"))
                {
                    result = v0 / v1;
                } else
                {
                    Console.WriteLine("Please format as below");
                    Console.WriteLine("5 + 4");
                }
                Console.WriteLine(result);
            } else if (input == 1)//This will be for 3 numbers, with order of operations
            {
                int result1 = 0;
                int result2 = 0;
                Console.WriteLine();
                Console.Write("Enter the equation > ");
                var numInput2 = Console.ReadLine();
                var a = numInput2.Split();
                int x0 = int.Parse(a[0]);
                int x1 = int.Parse(a[2]);
                int x2 = int.Parse(a[4]);
                //This is long, but covers order of operations
                if (a[1].Contains("+"))
                {
                    if (a[3].Contains("*"))
                    {
                        result1 = x1 * x2;
                        result2 = x0 + result1;
                    } else if (a[3].Contains("/"))
                    {
                        result1 = x1 / x2;
                        result2 = x0 + result1;
                    } else if (a[3].Contains("+"))
                    {
                        result1 = x1 + x2;
                        result2 = x0 + result1;
                    } else if (a[3].Contains("-"))
                    {
                        result1 = x0 + x1;
                        result2 = result1 - x2;
                    }
                } else if (a[1].Contains("-"))
                {
                    if (a[3].Contains("*"))
                    {
                        result1 = x1 * x2;
                        result2 = x0 - result1;
                    } else if (a[3].Contains("/"))
                    {
                        result1 = x1 / x2;
                        result2 = x0 - result1;
                    } else if (a[3].Contains("+"))
                    {
                        result1 = x0 + x1;
                        result2 = result1 - x2; 
                    } else if (a[3].Contains("-"))
                    {
                        result1 = x0 - x1;
                        result2 = result1 - x2;
                    }
                } else if (a[1].Contains("*"))
                {
                    result1 = x0 * x1;
                    if (a[3].Contains("*"))
                    {
                        result2 = result1 * x2;
                    } else if (a[3].Contains("/"))
                    {
                        result2 = result1 / x2;
                    } else if (a[3].Contains("+"))
                    {
                        result2 = result1 + x2;
                    } else if (a[3].Contains("-"))
                    {
                        result2 = result1 - x2;
                    }
                } else if (a[1].Contains("/"))
                {
                    result1 = x0 / x1;
                    if (a[3].Contains("*"))
                    {
                        result2 = result1 * x2;
                    } else if (a[3].Contains("/"))
                    {
                        result2 = result1 / x2;
                    } else if (a[3].Contains("+"))
                    {
                        result2 = result1 + x2;
                    } else if (a[3].Contains("-"))
                    {
                        result2 = result1 - x2;
                    }
                }
                Console.WriteLine(result2);
            }
        }

        private static void area(int switchValue) //Area calculator for circles, triangles, and rectangles
        {
            //Circle is switchVale 0
            //Rect is switchValue 1
            //Triangle is switchValue 2
            if (switchValue == 0) //Circle Area
            {
                Console.Write("Enter the radius of the circle: ");
                string strInput = Console.ReadLine();
                double radius = Convert.ToInt32(strInput);
                double rSquared = Math.Pow(radius, 2);
                double circleArea = (Math.PI * rSquared);
                Console.WriteLine(circleArea);
            } else if (switchValue == 1) //Rect Area
            {
                Console.Write("Enter the width: ");
                string strWidth = Console.ReadLine();
                Console.Write("\nEnter the length: ");
                string strLength = Console.ReadLine();
                double width = Convert.ToInt32(strWidth);
                double length = Convert.ToInt32(strLength);
                double area = width * length;
                Console.WriteLine(area);
            } else if (switchValue == 2) //Triangle Area
            {
                Console.Write("Enter the width: ");
                string strWidth = Console.ReadLine();
                Console.Write("\nEnter the height: ");
                string strLength = Console.ReadLine();
                double width = Convert.ToInt32(strWidth);
                double length = Convert.ToInt32(strLength);
                double area = width * length;
                Console.WriteLine(area / 2);
            }
        }
    }
}