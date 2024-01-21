using System;
using System.Collections.Generic;

namespace UniscoLMS.DataBaseModels;

public partial class Session
{
    public int SessionId { get; set; }

    public Guid UserId { get; set; }

    public string Token { get; set; } = null!;

    public DateTime ExpirationDate { get; set; }

    public virtual User User { get; set; } = null!;
}
