using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        Library library = new Library();
        
        while (true)
        {
            Console.WriteLine("Welcome to Library Management System");
            Console.WriteLine("Please choose what you would like to do in the library:");
            Console.WriteLine("1. Add Book");
            Console.WriteLine("2. List Books");
            Console.WriteLine("3. Remove Books");
            Console.WriteLine("4. Borrow Book");
            Console.WriteLine("5. Return Book");
            Console.WriteLine("6. Quit");
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
                    Console.WriteLine("Goodbye!");
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

    public void AddUser(User user)
    {
        users.Add(user);
    }

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
        return books.Find(book => book.Title == title);
    }

    public void RemoveBook(string title)
    {
        Book bookToRemove = books.Find(b => b.Title == title);
        if (bookToRemove != null)
        {
            books.Remove(bookToRemove);
            Console.WriteLine($"{title} has been removed from the library.");
        }
        else
        {
            Console.WriteLine($"{title} not found in the library.");
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
