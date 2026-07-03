using System;
using System.IO;
class Program
{
    // Переводим строку из файла в числа
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
        // Читаем данные из файла
        string[] lines = File.ReadAllLines("input2.txt");

        // Размер таблицы
        int[] size = Nums(lines[0]);
        int m = size[0];
        int n = size[1];

        // Запасы и потребности
        int[] a = Nums(lines[1]);
        int[] b = Nums(lines[2]);

        // c - стоимости, x - план
        int[,] c = new int[m, n];
        int[,] x = new int[m, n];

        // Заполняем стоимости
        for (int i = 0; i < m; i++)
        {
            int[] row = Nums(lines[i + 3]);

            for (int j = 0; j < n; j++)
            {
                c[i, j] = row[j];
            }
        }

        // Повторяем, пока можно что-то распределять
        while (true)
        {
            int min = 1000000;
            int r = -1;
            int col = -1;

            // Ищем самую дешевую свободную клетку
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (a[i] > 0 && b[j] > 0 && c[i, j] < min)
                    {
                        min = c[i, j];
                        r = i;
                        col = j;
                    }
                }
            }

            // Если клеток больше нет - выходим
            if (r == -1) break;

            // Берем минимальное из запаса и потребности
            int v;
            if (a[r] < b[col])
                v = a[r];
            else
                v = b[col];

            // Записываем перевозку
            x[r, col] = v;
            a[r] -= v;
            b[col] -= v;
        }

        // Считаем стоимость плана
        int sum = 0;
        for (int i = 0; i < m; i++)
            for (int j = 0; j < n; j++)
                sum += x[i, j] * c[i, j];

        // Записываем результат в файл
        StreamWriter f = new StreamWriter("output2.txt");
        f.WriteLine("Метод минимального элемента");
        f.WriteLine("План:");
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                f.Write(x[i, j] + " ");
            }

            f.WriteLine();
        }
        f.WriteLine("Стоимость = " + sum);
        f.Close();
    }
}

/*
Пример input2.txt:
4 5
14 14 14 14
13 5 13 12 13
16 26 12 24 3
5 2 19 27 2
29 23 25 16 8
2 25 14 15 21
*/
