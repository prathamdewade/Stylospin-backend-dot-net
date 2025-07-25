using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Stylo_Spin.Models;

public partial class TblCategory
{
    public int CId { get; set; }

    public string CName { get; set; } = null!;

    public bool Status { get; set; }

    [JsonIgnore]
    public virtual ICollection<TblProduct> TblProducts { get; set; } = new List<TblProduct>();
}
