using System.Collections.Generic;

namespace TestTaskRoutesAudit.Models.InputJson
{
    public class Json
    {
        public IEnumerable<Person> Persons { get; set; }
        public IEnumerable<Driver> Drivers { get; set; }
        public IEnumerable<Passenger> Passengers { get; set; }
        public IEnumerable<Station> Stations { get; set; }
        public IEnumerable<Ride> Rides { get; set; }
        public Route Route { get; set; }
    }
}
