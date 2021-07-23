namespace Doods.Framework.Mobile.Std.Servicies
{
    //internal class TelemetryService2 : ITelemetryService
    //{
    //    private readonly ISettingsBase _settings;

    //    public TelemetryService2(IConfiguration config, ISettingsBase settings)
    //    {
    //        _settings = settings;
    //        if (_settings.TelemetryIsActive && !string.IsNullOrEmpty(config.MobileCenterKey))
    //        {
    //            IsActive = true;
    //        }
    //    }

    //    public bool IsActive { get; }

    //    private Guid _sessionId = Guid.NewGuid();

    //    public Guid SessionId
    //    {
    //        get => _sessionId;
    //        set => _sessionId = value;
    //    }

    //    public void Event(string name, Dictionary<string, string> properties = null,
    //        Dictionary<string, double> measures = null, Guid? operationId = null)
    //    {
    //        if (!IsActive) return;

    //        try
    //        {
    //            var p = UpdateProperties(properties, measures);
    //            Analytics.TrackEvent(name, p);
    //        }
    //        catch (Exception e)
    //        {
    //            Exception(e);
    //        }
    //    }

    //    private Dictionary<string, string> UpdateProperties(Dictionary<string, string> properties,
    //        Dictionary<string, double> measures)
    //    {
    //        var final = new Dictionary<string, string>(5);

    //        if (!string.IsNullOrEmpty(_settings.BaseId))
    //        {
    //            var context = new StringBuilder();
    //            context.Append($"baseId={_settings.BaseId}");

    //            if (_settings.TypeMembreId.HasValue)
    //            {
    //                context.Append($";typeMembreId={_settings.TypeMembreId}");
    //            }

    //            final.Add("context", context.ToString());
    //        }

    //        var p = ExtractValues(properties);
    //        if (!string.IsNullOrEmpty(p))
    //        {
    //            final.Add("properties", p);
    //        }

    //        var m = ExtractValues(measures);
    //        if (!string.IsNullOrEmpty(m))
    //        {
    //            final.Add("measures", m);
    //        }

    //        //if (_settings.IncludeLocationInTelemetry)
    //        //{
    //        //    var location = App.LocationChangedEvent?.Location?.ToString();
    //        //    if (!string.IsNullOrEmpty(location))
    //        //    {
    //        //        final.Add("location", location);
    //        //    }
    //        //}

    //        return final;
    //    }

    //    private string ExtractValues<T>(Dictionary<string, T> values)
    //    {
    //        if (values.IsEmpty()) return null;

    //        var p = new StringBuilder();
    //        foreach (var property in values)
    //        {
    //            p.Append($"{property.Key}={property.Value};");
    //        }

    //        return p.ToString();
    //    }

    //    public void Metric(string name, double value, Dictionary<string, string> properties = null,
    //        Guid? operationId = null)
    //    {
    //        if (!IsActive) return;

    //        try
    //        {
    //            var measures = new Dictionary<string, double> {{name, value}};
    //            var p = UpdateProperties(properties, measures);
    //            Analytics.TrackEvent(name, p);
    //        }
    //        catch (Exception e)
    //        {
    //            Exception(e);
    //        }
    //    }

    //    public void Dependency(string type, string target, string name, string message, DateTimeOffset start,
    //        TimeSpan duration, string resultcode, bool success, Guid? operationId)
    //    {
    //        if (!IsActive) return;
    //        throw new NotImplementedException();
    //    }

    //    public void Exception(Exception exception, Dictionary<string, string> properties = null,
    //        Dictionary<string, double> measures = null, Guid? operationId = null)
    //    {
    //        if (!IsActive) return;

    //        properties = UpdateProperties(properties, measures);
    //        Crashes.TrackError(exception, properties);
    //    }

    //    public void Request(string name, DateTimeOffset start, TimeSpan duration, string responseCode, bool success,
    //        Guid? operationId)
    //    {
    //        if (!IsActive) return;
    //        throw new NotImplementedException();
    //    }

    //    public void Dispose()
    //    {

    //    }


    //    /// <summary>
    //    /// TODO : Smarties
    //    /// </summary>
    //    /// <param name="applicationName"></param>
    //    /// <param name="utilisateurName"></param>
    //    /// <param name="utilisateurId"></param>
    //    /// <param name="structureName"></param>
    //    /// <param name="structureId"></param>
    //    /// <param name="societeAgricoleId"></param>
    //    /// <param name="cycleProductionId"></param>
    //    public void SetContext(string applicationName, string utilisateurName, int utilisateurId, string structureName,
    //        int structureId,
    //        int societeAgricoleId, int cycleProductionId)
    //    {

    //    }

    //    public void SetContext(string moduleName)
    //    {
    //    }

    //    /// <summary>
    //    /// TODO : Smarties
    //    /// </summary>
    //    /// <param name="e"></param>
    //    public void Error(Exception e, Guid? operationId)
    //    {

    //    }

    //    /// <summary>
    //    /// TODO : Smarties
    //    /// </summary>
    //    /// <param name="e"></param>
    //    public void Fatal(Exception e, Guid? operationId)
    //    {
    //        Exception(new UnHandledException(e));
    //    }

    //    /// <summary>
    //    /// TODO : Smarties
    //    /// </summary>
    //    /// <param name="e"></param>
    //    public void Warning(Exception e, Guid? operationId)
    //    {

    //    }


    //    /// <summary>
    //    /// TODO : Smarties
    //    /// </summary>
    //    /// <param name="name"></param>
    //    /// <param name="duration"></param>
    //    /// <param name="properties"></param>
    //    /// <param name="measures"></param>
    //    public void Page(string name, TimeSpan duration, Dictionary<string, string> properties,
    //        Dictionary<string, double> measures, Guid? operationId)
    //    {

    //    }
    //}
}