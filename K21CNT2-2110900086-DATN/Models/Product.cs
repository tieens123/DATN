using System;
using System.Collections.Generic;

namespace K21CNT2_2110900086_DATN.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public string? Description { get; set; }

    public string? Image { get; set; }

    public int Price { get; set; }

    public int? UnitsInStock { get; set; }

    public int? CategoryId { get; set; }

    public int? SizeId { get; set; }

    public int? ColorId { get; set; }

    public string? AgeRange { get; set; }

    public string? Material { get; set; }

    public string? Brand { get; set; }

    public virtual Category? Category { get; set; }

    public virtual Color? Color { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual Size? Size { get; set; }
}
