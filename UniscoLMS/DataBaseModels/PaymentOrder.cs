using System;
using System.Collections.Generic;

namespace UniscoLMS.DataBaseModels;

public partial class PaymentOrder
{
    public int Id { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? PaymentDate { get; set; }

    public int StatusId { get; set; }

    public string Trn { get; set; } = null!;

    public int PaymentMethodId { get; set; }

    public Guid UserId { get; set; }

    public virtual ICollection<CardPaymentTypeInfo> CardPaymentTypeInfos { get; set; } = new List<CardPaymentTypeInfo>();

    public virtual PaymentMethod PaymentMethod { get; set; } = null!;

    public virtual Status Status { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public virtual ICollection<UserCourse> UserCourses { get; set; } = new List<UserCourse>();
}
