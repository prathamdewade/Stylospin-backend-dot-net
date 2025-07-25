using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Stylo_Spin.Models;

public partial class TblProduct
{
    public int PId { get; set; }

    public int? CId { get; set; }

    public string? PName { get; set; }

    public bool Status { get; set; }

    public string? Description { get; set; }

    public byte[]? ImageData { get; set; }

    public string? ImageName { get; set; }

    public decimal? Price { get; set; }

    public int ProductQuantity { get; set; }

    public virtual TblCategory? CIdNavigation { get; set; }

    [JsonIgnore]
    public virtual ICollection<TblOrder> TblOrders { get; set; } = new List<TblOrder>();
}
