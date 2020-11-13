

using System;
using System.Linq;

// 1.0 version
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
        private static bool Exists = false;
        private static int delIndex, number;

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

            //meny alternativ
            Console.WriteLine("\nWelcome to Prague Parking\n");
            Console.WriteLine("1) Park");
            Console.WriteLine("2) Retrive");
            Console.WriteLine("3) Search");
            Console.WriteLine("4) Swap");
            Console.WriteLine("5) Show Parked Vehicles");

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
                    Console.WriteLine("\n| Swap |\n");
                    swap();
                    return true;
                case "5":
                    Console.Clear();
                    Console.WriteLine("\n| Parked vehicles |\n");
                    showParkedVehicles();
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

            switch (Console.ReadLine())
            {
                case "1":
                    Console.Clear();
                    Console.WriteLine("\n| Parking Vehicle |\n");
                    ParkVehicle();
                    break;
                case "2":
                    Console.Clear();
                    isMc = true;
                    Console.WriteLine("\n| Parking Mc |\n");
                    ParkVehicle();
                    break;
                case "0":
                    Console.Clear();
                    // closes the menu
                    break;
            }
        }


        private static void ParkVehicle()
        {
            Console.Write("Input Your Vehicles Licesens -> ");
            string input = UserInputCheck(Console.ReadLine().ToUpper());


            if (isMc)
            {

                isMc = false;

                // checks after "-mc" in the array
                bool succesMc = lookForMc(input);


                if (!succesMc)
                {
                    AssigningParkingSpaceMC(input);
                }
                else if (succesMc)
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
                    Console.WriteLine($"Please Park Vehicle -> {input} <- At Space {i + 1}, Thank You");
                    return true;
                }

                // Compares array with '-', how many times it occurring
                char compare = '-';
                bool oneMc = ParkingLot[i].Count(r => r == compare) == 1; // once
                bool TwoMc = ParkingLot[i].Count(r => r == compare) >= 2; // twice

                //occurring once
                if (oneMc)
                {

                    ParkingLot[i] += " " + input + "-mc";
                    Console.WriteLine($"Please Park Vehicle -> {input} <- At Space {i + 1}, Thank You");
                    return true;
                }
                // twice
                if (TwoMc)
                {
                    continue;

                }

            }

            return false;

        }

        // user input check
        private static string UserInputCheck(string input)
        {

            while (true)
            {
                // null or empty
                // try again
                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("A License plate cant be empty.");
                    input = Console.ReadLine();
                }

                // must be between 4-10
                if (input.Length <= 10 && input.Length >= 4)
                {
                    return input;
                }
                else
                {
                    // error msg
                    Console.WriteLine("A Lisence plate Needs to be less than 12 and atleast 4 characters long.");
                    Console.Write("Input License plate: ->");
                    input = Console.ReadLine();
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

            if (succesMc)
            {
                Console.Clear();
                Console.WriteLine($"{input} is at space {delIndex + 1}");
                RemoveMc(input, delIndex);
            }
            if (succes)
            {
                Console.Clear();
                Console.WriteLine($"{input} is at space {delIndex + 1}");
                RemoveCar(input, delIndex);
            }
            else if (!succes && !succesMc)
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
                    delIndex = i;
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
                bool succes = ParkingLot[i] == input;
                if (succes)
                {
                    delIndex = i;
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
                            delIndex = i;
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

                Console.WriteLine($"{input} is parked on place -> {delIndex + 1}");
            }
            else
            {

                Console.WriteLine($"Vehicle {input} is not Parked here");
            }
        }


        //Deletes car
        private static void RemoveCar(string input, int index)
        {

            if (ParkingLot[index] == input)
            {
                Console.WriteLine($"{input} is cheaked out, Thank You");
                ParkingLot[index] = null;
            }

        }

        //Deletes mc
        private static bool RemoveMc(string input, int index)
        {
            for (int i = 0; i < ParkingLot.Length; i++)
            {
                if (ParkingLot[index] == input + "-mc")
                {
                    Console.WriteLine($"{input} is cheaked out, Thank You");
                    ParkingLot[index] = null;
                    index = -1;
                    return true;
                }

                bool TwoMc = false;
                bool exists = false;

                // catches exceptions
                try
                {
                    TwoMc = ParkingLot[i].Count(r => r == '-') >= 2;
                    exists = ParkingLot[i].Contains(input + "-mc");
                }
                catch (ArgumentNullException) { }
                catch (Exception) { }

                if (TwoMc)
                {
                    if (exists)
                    {
                        // split array att given index
                        // string[] search{"str1","str2"}
                        var search = ParkingLot[i].Split(" ");


                        input += "-mc";
                        //loop search array
                        for (int j = 0; j < search.Length; j++)
                        {
                            //if search[j] is same as given input
                            //remove it from parkinglot array
                            if (search[j] == input)
                            {
                                Console.WriteLine($"{input.Replace("-mc", "")} is checked out, Thank You");
                                ParkingLot[i] = ParkingLot[i].Replace(search[j], "").Trim();
                                index = -1;
                                return true;

                            }
                            else
                            {
                                continue;
                            }
                        }

                    }

                }

            }

            return false;
        }


        //Shows what vehicles are park and where
        private static void showParkedVehicles()
        {
            for (int i = 0; i < ParkingLot.Length; i++)
            {

                if (ParkingLot[i] == null)
                {
                    continue;
                }

                else
                {
                    Console.WriteLine("License Plate: {0}, parked on space: {1}", ParkingLot[i].ToString(), i + 1);
                }

            }


        }


        //changes a vehicle to a place to another
        private static void swap()
        {
            Console.Write("Enter Licence Plate: ");
            string input = UserInputCheck(Console.ReadLine().ToUpper());

            bool succes = lookForCar(input);

            //for car
            if (succes)
            {

                spacesLeft();

                Console.Write("Parkingspace you want to switch to -> ");
                int pSpace = ParseInput(Console.ReadLine());

                if (ParkingLot[pSpace - 1] == null)
                {
                    ParkingLot[delIndex] = null;
                    ParkingLot[pSpace - 1] = input;
                    Console.WriteLine($"Pls switch {input} from -> {delIndex + 1} to -> {pSpace}");
                }
                else
                {
                    Console.WriteLine("cant park here");
                }
            }

            bool succesMc = lookForMc(input);
            // for mc
            if (succesMc)
            {
                input += "-mc";


                spacesLeft();

                Console.Write("Parkingspace you want to switch to -> ");
                int pSpace = ParseInput(Console.ReadLine());


                try
                {
                    Exists = ParkingLot[delIndex].Contains(input);
                }
                catch (ArgumentNullException) { }
                catch (Exception) { }

                if (Exists)
                {

                    if (ParkingLot[delIndex] == input)
                    {

                        bool count = false;

                        try
                        {
                            //här kolla om det finns 2 mc eller inte
                            count = ParkingLot[pSpace].Count(r => r == '-') >= 2;

                        }
                        catch (ArgumentNullException) { }
                        catch (Exception) { }


                        if (ParkingLot[pSpace - 1] == null)
                        {
                            ParkingLot[delIndex] = null;
                            ParkingLot[pSpace - 1] = input;
                            Console.Clear();
                            Console.WriteLine($"Pls switch {input.Substring(0, input.Length - 3)} from space-> {delIndex + 1} to -> {pSpace}");
                        }
                        else if (count)
                        {
                            ParkingLot[pSpace - 1] += input;
                            ParkingLot[delIndex] = null;
                            Console.Clear();
                            Console.WriteLine($"Pls switch {input.Substring(0, input.Length - 3)} from space-> {delIndex + 1} to -> {pSpace}");
                        }
                        else if (!count)
                        {
                            Console.Clear();
                            Console.WriteLine("Cant Park here, space taken");
                        }
                    }
                    else
                    {
                        var search = ParkingLot[delIndex].Split(" ");
                        for (int i = 0; i < search.Length; i++)
                        {
                            if (search[i] == input)
                            {
                                if (ParkingLot[pSpace - 1] == null)
                                {
                                    ParkingLot[delIndex] = ParkingLot[delIndex].Replace(input, null).Trim();
                                    ParkingLot[pSpace - 1] = input;
                                    Console.Clear();
                                    Console.WriteLine($"Pls switch {input.Substring(0, input.Length - 3)} from space -> {delIndex + 1} to -> {pSpace}");
                                }
                                else
                                {
                                    // change space vehicle stood on
                                    ParkingLot[delIndex] = ParkingLot[delIndex].Replace(input, null).Trim();


                                    // add to new space
                                    ParkingLot[pSpace - 1] += " " + input;
                                    Console.Clear();
                                    Console.WriteLine($"Pls switch {input.Substring(0, input.Length - 3)} from space -> {delIndex + 1} to -> {pSpace}");
                                }
                            }


                            else
                            {
                                continue;
                            }
                        }
                    }

                }
                else if (!Exists)
                {
                    Console.Clear();
                    Console.WriteLine("cant park here");
                }

            }
            else if (!succes && !succesMc)
            {
                Console.Clear();
                Console.WriteLine($"{input.ToUpper()} is not parked here");
            }

        }

        //int parse the input
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

        //prints out the spaces thats left
        private static void spacesLeft()
        {
            for (int i = 0; i < ParkingLot.Length; i++)
            {
                if (ParkingLot[i] != null)
                {
                    continue;
                }


                Console.WriteLine($"Parking space Available: {i + 1}");

            }

        }

        // data input
        //private static void DummyMethod()
        //{

        //    ParkingLot[0] = "BLA123-mc MCTEST321-mc";
        //    ParkingLot[1] = null;
        //    ParkingLot[2] = "DEF456-mc";
        //    ParkingLot[3] = "GHI789";
        //    ParkingLot[5] = "TEST123-mc ENNA786-mc";
        //    ParkingLot[99] = "DERP345";
        //}
    }
}

/*
 * 

 */