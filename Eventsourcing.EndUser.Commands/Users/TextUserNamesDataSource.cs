using Eventsourcing.EndUser.Commands.Interfaces;

namespace Eventsourcing.EndUser.Commands.Users;

public class TextUserNamesDataSource : ITextUserNamesDataSource
{
    private readonly ITextUserNamesDataSourceOptions _options;
    private string _menNamesText = default!;
    private string _womenNamesText = default!;
    private string _lastNamesText = default!;

    public TextUserNamesDataSource(ITextUserNamesDataSourceOptions options)
    {
        _options = options ?? throw new ArgumentNullException(nameof(options));
        LoadCatalogData();
    }

    public string MenNames => _menNamesText;

    public string WomenNames => _womenNamesText;

    public string LastNames => _lastNamesText;

    private void LoadCatalogData()
    {
        _menNamesText = File.ReadAllText(_options.MenNamesFullpathDataSource, System.Text.Encoding.UTF8);
        _womenNamesText = File.ReadAllText(_options.WomenNamesFullpathDataSource, System.Text.Encoding.UTF8);
        _lastNamesText = File.ReadAllText(_options.LastNamesFullpathDataSource, System.Text.Encoding.UTF8);
    }
}
