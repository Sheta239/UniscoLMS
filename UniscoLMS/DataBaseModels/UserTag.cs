using System;
using System.Collections.Generic;

namespace UniscoLMS.DataBaseModels;

public partial class UserTag
{
    public int Id { get; set; }

    public int TagId { get; set; }

    public Guid UserId { get; set; }

    public virtual Tag Tag { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
