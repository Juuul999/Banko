namespace BankoCheater
{
    internal class Program
    {
        

        static void Main(string[] args)
        {

            //static test tables
            Dictionary<int, int[]> myTables = new Dictionary<int, int[]>();
            //test name Noah
            //myTables[1,1] accesses table 1, value 1, i.e. 10
            myTables.Add(1, [1, 10, 23, 45, 70, 2, 24, 35, 67, 86, 19, 57, 68, 76, 90]);
            myTables.Add(2, [14, 21, 32, 76, 86, 6, 15, 44, 56, 66, 29, 45, 58, 79, 88]);
            myTables.Add(3, [1, 20, 64, 70, 81, 11, 48, 54, 73, 82, 9, 15, 37, 59, 67]);
            myTables.Add(4, [1, 30, 42, 61, 70, 24, 45, 55, 64, 85, 15, 25, 34, 47, 69]);
            myTables.Add(5, [3, 20, 31, 52, 60, 6, 15, 21, 46, 86, 18, 25, 33, 79, 88]);
            Console.WriteLine("qq to quit, reset to reset, number between 1 and 90 to continue");

            string bankoInput = "";
            List<int> calledNums = new List<int>();
            //Looping input until quitting; add to list calledNums if valid, then loop over tables
            do
            {
                bankoInput = Console.ReadLine();
                switch (bankoInput)
                {
                    case "qq":
                        //quit
                        Console.WriteLine("Quitting");
                        break;
                    case "reset":
                        Console.WriteLine("Resetti");
                        //reset
                        break;
                    default:
                        if (int.TryParse(bankoInput, out int bankoNum) && (0 < bankoNum) && (91 > bankoNum) && (calledNums.Contains(bankoNum) != true))
                        {
                            //Console.WriteLine("Valid Num");
                            calledNums.Add(bankoNum);
                            Console.Write("Currently called numbers: ");
                            foreach (int number in calledNums) 
                            { 
                                Console.Write($"{number}, ");
                            }
                            Console.WriteLine();
                            //Console.WriteLine(myTables.Count);
                            for (int iTable = 1; iTable <= myTables.Count; iTable++)
                            {
                                checkTable(iTable);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. qq, reset, or unique number between 1 and 90");
                        }
                        //err
                        break;
                }
            }
            while (bankoInput != "qq");

            void checkTable(int tableID)
            {
                int[] bingoBankoCounter = [0, 0, 0];
                for (int row = 1; row < 4; row++)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        if (calledNums.Contains(myTables[tableID][i+5*(row-1)]))
                        {
                            bingoBankoCounter[row - 1]++;
                        }

                    }
                }
                if (bingoBankoCounter.Sum() == 15)
                {
                    Console.WriteLine($"Banko on all rows!");
                }
                else if (bingoBankoCounter.Contains(5))
                {
                    for (int i = 0; i<3; i++) 
                    {
                        if (bingoBankoCounter[i] == 5)
                        {
                            Console.WriteLine($"Banko on row {i+1}!");
                        }
                    }
                }
                else
                { Console.WriteLine($"row1: {bingoBankoCounter[0]}, row2: {bingoBankoCounter[1]}, row3: {bingoBankoCounter[2]}");
                }
                //Console.WriteLine($"Bingo-Banko on {row}");
            }
        }
/*myTables.Add(1, [1, 10, 23, 45, 70, 2, 24, 35, 67, 86, 19, 57, 68, 76, 90]);
            myTables.Add(2, [14, 21, 32, 76, 86, 6, 15, 44, 56, 66, 29, 45, 58, 79, 88]);
            myTables.Add(3, [1, 20, 64, 70, 81, 11, 48, 54, 73, 82, 9, 15, 37, 59, 67]);
            myTables.Add(4, [1, 30, 42, 61, 70, 24, 45, 55, 64, 85, 15, 25, 34, 47, 69]);
            myTables.Add(5, [3, 20, 31, 52, 60, 6, 15, 21, 46, 86, 18, 25, 33, 79, 88]);
        */    
        

    }
}
