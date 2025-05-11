
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using static System.Net.WebRequestMethods;
namespace BankoCheater
{
    internal class Program
    {
        

        static void Main(string[] args)
        {
            Dictionary<int, int[]> myTables = new Dictionary<int, int[]>();


            //SELENIUM INPUT TEST START
            Console.WriteLine("Enter a name to generate tables");
            string tableName = Console.ReadLine();
            int numTablesGen = 0;
            while ((numTablesGen < 1))
            {
                Console.WriteLine("Enter a number of tables to generate");
                numTablesGen = Int32.Parse(Console.ReadLine());
            }
            //Console.WriteLine($"tableName {tableName} and numTables {numTablesGen}");
            
            var options = new FirefoxOptions();
            options.AddArguments("-headless");
            IWebDriver driver = new FirefoxDriver(options);
            driver.Url = "https://mercantech.github.io/Banko/";

            IWebElement textField = driver.FindElement(By.Id("tekstboks"));
            IWebElement generateButton = driver.FindElement(By.Id("knap")); // Brug det faktiske ID

            for (int tableNum = 0; tableNum < numTablesGen; tableNum++)
            {
                textField.Clear();
                textField.SendKeys($"{tableName}" + $"{tableNum}");
                generateButton.Click();
                System.Threading.Thread.Sleep(2); // En simpel måde at vente, brug evt. WebDriverWait for bedre kontrol

                // Hent pladedata (fx rækker og ID)
                IWebElement row1 = driver.FindElement(By.CssSelector("#p11")); // Brug det faktiske CSS-selector
                IWebElement row2 = driver.FindElement(By.CssSelector("#p12")); 
                IWebElement row3 = driver.FindElement(By.CssSelector("#p13")); 
                string plateData = row1.Text + " " + row2.Text + " " + row3.Text;
                string[] rowsCombinedString = plateData.Split(' ');
                int[] rowsCombinedInt = Array.ConvertAll(rowsCombinedString, int.Parse);

                myTables.Add(tableNum, rowsCombinedInt);
            }
            // Luk browseren
            driver.Quit();

            //SELENIUM INPUT TEST END


            Console.WriteLine("Number between 1 and 90 to start. QQ quits, reset resets, del deletes last number.");

            string bankoInput = "";
            List<int> calledNums = new List<int>();

            //Looping input until quitting; add to list calledNums if valid, then loop over tables
            do
            {
                bankoInput = Console.ReadLine();
                switch (bankoInput)
                {
                    case "QQ":
                        //quit
                        Console.WriteLine("Quitting");
                        break;
                    case "reset":
                        calledNums.Clear();
                        break;
                    case "del":
                        if (calledNums.Count > 0)
                        {
                            calledNums.RemoveAt(calledNums.Count - 1);
                        }
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
                            for (int iTable = 0; iTable < myTables.Count; iTable++)
                            {
                                checkTable(iTable);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. QQ, reset, del, or unique number between 1 and 90");
                        }
                        //err
                        break;
                }
            }
            while (bankoInput != "QQ");

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
                    Console.WriteLine($"Banko on ALL rows of {tableName}{tableID}!");
                }
                else if (bingoBankoCounter.Contains(5))
                {
                    for (int i = 0; i<3; i++) 
                    {
                        if (bingoBankoCounter[i] == 5)
                        {
                            Console.WriteLine($"Banko row {i+1} of {tableName}{tableID}!");
                        }
                    }
                }
                /*else
                { Console.WriteLine($"row1: {bingoBankoCounter[0]}, row2: {bingoBankoCounter[1]}, row3: {bingoBankoCounter[2]}");
                }
                */
            }
        }

    }
}
