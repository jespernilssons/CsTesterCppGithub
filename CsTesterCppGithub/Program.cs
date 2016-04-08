using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CsTesterCppGithub
{
    class Program
    {
        
        static string saveDir = "..\\CppIterationReport.txt";
        private static string newLine = "\r\n";
        private static Random rnd = new Random();
        private static int iterations = rnd.Next(2, 10);

        static void Main(string[] args)
        {
            WriteStartTimeStampToFile();
            callCppApiInsertRandomElementValueAndCompute();
            WriteEndTimeStampToFile();
        }

        static void callCppApiInsertRandomElementValueAndCompute()
        {
            int iterationStartValue = iterations;

            for (int t = 0; t < iterations; t++)
            {
                int noElements = rnd.Next(100, 1000);
                int[] myArray = new int[noElements];

                for (int i = 0; i < noElements; i++)
                {
                    myArray[i] = i * 10;
                }

                unsafe
                {
                    fixed (int* pmyArray = &myArray[0])
                    {
                        CppWrapper.CppWrapperClass controlCpp = new CppWrapper.CppWrapperClass(pmyArray, noElements);
                        controlCpp.getSum();
                        int sumOfArray = controlCpp.sum;
                        ShowConsoleAndWriteToFile(sumOfArray, noElements);
                    }
                }
            }
            System.IO.File.AppendAllText(saveDir, "Iterations done: " + iterationStartValue + newLine);

        }

        static void WriteStartTimeStampToFile()
        {
            string currentTime = DateTime.Now.ToString();

            System.IO.File.AppendAllText(saveDir, "---- Iterations started: " + currentTime + " ----" + newLine);
        }

        static void WriteEndTimeStampToFile()
        {
            string currentTime = DateTime.Now.ToString();

            System.IO.File.AppendAllText(saveDir, "---- Iterations ended: " + currentTime + " ----" + newLine + newLine);
        }

        static void ShowConsoleAndWriteToFile(int showSum, int elementValue)
        {
            System.Diagnostics.Debug.WriteLine("Elements used: " + elementValue + " Testdata returned = " + showSum);
            string textSum = "Elements used: " + elementValue + " Testdata returned = " + showSum + newLine;
            System.IO.File.AppendAllText(saveDir, textSum);
        }
    }
}
