using System;
using System.IO;

class Program
{
    // Переводим строку из файла в массив чисел
    static int[] Nums(string s)
    {
        string[] parts = s.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
        int[] nums = new int[parts.Length];

        for (int i = 0; i < parts.Length; i++)
        {
            nums[i] = int.Parse(parts[i]);
        }
        return nums;
    }
    static void Main()
    {
        // Читаем весь файл
        string[] lines = File.ReadAllLines("input3.txt");

        // Количество вершин и стартовая вершина
        int[] first = Nums(lines[0]);
        int n = first[0];
        int start = first[1] - 1;

        // Матрица графа
        int[,] g = new int[n, n];
        for (int i = 0; i < n; i++)
        {
            int[] row = Nums(lines[i + 1]);

            for (int j = 0; j < n; j++)
            {
                g[i, j] = row[j];
            }
        }

        // Большое число вместо бесконечности
        int inf = 1000000000;
        int[] d = new int[n];
        bool[] used = new bool[n];

        // Сначала все расстояния неизвестны
        for (int i = 0; i < n; i++)
        {
            d[i] = inf;
        }

        // До стартовой вершины расстояние 0
        d[start] = 0;

        // Основной цикл Дейкстры
        for (int step = 0; step < n; step++)
        {
            int v = -1;

            // Ищем непосещенную вершину с минимальным расстоянием
            for (int i = 0; i < n; i++)
            {
                if (!used[i] && (v == -1 || d[i] < d[v]))
                {
                    v = i;
                }
            }

            // Если подходящей вершины нет - заканчиваем
            if (v == -1 || d[v] == inf) break;

            used[v] = true;

            // Пробуем улучшить расстояния до соседей
            for (int to = 0; to < n; to++)
            {
                if (g[v, to] > 0 && d[v] + g[v, to] < d[to])
                {
                    d[to] = d[v] + g[v, to];
                }
            }
        }

        // Записываем ответ в файл
        StreamWriter f = new StreamWriter("output3.txt");
        f.WriteLine("Алгоритм Дейкстры");
        for (int i = 0; i < n; i++)
        {
            if (d[i] == inf)
                f.WriteLine("До вершины " + (i + 1) + ": пути нет");
            else
                f.WriteLine("До вершины " + (i + 1) + ": " + d[i]);
        }
        f.Close();
    }
}

/*
Пример input3.txt:
6 1
0 7 9 0 0 14
7 0 10 15 0 0
9 10 0 11 0 2
0 15 11 0 6 0
0 0 0 6 0 9
14 0 2 0 9 0
*/
