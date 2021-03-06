﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ListsTask
{
    public class Node
    {
        public Node next { get; set; }
        public Node previous { get; set; }
        public int value { get; set; }

        public Node()//constructor makes sure next and previous are null on creation
        {
            //decided not to take value as a parameter instead will assign the value manually in a loop in Main
            next = null; previous = null;
        }//end of Constructor
    }//end of Node class
    class DoublyLinkedList
    {
        public Node head { get; set; }
        public Node tail { get; set; }
        public int size = 0;//keeps track of the list size

        public DoublyLinkedList()//default constructor
        {

        }//end of default constructor

        public void AddToTail(Node temp)//Create a temp node that holds pointers while transitioning the tail
        {
            if (size == 0)
            {
                tail = temp;
                head = temp;
            }
            else
            {
                tail.next = temp;
                temp.previous = tail;
                tail = temp;
            }
            size++;
        }//end of AddToTail

        public void DisplaySize()//size is a instance variable which increments when adding to list
        {
            Console.WriteLine("\t\tDisplay size:");
            Console.WriteLine("There are {0} items in the list", size);
        }//end of DisplaySize

        public void FindMiddleElement()//concept is to have two nodes one incrementing by 2 and the other incrementing by 1 which will be at halfway mark when other is at end of list
        {
            //variables
            int n = 0;
            Node tempOne = head;
            Node tempTwo = head;

            //task message
            Console.WriteLine("\t\tFind middle element:");
            if (size <= 2)
            {
                Console.WriteLine("List has a size of 2 or less, no middle value");
            }
            else
            {
                try
                {
                    while (tempTwo != null)
                    {
                        //increment the objects tempTwo goes up two leaving tempOne at half of the list
                        tempOne = tempOne.next;
                        tempTwo = tempTwo.next.next;
                        //increment counter to see which element is the middle
                        n++;
                    }
                    Console.WriteLine("The middle of the list is element: [{0}] Element [{1}] value is: {2}", n, n, tempOne.previous.value);//using .previous to point to the correct object of (n)counter
                }
                catch (Exception)//error checks if our next.next pointer goes too far for odd list size and object has no instance
                {
                    //increment counter properly. last iteration of the loop errors when size is uneven which cancels out the last counter increment
                    n++;
                    //use .previous pointer to point to null pointer instead of no instance.
                    //this takes us to tail.next which == null
                    tempTwo = tempTwo.previous;
                    //output to console
                    Console.WriteLine("The middle of the list is element: [{0}] Element [{1}] value is: {2}", n, n, tempOne.previous.value);
                }
            }
            return;
        }//end of FindMiddleElement

        public void DisplayPrimes()
        {
            //variables
            int counter = 0;
            Node temp = head;
            int n;
            int trigger;

            //task message
            Console.WriteLine("\t\tDisplay prime numbers:");
            while (temp != null)//loop through each node
            {
                //reset conditions
                trigger = 1;
                n = temp.value;

                //All the conditions for if it is not a prime number
                //Cancel out 1 and 0
                if (n < 2)
                {
                    trigger = 0;
                }
                //cancel all even numbers except 2
                else if (n % 2 == 0 && n != 2)
                {
                    trigger = 0;
                }
                //Loop condition is i*i <= n (Square root) 
                //a more efficient way can be to calculate square root outside the loop
                else
                {
                    for (int i = 2; i * i <= n; i++)
                    {
                        if (n % i == 0)
                        {
                            trigger = 0;
                            break;//break the loop. logic only needs to fail once
                        }
                    }
                }
                //If it is a prime number
                if (trigger == 1)
                {
                    counter++;
                    Console.Write("{0} ", n);
                    if(counter == 5)
                    {
                        counter = 0;
                        Console.WriteLine();
                    }
                }
                temp = temp.next;
            }
            Console.WriteLine();
        }//end of FindPrimes

        public void DisplayListReverse()//start from tail and go to head
        {
            //variables
            int count = size;
            Node temp = tail;

            Console.WriteLine("\t\tDisplay reversed list:");
            while (temp != null)
            {
                Console.WriteLine("Element:[{0}] value is {1}", count, temp.value);
                temp = temp.previous;
                count--;
            }
        }//end of DisplayListReverse

        public int[] GatherValues()//converts the list to an array for calculating the common values
        {
            //array to keep track of integers
            int[] a = new int[size];

            Node temp = head;
            for(int i=0;i<size-1;i++)
            {
                a[i] = temp.value;
                temp = temp.next;
            }
            return a;
        }//end of DisplayValues

        public bool FindValue(int num)//used to find all the common values between multiple lists
        {
            bool b = false;
            Node temp = head;
            while (temp != null)
            {
                if (temp.value == num)
                {
                    b = true;
                }
                temp = temp.next;
            }
            return b;
        }//end of FindValue
    }//end of DoublyLinkedList class
    class Program
    {
        static void Main(string[] args)
        {
            //variables
            DoublyLinkedList listOne;
            DoublyLinkedList listTwo;
            DoublyLinkedList listThree;
            string file;

            //Create 3 lists
            Console.WriteLine("Enter file path 1:");
            file = Console.ReadLine();
            listOne = CreateLists(file);
            Console.WriteLine("Enter file path 2:");
            file = Console.ReadLine();
            listTwo = CreateLists(file);
            Console.WriteLine("Enter file path 3:");
            file = Console.ReadLine();
            listThree = CreateLists(file);

            //display all list info
            Console.Clear();
            Console.WriteLine("\t\t[List 1]");
            DisplayListInfo(listOne);
            Console.WriteLine("\t\t[List 2]");
            DisplayListInfo(listTwo);
            Console.WriteLine("\t\t[List 3]");
            DisplayListInfo(listThree);

            //display all list values that are the same
            int[] aOne = listOne.GatherValues();
            int[] aTwo = listTwo.GatherValues();
            int[] aThree = listThree.GatherValues();

            Console.WriteLine("All common integers in the lists");
            switch (CalculateLength(aOne,aTwo,aThree))//
            {
                case 1:
                    DisplayValues(aOne, listTwo, listThree);
                    break;
                case 2:
                    DisplayValues(aTwo, listOne, listThree);
                    break;
                case 3:
                    DisplayValues(aThree, listOne, listTwo);
                    break;
                default:
                    break;
            }
            Console.WriteLine("Press enter to exit...");
            Console.ReadLine();
            return;
        }//end of Main

        public static DoublyLinkedList CreateLists(string file)//reads integers to a temp node and adds it to the tail of list
        {
            //variables
            DoublyLinkedList list = new DoublyLinkedList();
            Node temp;
            int num;

            //read text file
            string n = System.IO.File.ReadAllText(file);
            string[] lines = n.Split(',');
            //add each number from text file to a node value
            for(int i= 0;i<lines.Length;i++)
            {
                temp = new Node();
                num = Int32.Parse(lines[i]);
                temp.value = num;
                list.AddToTail(temp);
            }
            //return the list
            return list;
        }//end of CreateLists

        public static void DisplayListInfo(DoublyLinkedList list)//takes a list as parameter and displays the info of the list
        {
            //spacer
            Console.WriteLine();
            //output list size to console
            list.DisplaySize();
            Console.WriteLine();
            //find the middle element
            list.FindMiddleElement();
            Console.WriteLine();
            //display prime numbers
            list.DisplayPrimes();
            Console.WriteLine();
            //display list elements from tail to head
            list.DisplayListReverse();
            Console.WriteLine();
        }//end of DisplayListInfo

        public static int CalculateLength(int[] a, int[] a2, int[] a3)//calculate which array has the most elements this allows me to display the common integers in one sweep
        {
            int leastElements = 0;
            if(a.Length > a2.Length && a.Length > a3.Length)
            {
                leastElements = 1;
            }
            else if(a2.Length > a.Length && a2.Length > a3.Length)
            {
                leastElements = 2;
            }
            else
            {
                leastElements = 3;
            }
            return leastElements;
        }//end of CalculateLength

        public static void DisplayValues(int[] a, DoublyLinkedList list, DoublyLinkedList list2)//display same values in all three lists using the biggest list as reference
        {
            for(int i=0;i<a.Length;i++)
            {
                if(list.FindValue(a[i]) && list2.FindValue(a[i]))
                {
                    Console.Write("{0},",a[i]);
                }
            }
            Console.WriteLine("\n");
        }//end of DisplayValues
    }//end of default Program class
}//end of namespace

//for the last task to display all common integers between the lists I went with a more bruteforce approach I feel it could have been optimized even without the use of arrays 
//but i was having troubles with weird errors i couldn't debug so decided to stick with this approach.
