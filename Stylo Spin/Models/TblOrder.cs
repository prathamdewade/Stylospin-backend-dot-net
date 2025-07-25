using System;
using System.Collections.Generic;

namespace Stylo_Spin.Models;

public partial class TblOrder
{
    public int OId { get; set; }

    public int CId { get; set; }

    public int PId { get; set; }

    public int ProductQuantity { get; set; }

    public decimal TotalPrice { get; set; }

    public string? Message { get; set; }

    public string PaymentStatus { get; set; } = null!;

    public DateTime OrdaerDate { get; set; }

    public virtual TblCustomer CIdNavigation { get; set; } = null!;

    public virtual TblProduct PIdNavigation { get; set; } = null!;
}
