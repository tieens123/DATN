using System;
using System.Collections.Generic;

namespace K21CNT2_2110900086_DATN.Models;

public partial class Account
{
    public int AccountId { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? FullName { get; set; }

    public int? RoleId { get; set; }

    public DateTime? LastLogin { get; set; }

    public DateTime? CreateDate { get; set; }

    public virtual Role? Role { get; set; }
}
