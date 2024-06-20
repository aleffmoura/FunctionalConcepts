namespace BookApi.Infra.Data.Features.Books;

using BookApi.Domain.Features.Books;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal class BookEntityConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.ToTable(name: "Books");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedOnAdd();
        builder.Property(e => e.Title).IsRequired();
        builder.Property(e => e.Author).IsRequired();
        builder.Property(e => e.ReleaseDate).IsRequired();
        builder.Property(e => e.BookCoverUrl);
    }
}
