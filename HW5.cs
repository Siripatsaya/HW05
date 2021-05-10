using System;

namespace ConsoleApp20
{
    class Program
    {
        static string inputPath = "";
        static string convolutionPath = "";
        static string outputPath = "";
        static void Main(string[] args)
        {

            Console.WriteLine("Input your file input");
            inputPath = Console.ReadLine();

            Console.WriteLine("Input your file convolution");
            convolutionPath = Console.ReadLine();

            Console.WriteLine("Input your file output");
            outputPath = Console.ReadLine();

            double[,] arrayImg = ReadImageDataFromFile(inputPath);
            double[,] arrayImg_temp = new double[arrayImg.GetLength(0) + 2, arrayImg.GetLength(1) + 2];
            for (int i = 0; i < (arrayImg.GetLength(1) + 2); i++)
            {
                for (int j = 0; j < (arrayImg.GetLength(0) + 2); j++)
                {
                    try
                    {
                        if (i == 0 && j == 0)
                        {
                            arrayImg_temp[i, j] = arrayImg[arrayImg.GetLength(0) - 1, arrayImg.GetLength(1) - 1];
                        }
                        else if (i == 0 && j == (arrayImg.GetLength(0) + 1))
                        {
                            arrayImg_temp[i, j] = arrayImg[arrayImg.GetLength(0) - 1, 0];
                        }
                        else if (i == (arrayImg.GetLength(1) + 1) && j == 0)
                        {
                            arrayImg_temp[i, j] = arrayImg[0, arrayImg.GetLength(1) - 1];
                        }
                        else if (i == (arrayImg.GetLength(1) + 1) && j == (arrayImg.GetLength(0) + 1))
                        {
                            arrayImg_temp[i, j] = arrayImg[0, 0];
                        }
                        else if (i >= 1 && j >= 1 && j <= (arrayImg.GetLength(1) + 2))
                        {

                            arrayImg_temp[i, j] = arrayImg[i - 1, j - 1];
                        }
                    }
                    catch (Exception e)
                    {

                    }
                }
            }
            for (int i = 1; i < (arrayImg.GetLength(1) + 1); i++)
            {
                arrayImg_temp[i, 0] = arrayImg[i - 1, (arrayImg.GetLength(0) - 1)];
                arrayImg_temp[i, (arrayImg.GetLength(1) + 1)] = arrayImg[i - 1, 0];
                arrayImg_temp[(arrayImg.GetLength(1) + 1), i] = arrayImg[0, i - 1];
                arrayImg_temp[0, i] = arrayImg[(arrayImg.GetLength(0) - 1), i - 1];
            }

            double[,] arrayConvo = ReadImageDataFromFile(convolutionPath);
            double[,] outFile = new double[arrayImg.GetLength(0), arrayImg.GetLength(0)];
            double[] arrayConvo_1d = Convert1D(arrayConvo);
            for (int i = 1; i < arrayImg.GetLength(0) + 1; i++)
            {

                for (int j = 1; j < arrayImg.GetLength(0) + 1; j++)
                {
                    double sum = 0;
                    sum += arrayImg_temp[i - 1, j - 1] * arrayConvo_1d[0];
                    sum += arrayImg_temp[i - 1, j] * arrayConvo_1d[1];
                    sum += arrayImg_temp[i - 1, j + 1] * arrayConvo_1d[2];
                    sum += arrayImg_temp[i, j - 1] * arrayConvo_1d[3];
                    sum += arrayImg_temp[i, j] * arrayConvo_1d[4];
                    sum += arrayImg_temp[i, j + 1] * arrayConvo_1d[5];
                    sum += arrayImg_temp[i + 1, j - 1] * arrayConvo_1d[6];
                    sum += arrayImg_temp[i + 1, j] * arrayConvo_1d[7];
                    sum += arrayImg_temp[i + 1, j + 1] * arrayConvo_1d[8];

                    outFile[i - 1, j - 1] = sum;
                    Console.Write(sum + " ");

                }
                Console.WriteLine(" ");
            }
            WriteImageDataToFile(outputPath, outFile);
        }
        static double[] Convert1D(double[,] array_list)
        {
            int count = 0;
            double[] array_temp = new double[array_list.GetLength(0) * array_list.GetLength(1)];
            for (int i = 0; i < (array_list.GetLength(1)); i++)
            {
                for (int j = 0; j < (array_list.GetLength(0)); j++)
                {
                    array_temp[count] = array_list[i, j];
                    count += 1;
                }
            }
            return array_temp;
        }
        static double[,] ReadImageDataFromFile(string imageDataFilePath)
        {
            string[] lines = System.IO.File.ReadAllLines(imageDataFilePath);
            int imageHeight = lines.Length;
            int imageWidth = lines[0].Split(',').Length;
            double[,] imageDataArray = new double[imageHeight, imageWidth];

            for (int i = 0; i < imageHeight; i++)
            {
                string[] items = lines[i].Split(',');
                for (int j = 0; j < imageWidth; j++)
                {
                    imageDataArray[i, j] = double.Parse(items[j]);
                }
            }
            return imageDataArray;
        }
        static void WriteImageDataToFile(string imageDataFilePath, double[,] imageDataArray)
        {
            string imageDataString = "";
            for (int i = 0; i < imageDataArray.GetLength(0); i++)
            {
                for (int j = 0; j < imageDataArray.GetLength(1) - 1; j++)
                {
                    imageDataString += imageDataArray[i, j] + ", ";
                }
                imageDataString += imageDataArray[i, imageDataArray.GetLength(1) - 1];
                imageDataString += "\n";
            }
            System.IO.File.WriteAllText(imageDataFilePath, imageDataString);
            Console.ReadLine();
        }
    }
}
