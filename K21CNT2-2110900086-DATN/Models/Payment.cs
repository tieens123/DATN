using System;
using System.Collections.Generic;

namespace K21CNT2_2110900086_DATN.Models;

public partial class Payment
{
    public int PaymentId { get; set; }

    public string? PaymentMethod { get; set; }

    public DateTime? PaymentDate { get; set; }

    public int? OrderId { get; set; }

    public int? CustomerId { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Order? Order { get; set; }
}
