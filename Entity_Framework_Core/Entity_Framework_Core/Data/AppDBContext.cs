using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Entity_Framework_Core.Data
{
    public class AppDBContext(DbContextOptions<AppDBContext> options) : DbContext(options)
    {
        
                    protected override void OnModelCreating(ModelBuilder modelBuilder)
                    {
                        modelBuilder.Entity<Currencies>().HasData(
                            new Currencies() { Id = 1, Title = "INR", Description = "Indian INR" },
                            new Currencies() { Id = 2, Title = "Dollar", Description = "Dollar" },
                            new Currencies() { Id = 3, Title = "Euro", Description = "Euro" },
                            new Currencies() { Id = 4, Title = "Dinar", Description = "Dinar" }
                            );

                        modelBuilder.Entity<Language>().HasData(
                         new Language() { Id = 1, Title = "Hindi", Description = "Hindi" },
                         new Language() { Id = 2, Title = "Tamil", Description = "Tamil" },
                          new Language() { Id = 3, Title = "Punjabi", Description = "Punjabi" },
                         new Language() { Id = 4, Title = "Urdu", Description = "Urdu" }
                          );

                        modelBuilder.Entity<BookPrices>().HasData(
                            new BookPrices() { Id = 1, Amount = 200, BookId = 1, CurrencyId = 1 },
                            new BookPrices() { Id = 2, Amount = 300, BookId = 2, CurrencyId = 2 },
                            new BookPrices() { Id = 3, Amount = 400, BookId = 3, CurrencyId = 3 },
                            new BookPrices() { Id = 4, Amount = 500, BookId = 4, CurrencyId = 4 });

                        modelBuilder.Entity<Book>().HasData(
                            new Book() { Id = 1, Title = "ramayan", Description = "about ramayan", NoOfPages = 100, IsActive = true, CreatedOn = DateTime.Today, LanguageId = 1 },
                            new Book() { Id = 2, Title = "Mahabharath", Description = " about mahabharath", NoOfPages = 100, IsActive = true, CreatedOn = DateTime.Today, LanguageId = 1 },
                            new Book() { Id = 3, Title = "ShivTriology", Description = "about shiva ", NoOfPages = 100, IsActive = true, CreatedOn = DateTime.Today, LanguageId = 1 },
                            new Book() { Id = 4, Title = "Puranas", Description = "about puranas", NoOfPages = 100, IsActive = true, CreatedOn = DateTime.Today, LanguageId = 1 });


                    }


                    public DbSet<Currencies> Currencies { get; set; }

                    public DbSet<Book> Book { get; set; }

                    public DbSet<BookPrices> BookPrices { get; set; }

                    public DbSet<Language> Languages { get; set; }

                    public DbSet<Author> Author { get; set; }
      
        

    }
}
