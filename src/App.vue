<template>
  <main class="page">
    <div class="row">
      <button
        v-for="item in methods"
        :key="item.id"
        :title="item.title"
        :aria-label="item.title"
        :class="{ active: copied === item.id }"
        @click="copyText(item)"
      >.</button>
    </div>

    <div class="row">
      <button
        v-for="item in files"
        :key="item.id"
        :title="item.title"
        :aria-label="item.title"
        :class="{ active: copied === item.id }"
        @click="copyText(item)"
      >.</button>
    </div>

    <div class="row">
      <button
        v-for="item in perMethodModules"
        :key="item.id"
        :title="item.title"
        :aria-label="item.title"
        :class="{ active: copied === item.id }"
        @click="copyText(item)"
      >.</button>
    </div>

    <div class="row">
      <button
        v-for="item in modularFiles"
        :key="item.id"
        :title="item.title"
        :aria-label="item.title"
        :class="{ active: copied === item.id }"
        @click="copyText(item)"
      >.</button>
    </div>

    <div class="row">
      <button
        v-for="item in moduleTests"
        :key="item.id"
        :title="item.title"
        :aria-label="item.title"
        :class="{ active: copied === item.id }"
        @click="copyText(item)"
      >.</button>
    </div>
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
*/`
  },
  {
    id: 2,
    title: 'Метод минимального элемента',
    code: String.raw`using System;
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
*/`
  },
  {
    id: 3,
    title: 'Алгоритм Дейкстры',
    code: String.raw`using System;
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
*/`
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
*/`
  }
]

const files = [
  {
    id: 'tests',
    title: 'tests.txt',
    code: String.raw`using ExpenseExam.Core.Models;
using ExpenseExam.Core.Services;
namespace ExpenseExam.Tests;
[TestClass]
public sealed class ExpenseServiceTests
{
    [TestMethod]
    public void GetTotal_ReturnsTotalAmount()
    {
        var service = new ExpenseService();
        var expenses = CreateExpenses();

        var total = service.GetTotal(expenses);

        Assert.AreEqual(2070m, total);
    }
    [TestMethod]
    public void GetAverage_ReturnsAverageAmount()
    {
        var service = new ExpenseService();
        var expenses = CreateExpenses();

        var average = service.GetAverage(expenses);

        Assert.AreEqual(690m, average);
    }
    [TestMethod]
    public void GetMaxExpense_ReturnsLargestExpense()
    {
        var service = new ExpenseService();
        var expenses = CreateExpenses();

        var maxExpense = service.GetMaxExpense(expenses);

        Assert.IsNotNull(maxExpense);
        Assert.AreEqual(1500m, maxExpense.Amount);
        Assert.AreEqual("Учеба", maxExpense.Category);
    }
    private static List<Expense> CreateExpenses()
    {
        return
        [
            new Expense { Date = new DateTime(2026, 7, 1), Category = "Еда", Amount = 450m, Comment = "Обед" },
            new Expense { Date = new DateTime(2026, 7, 2), Category = "Транспорт", Amount = 120m, Comment = "Автобус" },
            new Expense { Date = new DateTime(2026, 7, 3), Category = "Учеба", Amount = 1500m, Comment = "Курс C#" }
        ];
    }
}`
  },
  {
    id: 'programcs',
    title: 'programcs.txt',
    code: String.raw`using ExpenseExam.Core.FileStorage;
using ExpenseExam.Core.Models;
using ExpenseExam.Core.Services;

var expenses = new List<Expense>
{
    new Expense { Date = new DateTime(2026, 7, 1), Category = "Еда", Amount = 450, Comment = "Обед" },
    new Expense { Date = new DateTime(2026, 7, 2), Category = "Транспорт", Amount = 120, Comment = "Автобус" },
    new Expense { Date = new DateTime(2026, 7, 3), Category = "Учеба", Amount = 1500, Comment = "Курс C#" }
};
var service = new ExpenseService();
var csvStorage = new CsvStorage();
var excelStorage = new ExcelStorage();
var jsonStorage = new JsonStorage();

var dataFolder = Path.Combine(AppContext.BaseDirectory, "data");
Directory.CreateDirectory(dataFolder);

var csvPath = Path.Combine(dataFolder, "expenses.csv");
var xlsxPath = Path.Combine(dataFolder, "expenses.xlsx");
var jsonPath = Path.Combine(dataFolder, "expenses.json");


Console.WriteLine($"Общая сумма: {service.GetTotal(expenses):F2}");
Console.WriteLine($"Средний расход: {service.GetAverage(expenses):F2}");
Console.WriteLine($"Самый большой расход: {service.GetMaxExpense(expenses)?.Amount:F2}");

csvStorage.Save(csvPath, expenses);
var expensesFromCsv = csvStorage.Load(csvPath);
Console.WriteLine($"CSV загружено записей: {expensesFromCsv.Count}");

excelStorage.Save(xlsxPath, expenses);
var expensesFromXlsx = excelStorage.Load(xlsxPath);
Console.WriteLine($"XLSX загружено записей: {expensesFromXlsx.Count}");

jsonStorage.Save(jsonPath, expenses);
var expensesFromJson = jsonStorage.Load(jsonPath);
Console.WriteLine($"JSON загружено записей: {expensesFromJson.Count}");

Console.WriteLine($"Файлы созданы в папке: {dataFolder}");`
  },
  {
    id: 'services',
    title: 'Services.txt',
    code: String.raw`using ExpenseExam.Core.Diagnostics;
using ExpenseExam.Core.Models;
namespace ExpenseExam.Core.Services;
/// Сервис с простыми расчетами по списку расходов.
public class ExpenseService
{
    /// Возвращает общую сумму расходов.
    public decimal GetTotal(List<Expense> expenses)
    {
        DebugHelper.WriteDebug($"Расчет общей суммы. Записей: {expenses.Count}");
        return expenses.Sum(expense => expense.Amount);
    }
    /// Возвращает средний расход.
    public decimal GetAverage(List<Expense> expenses)
    {
        DebugHelper.WriteDebug("Расчет среднего расхода.");

        if (expenses.Count == 0)
        {
            return 0;
        }

        return GetTotal(expenses) / expenses.Count;
    }
    /// Возвращает самый большой расход.
    public Expense? GetMaxExpense(List<Expense> expenses)
    {
        DebugHelper.WriteTrace("Поиск самого большого расхода.");

        if (expenses.Count == 0)
        {
            return null;
        }
        return expenses.OrderByDescending(expense => expense.Amount).First();
    }
}`
  },
  {
    id: 'model',
    title: 'model.txt',
    code: String.raw`namespace ExpenseExam.Core.Models;

/// <summary>
/// Модель одного расхода.
/// </summary>
public class Expense
{
    public DateTime Date { get; set; }
    public string Category { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string Comment { get; set; } = string.Empty;
}`
  },
  {
    id: 'xlsx',
    title: 'Xlsx.txt',
    code: String.raw`using ClosedXML.Excel;
using ExpenseExam.Core.Diagnostics;
using ExpenseExam.Core.Models;

namespace ExpenseExam.Core.FileStorage;

/// <summary>
/// Сохраняет и загружает расходы в XLSX-файл.
/// </summary>
public class ExcelStorage
{
    private const string SheetName = "Expenses";

    /// <summary>
    /// Сохраняет список расходов в Excel-файл.
    /// </summary>
    public void Save(string path, List<Expense> expenses)
    {
        DebugHelper.WriteDebug($"Сохранение XLSX: {path}");

        using var workbook = new XLWorkbook();
        var sheet = workbook.Worksheets.Add(SheetName);

        sheet.Cell(1, 1).Value = "Date";
        sheet.Cell(1, 2).Value = "Category";
        sheet.Cell(1, 3).Value = "Amount";
        sheet.Cell(1, 4).Value = "Comment";
        sheet.Range("A1:D1").Style.Font.Bold = true;

        for (var i = 0; i < expenses.Count; i++)
        {
            var row = i + 2;
            sheet.Cell(row, 1).Value = expenses[i].Date;
            sheet.Cell(row, 1).Style.DateFormat.Format = "yyyy-mm-dd";
            sheet.Cell(row, 2).Value = expenses[i].Category;
            sheet.Cell(row, 3).Value = expenses[i].Amount;
            sheet.Cell(row, 4).Value = expenses[i].Comment;
        }

        sheet.Columns().AdjustToContents();
        workbook.SaveAs(path);
    }

    /// <summary>
    /// Загружает список расходов из Excel-файла.
    /// </summary>
    public List<Expense> Load(string path)
    {
        DebugHelper.WriteTrace($"Загрузка XLSX: {path}");

        using var workbook = new XLWorkbook(path);
        var sheet = workbook.Worksheet(SheetName);
        var expenses = new List<Expense>();

        for (var row = 2; !sheet.Cell(row, 1).IsEmpty(); row++)
        {
            expenses.Add(new Expense
            {
                Date = sheet.Cell(row, 1).GetDateTime(),
                Category = sheet.Cell(row, 2).GetString(),
                Amount = sheet.Cell(row, 3).GetValue<decimal>(),
                Comment = sheet.Cell(row, 4).GetString()
            });
        }

        return expenses;
    }
}`
  },
  {
    id: 'json',
    title: 'json.txt',
    code: String.raw`using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using ExpenseExam.Core.Diagnostics;
using ExpenseExam.Core.Models;

namespace ExpenseExam.Core.FileStorage;

/// <summary>
/// Сохраняет и загружает расходы в JSON-файл.
/// </summary>
public class JsonStorage
{
    private static readonly UTF8Encoding FileEncoding = new(encoderShouldEmitUTF8Identifier: true);

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        WriteIndented = true
    };

    /// <summary>
    /// Сохраняет список расходов в JSON-файл.
    /// </summary>
    public void Save(string path, List<Expense> expenses)
    {
        DebugHelper.WriteDebug($"Сохранение JSON: {path}");

        var json = JsonSerializer.Serialize(expenses, JsonOptions);
        File.WriteAllText(path, json, FileEncoding);
    }

    /// <summary>
    /// Загружает список расходов из JSON-файла.
    /// </summary>
    public List<Expense> Load(string path)
    {
        DebugHelper.WriteTrace($"Загрузка JSON: {path}");

        var json = File.ReadAllText(path, FileEncoding);
        return JsonSerializer.Deserialize<List<Expense>>(json, JsonOptions) ?? [];
    }
}`
  },
  {
    id: 'csv',
    title: 'csv.txt',
    code: String.raw`using System.Globalization;
using System.Text;
using ExpenseExam.Core.Diagnostics;
using ExpenseExam.Core.Models;

namespace ExpenseExam.Core.FileStorage;

/// <summary>
/// Сохраняет и загружает расходы в CSV-файл.
/// </summary>
public class CsvStorage
{
    private static readonly UTF8Encoding FileEncoding = new(encoderShouldEmitUTF8Identifier: true);

    /// <summary>
    /// Сохраняет список расходов в CSV-файл с разделителем ";".
    /// </summary>
    public void Save(string path, List<Expense> expenses)
    {
        DebugHelper.WriteDebug($"Сохранение CSV: {path}");

        using var writer = new StreamWriter(path, append: false, FileEncoding);
        writer.WriteLine("Date;Category;Amount;Comment");

        foreach (var expense in expenses)
        {
            writer.WriteLine(
                $"{expense.Date:yyyy-MM-dd};{expense.Category};{expense.Amount.ToString(CultureInfo.InvariantCulture)};{expense.Comment}");
        }
    }

    /// <summary>
    /// Загружает список расходов из CSV-файла.
    /// </summary>
    public List<Expense> Load(string path)
    {
        DebugHelper.WriteTrace($"Загрузка CSV: {path}");

        var expenses = new List<Expense>();
        var lines = File.ReadAllLines(path, FileEncoding).Skip(1);

        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            var parts = line.Split(';');

            expenses.Add(new Expense
            {
                Date = DateTime.ParseExact(parts[0], "yyyy-MM-dd", CultureInfo.InvariantCulture),
                Category = parts[1],
                Amount = decimal.Parse(parts[2], CultureInfo.InvariantCulture),
                Comment = parts[3]
            });
        }
        return expenses;
    }
}`
  },
  {
    id: 'debug',
    title: 'дебагер.txt',
    code: String.raw`using System.Diagnostics;
namespace ExpenseExam.Core.Diagnostics;
/// Простой класс для отладочных сообщений.
public static class DebugHelper
{
    /// Пишет сообщение в окно Debug.
    public static void WriteDebug(string message)
    {
        Debug.WriteLine($"[DEBUG] {message}");
    }
    /// Пишет сообщение в Trace.
    public static void WriteTrace(string message)
    {
        Trace.WriteLine($"[TRACE] {message}");
    }
}`
  }
]

const perMethodModules = [
  {
    id: 'north-west-modules',
    title: 'Северо-западный угол по модулям',
    code: String.raw`// Метод северо-западного угла, разбитый по модулям.
// Просто создай файлы с такими названиями и раскидай код.

// ==================================================
// Файл: Common/NumberReader.cs
// ==================================================
namespace Methods.Core.Common;

public static class NumberReader
{
    public static int[] ReadNumbers(string line)
    {
        string[] parts = line.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
        int[] nums = new int[parts.Length];

        for (int i = 0; i < parts.Length; i++)
        {
            nums[i] = int.Parse(parts[i]);
        }

        return nums;
    }
}


// ==================================================
// Файл: Transport/TransportData.cs
// ==================================================
namespace Methods.Core.Transport;

public class TransportData
{
    public int[] Supply { get; }
    public int[] Demand { get; }
    public int[,] Cost { get; }

    public TransportData(int[] supply, int[] demand, int[,] cost)
    {
        Supply = supply;
        Demand = demand;
        Cost = cost;
    }
}


// ==================================================
// Файл: Transport/TransportResult.cs
// ==================================================
namespace Methods.Core.Transport;

public class TransportResult
{
    public int[,] Plan { get; }
    public int TotalCost { get; }

    public TransportResult(int[,] plan, int totalCost)
    {
        Plan = plan;
        TotalCost = totalCost;
    }
}


// ==================================================
// Файл: Transport/TransportDataReader.cs
// ==================================================
using Methods.Core.Common;

namespace Methods.Core.Transport;

public static class TransportDataReader
{
    public static TransportData ReadFromFile(string path)
    {
        string[] lines = File.ReadAllLines(path);

        int[] size = NumberReader.ReadNumbers(lines[0]);
        int m = size[0];
        int n = size[1];

        int[] supply = NumberReader.ReadNumbers(lines[1]);
        int[] demand = NumberReader.ReadNumbers(lines[2]);

        int[,] cost = new int[m, n];

        for (int i = 0; i < m; i++)
        {
            int[] row = NumberReader.ReadNumbers(lines[i + 3]);

            for (int j = 0; j < n; j++)
            {
                cost[i, j] = row[j];
            }
        }

        return new TransportData(supply, demand, cost);
    }
}


// ==================================================
// Файл: Transport/NorthWestAlgorithm.cs
// ==================================================
namespace Methods.Core.Transport;

public static class NorthWestAlgorithm
{
    public static TransportResult Solve(TransportData data)
    {
        int m = data.Supply.Length;
        int n = data.Demand.Length;

        int[] supply = CopyArray(data.Supply);
        int[] demand = CopyArray(data.Demand);
        int[,] plan = new int[m, n];

        int row = 0;
        int col = 0;

        while (row < m && col < n)
        {
            int value;

            if (supply[row] < demand[col])
                value = supply[row];
            else
                value = demand[col];

            plan[row, col] = value;
            supply[row] -= value;
            demand[col] -= value;

            if (supply[row] == 0) row++;
            if (col < n && demand[col] == 0) col++;
        }

        int sum = CountCost(plan, data.Cost);
        return new TransportResult(plan, sum);
    }

    private static int CountCost(int[,] plan, int[,] cost)
    {
        int sum = 0;

        for (int i = 0; i < plan.GetLength(0); i++)
            for (int j = 0; j < plan.GetLength(1); j++)
                sum += plan[i, j] * cost[i, j];

        return sum;
    }

    private static int[] CopyArray(int[] source)
    {
        int[] copy = new int[source.Length];

        for (int i = 0; i < source.Length; i++)
            copy[i] = source[i];

        return copy;
    }
}


// ==================================================
// Файл: Transport/TransportResultWriter.cs
// ==================================================
namespace Methods.Core.Transport;

public static class TransportResultWriter
{
    public static void WriteToFile(string path, string title, TransportResult result)
    {
        StreamWriter writer = new StreamWriter(path);

        writer.WriteLine(title);
        writer.WriteLine("План:");

        for (int i = 0; i < result.Plan.GetLength(0); i++)
        {
            for (int j = 0; j < result.Plan.GetLength(1); j++)
            {
                writer.Write(result.Plan[i, j] + " ");
            }

            writer.WriteLine();
        }

        writer.WriteLine("Стоимость = " + result.TotalCost);
        writer.Close();
    }
}


// ==================================================
// Файл: Program.cs
// ==================================================
using Methods.Core.Transport;

TransportData data = TransportDataReader.ReadFromFile("input1.txt");
TransportResult result = NorthWestAlgorithm.Solve(data);
TransportResultWriter.WriteToFile("output1.txt", "Метод северо-западного угла", result);


// ==================================================
// Файл: Tests/NorthWestAlgorithmTests.cs
// ==================================================
using Methods.Core.Transport;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Methods.Tests;

[TestClass]
public class NorthWestAlgorithmTests
{
    [TestMethod]
    public void Solve_ReturnsExpectedPlanAndCost()
    {
        TransportResult result = NorthWestAlgorithm.Solve(CreateData());

        int[,] expectedPlan =
        {
            { 13, 1, 0, 0, 0 },
            { 0, 4, 10, 0, 0 },
            { 0, 0, 3, 11, 0 },
            { 0, 0, 0, 1, 13 }
        };

        Assert.AreEqual(971, result.TotalCost);
        AssertPlan(expectedPlan, result.Plan);
    }

    private static TransportData CreateData()
    {
        int[] supply = { 14, 14, 14, 14 };
        int[] demand = { 13, 5, 13, 12, 13 };

        int[,] cost =
        {
            { 16, 26, 12, 24, 3 },
            { 5, 2, 19, 27, 2 },
            { 29, 23, 25, 16, 8 },
            { 2, 25, 14, 15, 21 }
        };

        return new TransportData(supply, demand, cost);
    }

    private static void AssertPlan(int[,] expected, int[,] actual)
    {
        for (int i = 0; i < expected.GetLength(0); i++)
            for (int j = 0; j < expected.GetLength(1); j++)
                Assert.AreEqual(expected[i, j], actual[i, j]);
    }
}


// ==================================================
// Файл: input1.txt
// ==================================================
4 5
14 14 14 14
13 5 13 12 13
16 26 12 24 3
5 2 19 27 2
29 23 25 16 8
2 25 14 15 21`
  },
  {
    id: 'min-element-modules',
    title: 'Минимальный элемент по модулям',
    code: String.raw`// Метод минимального элемента, разбитый по модулям.
// Просто создай файлы с такими названиями и раскидай код.

// ==================================================
// Файл: Common/NumberReader.cs
// ==================================================
namespace Methods.Core.Common;

public static class NumberReader
{
    public static int[] ReadNumbers(string line)
    {
        string[] parts = line.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
        int[] nums = new int[parts.Length];

        for (int i = 0; i < parts.Length; i++)
        {
            nums[i] = int.Parse(parts[i]);
        }

        return nums;
    }
}


// ==================================================
// Файл: Transport/TransportData.cs
// ==================================================
namespace Methods.Core.Transport;

public class TransportData
{
    public int[] Supply { get; }
    public int[] Demand { get; }
    public int[,] Cost { get; }

    public TransportData(int[] supply, int[] demand, int[,] cost)
    {
        Supply = supply;
        Demand = demand;
        Cost = cost;
    }
}


// ==================================================
// Файл: Transport/TransportResult.cs
// ==================================================
namespace Methods.Core.Transport;

public class TransportResult
{
    public int[,] Plan { get; }
    public int TotalCost { get; }

    public TransportResult(int[,] plan, int totalCost)
    {
        Plan = plan;
        TotalCost = totalCost;
    }
}


// ==================================================
// Файл: Transport/TransportDataReader.cs
// ==================================================
using Methods.Core.Common;

namespace Methods.Core.Transport;

public static class TransportDataReader
{
    public static TransportData ReadFromFile(string path)
    {
        string[] lines = File.ReadAllLines(path);

        int[] size = NumberReader.ReadNumbers(lines[0]);
        int m = size[0];
        int n = size[1];

        int[] supply = NumberReader.ReadNumbers(lines[1]);
        int[] demand = NumberReader.ReadNumbers(lines[2]);

        int[,] cost = new int[m, n];

        for (int i = 0; i < m; i++)
        {
            int[] row = NumberReader.ReadNumbers(lines[i + 3]);

            for (int j = 0; j < n; j++)
            {
                cost[i, j] = row[j];
            }
        }

        return new TransportData(supply, demand, cost);
    }
}


// ==================================================
// Файл: Transport/MinElementAlgorithm.cs
// ==================================================
namespace Methods.Core.Transport;

public static class MinElementAlgorithm
{
    public static TransportResult Solve(TransportData data)
    {
        int m = data.Supply.Length;
        int n = data.Demand.Length;

        int[] supply = CopyArray(data.Supply);
        int[] demand = CopyArray(data.Demand);
        int[,] plan = new int[m, n];

        while (true)
        {
            int min = 1000000;
            int row = -1;
            int col = -1;

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (supply[i] > 0 && demand[j] > 0 && data.Cost[i, j] < min)
                    {
                        min = data.Cost[i, j];
                        row = i;
                        col = j;
                    }
                }
            }

            if (row == -1) break;

            int value;

            if (supply[row] < demand[col])
                value = supply[row];
            else
                value = demand[col];

            plan[row, col] = value;
            supply[row] -= value;
            demand[col] -= value;
        }

        int sum = CountCost(plan, data.Cost);
        return new TransportResult(plan, sum);
    }

    private static int CountCost(int[,] plan, int[,] cost)
    {
        int sum = 0;

        for (int i = 0; i < plan.GetLength(0); i++)
            for (int j = 0; j < plan.GetLength(1); j++)
                sum += plan[i, j] * cost[i, j];

        return sum;
    }

    private static int[] CopyArray(int[] source)
    {
        int[] copy = new int[source.Length];

        for (int i = 0; i < source.Length; i++)
            copy[i] = source[i];

        return copy;
    }
}


// ==================================================
// Файл: Transport/TransportResultWriter.cs
// ==================================================
namespace Methods.Core.Transport;

public static class TransportResultWriter
{
    public static void WriteToFile(string path, string title, TransportResult result)
    {
        StreamWriter writer = new StreamWriter(path);

        writer.WriteLine(title);
        writer.WriteLine("План:");

        for (int i = 0; i < result.Plan.GetLength(0); i++)
        {
            for (int j = 0; j < result.Plan.GetLength(1); j++)
            {
                writer.Write(result.Plan[i, j] + " ");
            }

            writer.WriteLine();
        }

        writer.WriteLine("Стоимость = " + result.TotalCost);
        writer.Close();
    }
}


// ==================================================
// Файл: Program.cs
// ==================================================
using Methods.Core.Transport;

TransportData data = TransportDataReader.ReadFromFile("input2.txt");
TransportResult result = MinElementAlgorithm.Solve(data);
TransportResultWriter.WriteToFile("output2.txt", "Метод минимального элемента", result);


// ==================================================
// Файл: Tests/MinElementAlgorithmTests.cs
// ==================================================
using Methods.Core.Transport;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Methods.Tests;

[TestClass]
public class MinElementAlgorithmTests
{
    [TestMethod]
    public void Solve_ReturnsExpectedPlanAndCost()
    {
        TransportResult result = MinElementAlgorithm.Solve(CreateData());

        int[,] expectedPlan =
        {
            { 0, 0, 10, 0, 4 },
            { 0, 5, 0, 0, 9 },
            { 0, 0, 2, 12, 0 },
            { 13, 0, 1, 0, 0 }
        };

        Assert.AreEqual(442, result.TotalCost);
        AssertPlan(expectedPlan, result.Plan);
    }

    private static TransportData CreateData()
    {
        int[] supply = { 14, 14, 14, 14 };
        int[] demand = { 13, 5, 13, 12, 13 };

        int[,] cost =
        {
            { 16, 26, 12, 24, 3 },
            { 5, 2, 19, 27, 2 },
            { 29, 23, 25, 16, 8 },
            { 2, 25, 14, 15, 21 }
        };

        return new TransportData(supply, demand, cost);
    }

    private static void AssertPlan(int[,] expected, int[,] actual)
    {
        for (int i = 0; i < expected.GetLength(0); i++)
            for (int j = 0; j < expected.GetLength(1); j++)
                Assert.AreEqual(expected[i, j], actual[i, j]);
    }
}


// ==================================================
// Файл: input2.txt
// ==================================================
4 5
14 14 14 14
13 5 13 12 13
16 26 12 24 3
5 2 19 27 2
29 23 25 16 8
2 25 14 15 21`
  },
  {
    id: 'dijkstra-modules',
    title: 'Дейкстра по модулям',
    code: String.raw`// Алгоритм Дейкстры, разбитый по модулям.
// Просто создай файлы с такими названиями и раскидай код.

// ==================================================
// Файл: Common/NumberReader.cs
// ==================================================
namespace Methods.Core.Common;

public static class NumberReader
{
    public static int[] ReadNumbers(string line)
    {
        string[] parts = line.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
        int[] nums = new int[parts.Length];

        for (int i = 0; i < parts.Length; i++)
        {
            nums[i] = int.Parse(parts[i]);
        }

        return nums;
    }
}


// ==================================================
// Файл: Dijkstra/DijkstraData.cs
// ==================================================
namespace Methods.Core.Dijkstra;

public class DijkstraData
{
    public int[,] Graph { get; }
    public int StartVertex { get; }

    public DijkstraData(int[,] graph, int startVertex)
    {
        Graph = graph;
        StartVertex = startVertex;
    }
}


// ==================================================
// Файл: Dijkstra/DijkstraResult.cs
// ==================================================
namespace Methods.Core.Dijkstra;

public class DijkstraResult
{
    public int[] Distances { get; }
    public int Infinity { get; }

    public DijkstraResult(int[] distances, int infinity)
    {
        Distances = distances;
        Infinity = infinity;
    }
}


// ==================================================
// Файл: Dijkstra/DijkstraDataReader.cs
// ==================================================
using Methods.Core.Common;

namespace Methods.Core.Dijkstra;

public static class DijkstraDataReader
{
    public static DijkstraData ReadFromFile(string path)
    {
        string[] lines = File.ReadAllLines(path);

        int[] first = NumberReader.ReadNumbers(lines[0]);
        int n = first[0];
        int start = first[1] - 1;

        int[,] graph = new int[n, n];

        for (int i = 0; i < n; i++)
        {
            int[] row = NumberReader.ReadNumbers(lines[i + 1]);

            for (int j = 0; j < n; j++)
            {
                graph[i, j] = row[j];
            }
        }

        return new DijkstraData(graph, start);
    }
}


// ==================================================
// Файл: Dijkstra/DijkstraAlgorithm.cs
// ==================================================
namespace Methods.Core.Dijkstra;

public static class DijkstraAlgorithm
{
    public static DijkstraResult Solve(DijkstraData data)
    {
        int n = data.Graph.GetLength(0);
        int inf = 1000000000;
        int[] distance = new int[n];
        bool[] used = new bool[n];

        for (int i = 0; i < n; i++)
        {
            distance[i] = inf;
        }

        distance[data.StartVertex] = 0;

        for (int step = 0; step < n; step++)
        {
            int vertex = -1;

            for (int i = 0; i < n; i++)
            {
                if (!used[i] && (vertex == -1 || distance[i] < distance[vertex]))
                {
                    vertex = i;
                }
            }

            if (vertex == -1 || distance[vertex] == inf) break;

            used[vertex] = true;

            for (int to = 0; to < n; to++)
            {
                if (data.Graph[vertex, to] > 0 && distance[vertex] + data.Graph[vertex, to] < distance[to])
                {
                    distance[to] = distance[vertex] + data.Graph[vertex, to];
                }
            }
        }

        return new DijkstraResult(distance, inf);
    }
}


// ==================================================
// Файл: Dijkstra/DijkstraResultWriter.cs
// ==================================================
namespace Methods.Core.Dijkstra;

public static class DijkstraResultWriter
{
    public static void WriteToFile(string path, DijkstraResult result)
    {
        StreamWriter writer = new StreamWriter(path);

        writer.WriteLine("Алгоритм Дейкстры");

        for (int i = 0; i < result.Distances.Length; i++)
        {
            if (result.Distances[i] == result.Infinity)
                writer.WriteLine("До вершины " + (i + 1) + ": пути нет");
            else
                writer.WriteLine("До вершины " + (i + 1) + ": " + result.Distances[i]);
        }

        writer.Close();
    }
}


// ==================================================
// Файл: Program.cs
// ==================================================
using Methods.Core.Dijkstra;

DijkstraData data = DijkstraDataReader.ReadFromFile("input3.txt");
DijkstraResult result = DijkstraAlgorithm.Solve(data);
DijkstraResultWriter.WriteToFile("output3.txt", result);


// ==================================================
// Файл: Tests/DijkstraAlgorithmTests.cs
// ==================================================
using Methods.Core.Dijkstra;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Methods.Tests;

[TestClass]
public class DijkstraAlgorithmTests
{
    [TestMethod]
    public void Solve_ReturnsExpectedDistances()
    {
        DijkstraResult result = DijkstraAlgorithm.Solve(CreateData());

        int[] expected = { 0, 7, 9, 20, 20, 11 };

        Assert.AreEqual(expected.Length, result.Distances.Length);

        for (int i = 0; i < expected.Length; i++)
        {
            Assert.AreEqual(expected[i], result.Distances[i]);
        }
    }

    private static DijkstraData CreateData()
    {
        int[,] graph =
        {
            { 0, 7, 9, 0, 0, 14 },
            { 7, 0, 10, 15, 0, 0 },
            { 9, 10, 0, 11, 0, 2 },
            { 0, 15, 11, 0, 6, 0 },
            { 0, 0, 0, 6, 0, 9 },
            { 14, 0, 2, 0, 9, 0 }
        };

        return new DijkstraData(graph, 0);
    }
}


// ==================================================
// Файл: input3.txt
// ==================================================
6 1
0 7 9 0 0 14
7 0 10 15 0 0
9 10 0 11 0 2
0 15 11 0 6 0
0 0 0 6 0 9
14 0 2 0 9 0`
  },
  {
    id: 'prufer-modules',
    title: 'Код Прюфера по модулям',
    code: String.raw`// Составление кода Прюфера, разбитое по модулям.
// Просто создай файлы с такими названиями и раскидай код.

// ==================================================
// Файл: Prufer/PruferData.cs
// ==================================================
namespace Methods.Core.Prufer;

public class PruferData
{
    public int[,] Edges { get; }
    public int EdgeCount { get; }
    public int VertexCount { get; }

    public PruferData(int[,] edges, int edgeCount, int vertexCount)
    {
        Edges = edges;
        EdgeCount = edgeCount;
        VertexCount = vertexCount;
    }
}


// ==================================================
// Файл: Prufer/PruferResult.cs
// ==================================================
namespace Methods.Core.Prufer;

public class PruferResult
{
    public int[] Code { get; }

    public PruferResult(int[] code)
    {
        Code = code;
    }
}


// ==================================================
// Файл: Prufer/PruferDataReader.cs
// ==================================================
namespace Methods.Core.Prufer;

public static class PruferDataReader
{
    public static PruferData ReadFromFile(string path)
    {
        string[] lines = File.ReadAllLines(path);
        int edgeCount = 0;
        int vertexCount = 0;

        for (int i = 0; i < lines.Length; i++)
        {
            if (lines[i].Trim() == "") continue;

            string[] parts = lines[i].Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            int a = int.Parse(parts[0]);
            int b = int.Parse(parts[1]);

            if (a > vertexCount) vertexCount = a;
            if (b > vertexCount) vertexCount = b;
            edgeCount++;
        }

        int[,] edges = new int[edgeCount, 2];
        int index = 0;

        for (int i = 0; i < lines.Length; i++)
        {
            if (lines[i].Trim() == "") continue;

            string[] parts = lines[i].Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

            edges[index, 0] = int.Parse(parts[0]);
            edges[index, 1] = int.Parse(parts[1]);
            index++;
        }

        return new PruferData(edges, edgeCount, vertexCount);
    }
}


// ==================================================
// Файл: Prufer/PruferAlgorithm.cs
// ==================================================
namespace Methods.Core.Prufer;

public static class PruferAlgorithm
{
    public static PruferResult Solve(PruferData data)
    {
        int n = data.VertexCount;
        int[,] graph = new int[n + 1, n + 1];
        int[] degree = new int[n + 1];

        for (int i = 0; i < data.EdgeCount; i++)
        {
            int a = data.Edges[i, 0];
            int b = data.Edges[i, 1];

            graph[a, b] = 1;
            graph[b, a] = 1;
            degree[a]++;
            degree[b]++;
        }

        int[] code = new int[n - 2];

        for (int step = 0; step < n - 2; step++)
        {
            int leaf = 1;

            while (degree[leaf] != 1)
            {
                leaf++;
            }

            int next = 1;

            while (graph[leaf, next] == 0)
            {
                next++;
            }

            code[step] = next;

            graph[leaf, next] = 0;
            graph[next, leaf] = 0;
            degree[leaf]--;
            degree[next]--;
        }

        return new PruferResult(code);
    }
}


// ==================================================
// Файл: Prufer/PruferResultWriter.cs
// ==================================================
namespace Methods.Core.Prufer;

public static class PruferResultWriter
{
    public static void WriteToFile(string path, PruferResult result)
    {
        StreamWriter writer = new StreamWriter(path);

        writer.Write("Код Прюфера: ");

        for (int i = 0; i < result.Code.Length; i++)
        {
            if (i > 0) writer.Write(" ");
            writer.Write(result.Code[i]);
        }

        writer.Close();
    }
}


// ==================================================
// Файл: Program.cs
// ==================================================
using Methods.Core.Prufer;

PruferData data = PruferDataReader.ReadFromFile("input4.txt");
PruferResult result = PruferAlgorithm.Solve(data);
PruferResultWriter.WriteToFile("output4.txt", result);


// ==================================================
// Файл: Tests/PruferAlgorithmTests.cs
// ==================================================
using Methods.Core.Prufer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Methods.Tests;

[TestClass]
public class PruferAlgorithmTests
{
    [TestMethod]
    public void Solve_ReturnsExpectedCode()
    {
        PruferResult result = PruferAlgorithm.Solve(CreateData());

        int[] expected = { 1, 3, 3, 5 };

        Assert.AreEqual(expected.Length, result.Code.Length);

        for (int i = 0; i < expected.Length; i++)
        {
            Assert.AreEqual(expected[i], result.Code[i]);
        }
    }

    private static PruferData CreateData()
    {
        int[,] edges =
        {
            { 1, 2 },
            { 1, 3 },
            { 3, 4 },
            { 3, 5 },
            { 5, 6 }
        };

        return new PruferData(edges, 5, 6);
    }
}


// ==================================================
// Файл: input4.txt
// ==================================================
1 2
1 3
3 4
3 5
5 6`
  }
]

const modularMethods = [
  {
    id: 'modular-methods',
    title: '4 метода по модулям',
    code: String.raw`// Это один общий текст.
// Ниже написано, какой кусок в какой файл вставлять.
// Проект обычный консольный C#.

// ==================================================
// Файл: ModularMethods.csproj
// ==================================================
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net9.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>
</Project>


// ==================================================
// Файл: Program.cs
// ==================================================
using System.Text;
using ModularMethods;

Console.OutputEncoding = Encoding.UTF8;

Console.WriteLine("1 - Метод северо-западного угла");
Console.WriteLine("2 - Метод минимального элемента");
Console.WriteLine("3 - Алгоритм Дейкстры");
Console.WriteLine("4 - Код Прюфера");
Console.WriteLine("5 - Запустить все");
Console.Write("Выбор: ");

string? choice = args.Length > 0 ? args[0] : Console.ReadLine();

if (choice == "1")
    NorthWestMethod.Run();
else if (choice == "2")
    MinElementMethod.Run();
else if (choice == "3")
    DijkstraMethod.Run();
else if (choice == "4")
    PruferCodeMethod.Run();
else if (choice == "5")
{
    NorthWestMethod.Run();
    MinElementMethod.Run();
    DijkstraMethod.Run();
    PruferCodeMethod.Run();
}
else
    Console.WriteLine("Такого пункта нет");


// ==================================================
// Файл: NumberReader.cs
// ==================================================
namespace ModularMethods;

public static class NumberReader
{
    // Переводим строку из файла в массив чисел
    public static int[] Nums(string s)
    {
        string[] parts = s.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
        int[] nums = new int[parts.Length];

        for (int i = 0; i < parts.Length; i++)
        {
            nums[i] = int.Parse(parts[i]);
        }

        return nums;
    }
}


// ==================================================
// Файл: NorthWestMethod.cs
// ==================================================
namespace ModularMethods;

public static class NorthWestMethod
{
    public static void Run()
    {
        string[] lines = File.ReadAllLines("input1.txt");

        int[] size = NumberReader.Nums(lines[0]);
        int m = size[0];
        int n = size[1];

        int[] a = NumberReader.Nums(lines[1]);
        int[] b = NumberReader.Nums(lines[2]);

        int[,] c = new int[m, n];
        int[,] x = new int[m, n];

        for (int i = 0; i < m; i++)
        {
            int[] row = NumberReader.Nums(lines[i + 3]);

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
}


// ==================================================
// Файл: MinElementMethod.cs
// ==================================================
namespace ModularMethods;

public static class MinElementMethod
{
    public static void Run()
    {
        string[] lines = File.ReadAllLines("input2.txt");

        int[] size = NumberReader.Nums(lines[0]);
        int m = size[0];
        int n = size[1];

        int[] a = NumberReader.Nums(lines[1]);
        int[] b = NumberReader.Nums(lines[2]);

        int[,] c = new int[m, n];
        int[,] x = new int[m, n];

        for (int i = 0; i < m; i++)
        {
            int[] row = NumberReader.Nums(lines[i + 3]);

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
}


// ==================================================
// Файл: DijkstraMethod.cs
// ==================================================
namespace ModularMethods;

public static class DijkstraMethod
{
    public static void Run()
    {
        string[] lines = File.ReadAllLines("input3.txt");

        int[] first = NumberReader.Nums(lines[0]);
        int n = first[0];
        int start = first[1] - 1;

        int[,] g = new int[n, n];

        for (int i = 0; i < n; i++)
        {
            int[] row = NumberReader.Nums(lines[i + 1]);

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
}


// ==================================================
// Файл: PruferCodeMethod.cs
// ==================================================
namespace ModularMethods;

public static class PruferCodeMethod
{
    public static void Run()
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
}


// ==================================================
// Файл: input1.txt
// ==================================================
4 5
14 14 14 14
13 5 13 12 13
16 26 12 24 3
5 2 19 27 2
29 23 25 16 8
2 25 14 15 21


// ==================================================
// Файл: input2.txt
// ==================================================
4 5
14 14 14 14
13 5 13 12 13
16 26 12 24 3
5 2 19 27 2
29 23 25 16 8
2 25 14 15 21


// ==================================================
// Файл: input3.txt
// ==================================================
6 1
0 7 9 0 0 14
7 0 10 15 0 0
9 10 0 11 0 2
0 15 11 0 6 0
0 0 0 6 0 9
14 0 2 0 9 0


// ==================================================
// Файл: input4.txt
// ==================================================
1 2
1 3
3 4
3 5
5 6`
  }
]

const modularFiles = [
  {
    id: 'modular-files',
    title: 'Проект расходов по модулям + CSV JSON XLSX',
    code: String.raw`// Это один общий текст.
// Ниже написано, какой код в какой файл вставлять.
// Тут есть примеры сохранения и чтения из CSV, JSON и XLSX.

// ==================================================
// Файл: ExpenseExam.csproj
// ==================================================
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net9.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="ClosedXML" Version="0.105.0" />
  </ItemGroup>
</Project>


// ==================================================
// Файл: Models/Expense.cs
// ==================================================
namespace ExpenseExam.Core.Models;

public class Expense
{
    public DateTime Date { get; set; }
    public string Category { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string Comment { get; set; } = string.Empty;
}


// ==================================================
// Файл: Diagnostics/DebugHelper.cs
// ==================================================
using System.Diagnostics;

namespace ExpenseExam.Core.Diagnostics;

public static class DebugHelper
{
    public static void WriteDebug(string message)
    {
        Debug.WriteLine("[DEBUG] " + message);
    }

    public static void WriteTrace(string message)
    {
        Trace.WriteLine("[TRACE] " + message);
    }
}


// ==================================================
// Файл: Services/ExpenseService.cs
// ==================================================
using ExpenseExam.Core.Diagnostics;
using ExpenseExam.Core.Models;

namespace ExpenseExam.Core.Services;

public class ExpenseService
{
    public decimal GetTotal(List<Expense> expenses)
    {
        DebugHelper.WriteDebug("Считаем общую сумму");

        decimal sum = 0;

        for (int i = 0; i < expenses.Count; i++)
        {
            sum += expenses[i].Amount;
        }

        return sum;
    }

    public decimal GetAverage(List<Expense> expenses)
    {
        if (expenses.Count == 0)
        {
            return 0;
        }

        return GetTotal(expenses) / expenses.Count;
    }

    public Expense? GetMaxExpense(List<Expense> expenses)
    {
        if (expenses.Count == 0)
        {
            return null;
        }

        Expense max = expenses[0];

        for (int i = 1; i < expenses.Count; i++)
        {
            if (expenses[i].Amount > max.Amount)
            {
                max = expenses[i];
            }
        }

        return max;
    }
}


// ==================================================
// Файл: FileStorage/CsvStorage.cs
// ==================================================
using System.Globalization;
using System.Text;
using ExpenseExam.Core.Diagnostics;
using ExpenseExam.Core.Models;

namespace ExpenseExam.Core.FileStorage;

public class CsvStorage
{
    private static readonly UTF8Encoding FileEncoding = new(encoderShouldEmitUTF8Identifier: true);

    public void Save(string path, List<Expense> expenses)
    {
        DebugHelper.WriteDebug("Сохраняем CSV");

        StreamWriter writer = new StreamWriter(path, false, FileEncoding);

        writer.WriteLine("Date;Category;Amount;Comment");

        for (int i = 0; i < expenses.Count; i++)
        {
            Expense e = expenses[i];
            writer.WriteLine(e.Date.ToString("yyyy-MM-dd") + ";" +
                             e.Category + ";" +
                             e.Amount.ToString(CultureInfo.InvariantCulture) + ";" +
                             e.Comment);
        }

        writer.Close();
    }

    public List<Expense> Load(string path)
    {
        DebugHelper.WriteTrace("Читаем CSV");

        List<Expense> expenses = new List<Expense>();
        string[] lines = File.ReadAllLines(path, FileEncoding);

        for (int i = 1; i < lines.Length; i++)
        {
            if (lines[i].Trim() == "") continue;

            string[] p = lines[i].Split(';');

            Expense e = new Expense();
            e.Date = DateTime.ParseExact(p[0], "yyyy-MM-dd", CultureInfo.InvariantCulture);
            e.Category = p[1];
            e.Amount = decimal.Parse(p[2], CultureInfo.InvariantCulture);
            e.Comment = p[3];

            expenses.Add(e);
        }

        return expenses;
    }
}


// ==================================================
// Файл: FileStorage/JsonStorage.cs
// ==================================================
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using ExpenseExam.Core.Diagnostics;
using ExpenseExam.Core.Models;

namespace ExpenseExam.Core.FileStorage;

public class JsonStorage
{
    private static readonly UTF8Encoding FileEncoding = new(encoderShouldEmitUTF8Identifier: true);

    private static readonly JsonSerializerOptions Options = new JsonSerializerOptions
    {
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        WriteIndented = true
    };

    public void Save(string path, List<Expense> expenses)
    {
        DebugHelper.WriteDebug("Сохраняем JSON");

        string json = JsonSerializer.Serialize(expenses, Options);
        File.WriteAllText(path, json, FileEncoding);
    }

    public List<Expense> Load(string path)
    {
        DebugHelper.WriteTrace("Читаем JSON");

        string json = File.ReadAllText(path, FileEncoding);
        List<Expense>? expenses = JsonSerializer.Deserialize<List<Expense>>(json, Options);

        if (expenses == null)
        {
            return new List<Expense>();
        }

        return expenses;
    }
}


// ==================================================
// Файл: FileStorage/ExcelStorage.cs
// ==================================================
using ClosedXML.Excel;
using ExpenseExam.Core.Diagnostics;
using ExpenseExam.Core.Models;

namespace ExpenseExam.Core.FileStorage;

public class ExcelStorage
{
    public void Save(string path, List<Expense> expenses)
    {
        DebugHelper.WriteDebug("Сохраняем XLSX");

        XLWorkbook workbook = new XLWorkbook();
        IXLWorksheet sheet = workbook.Worksheets.Add("Expenses");

        sheet.Cell(1, 1).Value = "Date";
        sheet.Cell(1, 2).Value = "Category";
        sheet.Cell(1, 3).Value = "Amount";
        sheet.Cell(1, 4).Value = "Comment";

        for (int i = 0; i < expenses.Count; i++)
        {
            int row = i + 2;

            sheet.Cell(row, 1).Value = expenses[i].Date;
            sheet.Cell(row, 1).Style.DateFormat.Format = "yyyy-mm-dd";
            sheet.Cell(row, 2).Value = expenses[i].Category;
            sheet.Cell(row, 3).Value = expenses[i].Amount;
            sheet.Cell(row, 4).Value = expenses[i].Comment;
        }

        sheet.Columns().AdjustToContents();
        workbook.SaveAs(path);
        workbook.Dispose();
    }

    public List<Expense> Load(string path)
    {
        DebugHelper.WriteTrace("Читаем XLSX");

        XLWorkbook workbook = new XLWorkbook(path);
        IXLWorksheet sheet = workbook.Worksheet("Expenses");
        List<Expense> expenses = new List<Expense>();

        int row = 2;

        while (!sheet.Cell(row, 1).IsEmpty())
        {
            Expense e = new Expense();
            e.Date = sheet.Cell(row, 1).GetDateTime();
            e.Category = sheet.Cell(row, 2).GetString();
            e.Amount = sheet.Cell(row, 3).GetValue<decimal>();
            e.Comment = sheet.Cell(row, 4).GetString();

            expenses.Add(e);
            row++;
        }

        workbook.Dispose();
        return expenses;
    }
}


// ==================================================
// Файл: Program.cs
// ==================================================
using ExpenseExam.Core.FileStorage;
using ExpenseExam.Core.Models;
using ExpenseExam.Core.Services;

List<Expense> expenses = new List<Expense>();

expenses.Add(new Expense { Date = new DateTime(2026, 7, 1), Category = "Еда", Amount = 450, Comment = "Обед" });
expenses.Add(new Expense { Date = new DateTime(2026, 7, 2), Category = "Транспорт", Amount = 120, Comment = "Автобус" });
expenses.Add(new Expense { Date = new DateTime(2026, 7, 3), Category = "Учеба", Amount = 1500, Comment = "Курс C#" });

ExpenseService service = new ExpenseService();

CsvStorage csv = new CsvStorage();
JsonStorage json = new JsonStorage();
ExcelStorage xlsx = new ExcelStorage();

string folder = Path.Combine(AppContext.BaseDirectory, "data");
Directory.CreateDirectory(folder);

string csvPath = Path.Combine(folder, "expenses.csv");
string jsonPath = Path.Combine(folder, "expenses.json");
string xlsxPath = Path.Combine(folder, "expenses.xlsx");

Console.WriteLine("Общая сумма: " + service.GetTotal(expenses));
Console.WriteLine("Среднее: " + service.GetAverage(expenses));
Console.WriteLine("Максимальный расход: " + service.GetMaxExpense(expenses)?.Amount);

// Пример записи в CSV и чтения из CSV
csv.Save(csvPath, expenses);
List<Expense> fromCsv = csv.Load(csvPath);
Console.WriteLine("Из CSV прочитано: " + fromCsv.Count);

// Пример записи в JSON и чтения из JSON
json.Save(jsonPath, expenses);
List<Expense> fromJson = json.Load(jsonPath);
Console.WriteLine("Из JSON прочитано: " + fromJson.Count);

// Пример записи в XLSX и чтения из XLSX
xlsx.Save(xlsxPath, expenses);
List<Expense> fromXlsx = xlsx.Load(xlsxPath);
Console.WriteLine("Из XLSX прочитано: " + fromXlsx.Count);

Console.WriteLine("Файлы лежат тут: " + folder);`
  }
]

const moduleTests = [
  {
    id: 'module-tests',
    title: 'Тесты для модулей',
    code: String.raw`// Это один общий текст с тестами.
// Куски ниже надо разложить по файлам.
// Есть тесты для 4 алгоритмов и для модулей расходов: модель, сервис, CSV, JSON, XLSX, Debug.

// ==================================================
// Файл: ModularMethods.Tests/ModularMethods.Tests.csproj
// ==================================================
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>net9.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
    <IsPackable>false</IsPackable>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.12.0" />
    <PackageReference Include="MSTest.TestAdapter" Version="3.6.4" />
    <PackageReference Include="MSTest.TestFramework" Version="3.6.4" />
  </ItemGroup>

  <ItemGroup>
    <ProjectReference Include="..\ModularMethods\ModularMethods.csproj" />
  </ItemGroup>
</Project>


// ==================================================
// Файл: ModularMethods.Tests/AlgorithmModuleTests.cs
// ==================================================
using ModularMethods;

namespace ModularMethods.Tests;

[TestClass]
public class AlgorithmModuleTests
{
    [TestMethod]
    public void NumberReader_ReadsNumbers()
    {
        int[] nums = NumberReader.Nums("10 20 30");

        Assert.AreEqual(3, nums.Length);
        Assert.AreEqual(10, nums[0]);
        Assert.AreEqual(20, nums[1]);
        Assert.AreEqual(30, nums[2]);
    }

    [TestMethod]
    public void NorthWestMethod_CreatesCorrectCost()
    {
        File.WriteAllText("input1.txt", TransportInput());

        NorthWestMethod.Run();

        string text = File.ReadAllText("output1.txt");
        StringAssert.Contains(text, "Стоимость = 971");
    }

    [TestMethod]
    public void MinElementMethod_CreatesCorrectCost()
    {
        File.WriteAllText("input2.txt", TransportInput());

        MinElementMethod.Run();

        string text = File.ReadAllText("output2.txt");
        StringAssert.Contains(text, "Стоимость = 442");
    }

    [TestMethod]
    public void DijkstraMethod_CreatesCorrectDistances()
    {
        File.WriteAllText("input3.txt", DijkstraInput());

        DijkstraMethod.Run();

        string text = File.ReadAllText("output3.txt");
        StringAssert.Contains(text, "До вершины 1: 0");
        StringAssert.Contains(text, "До вершины 4: 20");
        StringAssert.Contains(text, "До вершины 6: 11");
    }

    [TestMethod]
    public void PruferCodeMethod_CreatesCorrectCode()
    {
        File.WriteAllText("input4.txt", PruferInput());

        PruferCodeMethod.Run();

        string text = File.ReadAllText("output4.txt");
        StringAssert.Contains(text, "Код Прюфера: 1 3 3 5");
    }

    private static string TransportInput()
    {
        return "4 5\n" +
               "14 14 14 14\n" +
               "13 5 13 12 13\n" +
               "16 26 12 24 3\n" +
               "5 2 19 27 2\n" +
               "29 23 25 16 8\n" +
               "2 25 14 15 21\n";
    }

    private static string DijkstraInput()
    {
        return "6 1\n" +
               "0 7 9 0 0 14\n" +
               "7 0 10 15 0 0\n" +
               "9 10 0 11 0 2\n" +
               "0 15 11 0 6 0\n" +
               "0 0 0 6 0 9\n" +
               "14 0 2 0 9 0\n";
    }

    private static string PruferInput()
    {
        return "1 2\n" +
               "1 3\n" +
               "3 4\n" +
               "3 5\n" +
               "5 6\n";
    }
}


// ==================================================
// Файл: ExpenseExam.Tests/ExpenseExam.Tests.csproj
// ==================================================
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>net9.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
    <IsPackable>false</IsPackable>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.12.0" />
    <PackageReference Include="MSTest.TestAdapter" Version="3.6.4" />
    <PackageReference Include="MSTest.TestFramework" Version="3.6.4" />
  </ItemGroup>

  <ItemGroup>
    <ProjectReference Include="..\ExpenseExam\ExpenseExam.csproj" />
  </ItemGroup>
</Project>


// ==================================================
// Файл: ExpenseExam.Tests/ExpenseModuleTests.cs
// ==================================================
using ExpenseExam.Core.Diagnostics;
using ExpenseExam.Core.FileStorage;
using ExpenseExam.Core.Models;
using ExpenseExam.Core.Services;

namespace ExpenseExam.Tests;

[TestClass]
public class ExpenseModuleTests
{
    [TestMethod]
    public void Expense_Model_StoresData()
    {
        Expense expense = new Expense();
        expense.Date = new DateTime(2026, 7, 1);
        expense.Category = "Еда";
        expense.Amount = 450;
        expense.Comment = "Обед";

        Assert.AreEqual(new DateTime(2026, 7, 1), expense.Date);
        Assert.AreEqual("Еда", expense.Category);
        Assert.AreEqual(450m, expense.Amount);
        Assert.AreEqual("Обед", expense.Comment);
    }

    [TestMethod]
    public void ExpenseService_ReturnsTotalAverageAndMax()
    {
        ExpenseService service = new ExpenseService();
        List<Expense> expenses = CreateExpenses();

        decimal total = service.GetTotal(expenses);
        decimal average = service.GetAverage(expenses);
        Expense? max = service.GetMaxExpense(expenses);

        Assert.AreEqual(2070m, total);
        Assert.AreEqual(690m, average);
        Assert.IsNotNull(max);
        Assert.AreEqual(1500m, max.Amount);
        Assert.AreEqual("Учеба", max.Category);
    }

    [TestMethod]
    public void CsvStorage_SavesAndLoadsExpenses()
    {
        string folder = CreateFolder();
        string path = Path.Combine(folder, "expenses.csv");

        CsvStorage storage = new CsvStorage();
        storage.Save(path, CreateExpenses());

        List<Expense> loaded = storage.Load(path);

        Assert.AreEqual(3, loaded.Count);
        Assert.AreEqual("Еда", loaded[0].Category);
        Assert.AreEqual(1500m, loaded[2].Amount);

        Directory.Delete(folder, true);
    }

    [TestMethod]
    public void JsonStorage_SavesAndLoadsExpenses()
    {
        string folder = CreateFolder();
        string path = Path.Combine(folder, "expenses.json");

        JsonStorage storage = new JsonStorage();
        storage.Save(path, CreateExpenses());

        List<Expense> loaded = storage.Load(path);

        Assert.AreEqual(3, loaded.Count);
        Assert.AreEqual("Транспорт", loaded[1].Category);
        Assert.AreEqual("Курс C#", loaded[2].Comment);

        Directory.Delete(folder, true);
    }

    [TestMethod]
    public void ExcelStorage_SavesAndLoadsExpenses()
    {
        string folder = CreateFolder();
        string path = Path.Combine(folder, "expenses.xlsx");

        ExcelStorage storage = new ExcelStorage();
        storage.Save(path, CreateExpenses());

        List<Expense> loaded = storage.Load(path);

        Assert.AreEqual(3, loaded.Count);
        Assert.AreEqual("Автобус", loaded[1].Comment);
        Assert.AreEqual(120m, loaded[1].Amount);

        Directory.Delete(folder, true);
    }

    [TestMethod]
    public void DebugHelper_WritesWithoutError()
    {
        DebugHelper.WriteDebug("test debug");
        DebugHelper.WriteTrace("test trace");

        Assert.IsTrue(true);
    }

    // Program.cs обычно отдельно не тестируют.
    // Его проверка идет через сервис и хранилища выше.

    private static List<Expense> CreateExpenses()
    {
        List<Expense> expenses = new List<Expense>();

        expenses.Add(new Expense { Date = new DateTime(2026, 7, 1), Category = "Еда", Amount = 450m, Comment = "Обед" });
        expenses.Add(new Expense { Date = new DateTime(2026, 7, 2), Category = "Транспорт", Amount = 120m, Comment = "Автобус" });
        expenses.Add(new Expense { Date = new DateTime(2026, 7, 3), Category = "Учеба", Amount = 1500m, Comment = "Курс C#" });

        return expenses;
    }

    private static string CreateFolder()
    {
        string folder = Path.Combine(Path.GetTempPath(), "expense_test_" + Guid.NewGuid().ToString());
        Directory.CreateDirectory(folder);
        return folder;
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
  flex-direction: column;
  gap: 10px;
  padding: 14px;
}

.row {
  display: flex;
  gap: 10px;
}

button {
  appearance: none;
  border: 0;
  background: transparent;
  color: #777;
  border-radius: 12px;
  width: 42px;
  height: 34px;
  cursor: pointer;
  font-size: 18px;
  line-height: 1;
}

button:hover {
  background: transparent;
}

.active {
  background: transparent;
}
</style>
