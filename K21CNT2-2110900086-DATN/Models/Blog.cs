using System;
using System.Collections.Generic;

namespace K21CNT2_2110900086_DATN.Models;

public partial class Blog
{
    public int BlogId { get; set; }

    public string? BlogName { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? Image { get; set; }

    public string? Author { get; set; }
}
