using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Regex_1.Model
{
    public class PdfModel
    {

        [JsonProperty("IsPolicy")]
        public bool IsPolicy { get; set; }
        [JsonProperty("IsOffer")]
        public bool IsOffer { get; set; }
        [JsonProperty("PolicyNo")]
        public string PolicyNo { get; set; }
        [JsonProperty("StartDate")]
        public string StartDate { get; set; }
        [JsonProperty("EndDate")]
        public string EndDate { get; set; }
        [JsonProperty("RegisterNo")]
        public string RegisterNo { get; set; }
        [JsonProperty("PlateNumber")]
        public string PlateNumber { get; set; }
        [JsonProperty("PermitNumber")]
        public string PermitNumber { get; set; }
        [JsonProperty("FrameNumber")]
        public string FrameNumber { get; set; }
        [JsonProperty("EngineNo")]
        public string EngineNo { get; set; }
        [JsonProperty("Brand")]
        public string Brand { get; set; }
        [JsonProperty("VehicleModel")]
        public string VehicleModel { get; set; }
        [JsonProperty("ModelYear")]
        public string ModelYear { get; set; }
        [JsonProperty("VehicleType")]
        public string VehicleType { get; set; }
        [JsonProperty("UsingType")]
        public string UsingType { get; set; }
        [JsonProperty("GrossPremium")]
        public string GrossPremium { get; set; }
        [JsonProperty("NetPremium")]
        public string NetPremium { get; set; }
        [JsonProperty("Branch")]
        public string Branch { get; set; }
        [JsonProperty("ZeyilNo")]
        public string ZeyilNo { get; set; }
        [JsonProperty("RenewNo")]
        public string RenewNo { get; set; }
        [JsonProperty("AcenteNo")]
        public string AcenteNo { get; set; }
        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("SurName")]
        public string SurName { get; set; }
        [JsonProperty("NationalNumber")]
        public string NationalNumber { get; set; }
        [JsonProperty("Firm")]
        public string Firm { get; set; }
        [JsonProperty("BrandCode")]
        public string BrandCode { get; set; }
        [JsonProperty("PersonCount")]
        public string PersonCount { get; set; }
        [JsonProperty("VknNo")]
        public string VknNo { get; set; }
        [JsonProperty("QueryType")]
        public string QueryType { get; set; }

        [JsonProperty("IsZeyil")]
        public bool? IsZeyil { get; set; }
        [JsonProperty("IsTecdit")]
        public bool? IsTecdit { get; set; }
        [JsonProperty("Country")]

        public string? Country { get; set; }
        [JsonProperty("BirthDate")]
        public string? BirthDate { get; set; }
        [JsonProperty("Network")]
        public string? Network { get; set; }
        public string? PDFFile { get; set; }
    }
}
