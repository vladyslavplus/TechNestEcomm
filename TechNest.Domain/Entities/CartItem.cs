﻿namespace TechNest.Domain.Entities;

public class CartItem
{
    public Guid Id { get; set; }
    public Guid? UserId { get; set; }
    public Guid ProductId { get; set; }
    public Product Product { get; set; } = null!;
    public int Quantity { get; set; }
    public DateTime AddedAt { get; set; } = DateTime.UtcNow;
}