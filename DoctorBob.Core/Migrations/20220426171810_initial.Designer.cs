﻿// <auto-generated />
using System;
using DoctorBob.Core.Common.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DoctorBob.Core.Migrations
{
    [DbContext(typeof(DoctorBobContext))]
    [Migration("20220426171810_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.13");

            modelBuilder.Entity("DoctorBob.Core.DrugManagement.Domain.Drug", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<int>("DoseInMg")
                        .HasColumnType("int");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Drugs");

                    b.HasData(
                        new
                        {
                            Id = 1000,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Senkung Puls",
                            DoseInMg = 5,
                            ModifiedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Bisoprolol"
                        },
                        new
                        {
                            Id = 1001,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Regulation Blutzucker",
                            DoseInMg = 14,
                            ModifiedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Metformin"
                        },
                        new
                        {
                            Id = 1002,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Schmerzmittel",
                            DoseInMg = 500,
                            ModifiedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Novalgin"
                        },
                        new
                        {
                            Id = 1003,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Senkung Blutdruck",
                            DoseInMg = 5,
                            ModifiedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Amlodipin"
                        });
                });

            modelBuilder.Entity("DoctorBob.Core.PatientManagement.Domain.Patient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("EntryDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("FirstName")
                        .HasColumnType("longtext");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("LeavingDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("MedicalHistory")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<int>("RoomId")
                        .HasColumnType("int");

                    b.Property<int>("TherapyId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoomId");

                    b.HasIndex("TherapyId");

                    b.ToTable("Patients");

                    b.HasData(
                        new
                        {
                            Id = 10002,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EntryDate = new DateTime(2022, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Antonio",
                            Gender = 0,
                            LastName = "Eichholzer",
                            LeavingDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            MedicalHistory = "",
                            ModifiedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            RoomId = 101,
                            TherapyId = 103
                        },
                        new
                        {
                            Id = 10003,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EntryDate = new DateTime(2022, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Martha",
                            Gender = 1,
                            LastName = "Watson",
                            LeavingDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            MedicalHistory = "Schlaganfall im Juli 2015, diverse tägliche Medikamentenzunahme",
                            ModifiedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            RoomId = 102,
                            TherapyId = 100
                        },
                        new
                        {
                            Id = 10004,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EntryDate = new DateTime(2022, 6, 26, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Flavio",
                            Gender = 0,
                            LastName = "Frei",
                            LeavingDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            MedicalHistory = "",
                            ModifiedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            RoomId = 103,
                            TherapyId = 102
                        },
                        new
                        {
                            Id = 10005,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EntryDate = new DateTime(2022, 6, 24, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Lisa",
                            Gender = 1,
                            LastName = "Zellweger",
                            LeavingDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            MedicalHistory = "Herzstillstand 05.2011 mit anschliessender Reanimation",
                            ModifiedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            RoomId = 103,
                            TherapyId = 101
                        });
                });

            modelBuilder.Entity("DoctorBob.Core.PatientManagement.Domain.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Rooms");

                    b.HasData(
                        new
                        {
                            Id = 101,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ModifiedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Raum 101"
                        },
                        new
                        {
                            Id = 102,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ModifiedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Raum 102"
                        },
                        new
                        {
                            Id = 103,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ModifiedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Raum 103"
                        },
                        new
                        {
                            Id = 104,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ModifiedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Raum 104"
                        },
                        new
                        {
                            Id = 900,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ModifiedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Spital-Apotheke"
                        },
                        new
                        {
                            Id = 999,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ModifiedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Homebase"
                        });
                });

            modelBuilder.Entity("DoctorBob.Core.RoboManagement.Domain.Robot", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Activity")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<int>("CurrentLocation")
                        .HasColumnType("int");

                    b.Property<int>("LastRoomId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Power")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LastRoomId");

                    b.ToTable("Robot");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Activity = 0,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CurrentLocation = 0,
                            LastRoomId = 999,
                            ModifiedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "DoctorBob 1.0",
                            Power = 100
                        });
                });

            modelBuilder.Entity("DoctorBob.Core.StaffManagement.Domain.Staff", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<string>("FirstName")
                        .HasColumnType("longtext");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("StaffMembers");

                    b.HasData(
                        new
                        {
                            Id = 100,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Angela",
                            LastName = "Schmitter",
                            ModifiedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Role = 2,
                            Username = "aschmitter"
                        },
                        new
                        {
                            Id = 101,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Rudolf",
                            LastName = "Sahli",
                            ModifiedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Role = 1,
                            Username = "rsahli"
                        },
                        new
                        {
                            Id = 102,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Ibrahim",
                            LastName = "Kesay",
                            ModifiedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Role = 1,
                            Username = "ikesay"
                        },
                        new
                        {
                            Id = 120,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Selina",
                            LastName = "Kluser",
                            ModifiedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Role = 0,
                            Username = "skluser"
                        },
                        new
                        {
                            Id = 121,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Jacqueline",
                            LastName = "Seitz",
                            ModifiedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Role = 0,
                            Username = "sseitz"
                        },
                        new
                        {
                            Id = 122,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Roland",
                            LastName = "Agger",
                            ModifiedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Role = 0,
                            Username = "ragger"
                        },
                        new
                        {
                            Id = 123,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Sybille",
                            LastName = "Fernandez",
                            ModifiedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Role = 0,
                            Username = "sfernandez"
                        },
                        new
                        {
                            Id = 130,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Walter",
                            LastName = "Seger",
                            ModifiedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Role = 3,
                            Username = "wseger"
                        },
                        new
                        {
                            Id = 140,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Katherina",
                            LastName = "Popp",
                            ModifiedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Role = 5,
                            Username = "kpopp"
                        },
                        new
                        {
                            Id = 160,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Fredi",
                            LastName = "Holenstein",
                            ModifiedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Role = 4,
                            Username = "fholenstein"
                        });
                });

            modelBuilder.Entity("DoctorBob.Core.TherapyManagement.Domain.Therapy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CaringStaffId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<int>("DrugId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

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
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DrugId = 1000,
                            ModifiedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "AAA 1x täglich, 50mg",
                            QuantityOfDrug = 1,
                            TimeModelId = 1001
                        },
                        new
                        {
                            Id = 101,
                            CaringStaffId = 121,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DrugId = 1001,
                            ModifiedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "BBB 3x täglich, 250mg",
                            QuantityOfDrug = 3,
                            TimeModelId = 1002
                        },
                        new
                        {
                            Id = 102,
                            CaringStaffId = 123,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DrugId = 1002,
                            ModifiedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "CCC 1x täglich, 75mg",
                            QuantityOfDrug = 1,
                            TimeModelId = 1003
                        },
                        new
                        {
                            Id = 103,
                            CaringStaffId = 122,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DrugId = 1003,
                            ModifiedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "DDD 2x täglich, 115mg",
                            QuantityOfDrug = 2,
                            TimeModelId = 1000
                        });
                });

            modelBuilder.Entity("DoctorBob.Core.TherapyManagement.Domain.TimeModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<string>("Time")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("TimeModels");

                    b.HasData(
                        new
                        {
                            Id = 1000,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ModifiedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Time = "08.30 / 11.00"
                        },
                        new
                        {
                            Id = 1001,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ModifiedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Time = "11.00"
                        },
                        new
                        {
                            Id = 1002,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ModifiedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Time = "08.00 / 14.30 / 18.30"
                        },
                        new
                        {
                            Id = 1003,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ModifiedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Time = "18.00"
                        });
                });

            modelBuilder.Entity("DoctorBob.Core.PatientManagement.Domain.Patient", b =>
                {
                    b.HasOne("DoctorBob.Core.PatientManagement.Domain.Room", "Room")
                        .WithMany()
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DoctorBob.Core.TherapyManagement.Domain.Therapy", "Therapy")
                        .WithMany()
                        .HasForeignKey("TherapyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Room");

                    b.Navigation("Therapy");
                });

            modelBuilder.Entity("DoctorBob.Core.RoboManagement.Domain.Robot", b =>
                {
                    b.HasOne("DoctorBob.Core.PatientManagement.Domain.Room", "LastRoom")
                        .WithMany()
                        .HasForeignKey("LastRoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LastRoom");
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
