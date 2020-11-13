using System;
using System.Linq;
using System.Text.RegularExpressions;

// 1.1 version
/*
 * Created by:
 * Simon GripenCreutz
 * 2020-11-13
 */
namespace Prague_Parking1._1
{
    class Program
    {
        private static string[] ParkingLot = new string[100];
        private static bool isMc = false;
        private static bool twoMc = false;
        private static bool Exists = false;
        private static int vehicleIndex, number, vehicleIndex2Mc;

        static void Main(string[] args)
        {
            //DummyMethod();

            Menus();
            Console.ReadLine();
        }

        // Holding the main menu
        private static void Menus()
        {

            bool showMenu = true;

            while (showMenu)
            {
                showMenu = MainMenu();
            }
        }

        // the main menu
        private static bool MainMenu()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;

            //meny alternativ
            Console.WriteLine("\nWelcome to Prague Parking 1.1\n");
            Console.WriteLine("1) Park");
            Console.WriteLine("2) Retrive");
            Console.WriteLine("3) Search");
            Console.WriteLine("4) Swap");
            Console.WriteLine("5) Show Parked Vehicles");
            Console.WriteLine("6) Parkinglot Overview");

            switch (Console.ReadLine())
            {
                case "1":
                    Console.Clear();
                    Console.WriteLine("\n| Park |\n");
                    ParkMenu();
                    return true;
                case "2":
                    Console.Clear();
                    Console.WriteLine("\n| Retrive |\n");
                    RetriveVehicle();
                    return true;
                case "3":
                    Console.Clear();
                    Console.WriteLine("\n| Search After Vehicle |\n");
                    search();
                    return true;
                case "4":
                    Console.Clear();
                    Console.WriteLine("\n| Swap Place |\n");
                    swap();
                    return true;
                case "5":
                    Console.Clear();
                    Console.WriteLine("\n| Parked vehicles |\n");
                    showParkedVehicles();
                    return true;
                case "6":
                    Console.Clear();
                    Console.WriteLine("\n| Parkinglot Overview |\n");
                    overView();
                    return true;
                default:
                    Console.Clear();
                    Console.WriteLine("Choose a option");
                    return true;
            }
        }

        // Parking Menu
        private static void ParkMenu()
        {
            Console.WriteLine("-> What Are You Parking <-");
            Console.WriteLine("1) Car");
            Console.WriteLine("2) Motorcycle");
            Console.WriteLine("0) Return");

            bool run = true;
            while (run)
            {
                switch (Console.ReadLine())
                {
                    case "1":
                        Console.Clear();
                        run = false;
                        Console.WriteLine("\n| Parking Vehicle |\n");
                        ParkVehicle();
                        break;
                    case "2":
                        Console.Clear();
                        run = false;
                        isMc = true;
                        Console.WriteLine("\n| Parking Mc |\n");
                        ParkVehicle();
                        break;
                    case "0":
                        run = false;
                        Console.Clear();
                        // closes the menu
                        break;
                    default:
                        Console.WriteLine("Choose a option");
                        break;
                }
            }

        }

        //holds methods that assings parking space
        private static void ParkVehicle()
        {
            Console.Write("Input Your Vehicles License -> ");
            string input = UserInputCheck(Console.ReadLine().ToUpper());


            if (isMc)
            {

                isMc = false;

                // checks after "-mc" in the array
                bool succesMc = lookForMc(input);


                if (!succesMc && !Exists)
                {
                    AssigningParkingSpaceMC(input);
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine($"{input} is already parked here");
                }

            }
            else
            {
                // looks for given input in array
                bool succes = lookForCar(input);
                if (!succes)
                {
                    Exists = false;
                    AssigningParkingSPace(input);
                }
                else if (succes)
                {
                    Console.Clear();
                    Console.WriteLine($"{input} is already parked here");
                }
            }

        }

        // car parking
        private static void AssigningParkingSPace(string input)
        {

            for (int i = 0; i < ParkingLot.Length; i++)
            {
                // if space is null, space becomes input
                if (ParkingLot[i] == null)
                {
                    ParkingLot[i] = input;
                    Console.Clear();
                    Console.WriteLine($"Please Park Vehicle -> {input} <- At Space {i + 1}, Thank You");
                    break;
                }
                else
                {
                    continue;
                }
            }

        }

        // mc parking
        private static bool AssigningParkingSpaceMC(string input)
        {
            for (int i = 0; i < ParkingLot.Length; i++)
            {
                if (ParkingLot[i] == null)
                {

                    ParkingLot[i] = input + "-mc";
                    Console.Clear();
                    Console.WriteLine($"Please Park Vehicle -> {input} <- At Space {i + 1}, Thank You");
                    return true;
                }

                // Compares array with '-', how many times it occurring
                bool oneOrMoreMc = ParkingLot[i].Count(r => r == '-') == 1; // once
                //bool TwoMc = ParkingLot[i].Count(r => r == compare) == 2; // twice

                //one mc
                if (oneOrMoreMc)
                {

                    ParkingLot[i] += " " + input + "-mc";
                    Console.Clear();
                    Console.WriteLine($"Please Park Vehicle -> {input} <- At Space {i + 1}, Thank You");
                    return true;
                }
                // two mc
                if (!oneOrMoreMc)
                {
                    continue;

                }

            }

            return false;

        }

        // user input check
        // VG funktion
        private static string UserInputCheck(string input)
        {
            // holds what characters and numbers that is allowed
            Regex r = new Regex("^[a-öA-Ö0-9]*$");

            while (true)
            {
                // null or empty
                // try again
                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Error, Try agian!");
                    Console.WriteLine("Input needs to be between 4-10 characters, no special characters allowed");
                    input = Console.ReadLine().ToUpper();
                }
                else if (r.IsMatch(input) && input.Length <= 10 && input.Length >= 4)
                {
                    return input;
                }
                else
                {
                    Console.WriteLine("Error, Try again!");
                    Console.WriteLine("Input needs to be between 4-10 characters, no special characters allowed");
                    input = Console.ReadLine().ToUpper();
                }
            }
        }


        //holds methods that finds the given vehicle
        private static void RetriveVehicle()
        {

            Console.Write("Input Your Vehicle Lisence Number -> ");
            string input = UserInputCheck(Console.ReadLine().ToUpper());

            //checks after input
            bool succesMc = lookForMc(input);
            bool succes = lookForCar(input);

            // succesMc || succes
            if (succesMc)
            {
                Console.Clear();
                Console.WriteLine($"{input} is at space {vehicleIndex + 1}");
                RemoveVehicle(input, vehicleIndex, vehicleIndex2Mc);
                Console.WriteLine($"{input.Substring(0, input.Length - 3)} is cheaked out, Thank You");
            }
            else if (succes)
            {
                Console.Clear();
                Console.WriteLine($"{input} is at space {vehicleIndex + 1}");
                RemoveVehicle(input, vehicleIndex, vehicleIndex2Mc);
                Console.WriteLine($"{input} is cheaked out, Thank You");
            }
            else
            {
                Console.Clear();
                Console.WriteLine($"{input} is not parked here");
            }

        }


        //searches the array after after a car
        private static bool lookForCar(string input)
        {
            for (int i = 0; i < ParkingLot.Length; i++)
            {
                bool succes = ParkingLot[i] == input;
                if (succes)
                {
                    vehicleIndex = i;
                    return true;
                }
                else
                {
                    continue;
                }
            }
            return false;
        }

        //searches for Mc, input + "-mc"
        private static bool lookForMc(string input)
        {
            input += "-mc";

            for (int i = 0; i < ParkingLot.Length; i++)
            {
                if (ParkingLot[i] == input.Substring(0, input.Length - 3))
                {
                    Exists = true;
                    return false;
                }

                bool succes = ParkingLot[i] == input;
                if (succes)
                {
                    vehicleIndex = i;
                    return true;
                }
                else
                {

                    if (string.IsNullOrEmpty(ParkingLot[i]))
                    {
                        continue;
                    }
                    var search = ParkingLot[i].Split(" ");
                    for (int j = 0; j < search.Length; j++)
                    {
                        if (search[j] == input)
                        {
                            vehicleIndex = i;
                            vehicleIndex2Mc = j;
                            twoMc = true;
                            return true;
                        }
                        else
                        {
                            continue;
                        }
                    }
                    continue;
                }

            }

            return false;

        }

        //holds search funktions
        private static void search()
        {
            Console.Write("Input Your Vehicle Lisence Number -> ");
            string input = UserInputCheck(Console.ReadLine().ToUpper());


            bool succesMc = lookForMc(input);
            bool succes = lookForCar(input);

            if (succesMc || succes)
            {
                Console.Clear();
                Console.WriteLine($"{input} is parked on place -> {vehicleIndex + 1}");
            }
            else
            {
                Console.Clear();
                Console.WriteLine($"Vehicle {input} is not Parked here");
            }
        }


        //Deletes vehicle
        private static bool RemoveVehicle(string input, int index, int index2Mc)
        {
            if (ParkingLot[index] == input)
            {
                ParkingLot[index] = null;
                return true;
            }
            else if (ParkingLot[index].Contains(input + "-mc"))
            {
                if (twoMc)
                {
                    var splitString = ParkingLot[index].Split(" "); // split array at index
                    string removeMc = splitString[index2Mc]; // the string i want to delete
                    ParkingLot[index] = ParkingLot[index].Replace(removeMc, null).Trim(); // removeMc is deleted
                    return true;
                }
                else
                {
                    ParkingLot[index] = null;
                    return true;
                }

            }
            return false;
        }


        //Shows what vehicles are park and where
        private static void showParkedVehicles()
        {

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("* Red = Occupied \n");

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("* Magenta = Space for 1 Motorcycle \n");

            Console.ResetColor();
            Console.WriteLine("* Licenseplate-mc = Motorcycle \n");


            for (int i = 0; i < ParkingLot.Length; i++)
            {
                if (ParkingLot[i] != null)
                {
                    bool Mc = ParkingLot[i].Contains("-mc");
                    int TwoMC = ParkingLot[i].Count(r => r == '-');
                    if (Mc)
                    {
                        if (TwoMC > 1)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"Parked on space: {i + 1} -> {ParkingLot[i]}");

                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.WriteLine($"Parked on space: {i + 1} -> {ParkingLot[i]}");
                        }
                    }
                    else
                    {

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Parked on space: {i + 1} -> {ParkingLot[i]}");
                    }


                }

                Console.ResetColor();

            }

        }

        // swaps vehicle to a new space
        private static void swap()
        {
            Console.Write("Enter Licence Plate: ");
            string input = UserInputCheck(Console.ReadLine().ToUpper());

            bool succes = lookForCar(input);
            bool succesMc = lookForMc(input);

            //for car
            if (succes)
            {
                overView();

                Console.Write("Parkingspace you want to switch to -> ");
                int pSpace = ParseInput(Console.ReadLine());

                if (ParkingLot[pSpace - 1] == null)
                {
                    ParkingLot[vehicleIndex] = null;
                    ParkingLot[pSpace - 1] = input;
                    Console.Clear();
                    Console.WriteLine($"Pls switch {input} from -> {vehicleIndex + 1} to -> {pSpace}");
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("cant park here");
                }
            }


            // for mc
            if (succesMc)
            {
                input += "-mc";
                overView();

                Console.Write("Parkingspace you want to switch to -> ");
                int pSpace = ParseInput(Console.ReadLine());

                if (ParkingLot[vehicleIndex].Contains(input))
                {

                    if (ParkingLot[pSpace - 1] == null)
                    {
                        ParkingLot[vehicleIndex] = null;
                        ParkingLot[pSpace - 1] = input;
                        Console.Clear();
                        Console.WriteLine($"Pls switch {input.Substring(0, input.Length - 3)} from space-> {vehicleIndex + 1} to -> {pSpace}");
                        
                    }
                    else if (twoMc)
                    {
                        var splitString = ParkingLot[vehicleIndex].Split(" ");
                        string SwitchMc = splitString[vehicleIndex2Mc];
                        ParkingLot[vehicleIndex] = ParkingLot[vehicleIndex].Replace(SwitchMc, null).Trim();
                        ParkingLot[pSpace - 1] += " " + input;
                        Console.Clear();
                        Console.WriteLine($"Pls switch {input.Substring(0, input.Length - 3)} from space-> {vehicleIndex + 1} to -> {pSpace}");
                    }

                } else
                {
                    Console.Clear();
                    Console.WriteLine("cant park here");
                }

            }
        }

        //parse user input
        private static int ParseInput(string input)
        {


            while (true)
            {
                // boolean that holds the input and "tries" to parse to a number.
                // if its "true" return the parsed number
                // "else" new input
                bool succes = Int32.TryParse(input, out number);

                if (succes && number <= 100 && number > 0)
                {
                    return number;
                }
                else
                {
                    Console.WriteLine("Invalid input, only numbers 1-100!");
                    input = Console.ReadLine();
                }
            }
        }

        // a overview of the parkinglot
        // VG funktion
        private static void overView()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("* Green = Open \n");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("* Red = Occupied \n");

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("* Magenta = Space for 1 Motorcycle \n");

            Console.ResetColor();
            Console.WriteLine("* Licenseplate-mc = Motorcycle \n");


            for (int i = 0; i < ParkingLot.Length; i++)
            {
                if (ParkingLot[i] != null)
                {
                    bool Mc = ParkingLot[i].Contains("-mc");
                    int TwoMC = ParkingLot[i].Count(r => r == '-');
                    if (Mc)
                    {
                        if (TwoMC > 1)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"Parked on space: {i + 1} -> {ParkingLot[i]}");

                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.WriteLine($"Parked on space: {i + 1} -> {ParkingLot[i]}");
                        }
                    }
                    else
                    {

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Parked on space: {i + 1} -> {ParkingLot[i]}");
                    }


                }
                if (ParkingLot[i] == null)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Open space -> {i + 1}");

                }

            }

            Console.ResetColor();

        }

        //dummy data
        private static void DummyMethod()
        {

            ParkingLot[0] = "BLA123-mc MCTEST321-mc";
            ParkingLot[1] = null;
            ParkingLot[2] = "DEF456-mc";
            ParkingLot[3] = "GHI789";
            ParkingLot[4] = "ENMC124-mc";
            ParkingLot[5] = "TEST123-mc ENNA786-mc";
            ParkingLot[99] = "DERP345";
        }
    }
}