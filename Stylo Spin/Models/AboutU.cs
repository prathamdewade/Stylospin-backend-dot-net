using System;
using System.Collections.Generic;

namespace Stylo_Spin.Models;

public partial class AboutU
{
    public int Id { get; set; }

    public string? Heading { get; set; }

    public string? Paragraph { get; set; }

    public string? SubHeading { get; set; }

    public byte[]? ImageData { get; set; }

    public string? ImageName { get; set; }
}
