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
using DoctorBob.Core.RoboManagement.Domain;
using DoctorBob.Core.Common.Domain;
using System.Threading;

namespace DoctorBob.Core.Common.Infrastructure.Context
{
    public class DoctorBobContext : DbContext
    {
        #region Domains
        public DbSet<Drug> Drugs { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<TimeModel> TimeModels { get; set; }
        public DbSet<Therapy> Therapies { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Staff> StaffMembers { get; set; }
        public DbSet<Robot> Robot { get; set; }
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
            String connectionString = "server = localhost; database = DoctorBob; user = root; password =PASSWORD";
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Book>(entity =>
            //{
            //    entity.HasKey(e => e.ISBN);
            //    entity.Property(e => e.Title).IsRequired();
            //    entity.HasOne(d => d.Publisher)
            //      .WithMany(p => p.Books);
            //});

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

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.LastName).IsRequired();
                entity.HasOne(e => e.Therapy);
                entity.HasOne(e => e.Room);
            });

            modelBuilder.Entity<Robot>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.HasOne(e => e.LastRoom);
            });

            //modelBuilder.HasSequence<int>("StaffNr")
            //    .StartsAt(100);

            //modelBuilder.HasSequence<int>("TherapyNr")
            //    .StartsAt(100);

            //modelBuilder.HasSequence<int>("RoomNr")
            //    .StartsAt(100);

            //modelBuilder.HasSequence<int>("DrugNr")
            //    .StartsAt(1_000);

            //modelBuilder.HasSequence<int>("TimeModelNr")
            //    .StartsAt(1_000);

            //modelBuilder.HasSequence<int>("PatientNr")
            //    .StartsAt(10_000);

            //modelBuilder.HasSequence<int>("RobotNr")
            //    .StartsAt(1);

            //modelBuilder.Entity<Robot>()
            //    .Property(r => r.Id)
            //    .HasDefaultValueSql("NEXT VALUE FOR shared.Id");

            //modelBuilder.Entity<Staff>()
            //    .Property(s => s.Id)
            //    .HasDefaultValueSql("NEXT VALUE FOR shared.Id");

            //modelBuilder.Entity<Therapy>()
            //    .Property(t => t.Id)
            //    .HasDefaultValueSql("NEXT VALUE FOR shared.Id");

            //modelBuilder.Entity<Drug>()
            //    .Property(d => d.Id)
            //    .HasDefaultValueSql("NEXT VALUE FOR shared.Id");

            //modelBuilder.Entity<Patient>()
            //    .Property(d => d.Id)
            //    .HasDefaultValueSql("NEXT VALUE FOR shared.Id");

            //modelBuilder.Entity<Therapy>()
            //      .HasOne(t => t.Drug);

            //modelBuilder.Entity<Therapy>()
            //    .HasOne(t => t.CaringStaff);

            //modelBuilder.Entity<Therapy>()
            //    .HasOne(t => t.TimeModel);

            //modelBuilder.Entity<Patient>()
            //    .HasOne(p => p.Therapy);

            //modelBuilder.Entity<Patient>()
            //    .HasOne(p => p.Room);

            //modelBuilder.Entity<Robot>()
            //    .HasOne(p => p.LastRoom);

            #region List of Staff
            var staffMembers = new List<Staff>
            {
                new Staff
                {
                    Id = 100,
                    FirstName = "Angela",
                    LastName = "Schmitter",
                    Role = Role.ChiefDoctor,
                    Username = "aschmitter",
                },
                new Staff
                {
                    Id = 101,
                    FirstName = "Rudolf",
                    LastName = "Sahli",
                    Role = Role.Doctor,
                    Username = "rsahli",
                },
                new Staff
                {
                    Id = 102,
                    FirstName = "Ibrahim",
                    LastName = "Kesay",
                    Role = Role.Doctor,
                    Username = "ikesay",
                },
                new Staff
                {
                    Id = 120,
                    FirstName = "Selina",
                    LastName = "Kluser",
                    Role = Role.Nurse,
                    Username = "skluser",
                },
                new Staff
                {
                    Id = 121,
                    FirstName = "Jacqueline",
                    LastName = "Seitz",
                    Role = Role.Nurse,
                    Username = "sseitz",
                },
                new Staff
                {
                    Id = 122,
                    FirstName = "Roland",
                    LastName = "Agger",
                    Role = Role.Nurse,
                    Username = "ragger",
                },
                new Staff
                {
                    Id = 123,
                    FirstName = "Sybille",
                    LastName = "Fernandez",
                    Role = Role.Nurse,
                    Username = "sfernandez",
                },
                new Staff
                {
                    Id = 130,
                    FirstName = "Walter",
                    LastName = "Seger",
                    Role = Role.Anesthetist,
                    Username = "wseger",
                },
                new Staff
                {
                    Id = 140,
                    FirstName = "Katherina",
                    LastName = "Popp",
                    Role = Role.Administration,
                    Username = "kpopp",
                },
                new Staff
                {
                    Id = 160,
                    FirstName = "Fredi",
                    LastName = "Holenstein",
                    Role = Role.Technician,
                    Username = "fholenstein",
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
                    Description = "Senkung Puls"
                },
                new Drug
                {
                    Id = 1001,
                    DoseInMg = 14,
                    Name = "Metformin",
                    Description = "Regulation Blutzucker"
                },
                new Drug
                {
                    Id = 1002,
                    DoseInMg = 500,
                    Name = "Novalgin",
                    Description = "Schmerzmittel"
                },
                new Drug
                {
                    Id = 1003,
                    DoseInMg = 5,
                    Name = "Amlodipin",
                    Description = "Senkung Blutdruck"
                }

            };
            #endregion

            #region List of Rooms
            var rooms = new List<Room>
            {
                new Room
                {
                    Id = 101,
                    Name = "Raum 101"
                },
                new Room
                {
                    Id = 102,
                    Name = "Raum 102"
                },
                new Room
                {
                    Id = 103,
                    Name = "Raum 103"
                },
                new Room
                {
                    Id = 104,
                    Name = "Raum 104"
                },
                new Room
                {
                    Id = 900,
                    Name = "Spital-Apotheke"
                },
                new Room
                {
                    Id = 999,
                    Name = "Homebase"
                }

            };
            #endregion

            #region List of TimeModels
            var timeModels = new List<TimeModel>
            {
                new TimeModel
                {
                    Id = 1000,
                    Time = "08.30 / 11.00"
                },
                new TimeModel
                {
                    Id = 1001,
                    Time = "11.00"
                },
                new TimeModel
                {
                    Id = 1002,
                    Time = "08.00 / 14.30 / 18.30"
                },
                new TimeModel
                {
                    Id = 1003,
                    Time = "18.00"
                }

            };
            #endregion

            #region List of Therapies
            var therapies = new List<Therapy>
            {
                new Therapy
                {
                    Id = 100,
                    Name = "AAA 1x täglich, 50mg",
                    QuantityOfDrug = 1,
                    DrugId = 1000,
                    CaringStaffId = 120,
                    TimeModelId = 1001
                },
                new Therapy
                {
                    Id = 101,
                    Name = "BBB 3x täglich, 250mg",
                    QuantityOfDrug = 3,
                    DrugId = 1001,
                    CaringStaffId = 121,
                    TimeModelId = 1002
                },
                new Therapy
                {
                    Id = 102,
                    Name = "CCC 1x täglich, 75mg",
                    QuantityOfDrug = 1,
                    DrugId = 1002,
                    CaringStaffId = 123,
                    TimeModelId = 1003
                },
                new Therapy
                {
                    Id = 103,
                    Name = "DDD 2x täglich, 115mg",
                    QuantityOfDrug = 2,
                    DrugId = 1003,
                    CaringStaffId = 122,
                    TimeModelId = 1000
                }
            };
            #endregion

            #region List of Patients
            var patients = new List<Patient>
            {
                //new Patient
                //{
                //    Id = 10000,
                //    FirstName = "Marco",
                //    LastName = "Inverardi",
                //    Gender = Gender.Male,
                //    TherapyId = 103,
                //    MedicalHistory = "Operation Blinddarum im Jahre 2018",
                //    EntryDate = new DateTime(2022,06,20),
                //    LeavingDate = new DateTime(2022,06,22)
                //},
                //new Patient
                //{
                //    Id = 10001,
                //    FirstName = "Heidi",
                //    LastName = "Geissbühler",
                //    Gender = Gender.Female,
                //    TherapyId = 102,
                //    MedicalHistory = "Hüftoperation 01.2017",
                //    EntryDate = new DateTime(2022,06,20),
                //    LeavingDate = new DateTime(2022,06,21)
                //},
                new Patient
                {
                    Id = 10002,
                    FirstName = "Antonio",
                    LastName = "Eichholzer",
                    Gender = Gender.Male,
                    RoomId = 101,
                    TherapyId = 103,
                    MedicalHistory = "",
                    EntryDate = new DateTime(2022,06,20),
                },
                new Patient
                {
                    Id = 10003,
                    FirstName = "Martha",
                    LastName = "Watson",
                    Gender = Gender.Female,
                    RoomId = 102,
                    TherapyId = 100,
                    MedicalHistory = "Schlaganfall im Juli 2015, diverse tägliche Medikamentenzunahme",
                    EntryDate = new DateTime(2022,06,25),
                },
                new Patient
                {
                    Id = 10004,
                    FirstName = "Flavio",
                    LastName = "Frei",
                    Gender = Gender.Male,
                    RoomId = 103,
                    TherapyId = 102,
                    MedicalHistory = "",
                    EntryDate = new DateTime(2022,06,26),
                },
                new Patient
                {
                    Id = 10005,
                    FirstName = "Lisa",
                    LastName = "Zellweger",
                    Gender = Gender.Female,
                    RoomId = 104,
                    TherapyId = 101,
                    MedicalHistory = "Herzstillstand 05.2011 mit anschliessender Reanimation",
                    EntryDate = new DateTime(2022,06,24),
                },
            };
            #endregion

            #region Robot
            var robot = new List<Robot>
            {
                new Robot
                {
                    Id = 1,
                    Name = "DoctorBob 1.0",
                    LastRoomId = 999,
                    CurrentLocation = CurrentLocation.Home,
                    Power = 100,
                    Activity = Activity.Standby
                }
            };
            #endregion

            #region Preload Data
            staffMembers.ForEach(s => modelBuilder.Entity<Staff>().HasData(s));
            drugs.ForEach(d => modelBuilder.Entity<Drug>().HasData(d));
            rooms.ForEach(r => modelBuilder.Entity<Room>().HasData(r));
            timeModels.ForEach(t => modelBuilder.Entity<TimeModel>().HasData(t));
            therapies.ForEach(t => modelBuilder.Entity<Therapy>().HasData(t));
            patients.ForEach(p => modelBuilder.Entity<Patient>().HasData(p));
            robot.ForEach(r => modelBuilder.Entity<Robot>().HasData(r));
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
