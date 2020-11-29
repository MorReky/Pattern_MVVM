using System.Collections.Generic;

namespace Pattern_MVVM.Models
{
    internal class CountryInfo : Country
    {
    public IEnumerable<ProvinceInfo> ProvinceCounts { get; set; }
    }

}
