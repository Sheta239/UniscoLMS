using System;
using System.Collections.Generic;

namespace UniscoLMS.DataBaseModels;

public partial class Status
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<PaymentOrder> PaymentOrders { get; set; } = new List<PaymentOrder>();
}
