using System;
using System.Collections.Generic;

namespace UniscoLMS.DataBaseModels;

public partial class Tag
{
    public int Id { get; set; }

    public string TagName { get; set; } = null!;

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

    public virtual ICollection<UserTag> UserTags { get; set; } = new List<UserTag>();
}
