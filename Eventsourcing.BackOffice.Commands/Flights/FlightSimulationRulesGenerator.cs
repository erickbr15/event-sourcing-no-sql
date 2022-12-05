namespace Eventsourcing.BackOffice.Commands.Flights;

public static class FlightSimulationRulesGenerator
{
    public static IList<FlightSimlationRule> GetFlightSimlationRules()
    {
        var simulationRules = new List<FlightSimlationRule>();
        simulationRules.Add(GenerateBeachFlightsRules());
        simulationRules.Add(GenerateNorthFlightsRule());
        simulationRules.Add(GenerateSouthFlightsRule());
        simulationRules.Add(GenerateAllCountryFlightsRule());

        return simulationRules;
    }
    
    private static FlightSimlationRule GenerateBeachFlightsRules()
    {
        var simlationRule = new FlightSimlationRule();
        simlationRule.CarrierCodes.Add("AEMAR");
        simlationRule.CarrierCodes.Add("MAG");
        simlationRule.SourceAirportCodesAndTimes.Add("ACA", new List<TimeOnly> { new TimeOnly(5,0),  new TimeOnly(5,30),  new TimeOnly(23, 30) });
        simlationRule.SourceAirportCodesAndTimes.Add("GDL", new List<TimeOnly> { new TimeOnly(5, 0), new TimeOnly(5, 30), new TimeOnly(23, 30) });
        simlationRule.SourceAirportCodesAndTimes.Add("MEX", new List<TimeOnly> { new TimeOnly(5, 0), new TimeOnly(5, 30), new TimeOnly(23, 30) });
        simlationRule.SourceAirportCodesAndTimes.Add("CUN", new List<TimeOnly> { new TimeOnly(5, 0), new TimeOnly(5, 30), new TimeOnly(23, 30) });
        simlationRule.SourceAirportCodesAndTimes.Add("CZM", new List<TimeOnly> { new TimeOnly(5, 0), new TimeOnly(5, 30), new TimeOnly(23, 30) });
        simlationRule.SourceAirportCodesAndTimes.Add("ZIH", new List<TimeOnly> { new TimeOnly(5, 0), new TimeOnly(5, 30), new TimeOnly(23, 30) });
        simlationRule.SourceAirportCodesAndTimes.Add("LAP", new List<TimeOnly> { new TimeOnly(5, 0), new TimeOnly(5, 30), new TimeOnly(23, 30) });
        simlationRule.SourceAirportCodesAndTimes.Add("ZLO", new List<TimeOnly> { new TimeOnly(5, 0), new TimeOnly(5, 30), new TimeOnly(23, 30) });
        simlationRule.SourceAirportCodesAndTimes.Add("MZT", new List<TimeOnly> { new TimeOnly(5, 0), new TimeOnly(5, 30), new TimeOnly(23, 30) });
        simlationRule.SourceAirportCodesAndTimes.Add("MID", new List<TimeOnly> { new TimeOnly(5, 0), new TimeOnly(5, 30), new TimeOnly(23, 30) });
        simlationRule.SourceAirportCodesAndTimes.Add("PVR", new List<TimeOnly> { new TimeOnly(5, 0), new TimeOnly(5, 30), new TimeOnly(23, 30) });
        simlationRule.SourceAirportCodesAndTimes.Add("SJD", new List<TimeOnly> { new TimeOnly(5, 0), new TimeOnly(5, 30), new TimeOnly(23, 30) });

        simlationRule.DestinationAirportCodes.Add("ACA");
        simlationRule.DestinationAirportCodes.Add("GDL");
        simlationRule.DestinationAirportCodes.Add("MEX");
        simlationRule.DestinationAirportCodes.Add("CUN");
        simlationRule.DestinationAirportCodes.Add("CZM");
        simlationRule.DestinationAirportCodes.Add("ZIH");
        simlationRule.DestinationAirportCodes.Add("LAP");
        simlationRule.DestinationAirportCodes.Add("ZLO");
        simlationRule.DestinationAirportCodes.Add("MZT");
        simlationRule.DestinationAirportCodes.Add("MID");
        simlationRule.DestinationAirportCodes.Add("PVR");
        simlationRule.DestinationAirportCodes.Add("SJD");

        return simlationRule;
    }

    private static FlightSimlationRule GenerateNorthFlightsRule()
    {
        var simlationRule = new FlightSimlationRule();

        simlationRule.CarrierCodes.Add("TAR");
        simlationRule.CarrierCodes.Add("VIV");
        simlationRule.SourceAirportCodesAndTimes.Add("MTY", new List<TimeOnly> { new TimeOnly(6, 0), new TimeOnly(7, 30), new TimeOnly(8, 0) });
        simlationRule.SourceAirportCodesAndTimes.Add("GDL", new List<TimeOnly> { new TimeOnly(6, 0), new TimeOnly(7, 30), new TimeOnly(8, 0) });
        simlationRule.SourceAirportCodesAndTimes.Add("MEX", new List<TimeOnly> { new TimeOnly(6, 0), new TimeOnly(7, 30), new TimeOnly(8, 0) });
        simlationRule.SourceAirportCodesAndTimes.Add("HMO", new List<TimeOnly> { new TimeOnly(6, 0), new TimeOnly(7, 30), new TimeOnly(8, 0) });
        simlationRule.SourceAirportCodesAndTimes.Add("TIJ", new List<TimeOnly> { new TimeOnly(6, 0), new TimeOnly(7, 30), new TimeOnly(8, 0) });
        simlationRule.SourceAirportCodesAndTimes.Add("CUU", new List<TimeOnly> { new TimeOnly(6, 0), new TimeOnly(7, 30), new TimeOnly(8, 0) });
        simlationRule.SourceAirportCodesAndTimes.Add("AGU", new List<TimeOnly> { new TimeOnly(6, 0), new TimeOnly(7, 30), new TimeOnly(8, 0) });
        simlationRule.SourceAirportCodesAndTimes.Add("TRC", new List<TimeOnly> { new TimeOnly(6, 0), new TimeOnly(7, 30), new TimeOnly(8, 0) });
        simlationRule.SourceAirportCodesAndTimes.Add("CUL", new List<TimeOnly> { new TimeOnly(6, 0), new TimeOnly(7, 30), new TimeOnly(8, 0) });
        simlationRule.SourceAirportCodesAndTimes.Add("CJS", new List<TimeOnly> { new TimeOnly(6, 0), new TimeOnly(7, 30), new TimeOnly(8, 0) });
        simlationRule.SourceAirportCodesAndTimes.Add("LAP", new List<TimeOnly> { new TimeOnly(6, 0), new TimeOnly(7, 30), new TimeOnly(8, 0) });
        simlationRule.SourceAirportCodesAndTimes.Add("REX", new List<TimeOnly> { new TimeOnly(6, 0), new TimeOnly(7, 30), new TimeOnly(8, 0) });

        simlationRule.DestinationAirportCodes.Add("MTY");
        simlationRule.DestinationAirportCodes.Add("GDL");
        simlationRule.DestinationAirportCodes.Add("MEX");
        simlationRule.DestinationAirportCodes.Add("HMO");
        simlationRule.DestinationAirportCodes.Add("TIJ");
        simlationRule.DestinationAirportCodes.Add("CUU");
        simlationRule.DestinationAirportCodes.Add("AGU");
        simlationRule.DestinationAirportCodes.Add("TRC");
        simlationRule.DestinationAirportCodes.Add("CUL");
        simlationRule.DestinationAirportCodes.Add("CJS");
        simlationRule.DestinationAirportCodes.Add("LAP");
        simlationRule.DestinationAirportCodes.Add("REX");

        return simlationRule;
    }

    private static FlightSimlationRule GenerateSouthFlightsRule()
    {
        var simlationRule = new FlightSimlationRule();

        var schedules = new List<TimeOnly>
        {
            new TimeOnly(13, 30),
            new TimeOnly(14, 0),
            new TimeOnly(15, 30)            
        };

        simlationRule.CarrierCodes.Add("VOL");
        simlationRule.CarrierCodes.Add("INT");
        
        simlationRule.SourceAirportCodesAndTimes.Add("GDL", new List<TimeOnly>(schedules));
        simlationRule.SourceAirportCodesAndTimes.Add("MEX", new List<TimeOnly>(schedules));
        simlationRule.SourceAirportCodesAndTimes.Add("MID", new List<TimeOnly>(schedules));
        simlationRule.SourceAirportCodesAndTimes.Add("MLM", new List<TimeOnly>(schedules));
        simlationRule.SourceAirportCodesAndTimes.Add("OAX", new List<TimeOnly>(schedules));
        simlationRule.SourceAirportCodesAndTimes.Add("PBC", new List<TimeOnly>(schedules));
        simlationRule.SourceAirportCodesAndTimes.Add("QRO", new List<TimeOnly>(schedules));
        simlationRule.SourceAirportCodesAndTimes.Add("SLP", new List<TimeOnly>(schedules));
        simlationRule.SourceAirportCodesAndTimes.Add("VER", new List<TimeOnly>(schedules));
        simlationRule.SourceAirportCodesAndTimes.Add("VSA", new List<TimeOnly>(schedules));

        simlationRule.DestinationAirportCodes.Add("GDL");
        simlationRule.DestinationAirportCodes.Add("MEX");
        simlationRule.DestinationAirportCodes.Add("MID");
        simlationRule.DestinationAirportCodes.Add("MLM");
        simlationRule.DestinationAirportCodes.Add("OAX");
        simlationRule.DestinationAirportCodes.Add("PBC");
        simlationRule.DestinationAirportCodes.Add("QRO");
        simlationRule.DestinationAirportCodes.Add("SLP");
        simlationRule.DestinationAirportCodes.Add("VER");
        simlationRule.DestinationAirportCodes.Add("VSA");

        return simlationRule;
    }

    private static FlightSimlationRule GenerateAllCountryFlightsRule()
    {
        var simlationRule = new FlightSimlationRule();

        var schedules = new List<TimeOnly>
        {                       
            new TimeOnly(13, 30),
            new TimeOnly(14, 0),            
            new TimeOnly(20, 0)            
        };

        simlationRule.CarrierCodes.Add("AEMEXC");
        simlationRule.CarrierCodes.Add("AEMEX");

        simlationRule.SourceAirportCodesAndTimes.Add("GDL", new List<TimeOnly>(schedules));
        simlationRule.SourceAirportCodesAndTimes.Add("MEX", new List<TimeOnly>(schedules));
        simlationRule.SourceAirportCodesAndTimes.Add("MTY", new List<TimeOnly>(schedules));
        simlationRule.SourceAirportCodesAndTimes.Add("ACA", new List<TimeOnly>(schedules));
        simlationRule.SourceAirportCodesAndTimes.Add("CUN", new List<TimeOnly>(schedules));
        simlationRule.SourceAirportCodesAndTimes.Add("CUU", new List<TimeOnly>(schedules));
        simlationRule.SourceAirportCodesAndTimes.Add("CJS", new List<TimeOnly>(schedules));
        simlationRule.SourceAirportCodesAndTimes.Add("ZIH", new List<TimeOnly>(schedules));
        simlationRule.SourceAirportCodesAndTimes.Add("MZT", new List<TimeOnly>(schedules));
        simlationRule.SourceAirportCodesAndTimes.Add("MID", new List<TimeOnly>(schedules));
        simlationRule.SourceAirportCodesAndTimes.Add("OAX", new List<TimeOnly>(schedules));
        simlationRule.SourceAirportCodesAndTimes.Add("PVR", new List<TimeOnly>(schedules));
        simlationRule.SourceAirportCodesAndTimes.Add("QRO", new List<TimeOnly>(schedules));
        simlationRule.SourceAirportCodesAndTimes.Add("SLP", new List<TimeOnly>(schedules));
        simlationRule.SourceAirportCodesAndTimes.Add("BJX", new List<TimeOnly>(schedules));
        simlationRule.SourceAirportCodesAndTimes.Add("TIJ", new List<TimeOnly>(schedules));
        simlationRule.SourceAirportCodesAndTimes.Add("TRC", new List<TimeOnly>(schedules));
        simlationRule.SourceAirportCodesAndTimes.Add("VER", new List<TimeOnly>(schedules));
        simlationRule.SourceAirportCodesAndTimes.Add("ZCL", new List<TimeOnly>(schedules));

        simlationRule.DestinationAirportCodes.Add("GDL");
        simlationRule.DestinationAirportCodes.Add("MEX");
        simlationRule.DestinationAirportCodes.Add("MTY");
        simlationRule.DestinationAirportCodes.Add("ACA");
        simlationRule.DestinationAirportCodes.Add("CUN");
        simlationRule.DestinationAirportCodes.Add("CUU");
        simlationRule.DestinationAirportCodes.Add("CJS");
        simlationRule.DestinationAirportCodes.Add("ZIH");
        simlationRule.DestinationAirportCodes.Add("MZT");
        simlationRule.DestinationAirportCodes.Add("MID");
        simlationRule.DestinationAirportCodes.Add("OAX");
        simlationRule.DestinationAirportCodes.Add("PVR");
        simlationRule.DestinationAirportCodes.Add("QRO");
        simlationRule.DestinationAirportCodes.Add("SLP");
        simlationRule.DestinationAirportCodes.Add("BJX");
        simlationRule.DestinationAirportCodes.Add("TIJ");
        simlationRule.DestinationAirportCodes.Add("TRC");
        simlationRule.DestinationAirportCodes.Add("VER");
        simlationRule.DestinationAirportCodes.Add("ZCL");

        return simlationRule;
    }

}
