namespace BankoCheater
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //current test tables
            BankoTable table1 = new BankoTable()
            {
                tableNums = [11, 31, 41, 61, 81, 12, 32, 42, 62, 82, 13, 33, 43, 63, 83],
                tableID = "testTable1"
            };
            BankoTable table2 = new BankoTable()
            {
                tableNums = [14, 34, 44, 64, 84, 15, 35, 45, 65, 85, 16, 36, 46, 66, 86],
                tableID = "testTable2"
            };
            Console.WriteLine($"Hello to {table2.tableID}");

            //Variable containing called numbers
            int[] calledNums = [];
            calledNums = [11, 17, 45, 16];

            CheckNumbs(table1.tableNums, calledNums);
            CheckNumbs(table2.tableNums, calledNums);

            //function to loop over table's numbers, comparing to called numbers
            static void CheckNumbs(int[] thisTableNums, int[] calledNums)
            {

                // Array.ForEach(thisTableNums, Console.WriteLine);
                // Console.WriteLine();
                //Array.ForEach(calledNums, Console.WriteLine);

                foreach (int num in calledNums)
                {
                    if (thisTableNums.Contains(num))
                    {
                        Console.WriteLine(num + "check");
                    }
                }
            }


        }
    }



    public class BankoTable
    {
        public int[]? tableNums;
        public string tableID;
    }


}
