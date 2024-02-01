using System.ComponentModel.DataAnnotations.Schema;

namespace Simplifly.Models
{
    public class Route:IEquatable<Route>
    {
        public int Id { get; set; }
        public int SourceAirportId { get; set; }
        [ForeignKey("SourceAirportId")]
        public Airport? SourceAirport { get; set; }
        public int DestinationAirportId { get; set;}
        [ForeignKey("DestinationAirportId")]
        public Airport? DestinationAirport { get; set; }

        public Route()
        {
            
        }

        public Route(int id, int sourceAirportId, Airport? sourceAirport, int destinationAirportId, Airport? destinationAirport)
        {
            Id = id;
            SourceAirportId = sourceAirportId;
            SourceAirport = sourceAirport;
            DestinationAirportId = destinationAirportId;
            DestinationAirport = destinationAirport;
        }

        public bool Equals(Route? other)
        {
            var route= other ?? new Route();
            return this.Id.Equals(route.Id);
        }
    }
}
