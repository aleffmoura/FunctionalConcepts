namespace BookApi.Domain.Bases;
using System;

public class Entity<TEntity>
    where TEntity : Entity<TEntity>
{
    public Guid Id { get; set; }
}
