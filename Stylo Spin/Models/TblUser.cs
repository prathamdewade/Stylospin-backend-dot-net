using System;
using System.Collections.Generic;

namespace Stylo_Spin.Models;

public partial class TblUser
{
    public int Id { get; set; }

    public string? Username { get; set; }

    public string? UserEmail { get; set; }

    public string? Password { get; set; }
}
