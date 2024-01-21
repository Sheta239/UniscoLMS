using System;
using System.Collections.Generic;

namespace UniscoLMS.DataBaseModels;

public partial class ValidationCode
{
    public int Id { get; set; }

    public Guid UserId { get; set; }

    public string Code { get; set; } = null!;

    public DateTime GeneratedDate { get; set; }

    public DateTime ExpirationDate { get; set; }

    public virtual User User { get; set; } = null!;
}
