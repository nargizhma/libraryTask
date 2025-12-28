using libraryTask;

class Program
{
    static void Main(string[] args)
    {
        Book book = new Book();
        int choice = 0;
        do
        {
            ShowMenu();
            Console.WriteLine("Seçiminizi daxil edin 0-5): ");
            choice = int.Parse(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    Book.ShowCurrentStock();
                    break;
                case 2:
                    book.AddBook();
                    break;
                case 3:
                    book.BorrowBook();
                    break;
                case 4:
                    book.ShowOperations();
                    break;
                case 5:
                    Book.MonthlyStatistics();
                    break;


            }

        } while (choice != 0);
        Console.WriteLine("Çıxış");
    }

    public static void ShowMenu()
    {
        Console.WriteLine("1 – Kitabların mövcud vəziyyətini göstər");
        Console.WriteLine("2 – Kitab daxil et (kitabxanaya yeni kitab əlavə et)");
        Console.WriteLine("3 – Kitab ver (oxucuya kitab verilməsi)");
        Console.WriteLine("4 – Dövriyyə tarixçəsini göstər");
        Console.WriteLine("5 – Aylıq kitab dövriyyə statistikasını hesabla");
        Console.WriteLine("0 – Çıxış");
    }
}

