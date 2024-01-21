using System;
using System.Collections.Generic;

namespace UniscoLMS.DataBaseModels;

public partial class UserCardPayment
{
    public int Id { get; set; }

    public Guid UserId { get; set; }

    public long? IsDefault { get; set; }

    public string CardNumber { get; set; } = null!;

    public string Token { get; set; } = null!;

    public virtual ICollection<CardPaymentTypeInfo> CardPaymentTypeInfos { get; set; } = new List<CardPaymentTypeInfo>();

    public virtual User User { get; set; } = null!;
}
