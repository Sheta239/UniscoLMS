using System;
using System.Collections.Generic;

namespace UniscoLMS.DataBaseModels;

public partial class Course
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public bool? IsCanceled { get; set; }

    public Guid ExpertUser { get; set; }

    public int Duration { get; set; }

    public int TagId { get; set; }

    public string? Notes { get; set; }

    public int SessinType { get; set; }

    public decimal Price { get; set; }

    public virtual User ExpertUserNavigation { get; set; } = null!;
    public virtual Tag Tag { get; set; } = null!;

    public virtual ICollection<UserCourse> UserCourses { get; set; } = new List<UserCourse>();
}
