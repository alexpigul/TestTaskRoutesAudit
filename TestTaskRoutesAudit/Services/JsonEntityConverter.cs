using System;
using System.Collections.Generic;
using System.Linq;
using TestTaskRoutesAudit.Enums;


namespace TestTaskRoutesAudit.Services
{
    public interface IJsonToEntityConverter
    {
        Entities.Context.EntityContext ConvertJsonRouteToEntityContext(Models.InputJson.Json inputJson);
    }


    public class JsonToEntityConverter : IJsonToEntityConverter
    {
        public Entities.Context.EntityContext ConvertJsonRouteToEntityContext(Models.InputJson.Json inputJson)
        {
            Models.Route jsonRoute = inputJson.Route;

            Entities.Route entityRoute = new Entities.Route
            {
                Id = jsonRoute.Id,
                Name = jsonRoute.Name,
                ActiveDays = Enum.Parse<Days>(jsonRoute.ActiveDays),
                StartDate = jsonRoute.StartDate,
                EndDate = jsonRoute.EndDate,
                Rides = MapJsonRides(jsonRoute.Rides, inputJson).ToList()
            };

            Entities.Context.EntityContext entityContext = new Entities.Context.EntityContext()
            {
                Routes = new List<Entities.Route>
                {
                    entityRoute
                },
                PassengerStationRelations = MapJsonPassengersStations(inputJson)
            };

            return entityContext;
        }


        private IEnumerable<Entities.Ride> MapJsonRides(IEnumerable<int> rides, Models.InputJson.Json inputJson)
        {
            IList<Entities.Ride> rideEntities = new List<Entities.Ride>();

            foreach (var rideId in rides)
            {
                Models.Ride jsonRide = inputJson.Rides.First(x => x.Id == rideId);

                Entities.Ride entityRide = new Entities.Ride
                {
                    Id = rideId,
                    Date = jsonRide.Date,
                    StartTime = jsonRide.StartTime,
                    PlannedStartTime = jsonRide.PlannedStartTime,
                    DriverId = jsonRide.Driver ?? 0,
                    Driver = MapJsonDriver(jsonRide.Driver, inputJson),
                    PlannedDriverId = jsonRide.PlannedDriver,
                    PlannedDriver = MapJsonDriver(jsonRide.PlannedDriver, inputJson),
                    Stations = MapJsonStations(jsonRide.Stations, inputJson).ToList(),
                    Cancelled = jsonRide.Cancelled
                };

                rideEntities.Add(entityRide);
            }

            return rideEntities;
        }

        private IEnumerable<Entities.Station> MapJsonStations(IEnumerable<int> stations, Models.InputJson.Json inputJson)
        {
            IList<Entities.Station> stationEntities = new List<Entities.Station>();

            foreach (var stationId in stations)
            {
                Models.Station jsonStation = inputJson.Stations.First(x => x.Id == stationId);

                Entities.Station entityStation = new Entities.Station
                {
                    Id = stationId,
                    Name = jsonStation.Name,
                    Address = jsonStation.Address,
                    Order = jsonStation.Order,
                    PlannedOrder = jsonStation.PlannedOrder,
                    IsActive = jsonStation.IsActive
                };

                stationEntities.Add(entityStation);
            }

            return stationEntities;
        }

        private IList<Entities.PassengerStationRelation> MapJsonPassengersStations(Models.InputJson.Json inputJson)
        {
            IList<Entities.PassengerStationRelation> passengerStationRelations =
                new List<Entities.PassengerStationRelation>();

            foreach (var inputJsonPassenger in inputJson.Passengers)
            {
                Entities.PassengerStationRelation passengerStationRelation = new Entities.PassengerStationRelation
                {
                    PassengerId = inputJsonPassenger.Id,
                    StationId = inputJsonPassenger.DestinationStation,
                    Passenger = MapJsonPassenger(inputJsonPassenger.Id, inputJson),
                    Station = MapJsonStation(inputJsonPassenger.DestinationStation, inputJson)
                };

                passengerStationRelations.Add(passengerStationRelation);
            }

            return passengerStationRelations;
        }

        private Entities.Station MapJsonStation(int stationId, Models.InputJson.Json inputJson)
        {
            Models.Station jsonStation = inputJson.Stations.First(x => x.Id == stationId);

            Entities.Station entityStation = new Entities.Station
            {
                Id = stationId,
                Name = jsonStation.Name,
                Address = jsonStation.Address,
                Order = jsonStation.Order,
                PlannedOrder = jsonStation.PlannedOrder,
                IsActive = jsonStation.IsActive
            };

            return entityStation;
        }


        private Entities.Passenger MapJsonPassenger(int passengerId, Models.InputJson.Json inputJson)
        {

            Models.Passenger jsonPassenger = inputJson.Passengers.First(x => x.Id == passengerId);

            Entities.Passenger entityPassenger = new Entities.Passenger
            {
                Id = passengerId,
                DestinationStationId = jsonPassenger.DestinationStation,
                PersonId = jsonPassenger.Person,
                Person = MapJsonPerson(jsonPassenger.Person, inputJson),
                IsActive = jsonPassenger.IsActive
            };

            return entityPassenger;
        }

        private Entities.Driver MapJsonDriver(int? jsonDriverId, Models.InputJson.Json inputJson)
        {
            if (jsonDriverId == null)
            {
                return null;
            }

            Models.Driver jsonDriver = inputJson.Drivers.First(x => x.Id == jsonDriverId);

            Entities.Driver entityDriver = new Entities.Driver
            {
                Id = jsonDriverId.Value,
                LicenseNumber = jsonDriver.LicenseNumber,
                PersonId = jsonDriver.Person,
                Person = MapJsonPerson(jsonDriver.Person, inputJson)
            };

            return entityDriver;
        }

        private Entities.Person MapJsonPerson(int jsonPersonId, Models.InputJson.Json inputJson)
        {
            Models.Person jsonPerson = inputJson.Persons.First(x => x.Id == jsonPersonId);

            Entities.Person entityPerson = new Entities.Person
            {
                Id = jsonPersonId,
                FirstName = jsonPerson.FirstName,
                LastName = jsonPerson.LastName
            };

            return entityPerson;
        }
    }
}
