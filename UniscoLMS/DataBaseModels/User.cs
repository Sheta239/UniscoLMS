using System;
using System.Collections.Generic;

namespace UniscoLMS.DataBaseModels;

public partial class User
{
    public Guid UserId { get; set; }

    public string FirstName { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? GoogleId { get; set; }

    public int RoleId { get; set; }

    public string? Mobile { get; set; }

    public bool? EmailValidation { get; set; }

    public bool? PhoneValidation { get; set; }

    public string? Bio { get; set; }

    public bool? Approved { get; set; }

    public bool? Verified { get; set; }


    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

    public virtual ICollection<PaymentOrder> PaymentOrders { get; set; } = new List<PaymentOrder>();

    public virtual ICollection<Review> ReviewReviwers { get; set; } = new List<Review>();

    public virtual ICollection<Review> ReviewUsers { get; set; } = new List<Review>();

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();

    public virtual ICollection<UserCardPayment> UserCardPayments { get; set; } = new List<UserCardPayment>();

    public virtual ICollection<UserCourse> UserCourses { get; set; } = new List<UserCourse>();

    public virtual ICollection<UserTag> UserTags { get; set; } = new List<UserTag>();

    public virtual ICollection<ValidationCode> ValidationCodes { get; set; } = new List<ValidationCode>();
}
