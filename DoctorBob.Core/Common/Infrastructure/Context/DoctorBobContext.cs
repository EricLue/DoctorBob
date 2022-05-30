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
        public DbSet<Therapy> Therapies { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Staff> StaffMembers { get; set; }
        public DbSet<CurrentLocation> CurrentLocations { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Robot> Robots { get; set; }
        public DbSet<State> States { get; set; }
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

            modelBuilder.Entity<Therapy>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.HasOne(e => e.Drug);
                entity.HasOne(e => e.CaringStaff);
                entity.HasOne(e => e.TimeModel);
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
                    ModifiedAt = new DateTime(2022,01,10,16,44,21)
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
                    ModifiedAt = new DateTime(2022,01,10,16,44,21)
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
                    ModifiedAt = new DateTime(2022,01,10,16,44,21)
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
                    ModifiedAt = new DateTime(2022,01,10,16,44,21)
                },
                new Staff
                {
                    Id = 121,
                    FirstName = "Jacqueline",
                    LastName = "Seitz",
                    RoleId = 1,
                    Username = "sseitz",
                    Password = "uiop98",
                    CreatedBy = "admin",
                    CreatedAt = new DateTime(2022,01,10,16,44,21),
                    ModifiedBy = "admin",
                    ModifiedAt = new DateTime(2022,01,10,16,44,21)
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
                    ModifiedAt = new DateTime(2022,01,10,16,44,21)
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
                    ModifiedAt = new DateTime(2022,01,10,16,44,21)
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
                    ModifiedAt = new DateTime(2022,01,10,16,44,21)
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
                    ModifiedAt = new DateTime(2022,01,10,16,44,21)
                },
                new Staff
                {
                    Id = 160,
                    FirstName = "Fredi",
                    LastName = "Holenstein",
                    RoleId = 5,
                    Username = "fholenstein",
                    Password = "santaclause11",
                    CreatedBy = "admin",
                    CreatedAt = new DateTime(2022,01,10,16,44,21),
                    ModifiedBy = "admin",
                    ModifiedAt = new DateTime(2022,01,10,16,44,21)
                }
            };
            #endregion

            #region List of Drugs
            var drugs = new List<Drug>
            {
                new Drug
                {
                    Id = 1000,
                    DoseInMg = 5,
                    Name = "Bisoprolol",
                    Description = "Senkung Puls",
                    CreatedBy = "admin",
                    CreatedAt = new DateTime(2022,01,10,16,44,21),
                    ModifiedBy = "admin",
                    ModifiedAt = new DateTime(2022,01,10,16,44,21)
                },
                new Drug
                {
                    Id = 1001,
                    DoseInMg = 850,
                    Name = "Metformin",
                    Description = "Regulation Blutzucker",
                    CreatedBy = "admin",
                    CreatedAt = new DateTime(2022,01,10,16,44,21),
                    ModifiedBy = "admin",
                    ModifiedAt = new DateTime(2022,01,10,16,44,21)
                },
                new Drug
                {
                    Id = 1002,
                    DoseInMg = 500,
                    Name = "Novalgin",
                    Description = "Schmerzmittel",
                    CreatedBy = "admin",
                    CreatedAt = new DateTime(2022,01,10,16,44,21),
                    ModifiedBy = "admin",
                    ModifiedAt = new DateTime(2022,01,10,16,44,21)
                },
                new Drug
                {
                    Id = 1003,
                    DoseInMg = 5,
                    Name = "Amlodipin",
                    Description = "Senkung Blutdruck",
                    CreatedBy = "admin",
                    CreatedAt = new DateTime(2022,01,10,16,44,21),
                    ModifiedBy = "admin",
                    ModifiedAt = new DateTime(2022,01,10,16,44,21)
                },
                new Drug
                {
                    Id = 1004,
                    DoseInMg = 33,
                    Name = "Aspirin",
                    Description = "Kopfschmerzen",
                    CreatedBy = "admin",
                    CreatedAt = new DateTime(2022,01,10,16,44,21),
                    ModifiedBy = "admin",
                    ModifiedAt = new DateTime(2022,01,10,16,44,21)
                },
                new Drug
                {
                    Id = 1005,
                    DoseInMg = 450,
                    Name = "Tafalgan",
                    Description = "Schmerzmittel",
                    CreatedBy = "admin",
                    CreatedAt = new DateTime(2022,01,10,16,44,21),
                    ModifiedBy = "admin",
                    ModifiedAt = new DateTime(2022,01,10,16,44,21)
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
                    ModifiedAt = new DateTime(2022,01,10,16,44,21)
                },
                new Room
                {
                    Id = 102,
                    Name = "Raum 102",
                    CreatedBy = "admin",
                    CreatedAt = new DateTime(2022,01,10,16,44,21),
                    ModifiedBy = "admin",
                    ModifiedAt = new DateTime(2022,01,10,16,44,21)
                },
                new Room
                {
                    Id = 103,
                    Name = "Raum 103",
                    CreatedBy = "admin",
                    CreatedAt = new DateTime(2022,01,10,16,44,21),
                    ModifiedBy = "admin",
                    ModifiedAt = new DateTime(2022,01,10,16,44,21)
                },
                new Room
                {
                    Id = 104,
                    Name = "Raum 104",
                    CreatedBy = "admin",
                    CreatedAt = new DateTime(2022,01,10,16,44,21),
                    ModifiedBy = "admin",
                    ModifiedAt = new DateTime(2022,01,10,16,44,21)
                },
                new Room
                {
                    Id = 900,
                    Name = "Spital-Apotheke",
                    CreatedBy = "admin",
                    CreatedAt = new DateTime(2022,01,10,16,44,21),
                    ModifiedBy = "admin",
                    ModifiedAt = new DateTime(2022,01,10,16,44,21)
                },
                new Room
                {
                    Id = 999,
                    Name = "Homebase",
                    CreatedBy = "admin",
                    CreatedAt = new DateTime(2022,01,10,16,44,21),
                    ModifiedBy = "admin",
                    ModifiedAt = new DateTime(2022,01,10,16,44,21)
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
                    ModifiedAt = new DateTime(2022,01,10,16,44,21)
                },
                new TimeModel
                {
                    Id = 1001,
                    Time = "11.00",
                    DailyNumber = 1,
                    CreatedBy = "admin",
                    CreatedAt = new DateTime(2022,01,10,16,44,21),
                    ModifiedBy = "admin",
                    ModifiedAt = new DateTime(2022,01,10,16,44,21)
                },
                new TimeModel
                {
                    Id = 1002,
                    Time = "08.00 / 14.30 / 18.30",
                    DailyNumber = 3,
                    CreatedBy = "admin",
                    CreatedAt = new DateTime(2022,01,10,16,44,21),
                    ModifiedBy = "admin",
                    ModifiedAt = new DateTime(2022,01,10,16,44,21)
                },
                new TimeModel
                {
                    Id = 1003,
                    Time = "18.00",
                    DailyNumber = 1,
                    CreatedBy = "admin",
                    CreatedAt = new DateTime(2022,01,10,16,44,21),
                    ModifiedBy = "admin",
                    ModifiedAt = new DateTime(2022,01,10,16,44,21)
                }

            };
            #endregion

            #region List of Therapies
            var therapies = new List<Therapy>
            {
                new Therapy
                {
                    Id = 100,
                    Name = "Bisoprolol 1x täglich, 5 mg",
                    QuantityOfDrug = 1,
                    DrugId = 1000,
                    CaringStaffId = 120,
                    TimeModelId = 1001,
                    CreatedBy = "admin",
                    CreatedAt = new DateTime(2022,01,10,16,44,21),
                    ModifiedBy = "admin",
                    ModifiedAt = new DateTime(2022,01,10,16,44,21)
                },
                new Therapy
                {
                    Id = 101,
                    Name = "Metformin 3x täglich, total 2550 mg",
                    QuantityOfDrug = 3,
                    DrugId = 1001,
                    CaringStaffId = 121,
                    TimeModelId = 1002,
                    CreatedBy = "admin",
                    CreatedAt = new DateTime(2022,01,10,16,44,21),
                    ModifiedBy = "admin",
                    ModifiedAt = new DateTime(2022,01,10,16,44,21)
                },
                new Therapy
                {
                    Id = 102,
                    Name = "Novalgin 1x täglich, 500 mg",
                    QuantityOfDrug = 1,
                    DrugId = 1002,
                    CaringStaffId = 123,
                    TimeModelId = 1003,
                    CreatedBy = "admin",
                    CreatedAt = new DateTime(2022,01,10,16,44,21),
                    ModifiedBy = "admin",
                    ModifiedAt = new DateTime(2022,01,10,16,44,21)
                },
                new Therapy
                {
                    Id = 103,
                    Name = "Amlodipin 2x täglich, total 10 mg",
                    QuantityOfDrug = 2,
                    DrugId = 1003,
                    CaringStaffId = 122,
                    TimeModelId = 1000,
                    CreatedBy = "admin",
                    CreatedAt = new DateTime(2022,01,10,16,44,21),
                    ModifiedBy = "admin",
                    ModifiedAt = new DateTime(2022,01,10,16,44,21)
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
                    Id = 10002,
                    FirstName = "Antonio",
                    LastName = "Eichholzer",
                    GenderId = 1,
                    RoomId = 101,
                    TherapyId = 103,
                    MedicalHistory = "",
                    EntryDate = new DateTime(2022,06,20,10,15,03),
                    CreatedBy = "sfernandez",
                    CreatedAt = new DateTime(2022,06,20,10,15,03),
                    ModifiedBy = "sfernandez",
                    ModifiedAt = new DateTime(2022,06,20,10,15,03)
                },
                new Patient
                {
                    Id = 10003,
                    FirstName = "Martha",
                    LastName = "Watson",
                    GenderId = 2,
                    RoomId = 102,
                    TherapyId = 100,
                    MedicalHistory = "Schlaganfall im Juli 2015, diverse tägliche Medikamentenzunahme",
                    EntryDate = new DateTime(2022,06,25,09,38,57),
                    CreatedBy = "skluser",
                    CreatedAt = new DateTime(2022,06,25,09,38,57),
                    ModifiedBy = "skluser",
                    ModifiedAt = new DateTime(2022,06,25,09,38,57)
                },
                new Patient
                {
                    Id = 10004,
                    FirstName = "Flavio",
                    LastName = "Frei",
                    GenderId = 1,
                    RoomId = 103,
                    TherapyId = 102,
                    MedicalHistory = "",
                    EntryDate = new DateTime(2022,06,26,20,42,03),
                    CreatedBy = "skluser",
                    CreatedAt = new DateTime(2022,06,26,20,42,03),
                    ModifiedBy = "skluser",
                    ModifiedAt = new DateTime(2022,06,26,20,42,03)
                },
                new Patient
                {
                    Id = 10005,
                    FirstName = "Lisa",
                    LastName = "Zellweger",
                    GenderId = 2,
                    RoomId = 104,
                    TherapyId = 101,
                    MedicalHistory = "Herzstillstand 05.2011 mit anschliessender Reanimation",
                    EntryDate = new DateTime(2022,06,24,05,18,22),
                    CreatedBy = "ragger",
                    CreatedAt = new DateTime(2022,06,24,05,18,22),
                    ModifiedBy = "ragger",
                    ModifiedAt = new DateTime(2022,06,24,05,18,22)
                },
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
                    Name = "Auf Weg zu Raum 101"
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
                    Power = 100,
                    ActivityId = 1,
                    CreatedBy = "admin",
                    CreatedAt = new DateTime(2022,01,10,16,44,21),
                    ModifiedBy = "admin",
                    ModifiedAt = new DateTime(2022,01,10,16,44,21)
                },
                new Robot
                {
                    Id = 2,
                    Name = "DoctorBob Prime 1",
                    LastRoomId = 900,
                    CurrentLocationId = 900,
                    Power = 35,
                    ActivityId = 2,
                    CreatedBy = "admin",
                    CreatedAt = new DateTime(2022,01,10,16,44,21),
                    ModifiedBy = "admin",
                    ModifiedAt = new DateTime(2022,01,10,16,44,21)
                },
                new Robot
                {
                    Id = 3,
                    Name = "DoctorBob McBobby 99",
                    LastRoomId = 900,
                    CurrentLocationId = 900,
                    Power = 5,
                    ActivityId = 2,
                    CreatedBy = "admin",
                    CreatedAt = new DateTime(2022,01,10,16,44,21),
                    ModifiedBy = "admin",
                    ModifiedAt = new DateTime(2022,01,10,16,44,21)
                }
            };
            #endregion

            #region List of States
            var states = new List<State>
            {
                new State
                {
                    Id = 1,
                    Name = "Boarding"
                },
                new State
                {
                    Id = 2,
                    Name = "Bestätigt"
                },
                new State
                {
                    Id = 3,
                    Name = "Wird ausgeführt"
                },
                new State
                {
                    Id = 4,
                    Name = "Abgeschlossen"
                },
            };
            #endregion

            #region Preload Data
            roles.ForEach(r => modelBuilder.Entity<Role>().HasData(r));
            staffMembers.ForEach(s => modelBuilder.Entity<Staff>().HasData(s));
            drugs.ForEach(d => modelBuilder.Entity<Drug>().HasData(d));
            rooms.ForEach(r => modelBuilder.Entity<Room>().HasData(r));
            timeModels.ForEach(t => modelBuilder.Entity<TimeModel>().HasData(t));
            therapies.ForEach(t => modelBuilder.Entity<Therapy>().HasData(t));
            genders.ForEach(g => modelBuilder.Entity<Gender>().HasData(g));
            patients.ForEach(p => modelBuilder.Entity<Patient>().HasData(p));
            currentlocations.ForEach(c => modelBuilder.Entity<CurrentLocation>().HasData(c));
            activities.ForEach(a => modelBuilder.Entity<Activity>().HasData(a));
            robot.ForEach(r => modelBuilder.Entity<Robot>().HasData(r));
            states.ForEach(s => modelBuilder.Entity<State>().HasData(s));
            //orders.ForEach(o => modelBuilder.Entity<Order>().HasData(o));
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
                    ((AuditableEntity)entityEntry.Entity).CreatedAt = DateTime.UtcNow;
                    //((AuditableEntity)entityEntry.Entity).CreatedBy = this.httpContextAccessor?.HttpContext?.User?.Identity?.Name ?? "MyApp";
                }
                else
                {
                    // If the state is Modified then we don't want
                    // to modify the CreatedAt and CreatedBy properties
                    // so we set their state as IsModified to false
                    Entry((AuditableEntity)entityEntry.Entity).Property(p => p.CreatedAt).IsModified = false;
                    Entry((AuditableEntity)entityEntry.Entity).Property(p => p.CreatedBy).IsModified = false;
                }



            // In any case we always want to set the properties
            // ModifiedAt and ModifiedBy
            ((AuditableEntity)entityEntry.Entity).ModifiedAt = DateTime.UtcNow;
                //((AuditableEntity)entityEntry.Entity).ModifiedBy = this.httpContextAccessor?.HttpContext?.User?.Identity?.Name ?? "MyApp";
            }



            // After we set all the needed properties
            // we call the base implementation of SaveChangesAsync
            // to actually save our entities in the database
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
