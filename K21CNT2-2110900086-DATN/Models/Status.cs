using System;
using System.Collections.Generic;

namespace K21CNT2_2110900086_DATN.Models;

public partial class Status
{
    public int StatusId { get; set; }

    public string? StatusName { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
