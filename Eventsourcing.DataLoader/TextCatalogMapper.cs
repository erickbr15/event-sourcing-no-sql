using Eventsourcing.DataLoader.Interfaces;
using Eventsourcing.DataLoader.Model;

namespace Eventsourcing.DataLoader;

public class TextCatalogMapper : ITextCatalogMapper
{
    private readonly ITextToInputModelMapper<AirportInputModel> _airportsInputModelMapper;
    private readonly ITextToInputModelMapper<CountryInputModel> _countryInputModelMapper;
    private readonly ITextToInputModelMapper<CityInputModel> _citiesInputModelMapper;
    private readonly ITextToInputModelMapper<CarrierInputModel> _carriersInputModelMapper;
    private readonly ITextToInputModelMapper<BookingStatusInputModel> _bookingStatusesInputModelMapper;

    public TextCatalogMapper(
    ITextToInputModelMapper<CountryInputModel> countryInputModelMapper,
    ITextToInputModelMapper<CityInputModel> citiesInputModelMapper,
    ITextToInputModelMapper<AirportInputModel> airportsInputModelMapper,
    ITextToInputModelMapper<CarrierInputModel> carriersInputModelMapper,
    ITextToInputModelMapper<BookingStatusInputModel> bookingStatusesInputModelMapper)
    {
        _countryInputModelMapper = countryInputModelMapper ?? throw new ArgumentNullException(nameof(countryInputModelMapper));
        _citiesInputModelMapper = citiesInputModelMapper ?? throw new ArgumentNullException(nameof(citiesInputModelMapper));
        _airportsInputModelMapper = airportsInputModelMapper ?? throw new ArgumentNullException(nameof(airportsInputModelMapper));
        _carriersInputModelMapper = carriersInputModelMapper ?? throw new ArgumentNullException(nameof(carriersInputModelMapper));
        _bookingStatusesInputModelMapper = bookingStatusesInputModelMapper ?? throw new ArgumentNullException(nameof(bookingStatusesInputModelMapper));
    }
    
    public CatalogsInputModel MapToInputModel(ITextCatalogDataSource dataSource)
    {
        if (dataSource is null)
        {
            return new CatalogsInputModel();
        }

        var catalogs = new CatalogsInputModel();

        catalogs.Countries.AddRange(_countryInputModelMapper.Map(GetLinesFromContent(dataSource.Countries)));
        catalogs.Cities.AddRange(_citiesInputModelMapper.Map(GetLinesFromContent(dataSource.Cities)));
        catalogs.Carriers.AddRange(_carriersInputModelMapper.Map(GetLinesFromContent(dataSource.Carriers)));
        catalogs.Airports.AddRange(_airportsInputModelMapper.Map(GetLinesFromContent(dataSource.Airports)));
        catalogs.BookingStatuses.AddRange(_bookingStatusesInputModelMapper.Map(GetLinesFromContent(dataSource.BookingStatuses)));        

        return catalogs;
    }

    private IEnumerable<string> GetLinesFromContent(string content)
    {
        if (string.IsNullOrEmpty(content))
        {
            return Enumerable.Empty<string>();
        }

        var lines = new List<string>();

        using var reader = new StringReader(content);
        while (reader.Peek() != -1)
        {
            lines.Add(reader.ReadLine() ?? string.Empty);
        }

        return lines;
    }    
}
