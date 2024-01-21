using System;
using System.Collections.Generic;

namespace UniscoLMS.DataBaseModels;

public partial class CardPaymentTypeInfo
{
    public int Id { get; set; }

    public int PaymentOrderId { get; set; }

    public int UserCardId { get; set; }

    public virtual PaymentOrder PaymentOrder { get; set; } = null!;

    public virtual UserCardPayment UserCard { get; set; } = null!;
}
