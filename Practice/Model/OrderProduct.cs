using System;
using System.Collections.Generic;

namespace Practice.Model;

public partial class OrderProduct
{
    public int IdOrder { get; set; }

    public int CountProduct { get; set; }

    public string ArticleProduct { get; set; } = null!;

    public virtual Product ArticleProductNavigation { get; set; } = null!;

    public virtual Order IdOrderNavigation { get; set; } = null!;
}
