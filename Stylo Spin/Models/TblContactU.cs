using System;
using System.Collections.Generic;

namespace Stylo_Spin.Models;

public partial class TblContactU
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? ContactNumber { get; set; }

    public string Query { get; set; } = null!;

    public string? Subject { get; set; }
}
