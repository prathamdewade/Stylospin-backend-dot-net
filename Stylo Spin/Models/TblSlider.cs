using System;
using System.Collections.Generic;

namespace Stylo_Spin.Models;

public partial class TblSlider
{
    public int Id { get; set; }

    public byte[]? ImageData { get; set; }

    public string? ImageName { get; set; }

    public string? Description { get; set; }
}
