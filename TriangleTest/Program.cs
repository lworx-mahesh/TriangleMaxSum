using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriangleTest
{
   
    class Program
    {
        static string fileName = "triangle.txt";
        static void Main(string[] args)
        {
           //SET THE SAMPLE TRIANGLE 
           /* UNCOMMENT FOR TESTING. 
            int[,] inputTriangle = { 
                        {5, 0, 0,0},
                        {9, 6, 0,0},
                        {4, 6, 8,0},
                        {0, 7, 1,5}
                                    };
            */

            Console.Write("Press y to execute with explanatory logs or press enter to execute normally. ");
            string userResponse = Console.ReadLine();
            /* THIS FUNCTION GENERATE THE FILE WITH GIVEN ROW RANGE UNCOMMENT AND RUN IT.   * */
            Console.Write("Press f to generate new file. ");
            string userResponseGenFile = Console.ReadLine();
            if(userResponseGenFile.ToLower() == "f")
            WriteFile();
            int[,] inputTriangle = ReadTheFile();// GET THE ARRAY FROM FILE

            TopToBottomOfTriangle(inputTriangle, userResponse.ToLower());
           Console.ReadLine();//HOLD FOR THE USER KEY

            #region Commented Code With Other Case
            /* JUST ADDED ONE MORE ARRAY TO TEST THE LOGIC UNCOMMENENT TO EXEUCUTE 
            int[,] inputTriangle2 = {
                        {5, 0, 0,0,0},
                        {9, 6, 0,0,0},
                        {4, 6, 8,0,0},
                        {0, 7, 1,5,0},
                        {0, 7, 10,5,11}
                                    };
            Console.Write(TopToBottomOfTriangle(inputTriangle2));
            Console.ReadLine();//HOLD FOR THE USER KEY
            */
            
            #endregion
        }
        /// <summary>
        /// This function is calculating and print the max traverse values of passed array(triangle).
        /// NOTE: set variable  IsLogRequired equals to true if needs to understand the logic. function is self explanatory
        /// </summary>
        /// <param name="inputTriangle">input parameter</param>
     
        static void TopToBottomOfTriangle(int[,] inputTriangle, string userResponse)
        {
            #region LocalVariable


            bool IsLogRequired = userResponse == "y";//SET THE TRUE FOR WRITE THE EXECUTION LOGIC
            float possiblePaths = (float)Math.Pow(2, inputTriangle.GetLength(0) - 1);// BECAUSE WE HAVE TO TRAVERSE LEFT AND RIGHT, POSSIBLE COMBINATION IS THE 2 POWER OF 3, HERE 2 IS REPRESENTING TWO DIRECTIONS (LEFT / RIGHT) 3 IS  REPRESENTING THE ARRAY LENGTH
            int currentTraversingSum;
            int index =0;
            int largestSum = 0;
            int loopCount = inputTriangle.GetLength(0) - 1;// LENGTH OF THE ARRAY SO INNER TRAVERSING LOOP .
            var pathTopToBottom = new List<float>();//TO GENERATE THE CURRENT PATH
            string largestSumPath = ""; //TO SHOW THE SELECTED NODE PATH WITH RESULT
            #endregion

            #region CoreLogic
            for (int pathIteration = 0; pathIteration <= possiblePaths; pathIteration++)
            {
                currentTraversingSum = inputTriangle[0, 0];// GET THE TOP PARENT NODE VALUE
                pathTopToBottom.Add(currentTraversingSum);// ALWAYS START WITH PARENT NODE VALUE
                index = 0;
                if (IsLogRequired) Console.WriteLine("TRAVERSING  PATH " + pathIteration);
                for (int nodeIndex = 0; nodeIndex < loopCount; nodeIndex++)
                {
                    if (IsLogRequired) Console.WriteLine("CHECING THE LEFT (0) / RIGHT (1) CHILD NODE OF THE PARENT " + (pathIteration >> nodeIndex & 1));
                    if (IsLogRequired) Console.WriteLine("NODE ADDRESS J+1  " + (nodeIndex + 1) + " & INDEX IS " + index);
                    index = index + (pathIteration >> nodeIndex & 1);
                    currentTraversingSum += inputTriangle[nodeIndex + 1, index];
                    pathTopToBottom.Add(inputTriangle[nodeIndex + 1, index]);
                }
                if (currentTraversingSum > largestSum)//ONLY ADD IF CURRENT TRAVERSING SUM IS GREATER THAN PERVIOUS LARGEST SUM
                {
                    largestSum = currentTraversingSum;
                    largestSumPath = string.Join("+", pathTopToBottom);//STORE THE FINAL NODE FOR DISPLAY PURPOSE.
                }
                pathTopToBottom.Clear();// CLEAR THE PATH AFTER EXECUTION FOR NEXT CYCLE
            } 
            #endregion

            #region Printing the input triangle
            Console.WriteLine("\n");
            int triangleLength = inputTriangle.GetLength(0);
            for (int rowindex = 0; rowindex < triangleLength; rowindex++)
            {
                for (int spaceIndex = triangleLength; spaceIndex > rowindex; spaceIndex--)
                {   //ADD THE SPACES TO REPRSENT DATA AS TRIANGLE
                    Console.Write("  ");
                }
                for (int columnIndex = 0; columnIndex <= rowindex; columnIndex++)
                {
                    //FOR THE PRINT COLUMNS WITH SPACE
                    Console.Write(inputTriangle[rowindex, columnIndex] + "  ");
                }
                Console.WriteLine("\n");//MOVE TO NEXT ROW
            }
            #endregion
            
            Console.WriteLine("\n");
            Console.WriteLine("Maximum total from top to bottom is: " + largestSumPath + " = "+ largestSum);//PRINT THE FINAL OUTPUT
           
        }

        /// <summary>
        /// THIS FUNCTION READ THE FILE AND RETURN THE TRIANGLE ARRAY
        /// </summary>
        static int[,] ReadTheFile()
        {
            string fullPath = Directory.GetCurrentDirectory() + @"\" + fileName;
            string[] lines = File.ReadAllLines(fullPath);
            int arryLength = lines.Count() / 2;// REMOVE THE WHITE SPACES
            int[,] inputTriangle = new int[arryLength, arryLength];
            int rowindex = 0, columnIndex = 0;
            foreach (var item in lines)
            {
                if (item != "")//AVOID BLANK LINES
                {
                    var rowArray = item.Split("\t".ToCharArray());
                    foreach (var item2 in rowArray)
                    {
                        if (item2 != "")
                            inputTriangle[rowindex, columnIndex] = Convert.ToInt32(item2);
                        columnIndex = columnIndex + 1;
                    }
                    rowindex = rowindex + 1;
                    columnIndex = 0;
                   
                }
            }
            return inputTriangle;
        }

        /// <summary>
        ///  THIS FUNCTION GENERATE THE FILE
        /// </summary>
        static void WriteFile()
        {
            int rowLoopIndex, zeroRowIndex, matrixLoopIndex, rowLimit;
            Console.Write("Enter the  triangle row = ");
            rowLimit = int.Parse(Console.ReadLine());
            Random rnd = new Random();
            string fullPath = Directory.GetCurrentDirectory() + @"\"+fileName;
            using (StreamWriter writer = new StreamWriter(fullPath))
            {
                for (rowLoopIndex = 1; rowLoopIndex <= rowLimit; rowLoopIndex++)
                {
                    for (matrixLoopIndex = 1; matrixLoopIndex <= rowLoopIndex; matrixLoopIndex++)
                    {
                        writer.Write(rnd.Next(11, 99) + "\t");
                    }
                    for (zeroRowIndex = 1; zeroRowIndex <= rowLimit - rowLoopIndex; zeroRowIndex++)
                    {
                        writer.Write("0\t");
                    }
                    writer.WriteLine("\n");
                }
            }
            // Write file using StreamWriter  
        }
    }
}
