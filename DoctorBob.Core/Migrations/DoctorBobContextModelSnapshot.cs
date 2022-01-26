﻿// <auto-generated />
using System;
using DoctorBob.Core.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DoctorBob.Core.Migrations
{
    [DbContext(typeof(DoctorBobContext))]
    partial class DoctorBobContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.HasSequence<int>("DrugNr", "shared")
                .StartsAt(1000L);

            modelBuilder.HasSequence<int>("PatientNr", "shared")
                .StartsAt(10000L);

            modelBuilder.HasSequence<int>("RobotNr", "shared");

            modelBuilder.HasSequence<int>("StaffNr", "shared")
                .StartsAt(100L);

            modelBuilder.HasSequence<int>("TherapyNr", "shared")
                .StartsAt(100L);

            modelBuilder.HasSequence<int>("TimeModelNr", "shared")
                .StartsAt(1000L);

            modelBuilder.Entity("DoctorBob.Core.DrugManagement.Domain.Drug", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DoseInMg")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Drugs");

                    b.HasData(
                        new
                        {
                            Id = 1000,
                            DoseInMg = 50,
                            Name = "AAA"
                        },
                        new
                        {
                            Id = 1001,
                            DoseInMg = 250,
                            Name = "BBB"
                        },
                        new
                        {
                            Id = 1002,
                            DoseInMg = 75,
                            Name = "CCC"
                        },
                        new
                        {
                            Id = 1003,
                            DoseInMg = 115,
                            Name = "DDD"
                        });
                });

            modelBuilder.Entity("DoctorBob.Core.PatientManagement.Domain.Patient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("EntryDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LeavingDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("MedicalHistory")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TherapyId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TherapyId");

                    b.ToTable("Patients");

                    b.HasData(
                        new
                        {
                            Id = 10000,
                            EntryDate = new DateTime(2022, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Marco",
                            Gender = 0,
                            LastName = "Inverardi",
                            LeavingDate = new DateTime(2022, 6, 22, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            MedicalHistory = "Operation Blinddarum im Jahre 2018",
                            TherapyId = 103
                        },
                        new
                        {
                            Id = 10001,
                            EntryDate = new DateTime(2022, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Heidi",
                            Gender = 1,
                            LastName = "Geissbühler",
                            LeavingDate = new DateTime(2022, 6, 21, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            MedicalHistory = "Hüftoperation 01.2017",
                            TherapyId = 102
                        },
                        new
                        {
                            Id = 10002,
                            EntryDate = new DateTime(2022, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Antonio",
                            Gender = 0,
                            LastName = "Eichholzer",
                            LeavingDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            MedicalHistory = "",
                            TherapyId = 103
                        },
                        new
                        {
                            Id = 10003,
                            EntryDate = new DateTime(2022, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Martha",
                            Gender = 1,
                            LastName = "Watson",
                            LeavingDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            MedicalHistory = "Schlaganfall im Juli 2015, diverse tägliche Medikamentenzunahme",
                            TherapyId = 100
                        },
                        new
                        {
                            Id = 10004,
                            EntryDate = new DateTime(2022, 6, 26, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Flavio",
                            Gender = 0,
                            LastName = "Frei",
                            LeavingDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            MedicalHistory = "",
                            TherapyId = 102
                        },
                        new
                        {
                            Id = 10005,
                            EntryDate = new DateTime(2022, 6, 24, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Lisa",
                            Gender = 1,
                            LastName = "Zellweger",
                            LeavingDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            MedicalHistory = "Herzstillstand 05.2011 mit anschliessender Reanimation",
                            TherapyId = 101
                        });
                });

            modelBuilder.Entity("DoctorBob.Core.RoboManagement.Domain.Robot", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Activity")
                        .HasColumnType("int");

                    b.Property<int>("Location")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Power")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Robot");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Activity = 0,
                            Location = 0,
                            Name = "DoctorBob 1.0",
                            Power = 100
                        });
                });

            modelBuilder.Entity("DoctorBob.Core.StaffManagement.Domain.Staff", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("StaffMembers");

                    b.HasData(
                        new
                        {
                            Id = 100,
                            Email = "",
                            FirstName = "Angela",
                            LastName = "Schmitter",
                            Role = 2,
                            Username = "aschmitter"
                        },
                        new
                        {
                            Id = 101,
                            Email = "",
                            FirstName = "Rudolf",
                            LastName = "Sahli",
                            Role = 1,
                            Username = "rsahli"
                        },
                        new
                        {
                            Id = 102,
                            Email = "",
                            FirstName = "Ibrahim",
                            LastName = "Kesay",
                            Role = 1,
                            Username = "ikesay"
                        },
                        new
                        {
                            Id = 120,
                            Email = "",
                            FirstName = "Selina",
                            LastName = "Kluser",
                            Role = 0,
                            Username = "skluser"
                        },
                        new
                        {
                            Id = 121,
                            Email = "",
                            FirstName = "Jacqueline",
                            LastName = "Seitz",
                            Role = 0,
                            Username = "sseitz"
                        },
                        new
                        {
                            Id = 122,
                            Email = "",
                            FirstName = "Roland",
                            LastName = "Agger",
                            Role = 0,
                            Username = "ragger"
                        },
                        new
                        {
                            Id = 123,
                            Email = "",
                            FirstName = "Sybille",
                            LastName = "Fernandez",
                            Role = 0,
                            Username = "sfernandez"
                        },
                        new
                        {
                            Id = 130,
                            Email = "",
                            FirstName = "Walter",
                            LastName = "Seger",
                            Role = 3,
                            Username = "wseger"
                        },
                        new
                        {
                            Id = 140,
                            Email = "",
                            FirstName = "Katherina",
                            LastName = "Popp",
                            Role = 4,
                            Username = "kpopp"
                        });
                });

            modelBuilder.Entity("DoctorBob.Core.TherapyManagement.Domain.Therapy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CaringStaffId")
                        .HasColumnType("int");

                    b.Property<int>("DrugId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("QuantityOfDrug")
                        .HasColumnType("int");

                    b.Property<int>("TimeModelId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CaringStaffId");

                    b.HasIndex("DrugId");

                    b.HasIndex("TimeModelId");

                    b.ToTable("Therapies");

                    b.HasData(
                        new
                        {
                            Id = 100,
                            CaringStaffId = 120,
                            DrugId = 1000,
                            Name = "AAA 1x täglich, 50mg",
                            QuantityOfDrug = 1,
                            TimeModelId = 1001
                        },
                        new
                        {
                            Id = 101,
                            CaringStaffId = 121,
                            DrugId = 1001,
                            Name = "BBB 3x täglich, 250mg",
                            QuantityOfDrug = 3,
                            TimeModelId = 1002
                        },
                        new
                        {
                            Id = 102,
                            CaringStaffId = 123,
                            DrugId = 1002,
                            Name = "CCC 1x täglich, 75mg",
                            QuantityOfDrug = 1,
                            TimeModelId = 1003
                        },
                        new
                        {
                            Id = 103,
                            CaringStaffId = 122,
                            DrugId = 1003,
                            Name = "DDD 2x täglich, 115mg",
                            QuantityOfDrug = 2,
                            TimeModelId = 1000
                        });
                });

            modelBuilder.Entity("DoctorBob.Core.TherapyManagement.Domain.TimeModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Time")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TimeModels");

                    b.HasData(
                        new
                        {
                            Id = 1000,
                            Time = "08.30 / 11.00"
                        },
                        new
                        {
                            Id = 1001,
                            Time = "11.00"
                        },
                        new
                        {
                            Id = 1002,
                            Time = "08.00 / 14.30 / 18.30"
                        },
                        new
                        {
                            Id = 1003,
                            Time = "18.00"
                        });
                });

            modelBuilder.Entity("DoctorBob.Core.PatientManagement.Domain.Patient", b =>
                {
                    b.HasOne("DoctorBob.Core.TherapyManagement.Domain.Therapy", "Therapy")
                        .WithMany()
                        .HasForeignKey("TherapyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Therapy");
                });

            modelBuilder.Entity("DoctorBob.Core.TherapyManagement.Domain.Therapy", b =>
                {
                    b.HasOne("DoctorBob.Core.StaffManagement.Domain.Staff", "CaringStaff")
                        .WithMany()
                        .HasForeignKey("CaringStaffId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DoctorBob.Core.DrugManagement.Domain.Drug", "Drug")
                        .WithMany()
                        .HasForeignKey("DrugId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DoctorBob.Core.TherapyManagement.Domain.TimeModel", "TimeModel")
                        .WithMany()
                        .HasForeignKey("TimeModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CaringStaff");

                    b.Navigation("Drug");

                    b.Navigation("TimeModel");
                });
#pragma warning restore 612, 618
        }
    }
}
