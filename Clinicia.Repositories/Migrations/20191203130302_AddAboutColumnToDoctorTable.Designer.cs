﻿// <auto-generated />
using System;
using Clinicia.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Clinicia.Repositories.Migrations
{
    [DbContext(typeof(CliniciaDbContext))]
    [Migration("20191203130302_AddAboutColumnToDoctorTable")]
    partial class AddAboutColumnToDoctorTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Clinicia.Repositories.Schemas.DbAppointment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AppointmentDate");

                    b.Property<Guid>("CheckingServiceId");

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<string>("CreatedUser")
                        .HasMaxLength(50);

                    b.Property<Guid>("DoctorId");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDelete");

                    b.Property<string>("Note");

                    b.Property<Guid>("PatientId");

                    b.Property<string>("PrivateResult");

                    b.Property<string>("PublicResult");

                    b.Property<int>("SendNotificationBeforeMinutes");

                    b.Property<int>("Status");

                    b.Property<int>("TotalMinutes");

                    b.Property<decimal>("TotalPrice");

                    b.Property<DateTime?>("UpdatedDate");

                    b.Property<string>("UpdatedUser")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("CheckingServiceId");

                    b.HasIndex("DoctorId");

                    b.HasIndex("PatientId");

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("Clinicia.Repositories.Schemas.DbCheckingService", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<string>("CreatedUser")
                        .HasMaxLength(50);

                    b.Property<string>("Description")
                        .HasMaxLength(500);

                    b.Property<Guid>("DoctorId");

                    b.Property<int>("DurationInMinutes");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDelete");

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<decimal>("Price");

                    b.Property<DateTime?>("UpdatedDate");

                    b.Property<string>("UpdatedUser")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("DoctorId");

                    b.ToTable("CheckingServices");
                });

            modelBuilder.Entity("Clinicia.Repositories.Schemas.DbDeviceUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<string>("CreatedUser")
                        .HasMaxLength(50);

                    b.Property<string>("DeviceToken")
                        .IsRequired();

                    b.Property<string>("DeviceType")
                        .IsRequired();

                    b.Property<string>("DeviceUuid")
                        .IsRequired();

                    b.Property<DateTime?>("ExpiredAt");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDelete");

                    b.Property<DateTime?>("UpdatedDate");

                    b.Property<string>("UpdatedUser")
                        .HasMaxLength(50);

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("DeviceUsers");
                });

            modelBuilder.Entity("Clinicia.Repositories.Schemas.DbFavorite", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<string>("CreatedUser")
                        .HasMaxLength(50);

                    b.Property<Guid>("DoctorId");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDelete");

                    b.Property<Guid>("PatientId");

                    b.Property<DateTime?>("UpdatedDate");

                    b.Property<string>("UpdatedUser")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("DoctorId");

                    b.HasIndex("PatientId");

                    b.ToTable("Favorites");
                });

            modelBuilder.Entity("Clinicia.Repositories.Schemas.DbLocation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FormattedAddress");

                    b.Property<decimal>("Latitude");

                    b.Property<decimal>("Longitude");

                    b.HasKey("Id");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("Clinicia.Repositories.Schemas.DbNoAttendance", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<string>("CreatedUser")
                        .HasMaxLength(50);

                    b.Property<Guid>("DoctorId");

                    b.Property<DateTime>("FromDate");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDelete");

                    b.Property<string>("Reason")
                        .HasMaxLength(256);

                    b.Property<DateTime>("ToDate");

                    b.Property<DateTime?>("UpdatedDate");

                    b.Property<string>("UpdatedUser")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("DoctorId");

                    b.ToTable("NoAttendances");
                });

            modelBuilder.Entity("Clinicia.Repositories.Schemas.DbNotification", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content")
                        .IsRequired();

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<string>("CreatedUser")
                        .HasMaxLength(50);

                    b.Property<bool>("HasRead");

                    b.Property<string>("Image");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDelete");

                    b.Property<DateTime>("NotificationDate");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.Property<DateTime?>("UpdatedDate");

                    b.Property<string>("UpdatedUser")
                        .HasMaxLength(50);

                    b.Property<Guid?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("Clinicia.Repositories.Schemas.DbReview", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Comment");

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<string>("CreatedUser")
                        .HasMaxLength(50);

                    b.Property<Guid>("DoctorId");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDelete");

                    b.Property<Guid>("PatientId");

                    b.Property<int>("Rating");

                    b.Property<DateTime>("ReviewDate");

                    b.Property<DateTime?>("UpdatedDate");

                    b.Property<string>("UpdatedUser")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("DoctorId");

                    b.HasIndex("PatientId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("Clinicia.Repositories.Schemas.DbRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Clinicia.Repositories.Schemas.DbSpecialty", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<string>("CreatedUser")
                        .HasMaxLength(50);

                    b.Property<string>("Image");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDelete");

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<DateTime?>("UpdatedDate");

                    b.Property<string>("UpdatedUser")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Specialties");
                });

            modelBuilder.Entity("Clinicia.Repositories.Schemas.DbUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("date");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<string>("CreatedUser")
                        .HasMaxLength(50);

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName")
                        .HasMaxLength(256);

                    b.Property<bool?>("Gender");

                    b.Property<string>("ImageProfile");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDelete");

                    b.Property<string>("LastName")
                        .HasMaxLength(256);

                    b.Property<Guid?>("LocationId");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("OtpCode")
                        .HasMaxLength(6);

                    b.Property<DateTime?>("OtpExpiredAt");

                    b.Property<string>("OtpToken");

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<DateTime?>("UpdatedDate");

                    b.Property<string>("UpdatedUser")
                        .HasMaxLength(50);

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("Users");

                    b.HasDiscriminator<string>("Discriminator").HasValue("DbUser");
                });

            modelBuilder.Entity("Clinicia.Repositories.Schemas.DbUserRole", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<Guid>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("Clinicia.Repositories.Schemas.DbWorkingSchedule", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<string>("CreatedUser")
                        .HasMaxLength(50);

                    b.Property<Guid>("DoctorId");

                    b.Property<DateTime>("FromDate");

                    b.Property<string>("Hours")
                        .HasMaxLength(126);

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDelete");

                    b.Property<DateTime?>("UpdatedDate");

                    b.Property<string>("UpdatedUser")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("DoctorId");

                    b.ToTable("WorkingSchedules");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<Guid>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderDisplayName");

                    b.Property<Guid>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .HasMaxLength(128);

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserTokens");
                });

            modelBuilder.Entity("Clinicia.Repositories.Schemas.DbDoctor", b =>
                {
                    b.HasBaseType("Clinicia.Repositories.Schemas.DbUser");

                    b.Property<string>("About");

                    b.Property<string>("Awards");

                    b.Property<string>("Clinic")
                        .HasMaxLength(256);

                    b.Property<string>("MedicalSchool")
                        .HasMaxLength(256);

                    b.Property<Guid?>("SpecialtyId");

                    b.Property<int?>("YearExperience");

                    b.HasIndex("SpecialtyId");

                    b.ToTable("Doctors");

                    b.HasDiscriminator().HasValue("DbDoctor");
                });

            modelBuilder.Entity("Clinicia.Repositories.Schemas.DbPatient", b =>
                {
                    b.HasBaseType("Clinicia.Repositories.Schemas.DbUser");

                    b.Property<bool>("PushNotificationEnabled")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<int>("UnseenNotificationCount");

                    b.ToTable("Patients");

                    b.HasDiscriminator().HasValue("DbPatient");
                });

            modelBuilder.Entity("Clinicia.Repositories.Schemas.DbAppointment", b =>
                {
                    b.HasOne("Clinicia.Repositories.Schemas.DbCheckingService", "CheckingService")
                        .WithMany()
                        .HasForeignKey("CheckingServiceId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Clinicia.Repositories.Schemas.DbDoctor", "Doctor")
                        .WithMany("Appointments")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Clinicia.Repositories.Schemas.DbPatient", "Patient")
                        .WithMany("Appointments")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Clinicia.Repositories.Schemas.DbCheckingService", b =>
                {
                    b.HasOne("Clinicia.Repositories.Schemas.DbDoctor", "Doctor")
                        .WithMany("CheckingServices")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Clinicia.Repositories.Schemas.DbDeviceUser", b =>
                {
                    b.HasOne("Clinicia.Repositories.Schemas.DbUser", "User")
                        .WithMany("Devices")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Clinicia.Repositories.Schemas.DbFavorite", b =>
                {
                    b.HasOne("Clinicia.Repositories.Schemas.DbDoctor", "Doctor")
                        .WithMany("Favorites")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Clinicia.Repositories.Schemas.DbPatient", "Patient")
                        .WithMany("Favorites")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Clinicia.Repositories.Schemas.DbNoAttendance", b =>
                {
                    b.HasOne("Clinicia.Repositories.Schemas.DbDoctor", "Doctor")
                        .WithMany("NoAttendances")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Clinicia.Repositories.Schemas.DbNotification", b =>
                {
                    b.HasOne("Clinicia.Repositories.Schemas.DbUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Clinicia.Repositories.Schemas.DbReview", b =>
                {
                    b.HasOne("Clinicia.Repositories.Schemas.DbDoctor", "Doctor")
                        .WithMany("Reviews")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Clinicia.Repositories.Schemas.DbPatient", "Patient")
                        .WithMany("Reviews")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Clinicia.Repositories.Schemas.DbUser", b =>
                {
                    b.HasOne("Clinicia.Repositories.Schemas.DbLocation", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId");
                });

            modelBuilder.Entity("Clinicia.Repositories.Schemas.DbUserRole", b =>
                {
                    b.HasOne("Clinicia.Repositories.Schemas.DbRole", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Clinicia.Repositories.Schemas.DbUser", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Clinicia.Repositories.Schemas.DbWorkingSchedule", b =>
                {
                    b.HasOne("Clinicia.Repositories.Schemas.DbDoctor", "Doctor")
                        .WithMany("WorkingSchedules")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("Clinicia.Repositories.Schemas.DbRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("Clinicia.Repositories.Schemas.DbUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("Clinicia.Repositories.Schemas.DbUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("Clinicia.Repositories.Schemas.DbUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Clinicia.Repositories.Schemas.DbDoctor", b =>
                {
                    b.HasOne("Clinicia.Repositories.Schemas.DbSpecialty", "Specialty")
                        .WithMany("Doctors")
                        .HasForeignKey("SpecialtyId");
                });
#pragma warning restore 612, 618
        }
    }
}
