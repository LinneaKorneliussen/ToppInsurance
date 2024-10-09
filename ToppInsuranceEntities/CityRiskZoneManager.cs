using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopInsuranceEntities
{
    public class CityRiskZoneManager
    {
        public Dictionary<string, RiskZone> CityRiskZones { get; set; }

        public CityRiskZoneManager()
        {
            CityRiskZones = new Dictionary<string, RiskZone>
        {
            { "Stockholm", RiskZone.Z1 },
            { "Göteborg", RiskZone.Z2 },
            { "Malmö", RiskZone.Z3 },
            { "Uppsala", RiskZone.Z4 },
            { "Linköping", RiskZone.Z3 },
            { "Västerås", RiskZone.Z2 },
            { "Örebro", RiskZone.Z4 }
        };
        }


        public RiskZone GetRiskZoneByCity(string city)
        {
            if (CityRiskZones.TryGetValue(city, out RiskZone riskZone))
            {
                return riskZone;
            }
            else
            {
                throw new ArgumentException("Staden finns inte i listan över riskzoner");
            }
        }

    }

}
