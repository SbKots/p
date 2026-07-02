<template>
  <main class="page">
    <button
      v-for="item in methods"
      :key="item.id"
      :title="item.title"
      :aria-label="item.title"
      :class="{ active: copied === item.id }"
      @click="copyText(item)"
    ></button>
  </main>
</template>

<script setup>
import { ref } from 'vue'

const copied = ref(0)

const methods = [
  {
    id: 1,
    title: 'Метод северо-западного угла',
    code: String.raw`using System;
using System.IO;

class Program
{
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
        string[] lines = File.ReadAllLines("input1.txt");
        int[] size = Nums(lines[0]);
        int m = size[0];
        int n = size[1];
        int[] a = Nums(lines[1]);
        int[] b = Nums(lines[2]);
        int[,] c = new int[m, n];
        int[,] x = new int[m, n];
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
        while (r < m && col < n)
        {
            int v;

            if (a[r] < b[col])
                v = a[r];
            else
                v = b[col];

            x[r, col] = v;
            a[r] -= v;
            b[col] -= v;

            if (a[r] == 0) r++;
            if (col < n && b[col] == 0) col++;
        }
        int sum = 0;
        for (int i = 0; i < m; i++)
            for (int j = 0; j < n; j++)
                sum += x[i, j] * c[i, j];
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
}`
  },
  {
    id: 2,
    title: 'Метод минимального элемента',
    code: String.raw`using System;
using System.IO;
class Program
{
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
        string[] lines = File.ReadAllLines("input2.txt");
        int[] size = Nums(lines[0]);
        int m = size[0];
        int n = size[1];
        int[] a = Nums(lines[1]);
        int[] b = Nums(lines[2]);
        int[,] c = new int[m, n];
        int[,] x = new int[m, n];
        for (int i = 0; i < m; i++)
        {
            int[] row = Nums(lines[i + 3]);

            for (int j = 0; j < n; j++)
            {
                c[i, j] = row[j];
            }
        }
        while (true)
        {
            int min = 1000000;
            int r = -1;
            int col = -1;

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
            if (r == -1) break;
            int v;
            if (a[r] < b[col])
                v = a[r];
            else
                v = b[col];

            x[r, col] = v;
            a[r] -= v;
            b[col] -= v;
        }
        int sum = 0;
        for (int i = 0; i < m; i++)
            for (int j = 0; j < n; j++)
                sum += x[i, j] * c[i, j];
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
}`
  },
  {
    id: 3,
    title: 'Алгоритм Дейкстры',
    code: String.raw`using System;
using System.IO;

class Program
{
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
        string[] lines = File.ReadAllLines("input3.txt");
        int[] first = Nums(lines[0]);
        int n = first[0];
        int start = first[1] - 1;
        int[,] g = new int[n, n];
        for (int i = 0; i < n; i++)
        {
            int[] row = Nums(lines[i + 1]);

            for (int j = 0; j < n; j++)
            {
                g[i, j] = row[j];
            }
        }
        int inf = 1000000000;
        int[] d = new int[n];
        bool[] used = new bool[n];

        for (int i = 0; i < n; i++)
        {
            d[i] = inf;
        }
        d[start] = 0;
        for (int step = 0; step < n; step++)
        {
            int v = -1;

            for (int i = 0; i < n; i++)
            {
                if (!used[i] && (v == -1 || d[i] < d[v]))
                {
                    v = i;
                }
            }

            if (v == -1 || d[v] == inf) break;

            used[v] = true;

            for (int to = 0; to < n; to++)
            {
                if (g[v, to] > 0 && d[v] + g[v, to] < d[to])
                {
                    d[to] = d[v] + g[v, to];
                }
            }
        }
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
}`
  },
  {
    id: 4,
    title: 'Составление кода Прюфера',
    code: String.raw`using System;
using System.IO;

class Program
{
    static void Main()
    {
        string[] lines = File.ReadAllLines("input4.txt");
        int n = 0;
        for (int i = 0; i < lines.Length; i++)
        {
            if (lines[i].Trim() == "") continue;
            string[] p = lines[i].Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            int a = int.Parse(p[0]);
            int b = int.Parse(p[1]);
            if (a > n) n = a;
            if (b > n) n = b;
        }
        int[,] g = new int[n + 1, n + 1];
        int[] deg = new int[n + 1];
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
        string code = "";
        for (int step = 0; step < n - 2; step++)
        {
            int leaf = 1;
            while (deg[leaf] != 1)
            {
                leaf++;
            }
            int next = 1;
            while (g[leaf, next] == 0)
            {
                next++;
            }
            if (code != "") code += " ";
            code += next;
            g[leaf, next] = 0;
            g[next, leaf] = 0;
            deg[leaf]--;
            deg[next]--;
        }
        File.WriteAllText("output4.txt", "Код Прюфера: " + code);
    }
}`
  }
]

async function copyText(item) {
  try {
    await navigator.clipboard.writeText(item.code)
  } catch {
    let area = document.createElement('textarea')
    area.value = item.code
    document.body.appendChild(area)
    area.select()
    document.execCommand('copy')
    document.body.removeChild(area)
  }

  copied.value = item.id

  setTimeout(() => {
    copied.value = 0
  }, 1200)
}
</script>

<style>
* {
  box-sizing: border-box;
}

body {
  margin: 0;
  background: #fbfbfa;
  font-family: Arial, sans-serif;
}

.page {
  min-height: 100vh;
  display: flex;
  gap: 10px;
  padding: 14px;
}

button {
  border: 0;
  background: #fbfbfa;
  border-radius: 12px;
  width: 42px;
  height: 34px;
  cursor: pointer;
}

button:hover {
  background: #f5f5f4;
}

.active {
  background: #f0f0ef;
}
</style>
