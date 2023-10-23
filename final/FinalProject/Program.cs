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
            Console.WriteLine("please choose what you would like to do in Library");
            Console.WriteLine("1. Add Book");
            Console.WriteLine("2. List Books");
            Console.WriteLine("3. Remove Books");
            Console.WriteLine("4. Borrow book");
            Console.WriteLine("5. Return book");
            Console.WriteLine("6. quit");
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
        // Code for borrowing a book.
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
        // Code for returning a book.
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

                default:
                         Console.WriteLine("Invalid choice. Please select a valid option.");
                break;
                case "6":
               // Code for quitting the program (as shown in the original code).
                Console.WriteLine("Goodbye!");
                return;
            }
        }}

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
        User user1 = new User("User1", "12345");
        User user2 = new User("User2", "67890");

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

private List<string> assignedCardNumbers = new List<string>();

    public User CreateUser(string name, string cardNumber)
    {
        //string cardNumber;
        do
        {
            cardNumber = GenerateUniqueCardNumber();
        } while (assignedCardNumbers.Contains(cardNumber));

        assignedCardNumbers.Add(cardNumber);

        User newUser = new User(name, cardNumber);
        // Add the user to your user list or database.
        User user1 = new User("User1", "12345");
        User user2 = new User("User2", "67890");

        return newUser;
    }

    private string GenerateUniqueCardNumber()
    {
        

         Library library = new Library();

        // Create users
        User user1 = new User("User1", "12345");
        User user2 = new User("User2", "67890");

        // Create books
        //Book book1 = new Book("Book1");
       // Book book2 = new Book("Book2");

        library.AddUser(user1);
        library.AddUser(user2);

        //library.AddBook(book1);
        //library.AddBook(book2);

        // Example usage of GetUserByLibraryCardNumber
        string cardNumber = "12345";
        User foundUser = library.GetUserByLibraryCardNumber(cardNumber);

        if (foundUser != null)
        {
            Console.WriteLine($"User Found: {foundUser.Name}");
        }
        else
        {
            Console.WriteLine("User Not Found");
        }

        // Example usage of GetBookByTitle
        string bookTitle = "Book1";
        Book foundBook = library.GetBookByTitle(bookTitle);

        if (foundBook != null)
        {
            Console.WriteLine($"Book Found: {foundBook.Title}");
        }
        else
        {
            Console.WriteLine("Book Not Found");
        }
    

        return Guid.NewGuid().ToString("N").Substring(0, 8); // Generates an 8-character unique string.
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

class Catalog
{
    private List<Book> catalogBooks = new List<Book>();

    // Method to add a book to the catalog
    public void AddBookToCatalog(Book book)
    {
        catalogBooks.Add(book);
        Console.WriteLine($"Book added to the catalog: {book.Title} by {book.Author}");
    }

    // Method to list all books in the catalog
    public List<Book> ListAllBooks()
    {
        return catalogBooks;
    }

    // Method to search for books by title
    public List<Book> SearchByTitle(string title)
    {
        return catalogBooks.FindAll(book => book.Title.Contains(title, StringComparison.OrdinalIgnoreCase));
    }

    // Method to search for books by author
    public List<Book> SearchByAuthor(string author)
    {
        return catalogBooks.FindAll(book => book.Author.Contains(author, StringComparison.OrdinalIgnoreCase));
    }

    // I can even add more search methods (e.g., by genre, publication year) as needed but next time.

    // Other methods for categorizing and searching I can add it here.
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

    // Method to log a new transaction
    public void LogTransaction()
    {
        Console.WriteLine($"{User.Name} has borrowed the book: {Book.Title}");
        Console.WriteLine($"Due Date: {DueDate}");
    }

    // Method to log the return of a book
    public void LogReturn()
    {
        Console.WriteLine($"{User.Name} has returned the book: {Book.Title}");
        Console.WriteLine($"Returned on: {DateTime.Now}");
    }

    // Method to check if the book is overdue
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

class Patron
{
    public string Name { get; set; }
    public string LibraryCardNumber { get; set; }
    public string ContactDetails { get; set; }
    public List<Transaction> BorrowedBooksHistory { get; set; }

    public Patron(string name, string cardNumber, string contactDetails)
    {
        Name = name;
        LibraryCardNumber = cardNumber;
        ContactDetails = contactDetails;
        BorrowedBooksHistory = new List<Transaction>();
    }

    // I can even add many methods to manage user-related actions, such as updating contact details.
}


}
