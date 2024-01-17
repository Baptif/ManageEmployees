using System;
using System.Collections.Generic;
using ManageEmployees.Entities;
using Microsoft.EntityFrameworkCore;

namespace ManageEmployees.Infrastructures.Database;

public partial class ManageEmployeeDbContext : DbContext
{
    public ManageEmployeeDbContext()
    {
    }

    public ManageEmployeeDbContext(DbContextOptions<ManageEmployeeDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Attendance> Attendances { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<EmployeesDepartment> EmployeesDepartments { get; set; }

    public virtual DbSet<LeaveRequest> LeaveRequests { get; set; }

    public virtual DbSet<LeaveRequestStatus> LeaveRequestStatuses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Attendance>(entity =>
        {
            entity.HasKey(e => e.AttendanceId).HasName("PK__Attendan__8B69261C72751750");

            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.StartDate).HasColumnType("datetime");

            entity.HasOne(d => d.Employee).WithMany(p => p.Attendances)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Attendances_EmployeeId");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("PK__Departme__B2079BED1413D17A");

            entity.HasIndex(e => e.Name, "UK_Departments_Name").IsUnique();

            entity.HasIndex(e => e.Name, "UQ__Departme__737584F64E103E14").IsUnique();

            entity.Property(e => e.Address)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Description)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__7AD04F111C4DAF04");

            entity.HasIndex(e => e.Email, "UK_Employees_Email").IsUnique();

            entity.Property(e => e.BirthdDate).HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(13)
                .IsUnicode(false);
            entity.Property(e => e.Position)
                .HasMaxLength(150)
                .IsUnicode(false);
        });

        modelBuilder.Entity<EmployeesDepartment>(entity =>
        {
            entity.HasKey(e => e.EmployeesDepartmentsId).HasName("PK__Employee__1B69212B00E8366B");

            entity.HasOne(d => d.Department).WithMany(p => p.EmployeesDepartments)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmployeesDepartments_DepartmentId");

            entity.HasOne(d => d.Employee).WithMany(p => p.EmployeesDepartments)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmployeesDepartments_EmployeeId");
        });

        modelBuilder.Entity<LeaveRequest>(entity =>
        {
            entity.HasKey(e => e.LeaveRequestId).HasName("PK__LeaveReq__609421EEDD31E41C");

            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.RequestDate).HasColumnType("datetime");
            entity.Property(e => e.StartDate).HasColumnType("datetime");

            entity.HasOne(d => d.Employee).WithMany(p => p.LeaveRequests)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LeaveRequests_EmployeeId");

            entity.HasOne(d => d.LeaveRequestStatus).WithMany(p => p.LeaveRequests)
                .HasForeignKey(d => d.LeaveRequestStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LeaveRequests_LeaveRequestStatusId");
        });

        modelBuilder.Entity<LeaveRequestStatus>(entity =>
        {
            entity.HasKey(e => e.LeaveRequestStatusId).HasName("PK__LeaveReq__14C2CED11CD5470E");

            entity.ToTable("LeaveRequestStatus");

            entity.HasIndex(e => e.Status, "UQ__LeaveReq__3A15923F05A8F4BC").IsUnique();

            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
