namespace Eventsourcing.DataLoader.Model;

public class CatalogsInputModel
{
    public List<CountryInputModel> Countries { get; init; } = new List<CountryInputModel>();
    public List<CityInputModel> Cities { get; init; } = new List<CityInputModel>();
    public List<AirportInputModel> Airports { get; init; } = new List<AirportInputModel>();
    public List<CarrierInputModel> Carriers { get; init; } = new List<CarrierInputModel>();
    public List<BookingStatusInputModel> BookingStatuses { get; init; } = new List<BookingStatusInputModel>();    
}
