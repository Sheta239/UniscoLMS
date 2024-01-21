using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace UniscoLMS.DataBaseModels;

public partial class UniscoDbContext : DbContext
{
    public UniscoDbContext()
    {
    }

    public UniscoDbContext(DbContextOptions<UniscoDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CardPaymentTypeInfo> CardPaymentTypeInfos { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }

    public virtual DbSet<PaymentOrder> PaymentOrders { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Session> Sessions { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserCardPayment> UserCardPayments { get; set; }

    public virtual DbSet<UserCourse> UserCourses { get; set; }

    public virtual DbSet<UserTag> UserTags { get; set; }

    public virtual DbSet<ValidationCode> ValidationCodes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
