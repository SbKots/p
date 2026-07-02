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
  border: 0;
  background: #e0e0e0;
  color: #777;
  border-radius: 12px;
  width: 42px;
  height: 34px;
  cursor: pointer;
  font-size: 18px;
  line-height: 1;
}

button:hover {
  background: #dcdcdc;
}

.active {
  background: #d6d6d6;
}
</style>
