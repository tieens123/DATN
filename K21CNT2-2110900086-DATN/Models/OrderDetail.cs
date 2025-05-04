using System;
using System.Collections.Generic;

namespace K21CNT2_2110900086_DATN.Models;

public partial class OrderDetail
{
    public int OrderDetailId { get; set; }

    public int? OrderId { get; set; }

    public int? ProductId { get; set; }

    public int? Quantity { get; set; }

    public int? Total { get; set; }

    public virtual Order? Order { get; set; }

    public virtual Product? Product { get; set; }
}
