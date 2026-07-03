using System;
using System.IO;

class Program
{
    static void Main()
    {
        // Читаем ребра дерева из файла
        string[] lines = File.ReadAllLines("input4.txt");
        int n = 0;

        // Ищем самый большой номер вершины
        for (int i = 0; i < lines.Length; i++)
        {
            if (lines[i].Trim() == "") continue;
            string[] p = lines[i].Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            int a = int.Parse(p[0]);
            int b = int.Parse(p[1]);
            if (a > n) n = a;
            if (b > n) n = b;
        }

        // g - матрица смежности, deg - степени вершин
        int[,] g = new int[n + 1, n + 1];
        int[] deg = new int[n + 1];

        // Заполняем дерево
        for (int i = 0; i < lines.Length; i++)
        {
            if (lines[i].Trim() == "") continue;
            string[] p = lines[i].Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            int a = int.Parse(p[0]);
            int b = int.Parse(p[1]);
            g[a, b] = 1;
            g[b, a] = 1;
            deg[a]++;
            deg[b]++;
        }

        // Тут будет код Прюфера
        string code = "";

        // В коде Прюфера будет n - 2 числа
        for (int step = 0; step < n - 2; step++)
        {
            int leaf = 1;

            // Ищем самый маленький лист
            while (deg[leaf] != 1)
            {
                leaf++;
            }
            int next = 1;

            // Ищем соседа этого листа
            while (g[leaf, next] == 0)
            {
                next++;
            }

            // Добавляем соседа в код
            if (code != "") code += " ";
            code += next;

            // Удаляем лист из дерева
            g[leaf, next] = 0;
            g[next, leaf] = 0;
            deg[leaf]--;
            deg[next]--;
        }

        // Записываем готовый код в файл
        File.WriteAllText("output4.txt", "Код Прюфера: " + code);
    }
}

/*
Пример input4.txt:
1 2
1 3
3 4
3 5
5 6
*/
