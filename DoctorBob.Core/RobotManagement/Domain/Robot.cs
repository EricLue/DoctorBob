using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.ComponentModel.DataAnnotations.Schema;
using DoctorBob.Core.Common.Domain;
using DoctorBob.Core.PatientManagement.Domain;

namespace DoctorBob.Core.RoboManagement.Domain
{
    public class Robot : AuditableEntity
    {
        public int Id { get; set; }
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Name { get; set; }
        public int LastRoomId { get; set; }
        public Room LastRoom { get; set; }
        public CurrentLocation CurrentLocation { get; set; }
        public int Power { get; set; }
        public Activity Activity { get; set; }

        // Warenkorb aka Auftrag verschiedene Patienten

    }

    public enum CurrentLocation
    {
        Home,
        DrugStore,
        WayToRoom1,
        Room1,
        WayToRoom2,
        Room2,
        WayToRoom3,
        Room3,
        WayToRoom4,
        Room4,
        WayToHome
    }
    
    public enum Activity
    {
        Standby,
        Charging,
        GettingDrugs,
        LoadedDrugs,
        OnWayToRoom,
        DeliverDrugs,
        DeliverySuccessfull,
        LeavingRoom,
        ReturningHome
    }
}
