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
            { "Göteborg", RiskZone.Z1 },
            { "Malmö", RiskZone.Z2 },
            { "Uppsala", RiskZone.Z2 },
            { "Linköping", RiskZone.Z3 },
            { "Västerås", RiskZone.Z3 },
            { "Örebro", RiskZone.Z4 },
            { "Jönköping", RiskZone.Z3 }

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
