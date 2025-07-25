using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Stylo_Spin.Models;

public partial class TblCustomer
{
    public int Id { get; set; }

    public string CName { get; set; } = null!;

    public string CEmail { get; set; } = null!;

    public string? ContactNumber { get; set; }

    public string? Address { get; set; }

    [JsonIgnore]
    public virtual ICollection<TblOrder> TblOrders { get; set; } = new List<TblOrder>();
}
