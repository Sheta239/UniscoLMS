using System.Collections.Generic;

namespace UniscoLMS.ViewModels.Requests
{
    public class CalculationRequest
    {
        public long countryId { get; set; }
        public List<long> id { get; set; }
    }
}
