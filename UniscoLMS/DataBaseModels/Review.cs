using System;
using System.Collections.Generic;

namespace UniscoLMS.DataBaseModels;

public partial class Review
{
    public int Id { get; set; }

    public int Rate { get; set; }

    public string? Comment { get; set; }

    public Guid ReviwerId { get; set; }

    public Guid UserId { get; set; }

    public virtual User Reviwer { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
