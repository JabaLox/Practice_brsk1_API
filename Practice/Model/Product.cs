using System;
using System.Collections.Generic;

namespace Practice.Model;

public partial class Product
{
    public string? ArticleProduct { get; set; } = null!;

    public string? NameProduct { get; set; } = null!;

    public string? DesriptionProduct { get; set; } = null!;

    public decimal? CostProduct { get; set; }

    public int? Category { get; set; }

    public int? Manufacture { get; set; }

    public int? DiscountProduct { get; set; }

    public int? CountInStockProduct { get; set; }

    public virtual Category? CategoryNavigation { get; set; } = null!;

    public virtual Manufacture? ManufactureNavigation { get; set; } = null!;

    public virtual ICollection<OrderProduct>? OrderProducts { get; set; } = new List<OrderProduct>();
}
