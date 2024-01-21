using System;
using System.Collections.Generic;

namespace UniscoLMS.DataBaseModels;

public partial class UserCourse
{
    public int Id { get; set; }

    public int CourseId { get; set; }

    public int? TransactionId { get; set; }

    public Guid LearnerId { get; set; }

    public virtual User Learner { get; set; } = null!;

    public virtual Course Course { get; set; } = null!;

    public virtual PaymentOrder? Transaction { get; set; }
}
