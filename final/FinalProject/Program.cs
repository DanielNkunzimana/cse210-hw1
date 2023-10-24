using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        Library library = new Library();
        Catalog catalog = new Catalog();

        while (true)
        {
            Console.WriteLine("Welcome to Library Management System");
            Console.WriteLine("Please choose what you would like to do in the library:");
            Console.WriteLine("1. Add Book");
            Console.WriteLine("2. List Books");
            Console.WriteLine("3. Remove Book");
            Console.WriteLine("4. Borrow Book");
            Console.WriteLine("5. Return Book");
            Console.WriteLine("6. Manage Patrons");
            Console.WriteLine("7. Manage Catalog");
            Console.WriteLine("8. Quit");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.WriteLine("Enter book title: ");
                    string title = Console.ReadLine();
                    Console.WriteLine("Enter author: ");
                    string author = Console.ReadLine();
                    Book newBook = new Book(title, author);
                    library.AddBook(newBook);
                    catalog.AddBookToCatalog(newBook);
                    Console.WriteLine("Book added to the library.");
                    break;

                case "2":
                    Console.WriteLine("Books in the library:");
                    List<Book> books = library.GetBooks();
                    foreach (Book book in books)
                    {
                        Console.WriteLine($"{book.Title} by {book.Author}");
                    }
                    break;

                case "3":
                    Console.WriteLine("Enter the title of the book to remove: ");
                    string bookTitleToRemove = Console.ReadLine();
                    library.RemoveBook(bookTitleToRemove);
                    catalog.RemoveBookFromCatalog(bookTitleToRemove);
                    break;

                case "4":
                    Console.WriteLine("Enter your library card number: ");
                    string userCardNumber = Console.ReadLine();
                    User user = library.GetUserByLibraryCardNumber(userCardNumber);

                    if (user == null)
                    {
                        Console.WriteLine("User not found. Please check your library card number.");
                    }
                    else
                    {
                        Console.WriteLine("Enter the title of the book to borrow: ");
                        string bookTitleToBorrow = Console.ReadLine();
                        Book bookToBorrow = library.GetBookByTitle(bookTitleToBorrow);

                        if (bookToBorrow == null)
                        {
                            Console.WriteLine("Book not found. Please check the book title.");
                        }
                        else
                        {
                            user.BorrowBook(bookToBorrow);
                        }
                    }
                    break;

                case "5":
                    Console.WriteLine("Enter your library card number: ");
                    userCardNumber = Console.ReadLine();
                    user = library.GetUserByLibraryCardNumber(userCardNumber);

                    if (user == null)
                    {
                        Console.WriteLine("User not found. Please check your library card number.");
                    }
                    else
                    {
                        Console.WriteLine("Enter the title of the book to return: ");
                        string bookTitleToReturn = Console.ReadLine();
                        Book bookToReturn = library.GetBookByTitle(bookTitleToReturn);

                        if (bookToReturn == null)
                        {
                            Console.WriteLine("Book not found. Please check the book title.");
                        }
                        else
                        {
                            user.ReturnBook(bookToReturn);
                        }
                    }
                    break;

                case "6":
                    Console.WriteLine("Choose patron management option:");
                    Console.WriteLine("1. Add Patron");
                    Console.WriteLine("2. List Patrons");
                    Console.WriteLine("3. Remove Patron");
                    string patronChoice = Console.ReadLine();

                    switch (patronChoice)
                    {
                        case "1":
                            Console.WriteLine("Enter patron name: ");
                            string patronName = Console.ReadLine();
                            Patron newPatron = new Patron(patronName);
                            library.AddPatron(newPatron);
                            Console.WriteLine($"Patron {patronName} has been added.");
                            break;

                        case "2":
                            Console.WriteLine("Patrons in the library:");
                            List<Patron> patrons = library.GetPatrons();
                            foreach (Patron patron in patrons)
                            {
                                Console.WriteLine(patron.Name);
                            }
                            break;

                        case "3":
                            Console.WriteLine("Enter the name of the patron to remove: ");
                            string patronNameToRemove = Console.ReadLine();
                            library.RemovePatron(patronNameToRemove);
                            Console.WriteLine($"Patron {patronNameToRemove} has been removed.");
                            break;

                        default:
                            Console.WriteLine("Invalid choice for patron management.");
                            break;
                    }
                    break;

                case "7":
                    Console.WriteLine("Choose catalog management option:");
                    Console.WriteLine("1. List All Books in Catalog");
                    Console.WriteLine("2. Search for Books by Title");
                    Console.WriteLine("3. Search for Books by Author");
                    string catalogChoice = Console.ReadLine();

                    switch (catalogChoice)
                    {
                        case "1":
                            Console.WriteLine("Books in the catalog:");
                            List<Book> catalogBooks = catalog.ListAllBooks();
                            foreach (Book book in catalogBooks)
                            {
                                Console.WriteLine($"{book.Title} by {book.Author}");
                            }
                            break;

                        case "2":
                            Console.WriteLine("Enter the title to search for: ");
                            string searchTitle = Console.ReadLine();
                            List<Book> searchResultByTitle = catalog.SearchByTitle(searchTitle);
                            if (searchResultByTitle.Any())
                            {
                                Console.WriteLine("Matching books in the catalog:");
                                foreach (Book book in searchResultByTitle)
                                {
                                    Console.WriteLine($"{book.Title} by {book.Author}");
                                }
                            }
                            else
                            {
                                Console.WriteLine("No matching books found.");
                            }
                            break;

                        case "3":
                            Console.WriteLine("Enter the author to search for: ");
                            string searchAuthor = Console.ReadLine();
                            List<Book> searchResultByAuthor = catalog.SearchByAuthor(searchAuthor);
                            if (searchResultByAuthor.Any())
                            {
                                Console.WriteLine("Matching books in the catalog:");
                                foreach (Book book in searchResultByAuthor)
                                {
                                    Console.WriteLine($"{book.Title} by {book.Author}");
                                }
                            }
                            else
                            {
                                Console.WriteLine("No matching books found.");
                            }
                            break;

                        default:
                            Console.WriteLine("Invalid choice for catalog management.");
                            break;
                    }
                    break;

                case "8":
                    Console.WriteLine("Goodbye!");
                    Console.WriteLine("thank you for coming in our library");
                    return;

                default:
                    Console.WriteLine("Invalid choice. Please select a valid option.");
                    break;
            }
        }
    }
}

class Book
{
    public string Title { get; private set; }
    public string Author { get; private set; }

    public Book(string title, string author)
    {
        Title = title;
        Author = author;
    }
}

class Library
{
    private List<User> users = new List<User>();
    private List<Book> books = new List<Book>();
    private List<Patron> patrons = new List<Patron>();

    public void AddBook(Book book)
    {
        books.Add(book);
    }

    public List<Book> GetBooks()
    {
        return books;
    }

    public User GetUserByLibraryCardNumber(string cardNumber)
    {
        return users.Find(user => user.LibraryCardNumber == cardNumber);
    }

    public Book GetBookByTitle(string title)
    {
        return books.Find(book => book.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
    }

    public void RemoveBook(string title)
    {
        Book bookToRemove = books.Find(b => b.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
        if (bookToRemove != null)
        {
            books.Remove(bookToRemove);
        }
    }

    public void AddPatron(Patron patron)
    {
        patrons.Add(patron);
    }

    public List<Patron> GetPatrons()
    {
        return patrons;
    }

    public void RemovePatron(string name)
    {
        Patron patronToRemove = patrons.Find(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        if (patronToRemove != null)
        {
            patrons.Remove(patronToRemove);
        }
    }
}

class User
{
    public string Name { get; set; }
    public string LibraryCardNumber { get; private set; }
    public List<Book> BorrowedBooks { get; private set; }

    public User(string name, string cardNumber)
    {
        Name = name;
        LibraryCardNumber = cardNumber;
        BorrowedBooks = new List<Book>();
    }

    public void BorrowBook(Book book)
    {
        if (BorrowedBooks.Count >= 3)
        {
            Console.WriteLine("You have reached the maximum limit of borrowed books (3 books). Please return a book before borrowing another one.");
        }
        else
        {
            BorrowedBooks.Add(book);
            Console.WriteLine($"{Name} has borrowed the book: {book.Title}");
        }
    }

    public void ReturnBook(Book book)
    {
        if (BorrowedBooks.Contains(book))
        {
            BorrowedBooks.Remove(book);
            Console.WriteLine($"{Name} has returned the book: {book.Title}");
        }
        else
        {
            Console.WriteLine($"{Name} does not have the book: {book.Title} to return.");
        }
    }
}

class Patron
{
    public string Name { get; set; }
    public string LibraryCardNumber { get; set; }
    public string ContactDetails { get; set; }
    public List<Transaction> BorrowedBooksHistory { get; set; }

    public Patron(string patronName)
    {
        Name = patronName;
    }

    public Patron(string name, string cardNumber, string contactDetails)
    {
        Name = name;
        LibraryCardNumber = cardNumber;
        ContactDetails = contactDetails;
        BorrowedBooksHistory = new List<Transaction>();
    }
}

class Catalog
{
    private List<Book> catalogBooks = new List<Book>();

    public void AddBookToCatalog(Book book)
    {
        catalogBooks.Add(book);
    }

    public List<Book> ListAllBooks()
    {
        return catalogBooks;
    }

    public List<Book> SearchByTitle(string title)
    {
        return catalogBooks.FindAll(book => book.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
    }

    public List<Book> SearchByAuthor(string author)
    {
        return catalogBooks.FindAll(book => book.Author.Equals(author, StringComparison.OrdinalIgnoreCase));
    }

    public void RemoveBookFromCatalog(string title)
    {
        Book bookToRemove = catalogBooks.Find(b => b.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
        if (bookToRemove != null)
        {
            catalogBooks.Remove(bookToRemove);
        }
    }
}

class Transaction
{
    public User User { get; private set; }
    public Book Book { get; private set; }
    public DateTime DueDate { get; private set; }

    public Transaction(User user, Book book, DateTime dueDate)
    {
        User = user;
        Book = book;
        DueDate = dueDate;
    }

    public void LogTransaction()
    {
        Console.WriteLine($"{User.Name} has borrowed the book: {Book.Title}");
        Console.WriteLine($"Due Date: {DueDate}");
    }

    public void LogReturn()
    {
        Console.WriteLine($"{User.Name} has returned the book: {Book.Title}");
        Console.WriteLine($"Returned on: {DateTime.Now}");
    }

    public bool IsOverdue()
    {
        return DateTime.Now > DueDate;
    }
}

class LibraryMember
{
    public string MemberName { get; set; }
    public string MemberCardNumber { get; set; }
    public List<Book> BorrowedBooks { get; private set; }

    public LibraryMember(string name, string cardNumber)
    {
        MemberName = name;
        MemberCardNumber = cardNumber;
        BorrowedBooks = new List<Book>();
    }

    public void BorrowBook(Book book)
    {
        if (BorrowedBooks.Count >= 3)
        {
            Console.WriteLine("You have reached the maximum limit of borrowed books (3 books). Please return a book before borrowing another one.");
        }
        else
        {
            BorrowedBooks.Add(book);
            Console.WriteLine($"{MemberName} has borrowed the book: {book.Title}");
        }
    }

    public void ReturnBook(Book book)
    {
        if (BorrowedBooks.Contains(book))
        {
            BorrowedBooks.Remove(book);
            Console.WriteLine($"{MemberName} has returned the book: {book.Title}");
        }
        else
        {
            Console.WriteLine($"{MemberName} does not have the book: {book.Title} to return.");
        }
    }
}

