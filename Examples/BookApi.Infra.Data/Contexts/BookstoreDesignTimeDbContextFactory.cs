namespace BookApi.Infra.Data.Contexts;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class BookstoreDesignTimeDbContextFactory : IDesignTimeDbContextFactory<BookstoreContext>
{
    public BookstoreContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<BookstoreContext>();
        optionsBuilder.UseSqlite("Data Source=app.db");

        return new BookstoreContext(optionsBuilder.Options);
    }
}
