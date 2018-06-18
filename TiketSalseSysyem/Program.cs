using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiketSalseSysyem {
    class Program {
        static void Main(string[] args) {
            //Tikets that can be purchesed
            List<Tiket> TiketTypes = new List<Tiket>();
            //Tikets that has been purchesed
            List<Tiket> TiketsPurchesed = new List<Tiket>();
            //Calculates what day it is
            DateTime ClockInfoFromSystem = DateTime.Now;
            //Adds the tikets to the list
            TiketTypes = AddTikets();
            //iuser input
            string input = "";
            //Introduction
            Console.WriteLine("Welcome to QA Cinemas Tiket Prossesing sysyem\n");
            //prints the options for input
            PrintOptions();
            Console.WriteLine();
            //Askes for the users input
            Console.WriteLine("Plese select an option to continue\n");
            //Main loop
            while (true) {
                //Gets the userinput
                input = Console.ReadLine();
                Console.WriteLine("You have selected opption " + input);
                //Makes a decision based on the input
                switch (input) {
                    //Print the optons
                    case ("0"):
                        Console.WriteLine();
                        //Prints the types of tikets
                        PrintTiketInfomation(TiketTypes, false);
                        break;
                    //Buy a tiket
                    case ("1"):
                        Console.WriteLine();
                        BuyTiket(ref TiketsPurchesed, TiketTypes);
                        break;
                    //Remove a tiket
                    case ("2"):
                        Console.WriteLine();
                        //Only alows use if tikets have been purchesed
                        if (TiketsPurchesed.Count > 0) {
                            RemoveTiket(ref TiketsPurchesed);
                        }
                        //If no tikets have been purchesed
                        else {
                            Console.WriteLine("No tikets have been purchesed");
                        }
                        break;
                    //Printing the basket
                    case ("3"):
                        Console.WriteLine();
                        //Only alows use if tikets have been purchesed
                        if (TiketsPurchesed.Count > 0) {
                            Console.WriteLine("Here is your order so far");
                            PrintTiketInfomation(TiketsPurchesed, true);
                        }
                        //If no tikets have been purchesed
                        else {
                            Console.WriteLine("No tikets have been purchesed");
                        }
                        break;
                    //Checkout
                    case ("4"):
                        //If the user has purched tikets
                        if (TiketsPurchesed.Count > 0) {
                            Console.WriteLine();
                            if (Checkout(TiketsPurchesed, ClockInfoFromSystem.DayOfWeek == DayOfWeek.Wednesday)) {
                                input = "5";
                            }
                        }
                        break;
                    //Exit the application
                    case ("5"):
                        break;
                    //Invalid opption
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }
                //Exits the loop
                if (input == "5") break;
                //prints the options for input
                Console.WriteLine();
                PrintOptions();
                Console.WriteLine("Plese select an option to continue");
            }
            //Exit the application
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        //Alows the user to revew thier purchis
        public static bool Checkout(List<Tiket> Basket, bool b) {
            Console.WriteLine("Here are this tikets you have purchesed");
            //Prinsts the tikets that have been purchesed
            Console.WriteLine("----------------------------------------");
            PrintTiketInfomation(Basket, true);
            Console.WriteLine("----------------------------------------");
            float totle = 0, discount = 0;
            float amout = 0;
            foreach(Tiket t in Basket) {
                totle += t.Price * t.Amount;
                amout += t.Amount;
            }
            //writes the sub total
            Console.WriteLine("Sub Totle: £" + totle);
            //calculates discounnt
            if(b) {
                discount = amout * 2;
            }
            //Writes the sub total
            Console.WriteLine("Discount: £" + discount);
            totle -= discount;
            Console.WriteLine("----------------------------------------");
            //Writes the totle price
            Console.WriteLine("Total: £" + totle);
            //waits for user input
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
            return true;
        }

        //Print what the user can do
        public static void PrintOptions() {
            Console.WriteLine("Here are your options");
            Console.WriteLine(
                "0: Show Tiket Prices" + "\n" +
                "1: Buy a tiket" + "\n" +
                "2: Remove a tiket" + "\n" +
                "3: Basket" + "\n" +
                "4: Check out" + "\n" +
                "5: Exit" + "\n");
        }

        //Alows the user to purches a tiket
        public static void BuyTiket(ref List<Tiket> UT, List<Tiket> LT) {
            //Shows tiket prices
            Console.WriteLine("Select A tiket to purchesed");
            PrintTiketInfomation(LT, false);
            //Alows the uset to select a tiket
            Console.WriteLine("Type a number to select a tiket");
            //Gets the user input
            string tempInput = Console.ReadLine();
            //loops until no more tikets are needed
            while (true) {
                //Test to see if the user can use the input
                int input = canUseNumber(tempInput);
                //if the tiket option is valid
                if (input >= 0 && input < LT.Count) {
                    //Tempery tiket
                    Tiket t = new Tiket(LT[input].Name,LT[input].Price);
                    Console.WriteLine("You have slected a " + t.Name + " tiket" + "\n" + "Select how many are needed");
                    //Gets the users input
                    tempInput = Console.ReadLine();
                    //Loop for the userts input
                    while (true) {
                        //Test to see if the user can use the input
                        float amount = canUseNumber(tempInput);
                        //If the user has ented a valid amount of tikets
                        if (amount >= 0) {
                            //Changes the amount
                            t.Amount = amount;
                            ModifiyTiketList(ref UT, t, true);
                            Console.WriteLine("\nHere is your curent order.\n");
                            PrintTiketInfomation(UT, true);
                            break;
                        }
                        else {
                            Console.WriteLine("Invalid selection");
                            Console.WriteLine("Enter a valid option ot type exit to retyrn");
                            tempInput = Console.ReadLine();
                            Console.WriteLine(tempInput);
                            if (tempInput == "exit") break;
                        }
                    }
                }
                //invalid option
                else {
                    Console.WriteLine("Invalid selection");
                }
                Console.WriteLine("\nEnter a another type of tiekt\nor type exit to return\n");
                tempInput = Console.ReadLine();
                if (tempInput == "exit") break;
            }
        }

        //Alows the user to remove a tiket
        public static void RemoveTiket(ref List<Tiket> UT) {
            Console.WriteLine("Select A tiket to remove");
            //Prints the tikets that have already been purchesed
            PrintTiketInfomation(UT, true);
            //Gets the user input
            string tempInput = Console.ReadLine();
            //Loop for the user to remove tikets
            while (true) {
                //Test to see if the user can use the input
                int input = canUseNumber(tempInput);
                //if the tiket option is valid
                if (input >= 0 && input < UT.Count) {
                    Tiket t = new Tiket(UT[input].Name, UT[input].Amount);
                    Console.WriteLine("You have slected a " + UT[input].Name + " tiket" + "\n" + "You curently have " + UT[input].Amount + ".\n" + "How many would you like to remove?");
                    //Gets the users input
                    tempInput = Console.ReadLine();
                    while (true) {
                        //Test to see if the user can use the input
                        float amount = canUseNumber(tempInput);
                        //If the user has ented a valid amount of tikets
                        if (amount > 0) {
                            //Changes the amount
                            t.Amount = amount;
                            t.Amount *= -1;
                            //Modifiys the list
                            ModifiyTiketList(ref UT, t, true);
                            Console.WriteLine("\nHere is your curent order.\n");
                            PrintTiketInfomation(UT, true);
                            break;
                        }
                        else {
                            Console.WriteLine("Invalid selection");
                            Console.WriteLine("Enter a valid option ot type exit to retyrn");
                            tempInput = Console.ReadLine();
                            Console.WriteLine(tempInput);
                            if (tempInput == "exit") break;
                        }
                    }
                }
                //Invalid option
                else {
                    Console.WriteLine("Invalid selection");
                }
                //If there are not tikets in the basket
                if (UT.Count < 1) break;
                Console.WriteLine("\nEnter a another type of tiekt to remove\nor type exit to return\n");
                tempInput = Console.ReadLine();
                if (tempInput == "exit") break;
            }
        }

        //Modifiys Tiket values
        public static void ModifiyTiketList(ref List<Tiket> UsersTikets, Tiket Tiket, bool b) {
            //Flag if the operation was complete
            bool flag = false;
            //Loops through the useres tiket to find aduplicate tiket
            for (int i = 0; i < UsersTikets.Count; i++) {
                //If they are the same tiket type
                if(Tiket.Name == UsersTikets[i].Name) {
                    //Sets the flag to true;
                    flag = true;
                    //Modifiys tiket amounts
                    UsersTikets[i].Amount += Tiket.Amount;
                    //If there are less than 1 tiket in the selkection
                    //remove it
                    if (UsersTikets[i].Amount < 1) {
                        UsersTikets.RemoveAt(i);
                    }
                    break;
                }
            }
            //If the operation was not complet complet and the operation was to add a tiket
            if(!flag & b) {
                UsersTikets.Add(Tiket);
            }
        }

        //Cheks the input
        public static int canUseNumber(string s) {
            //int to return the input
            int i;
            try {
                //Trys to turn the sting into an int
                i = int.Parse(s);
            }
            catch {
                //failed input
                return -1;
            }
            //if the uer has a valid input
            return i;
        }

        //Prints all of the tikets infomation
        public static void PrintTiketInfomation(List<Tiket> LT, bool b) {
            //Loops through theb array and
            //prints out the tiket infomation
            for(int i = 0; i < LT.Count; i++) {
                if (b) {
                    Console.WriteLine(i + ": Tiket Type:" + LT[i].Name + " : Price Per Tiket: £" + LT[i].Price + " : Amounnt:" + LT[i].Amount + "\nTotle price: £" + LT[i].Price * LT[i].Amount);
                }
                else {
                    Console.WriteLine(i + ": Tiket Type:" + LT[i].Name + " : Price: £" + LT[i].Price);
                }
            }
        }

        //Adds the tikets
        public static List<Tiket> AddTikets() {
            List<Tiket> TL = new List<Tiket>();
            TL.Add(new Tiket("Standard", 8));
            TL.Add(new Tiket("OAP", 6));
            TL.Add(new Tiket("Student", 6));
            TL.Add(new Tiket("Child", 4));
            return TL;
        }
    }
}