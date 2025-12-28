using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using static System.Reflection.Metadata.BlobBuilder;

namespace libraryTask
{
    internal class Book
    {
        FileOperation fileOperation = new FileOperation();
        public string Title { get; set; }
        public string AuthorName { get; set; }
        public int Quantity { get; set; }
        public DateTime OperationDate { get; set; } = DateTime.Now;
        public static int BorrowedBooks { get; set; } = 0;
        public static int AddedBooks { get; set; } = 0;
        public static int Operations { get; set; } = 0;

        public void AddBook()
        {
            Console.WriteLine("Title of the Book?");
            Title = Console.ReadLine();
            Console.WriteLine("Author name of the Book?");
            AuthorName = Console.ReadLine();
            Console.WriteLine("Quantity of the Book you are adding?");
            Quantity = int.Parse(Console.ReadLine());

            AddedBooks += Quantity;
            Operations ++;

            string lineData = $"{Title};{AuthorName};{Quantity};{OperationDate};ADD";
            fileOperation.saveToFile(lineData);

        }
        public void BorrowBook()
        {
            Console.WriteLine("Title of the Book?");
            Title = Console.ReadLine();
            Console.WriteLine("Author name of the Book?");
            AuthorName = Console.ReadLine();
            Console.WriteLine("Quantity of the Book you are borrowing?");
            Quantity = int.Parse(Console.ReadLine());
            
            string key = Title + "|" + AuthorName;

            var stock = CalculateStock();
            int available = stock.ContainsKey(key) ? stock[key] : 0;

            if (available < Quantity)
            {
                Console.WriteLine("Kifayət qədər kitab yoxdur");
                return;
            }
            Operations ++;
            BorrowedBooks += Quantity;
            string lineData = $"{Title};{AuthorName};{Quantity};{OperationDate};BORROW";
            fileOperation.saveToFile(lineData);
        }

        public void ShowOperations()
        {
            var books = fileOperation.ReadData();
            foreach (var book in books)
            {
                Console.WriteLine(book);
            }
        }
        static Dictionary<string, int> CalculateStock()
        {
            Dictionary<string, int> stock = new Dictionary<string, int>();

            string[] lines = File.ReadAllLines(FileOperation.filePath);

            foreach (string line in lines)
            {
                string[] parts = line.Split(';');

                string bookName = parts[0];
                string author = parts[1];
                string type = parts[4];
                int amount = int.Parse(parts[2]);

                string key = bookName + "|" + author;

                if (!stock.ContainsKey(key))
                    stock[key] = 0;

                if (type == "ADD")
                    stock[key] += amount;
                else if (type == "BORROW")
                    stock[key] -= amount;
            }

            return stock;
        }
        public static void ShowCurrentStock()
        {
            var stock = CalculateStock();

            Console.WriteLine("Mövcud kitablar:");

            foreach (var item in stock)
            {
                if (item.Value > 0)
                {
                    string[] parts = item.Key.Split('|');
                    Console.WriteLine($"{parts[0]} - {parts[1]} : {item.Value}");
                }
            }
        }
        public static void MonthlyStatistics()
        {
            Console.WriteLine($"Ümumi daxil edilən kitabların sayı: {AddedBooks}");
            Console.WriteLine($"Ümumi verilən kitabların miqdarı: {BorrowedBooks}");
            Console.WriteLine($"Aparılan əməliyyatların sayı: {Operations}");

            if(AddedBooks > BorrowedBooks)
            {
                Console.WriteLine("Kitabxana ayı ehtiyat artımı ilə bağladı");
            }
            else if(AddedBooks < BorrowedBooks)
            {
                Console.WriteLine("Kitabxana ayı ehtiyat azalması ilə bağladı");
            }
            else
            {
                Console.WriteLine("Kitabxananın ehtiyatı sabit qaldı");
            }
        }
        /* AYLIQ NƏTİCƏ
Hesablamaya əsasən aylıq vəziyyət müəyyən edilməlidir:
- Daxil edilən kitablar veriləndən çoxdursa →
  “Kitabxana ayı ehtiyat artımı ilə bağladı”
- Verilən kitablar çoxdursa →
  “Kitabxana ayı ehtiyat azalması ilə bağladı”
- Miqdarlar bərabərdirsə →
  “Kitabxananın ehtiyatı sabit qaldı”*/

    }
}
