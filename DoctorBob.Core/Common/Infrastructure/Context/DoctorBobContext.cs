using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DoctorBob.Core.DrugManagement.Domain;
using DoctorBob.Core.PatientManagement.Domain;
using DoctorBob.Core.TherapyManagement.Domain;
using DoctorBob.Core.StaffManagement.Domain;
using DoctorBob.Core.RobotManagement.Domain;
using DoctorBob.Core.OrderManagement.Domain;
using DoctorBob.Core.Common.Domain;
using System.Threading;

namespace DoctorBob.Core.Common.Infrastructure.Context
{
    public class DoctorBobContext : DbContext
    {
        #region Domains
        public DbSet<Drug> Drugs { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<TimeModel> TimeModels { get; set; }
        public DbSet<IntakeCategory> IntakeCategories { get; set; }
        public DbSet<Therapy> Therapies { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Staff> StaffMembers { get; set; }
        public DbSet<CurrentLocation> CurrentLocations { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Robot> Robots { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<OrderPatient> OrderPatients { get; set; }
        public DbSet<Order> Orders { get; set; }
        #endregion

        public DoctorBobContext()
        {

        }

        public DoctorBobContext(DbContextOptions<DoctorBobContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            String connectionString = "server = localhost; database = DoctorBob; user = root; password =Esojogacora17";
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Staff>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.LastName).IsRequired();
            });


            modelBuilder.Entity<IntakeCategory>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Therapy>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.HasOne(e => e.Drug);
                entity.HasOne(e => e.TimeModel);
                entity.HasOne(e => e.IntakeCategory);
                entity.HasOne(e => e.ResponsibleStaff);
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Drug>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<TimeModel>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Time).IsRequired();
            });

            modelBuilder.Entity<Gender>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.LastName).IsRequired();
                entity.HasOne(e => e.Gender);
                entity.HasOne(e => e.Therapy);
                entity.HasOne(e => e.CaringStaff);
                entity.HasOne(e => e.Room);
            });

            modelBuilder.Entity<CurrentLocation>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Activity>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Robot>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.HasOne(e => e.LastRoom);
                entity.HasOne(e => e.CurrentLocation);
                entity.HasOne(e => e.Activity);
            });

            modelBuilder.Entity<State>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<OrderPatient>()
                    .HasKey(op => new { op.OrderId, op.PatientId });
            modelBuilder.Entity<OrderPatient>()
                .HasOne(op => op.Order)
                .WithMany(p => p.OrderPatients)
                .HasForeignKey(op => op.OrderId);
            modelBuilder.Entity<OrderPatient>()
                .HasOne(op => op.Patient)
                .WithMany(p => p.OrderPatients)
                .HasForeignKey(op => op.PatientId);

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Robot);
             
                entity.HasOne(e => e.State);
            });


            #region List of Roles
            var roles = new List<Role>
            {
                new Role
                {
                    Id = 1,
                    Name = "Pflegefachperson"
                },
                new Role
                {
                    Id = 2,
                    Name = "Arzt"
                },
                new Role
                {
                    Id = 3,
                    Name = "Chefarzt"
                },
                new Role
                {
                    Id = 4,
                    Name = "Anästhesie"
                },
                new Role
                {
                    Id = 5,
                    Name = "Technik"
                },
                new Role
                {
                    Id = 6,
                    Name = "Administration"
                }
            };
            #endregion

            #region List of Staff
            var staffMembers = new List<Staff>
            {
                new Staff
                {
                    Id = 100,
                    FirstName = "Angela",
                    LastName = "Schmitter",
                    RoleId = 3,
                    Username = "aschmitter",
                    Password = "bobby123",
                    CreatedBy = "admin",
                    CreatedAt = new DateTime(2022,01,10,16,44,21),
                    ModifiedBy = "admin",
                    ModifiedAt = new DateTime(2022,01,10,16,44,21),
                    History = "⊕ 10.01.2022 15:44:21 - admin / Angela Schmitter / Chefarzt / aschmitter / *** - AKTIV",
                    Active = true
                },
                new Staff
                {
                    Id = 101,
                    FirstName = "Rudolf",
                    LastName = "Sahli",
                    RoleId = 2,
                    Username = "rsahli",
                    Password = "goku99",
                    CreatedBy = "admin",
                    CreatedAt = new DateTime(2022,01,10,16,44,21),
                    ModifiedBy = "admin",
                    ModifiedAt = new DateTime(2022,01,10,16,44,21),
                    History = "⊕ 10.01.2022 15:44:21 - admin / Rudolf Sahli / Arzt / rsahli / *** - AKTIV",
                    Active = true
                },
                new Staff
                {
                    Id = 102,
                    FirstName = "Ibrahim",
                    LastName = "Kesay",
                    RoleId = 2,
                    Username = "ikesay",
                    Password = "jacky91",
                    CreatedBy = "admin",
                    CreatedAt = new DateTime(2022,01,10,16,44,21),
                    ModifiedBy = "admin",
                    ModifiedAt = new DateTime(2022,01,10,16,44,21),
                    History = "⊕ 10.01.2022 15:44:21 - admin / Ibrahim Kesay / Arzt / ikesay / *** - AKTIV",
                    Active = true
                },
                new Staff
                {
                    Id = 120,
                    FirstName = "Selina",
                    LastName = "Kluser",
                    RoleId = 1,
                    Username = "skluser",
                    Password = "sloth17",
                    CreatedBy = "admin",
                    CreatedAt = new DateTime(2022,01,10,16,44,21),
                    ModifiedBy = "admin",
                    ModifiedAt = new DateTime(2022,01,10,16,44,21),
                    History = "⊕ 10.01.2022 15:44:21 - admin / Selina Kluser / Pflegefachperson / skluser / *** - AKTIV",
                    Active = true
                },
                new Staff
                {
                    Id = 121,
                    FirstName = "Jacqueline",
                    LastName = "Seitz",
                    RoleId = 1,
                    Username = "jseitz",
                    Password = "uiop98",
                    CreatedBy = "admin",
                    CreatedAt = new DateTime(2022,01,10,16,44,21),
                    ModifiedBy = "admin",
                    ModifiedAt = new DateTime(2022,01,10,16,44,21),
                    History = "⊕ 10.01.2022 15:44:21 - admin / Jacqueline Seitz / Pflegefachperson / jseitz / *** - AKTIV",
                    Active = true
                },
                new Staff
                {
                    Id = 122,
                    FirstName = "Roland",
                    LastName = "Agger",
                    RoleId = 1,
                    Username = "ragger",
                    Password = "65lolapola",
                    CreatedBy = "admin",
                    CreatedAt = new DateTime(2022,01,10,16,44,21),
                    ModifiedBy = "admin",
                    ModifiedAt = new DateTime(2022,01,10,16,44,21),
                    History = "⊕ 10.01.2022 15:44:21 - admin / Roland Agger / Pflegefachperson / ragger / *** - AKTIV",
                    Active = true
                },
                new Staff
                {
                    Id = 123,
                    FirstName = "Sybille",
                    LastName = "Fernandez",
                    RoleId = 1,
                    Username = "sfernandez",
                    Password = "esel15",
                    CreatedBy = "admin",
                    CreatedAt = new DateTime(2022,01,10,16,44,21),
                    ModifiedBy = "admin",
                    ModifiedAt = new DateTime(2022,01,10,16,44,21),
                    History = "⊕ 10.01.2022 15:44:21 - admin / Sybille Fernandez / Pflegefachperson / sfernandez / *** - AKTIV",
                    Active = true
                },
                new Staff
                {
                    Id = 130,
                    FirstName = "Walter",
                    LastName = "Seger",
                    RoleId = 4,
                    Username = "wseger",
                    Password = "losangeles20",
                    CreatedBy = "admin",
                    CreatedAt = new DateTime(2022,01,10,16,44,21),
                    ModifiedBy = "admin",
                    ModifiedAt = new DateTime(2022,01,10,16,44,21),
                    History = "⊕ 10.01.2022 15:44:21 - admin / Walter Seger / Anästhesie / wseger / *** - AKTIV",
                    Active = true
                },
                new Staff
                {
                    Id = 140,
                    FirstName = "Katherina",
                    LastName = "Popp",
                    RoleId = 6,
                    Username = "kpopp",
                    Password = "gandalf9!",
                    CreatedBy = "admin",
                    CreatedAt = new DateTime(2022,01,10,16,44,21),
                    ModifiedBy = "admin",
                    ModifiedAt = new DateTime(2022,01,10,16,44,21),
                    History = "⊕ 10.01.2022 15:44:21 - admin / Katherina Popp / Administration / kpopp / *** - AKTIV",
                    Active = true
                },
                new Staff
                {
                    Id = 160,
                    FirstName = "Eric",
                    LastName = "Luechinger",
                    RoleId = 5,
                    Username = "eluechinger",
                    Password = "DoctorBob2022!",
                    CreatedBy = "admin",
                    CreatedAt = new DateTime(2022,01,10,16,44,21),
                    ModifiedBy = "admin",
                    ModifiedAt = new DateTime(2022,01,10,16,44,21),
                    History = "⊕ 10.01.2022 15:44:21 - admin / Eric Luechinger / Administration / eluechinger / *** - AKTIV",
                    Active = true
                },
                new Staff
                {
                    Id = 161,
                    FirstName = "Admin",
                    LastName = "",
                    RoleId = 5,
                    Username = "admin",
                    Password = "DoctorBob2022!",
                    CreatedBy = "admin",
                    CreatedAt = new DateTime(2022,01,10,16,44,21),
                    ModifiedBy = "admin",
                    ModifiedAt = new DateTime(2022,01,10,16,44,21),
                    History = "⊕ 10.01.2022 15:44:21 - admin / Admin / Administration / admin / *** - AKTIV",
                    Active = true
                }
            };
            #endregion

            #region List of Drugs
            var drugs = new List<Drug>
            {
                new Drug
                {
                    Id = 1,
                    DoseInMg = 5,
                    Name = "Bisoprolol",
                    Description = "Senkung Puls",
                    CreatedBy = "admin",
                    CreatedAt = new DateTime(2022,01,10,16,44,21),
                    ModifiedBy = "admin",
                    ModifiedAt = new DateTime(2022,01,10,16,44,21),
                    History = "⊕ 10.01.2022 15:44:21 - admin / Bisoprolol / 5 / Senkung Puls - AKTIV",
                    Active = true
                },
                new Drug
                {
                    Id = 2,
                    DoseInMg = 850,
                    Name = "Metformin",
                    Description = "Regulation Blutzucker",
                    CreatedBy = "admin",
                    CreatedAt = new DateTime(2022,01,10,16,44,21),
                    ModifiedBy = "admin",
                    ModifiedAt = new DateTime(2022,01,10,16,44,21),
                    History = "⊕ 10.01.2022 15:44:21 - admin / Metformin / 850 / Regulation Blutzucker - AKTIV",
                    Active = true
                },
                new Drug
                {
                    Id = 3,
                    DoseInMg = 500,
                    Name = "Novalgin",
                    Description = "Schmerzmittel",
                    CreatedBy = "admin",
                    CreatedAt = new DateTime(2022,01,10,16,44,21),
                    ModifiedBy = "admin",
                    ModifiedAt = new DateTime(2022,01,10,16,44,21),
                    History = "⊕ 10.01.2022 15:44:21 - admin / Novalgin / 500 / Schmerzmittel - AKTIV",
                    Active = true
                },
                new Drug
                {
                    Id = 4,
                    DoseInMg = 5,
                    Name = "Amlodipin",
                    Description = "Senkung Blutdruck",
                    CreatedBy = "admin",
                    CreatedAt = new DateTime(2022,01,10,16,44,21),
                    ModifiedBy = "admin",
                    ModifiedAt = new DateTime(2022,01,10,16,44,21),
                    History = "⊕ 10.01.2022 15:44:21 - admin / Amlodipin / 5 / Senkung Blutdruck - AKTIV",
                    Active = true
                },
                new Drug
                {
                    Id = 5,
                    DoseInMg = 33,
                    Name = "Aspirin",
                    Description = "Kopfschmerzen",
                    CreatedBy = "admin",
                    CreatedAt = new DateTime(2022,01,10,16,44,21),
                    ModifiedBy = "admin",
                    ModifiedAt = new DateTime(2022,01,10,16,44,21),
                    History = "⊕ 10.01.2022 15:44:21 - admin / Bisoprolol / 5 / Senkung Puls - INAKTIV",
                    Active = false
                },
                new Drug
                {
                    Id = 6,
                    DoseInMg = 450,
                    Name = "Tafalgan",
                    Description = "Schmerzmittel",
                    CreatedBy = "admin",
                    CreatedAt = new DateTime(2022,01,10,16,44,21),
                    ModifiedBy = "admin",
                    ModifiedAt = new DateTime(2022,01,10,16,44,21),
                    History = "⊕ 10.01.2022 15:44:21 - admin / Tafalgan / 450 / Schmerzmittel - INAKTIV",
                    Active = false
                }
            };
            #endregion

            #region List of Rooms
            var rooms = new List<Room>
            {
                new Room
                {
                    Id = 101,
                    Name = "Raum 101",
                    CreatedBy = "admin",
                    CreatedAt = new DateTime(2022,01,10,16,44,21),
                    ModifiedBy = "admin",
                    ModifiedAt = new DateTime(2022,01,10,16,44,21),
                    History = "⊕ 10.01.2022 15:44:21 - admin / Raum 101 - AKTIV",
                    Active = true
                },
                new Room
                {
                    Id = 102,
                    Name = "Raum 102",
                    CreatedBy = "admin",
                    CreatedAt = new DateTime(2022,01,10,16,44,21),
                    ModifiedBy = "admin",
                    ModifiedAt = new DateTime(2022,01,10,16,44,21),
                    History = "⊕ 10.01.2022 15:44:21 - admin / Raum 102 - AKTIV",
                    Active = true
                },
                new Room
                {
                    Id = 103,
                    Name = "Raum 103",
                    CreatedBy = "admin",
                    CreatedAt = new DateTime(2022,01,10,16,44,21),
                    ModifiedBy = "admin",
                    ModifiedAt = new DateTime(2022,01,10,16,44,21),
                    History = "⊕ 10.01.2022 15:44:21 - admin / Raum 103 - AKTIV",
                    Active = true
                },
                new Room
                {
                    Id = 104,
                    Name = "Raum 104",
                    CreatedBy = "admin",
                    CreatedAt = new DateTime(2022,01,10,16,44,21),
                    ModifiedBy = "admin",
                    ModifiedAt = new DateTime(2022,01,10,16,44,21),
                    History = "⊕ 10.01.2022 15:44:21 - admin / Raum 104 - AKTIV",
                    Active = true
                },
                new Room
                {
                    Id = 900,
                    Name = "Spital-Apotheke",
                    CreatedBy = "admin",
                    CreatedAt = new DateTime(2022,01,10,16,44,21),
                    ModifiedBy = "admin",
                    ModifiedAt = new DateTime(2022,01,10,16,44,21),
                    History = "⊕ 10.01.2022 15:44:21 - admin / Spital-Apotheke - AKTIV",
                    Active = true
                },
                new Room
                {
                    Id = 999,
                    Name = "Homebase",
                    CreatedBy = "admin",
                    CreatedAt = new DateTime(2022,01,10,16,44,21),
                    ModifiedBy = "admin",
                    ModifiedAt = new DateTime(2022,01,10,16,44,21),
                    History = "⊕ 10.01.2022 15:44:21 - admin / Homebase - AKTIV",
                    Active = true
                }

            };
            #endregion

            #region List of TimeModels
            var timeModels = new List<TimeModel>
            {
                new TimeModel
                {
                    Id = 1000,
                    Time = "08.30 / 11.00",
                    DailyNumber = 2,
                    CreatedBy = "admin",
                    CreatedAt = new DateTime(2022,01,10,16,44,21),
                    ModifiedBy = "admin",
                    ModifiedAt = new DateTime(2022,01,10,16,44,21),
                    History = "⊕ 10.01.2022 15:44:21 - admin / 08.30 / 11.00 / 2 - AKTIV",
                    Active = true
                },
                new TimeModel
                {
                    Id = 1001,
                    Time = "11.00",
                    DailyNumber = 1,
                    CreatedBy = "admin",
                    CreatedAt = new DateTime(2022,01,10,16,44,21),
                    ModifiedBy = "admin",
                    ModifiedAt = new DateTime(2022,01,10,16,44,21),
                    History = "⊕ 10.01.2022 15:44:21 - admin / 11.00 / 1 - AKTIV",
                    Active = true
                },
                new TimeModel
                {
                    Id = 1002,
                    Time = "08.00 / 14.30 / 18.30",
                    DailyNumber = 3,
                    CreatedBy = "admin",
                    CreatedAt = new DateTime(2022,01,10,16,44,21),
                    ModifiedBy = "admin",
                    ModifiedAt = new DateTime(2022,01,10,16,44,21),
                    History = "⊕ 10.01.2022 15:44:21 - admin / 08.00 / 14.30 / 18.30 / 3 - AKTIV",
                    Active = true
                },
                new TimeModel
                {
                    Id = 1003,
                    Time = "18.00",
                    DailyNumber = 1,
                    CreatedBy = "admin",
                    CreatedAt = new DateTime(2022,01,10,16,44,21),
                    ModifiedBy = "admin",
                    ModifiedAt = new DateTime(2022,01,10,16,44,21),
                    History = "⊕ 10.01.2022 15:44:21 - admin / 18.00 / 1 - AKTIV",
                    Active = true
                }

            };
            #endregion

            #region List of IntakeCategories
            var intakeCategories = new List<IntakeCategory>
            {
                new IntakeCategory
                {
                    Id = 1,
                    Name = "oral"
                },
                new IntakeCategory
                {
                    Id = 2,
                    Name = "nasal"
                },
                new IntakeCategory
                {
                    Id = 3,
                    Name = "Injektion"
                },
                new IntakeCategory
                {
                    Id = 4,
                    Name = "anal"
                }
            };
            #endregion

            #region List of Therapies
            var therapies = new List<Therapy>
            {
                new Therapy
                {
                    Id = 101,
                    Name = "Bisoprolol 1x, 5 mg",
                    QuantityOfDrug = 1,
                    DrugId = 1,
                    TimeModelId = 1001,
                    CreatedBy = "admin",
                    CreatedAt = new DateTime(2022,01,10,16,44,21),
                    ModifiedBy = "admin",
                    ModifiedAt = new DateTime(2022,01,10,16,44,21),
                    IntakeCategoryId = 1,
                    ResponsibleStaffId = 101,
                    History = "⊕ 10.01.2022 15:44:21 - admin / Bisoprolol 1x, 5 mg / 1 / Bisoprolol / 1001 / oral / Sahli - AKTIV",
                    Active = true
                },
                new Therapy
                {
                    Id = 102,
                    Name = "Metformin 3x, 2550 mg",
                    QuantityOfDrug = 3,
                    DrugId = 2,
                    TimeModelId = 1002,
                    CreatedBy = "admin",
                    CreatedAt = new DateTime(2022,01,10,16,44,21),
                    ModifiedBy = "admin",
                    ModifiedAt = new DateTime(2022,01,10,16,44,21),
                    IntakeCategoryId = 1,
                    ResponsibleStaffId = 100,
                    History = "⊕ 10.01.2022 15:44:21 - admin / Metformin 3x, 2550 mg / 3 / Metformin / 1002 / oral / Schmitter - AKTIV",
                    Active = true
                },
                new Therapy
                {
                    Id = 103,
                    Name = "Novalgin 1x, 500 mg",
                    QuantityOfDrug = 1,
                    DrugId = 3,
                    TimeModelId = 1003,
                    CreatedBy = "admin",
                    CreatedAt = new DateTime(2022,01,10,16,44,21),
                    ModifiedBy = "admin",
                    ModifiedAt = new DateTime(2022,01,10,16,44,21),
                    IntakeCategoryId = 1,
                    ResponsibleStaffId = 102,
                    History = "⊕ 10.01.2022 15:44:21 - admin / Novalgin 1x, 500 mg / 1 / Novalgin / 1003 / oral / Kesay - AKTIV",
                    Active = true
                },
                new Therapy
                {
                    Id = 104,
                    Name = "Amlodipin 2x, 10 mg",
                    QuantityOfDrug = 2,
                    DrugId = 4,
                    TimeModelId = 1000,
                    CreatedBy = "admin",
                    CreatedAt = new DateTime(2022,01,10,16,44,21),
                    ModifiedBy = "admin",
                    ModifiedAt = new DateTime(2022,01,10,16,44,21),
                    IntakeCategoryId = 1,
                    ResponsibleStaffId = 101,
                    History = "⊕ 10.01.2022 15:44:21 - admin / Amlodipin 2x, 10 mg / 2 / Amlodipin / 1001 / oral / Sahli - AKTIV",
                    Active = true
                }
            };
            #endregion

            #region List of Genders
            var genders = new List<Gender>
            {
                new Gender
                {
                    Id = 1,
                    Name = "Mann"
                },
                new Gender
                {
                    Id = 2,
                    Name = "Frau"
                },
                new Gender
                {
                    Id = 3,
                    Name = "Divers"
                }
            };
            #endregion

            #region List of Patients
            var patients = new List<Patient>
            {
                new Patient
                {
                    Id = 1001,
                    FirstName = "Antonio",
                    LastName = "Eichholzer",
                    GenderId = 1,
                    RoomId = 101,
                    TherapyId = 101,
                    CaringStaffId = 123,
                    MedicalHistory = "Blinddarm-OP",
                    EntryDate = new DateTime(2022,06,20,10,15,03),
                    CreatedBy = "sfernandez",
                    CreatedAt = new DateTime(2022,06,20,10,15,03).AddHours(2),
                    ModifiedBy = "sfernandez",
                    ModifiedAt = new DateTime(2022,06,20,10,15,03).AddHours(2),
                    History = "⊕ 20.06.2022 10:15:03 - sfernandez / A. Eichholzer / M / Raum 101 / Bisoprolol 1x, 5 mg / Fernandez / Blinddarm-OP / IN: 22.06.2022 10:15:03",
                    Active = true
                },
                new Patient
                {
                    Id = 1002,
                    FirstName = "Chantal",
                    LastName = "Egger",
                    GenderId = 2,
                    RoomId = 102,
                    TherapyId = 104,
                    CaringStaffId = 120,
                    MedicalHistory = "Hüftoperation 25.06.2022, Schlaganfall im Juli 2015",
                    EntryDate = new DateTime(2022,06,25,09,38,57),
                    CreatedBy = "skluser",
                    CreatedAt = new DateTime(2022,06,25,09,38,57).AddHours(2),
                    ModifiedBy = "skluser",
                    ModifiedAt = new DateTime(2022,06,25,09,38,57).AddHours(2),
                    History = "⊕ 25.06.2022 09:38:57 - skluser / C. Egger / F / Raum 102 / Amlodipin 2x, 10 mg / Kluser / Hüftoperation 25.06.2022, Schlaganfall im Juli 2015 / IN: 25.06.2022 09:38:57",
                    Active = true
                },
                new Patient
                {
                    Id = 1003,
                    FirstName = "Corsin",
                    LastName = "Castellazzi",
                    GenderId = 1,
                    RoomId = 103,
                    TherapyId = 101,
                    CaringStaffId = 121,
                    MedicalHistory = "Beinbruch rechts 26.06.2022",
                    EntryDate = new DateTime(2022,06,26,20,42,03),
                    CreatedBy = "skluser",
                    CreatedAt = new DateTime(2022,06,26,20,42,03).AddHours(2),
                    ModifiedBy = "skluser",
                    ModifiedAt = new DateTime(2022,06,26,20,42,03).AddHours(2),
                    History = "⊕ 26.06.2022 20:42:03 - skluser / C. Castellazzi / M / Raum  103 / Bisoprolol 1x, 5 mg / Seitz / Beinbruch rechts 26.06.2022 / IN: 26.06.2022 20:42:03",
                    Active = true
                }
            };
            #endregion

            #region List of current Locations
            var currentlocations = new List<CurrentLocation>
            {
                new CurrentLocation
                {
                    Id = 999,
                    Name = "HomeBase"
                },
                new CurrentLocation
                {
                    Id = 900,
                    Name = "Spital-Apotheke"
                },
                new CurrentLocation
                {
                    Id = 1,
                    Name = "Unterwegs"
                },
                new CurrentLocation
                {
                    Id = 101,
                    Name = "Raum 101"
                },
                new CurrentLocation
                {
                    Id = 2,
                    Name = "Auf Weg zu Raum 102"
                },
                new CurrentLocation
                {
                    Id = 102,
                    Name = "Raum 102"
                },
                new CurrentLocation
                {
                    Id = 3,
                    Name = "Auf Weg zu Raum 103"
                },
                new CurrentLocation
                {
                    Id = 103,
                    Name = "Raum 103"
                },
                new CurrentLocation
                {
                    Id = 4,
                    Name = "Auf Weg zu Raum 104"
                },
                new CurrentLocation
                {
                    Id = 104,
                    Name = "Raum 104"
                },
                new CurrentLocation
                {
                    Id = 998,
                    Name = "Auf Weg zu HomeBase"
                }
            };
            #endregion

            #region List of Activities
            var activities = new List<Activity>
            {
                new Activity
                {
                    Id = 1,
                    Name = "Bereit"
                },
                new Activity
                {
                    Id = 2,
                    Name = "Lade Akku"
                },
                new Activity
                {
                    Id = 3,
                    Name = "Lade Medikamente"
                },
                new Activity
                {
                    Id = 4,
                    Name = "Medikamente geladen"
                },
                new Activity
                {
                    Id = 5,
                    Name = "Unterwegs"
                },
                new Activity
                {
                    Id = 6,
                    Name = "Lade Medikament ab"
                },
                new Activity
                {
                    Id = 7,
                    Name = "Medikamente abgeladen"
                },
                new Activity
                {
                    Id = 8,
                    Name = "Verlasse Raum"
                },
                new Activity
                {
                    Id = 9,
                    Name = "Auf Weg zurück"
                },
                new Activity
                {
                    Id = 10,
                    Name = "Fehler - Brauche Hilfe"
                },
            };
            #endregion

            #region List of Robots
            var robot = new List<Robot>
            {
                new Robot
                {
                    Id = 1,
                    Name = "DoctorBob R2D2",
                    LastRoomId = 999,
                    CurrentLocationId = 999,
                    Power = 85,
                    ActivityId = 1,
                    CreatedBy = "admin",
                    CreatedAt = new DateTime(2022,01,10,16,44,21),
                    ModifiedBy = "admin",
                    ModifiedAt = new DateTime(2022,01,10,16,44,21),
                    History = "⊕ 10.01.2022 15:44:21 - admin / DoctorBob R2D2 - AKTIV",
                    Active = true
                }
            };
            #endregion

            #region List of States
            var states = new List<State>
            {
                new State
                {
                    Id = 1,
                    Name = "Bereit"
                },
                new State
                {
                    Id = 2,
                    Name = "Wird ausgeführt"
                },
                new State
                {
                    Id = 3,
                    Name = "Erfolgreich abgeschlossen"
                },
                new State
                {
                    Id = 4,
                    Name = "Fehler"
                },
            };
            #endregion

            #region Preload Data
            roles.ForEach(r => modelBuilder.Entity<Role>().HasData(r));
            staffMembers.ForEach(s => modelBuilder.Entity<Staff>().HasData(s));
            drugs.ForEach(d => modelBuilder.Entity<Drug>().HasData(d));
            rooms.ForEach(r => modelBuilder.Entity<Room>().HasData(r));
            timeModels.ForEach(t => modelBuilder.Entity<TimeModel>().HasData(t));
            intakeCategories.ForEach(i => modelBuilder.Entity<IntakeCategory>().HasData(i));
            therapies.ForEach(t => modelBuilder.Entity<Therapy>().HasData(t));
            genders.ForEach(g => modelBuilder.Entity<Gender>().HasData(g));
            patients.ForEach(p => modelBuilder.Entity<Patient>().HasData(p));
            currentlocations.ForEach(c => modelBuilder.Entity<CurrentLocation>().HasData(c));
            activities.ForEach(a => modelBuilder.Entity<Activity>().HasData(a));
            robot.ForEach(r => modelBuilder.Entity<Robot>().HasData(r));
            states.ForEach(s => modelBuilder.Entity<State>().HasData(s));
            //orders.ForEach(o => modelBuilder.Entity<Order>().HasData(o));
            //orderPatients.ForEach(o => modelBuilder.Entity<OrderPatient>().HasData(o));
            #endregion
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // Get all the entities that inherit from AuditableEntity
            // and have a state of Added or Modified
            var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is AuditableEntity && (
            e.State == EntityState.Added
            || e.State == EntityState.Modified));



            // For each entity we will set the Audit properties
            foreach (var entityEntry in entries)
            {
                // If the entity state is Added let's set
                // the CreatedAt and CreatedBy properties
                if (entityEntry.State == EntityState.Added)
                {
                    ((AuditableEntity)entityEntry.Entity).CreatedAt = DateTimeOffset.Now.AddHours(2);
                    //((AuditableEntity)entityEntry.Entity).CreatedBy = this.httpContextAccessor?.HttpContext?.User?.Identity?.Name ?? "MyApp";
                }
                else
                {
                    // If the state is Modified then we don't want
                    // to modify the CreatedAt and CreatedBy properties
                    // so we set their state as IsModified to false
                    Entry((AuditableEntity)entityEntry.Entity).Property(p => p.CreatedAt).IsModified = false;
                    Entry((AuditableEntity)entityEntry.Entity).Property(p => p.CreatedBy).IsModified = false;
                    Entry((AuditableEntity)entityEntry.Entity).Property(p => p.History).IsModified = false;

                    if (!String.IsNullOrEmpty(((AuditableEntity)entityEntry.Entity).HistoryTemp))
                    {
                        string oldHistory = ((AuditableEntity)entityEntry.Entity).HistoryTempInternal;
                        string newHistory = ((AuditableEntity)entityEntry.Entity).HistoryTemp;
                        ((AuditableEntity)entityEntry.Entity).History = newHistory + "\r\n" + oldHistory;
                        ((AuditableEntity)entityEntry.Entity).HistoryTemp = null;
                    }
                }



            // In any case we always want to set the properties
            // ModifiedAt and ModifiedBy
            ((AuditableEntity)entityEntry.Entity).ModifiedAt = DateTimeOffset.Now.AddHours(2);
                //((AuditableEntity)entityEntry.Entity).ModifiedAt = DateTimeOffset.Now;
                //((AuditableEntity)entityEntry.Entity).ModifiedBy = this.httpContextAccessor?.HttpContext?.User?.Identity?.Name ?? "MyApp";
            }



            // After we set all the needed properties
            // we call the base implementation of SaveChangesAsync
            // to actually save our entities in the database
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
