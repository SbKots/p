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
        // Считываем все строки из файла
        string[] lines = File.ReadAllLines("input1.txt");

        // Первая строка - размер таблицы
        int[] size = Nums(lines[0]);
        int m = size[0];
        int n = size[1];

        // Запасы поставщиков и потребности покупателей
        int[] a = Nums(lines[1]);
        int[] b = Nums(lines[2]);

        // c - стоимости, x - будущий план
        int[,] c = new int[m, n];
        int[,] x = new int[m, n];

        // Заполняем таблицу стоимостей
        for (int i = 0; i < m; i++)
        {
            int[] row = Nums(lines[i + 3]);

            for (int j = 0; j < n; j++)
            {
                c[i, j] = row[j];
            }
        }
        int r = 0;
        int col = 0;

        // Идем с левого верхнего угла
        while (r < m && col < n)
        {
            int v;

            // Берем минимальное из запаса и потребности
            if (a[r] < b[col])
                v = a[r];
            else
                v = b[col];

            // Записываем перевозку в план
            x[r, col] = v;
            a[r] -= v;
            b[col] -= v;

            // Если строка или столбец закончились - идем дальше
            if (a[r] == 0) r++;
            if (col < n && b[col] == 0) col++;
        }

        // Считаем итоговую стоимость
        int sum = 0;
        for (int i = 0; i < m; i++)
            for (int j = 0; j < n; j++)
                sum += x[i, j] * c[i, j];

        // Записываем ответ в файл
        StreamWriter f = new StreamWriter("output1.txt");
        f.WriteLine("Метод северо-западного угла");
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
Пример input1.txt:
4 5
14 14 14 14
13 5 13 12 13
16 26 12 24 3
5 2 19 27 2
29 23 25 16 8
2 25 14 15 21
*/
