using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1CSharp
{
    class Program
    {
        struct FieldData
        {
            public byte[,] matrix;
            public int minX;
            public int minY;
            public List<int[]> visitedDots;
            public List<int[]> allDots;
        }

        static void Main(string[] args)
        {
            Console.Write("Введите координаты точек на плоскости через пробел(пример - 1,2 - точка с x = 1, y = 2): ");
            List<int[]> dots = InputMatrDots(Console.ReadLine());
            FieldData field = new FieldData();
            field = MakeMatrix(dots);
            Console.Write("Введите начальную точку: ");
            int[] inpDot = InputStartDot(Console.ReadLine());
            int[] realDot = { inpDot[0] - field.minX, inpDot[1] - field.minY };
            
            bool endMoving = false;
            Console.Write("Введите направление робота(Left, Right, Up, Down): ");
            string whereToMove = InputDirection(Console.ReadLine());
            Console.Write("Движение по часовой стрелке или против(cw - по часовой, ccw - против): ");
            string clockwise = Console.ReadLine();
            int funcCalls = 0;
            int[] startDot = { realDot[0], realDot[1] };
            Console.WriteLine("Везде 2 - это нынешнее положение робота");
            while (!endMoving)
            {  
                RobotMove(ref endMoving, ref realDot, ref whereToMove, clockwise, ref field, funcCalls, startDot);
                funcCalls++;
            }
            Console.ReadKey();
        }

        static List<int[]> InputMatrDots(string input)
        {
            //string input = Console.ReadLine();
            List<int[]> dots = new List<int[]>();
            try
            {
                string[] dotsStr = input.Split(' ');
                foreach (string dotStr in dotsStr)
                {
                    string[] dotCoords = dotStr.Split(',');
                    int[] dot = { Convert.ToInt32(dotCoords[0]), Convert.ToInt32(dotCoords[1]) };
                    dots.Add(dot);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Неправильный ввод");
                dots = InputMatrDots(Console.ReadLine());
            }
            return dots;
        }

        static int[] InputStartDot(string input)
        {
            string[] dotCoords = input.Split(',');
            int[] dot = { Convert.ToInt32(dotCoords[0]), Convert.ToInt32(dotCoords[1]) };
            Console.WriteLine("x = " + dot[0] + ", y = " + dot[1]);
            return dot;
        }

        static string InputDirection(string input)
        {
            if (input == "Up" || input == "Down" || input == "Right" || input == "Left")
            {
                return input;
            }
            else 
            {
                try
                {
                    Convert.ToDouble(input);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Неправильный ввод");
                    return InputDirection(Console.ReadLine());
                }

                return input;
            }
        }

        static FieldData MakeMatrix(List<int[]> dots)
        {
            FieldData field = new FieldData();
            field.allDots = new List<int[]>();
            field.visitedDots = new List<int[]>();
            List<int> xs = new List<int>();
            List<int> ys = new List<int>();
            foreach (int[] dot in dots)
            {
                xs.Add(dot[0]);
                ys.Add(dot[1]);
            }
            int minX = xs[0], maxX = xs[0], minY = ys[0], maxY = ys[0];
            for (int i = 0; i < dots.Count(); i++)
            {
                if (xs[i] < minX)
                {
                    minX = xs[i];
                }
                else if (xs[i] > maxX)
                {
                    maxX = xs[i]; 
                }

                if (ys[i] < minY)
                {
                    minY = ys[i];
                }
                else if (ys[i] > maxY)
                {
                    maxY = ys[i];
                }
            }

            int sizeX = maxX - minX + 1;
            int sizeY = maxY - minY + 1;
            byte[,] matrix = new byte[sizeX, sizeY];
            for (int i = 0; i < sizeY; i++)
            {
                for (int j = 0; j < sizeX; j++)
                {
                    foreach (int[] dot in dots)
                    {
                        if (dot[0] == j + minX && dot[1] == i + minY)
                        {
                            matrix[j, i] = 1;
                            int[] arr = {i, j};
                            field.allDots.Add(arr);
                        }
                    }
                }
            }
            field.minX = minX;
            field.minY = minY;
            field.matrix = matrix;

            ShowMatr(field, sizeX, sizeY);
            
            return field;
        }

        static void ShowMatr(FieldData data, int sizeX, int sizeY, int[] robotPos = null)
        {
            byte pos = 0;
            if (robotPos != null)
            {
                pos = data.matrix[robotPos[0], robotPos[1]];
                data.matrix[robotPos[0], robotPos[1]] = 2;
                Console.WriteLine("RobotPos: x = " + robotPos[0] + ", y = " + robotPos[1]);
            }
            for (int i = 0; i < sizeY; i++)
            {
                for (int j = 0; j < sizeX; j++)
                {
                    Console.Write(data.matrix[j, i] + " ");
                }
                Console.WriteLine("");
            }
            Console.WriteLine("");
            if (robotPos != null)
            {
                data.matrix[robotPos[0], robotPos[1]] = pos;
            }
        }

        static bool IsDotVisited(int[] dot, List<int[]> visitedDots)
        {
            foreach (int[] visitedDot in visitedDots)
            {
                if (dot[0] == visitedDot[0] && dot[1] == visitedDot[1])
                {
                    return true;
                }
            }
            return false;
        }

        static void RobotMove(ref bool endOfMoving, ref int[] nowPosition, ref string whereToMove, string clockwise, 
            ref FieldData field, int funcCalls, int[] startDot)
        {
            try
            {

                Console.WriteLine("Direction - " + whereToMove);
                ShowMatr(field, field.matrix.GetLength(0), field.matrix.GetLength(1), nowPosition);

                if (funcCalls == 0)
                {
                    if (field.matrix[nowPosition[0], nowPosition[1]] == 1)
                    {
                        field.visitedDots.Add(new int[] { nowPosition[0], nowPosition[1] });
                    }
                    ChangePos(whereToMove, ref nowPosition);
                    return;

                }
                if (field.matrix[nowPosition[0], nowPosition[1]] == 1)
                {
                    if (IsDotVisited(nowPosition, field.visitedDots))
                    {
                        
                        if (field.visitedDots.Count == field.allDots.Count)
                        {
                            endOfMoving = true;
                            Console.WriteLine("Робот может достичь всех точек");
                            return;
                        }
                        else if (nowPosition[0] == startDot[0] && nowPosition[1] == startDot[1])
                        {
                            endOfMoving = true;
                            Console.WriteLine("Робот достиг начальной точки, все точки не достигнуты");
                            return;
                        }
                        
                    }
                    else
                    {
                        field.visitedDots.Add(new int[] {nowPosition[0], nowPosition[1]});
                        if (clockwise == "ccw")
                        {
                            switch (whereToMove)
                            {
                                case "Up": whereToMove = "Left";
                                    break;
                                case "Down": whereToMove = "Right";
                                    break;
                                case "Left": whereToMove = "Down";
                                    break;
                                case "Right": whereToMove = "Up";
                                    break;
                            }
                        }
                        else if (clockwise == "cw")
                        {
                            switch (whereToMove)
                            {
                                case "Up": whereToMove = "Right";
                                    break;
                                case "Down": whereToMove = "Left";
                                    break;
                                case "Left": whereToMove = "Up";
                                    break;
                                case "Right": whereToMove = "Down";
                                    break;
                            }
                        }
                        
                    }
                }
            }
            catch (IndexOutOfRangeException)
            {
                endOfMoving = true;
                Console.WriteLine("Робот не может достичь всех точек");
            }
            ChangePos(whereToMove, ref nowPosition);
        }

        static void ChangePos(string whereToMove, ref int[] nowPosition)
        {
            switch (whereToMove)
            {
                case "Up": nowPosition[1]--;
                    break;
                case "Down": nowPosition[1]++;
                    break;
                case "Left": nowPosition[0]--;
                    break;
                case "Right": nowPosition[0]++;
                    break;
                default:
                    whereToMove += "Ahaa";
                    break;
            }
        }

    }
}
