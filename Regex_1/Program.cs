using iTextSharp.text.pdf.parser;
using iTextSharp.text.pdf;
using Newtonsoft.Json;
using Regex_1.Enums;
using Regex_1.Model;
using System.Text.RegularExpressions;

try
{
    bool IsPolicy = false;
    bool IsOffer = false;
    bool IsZeyil = false;

    List<PdfModel> response = new List<PdfModel>();
    List<RegexModel> patternList = new List<RegexModel>();
    Dictionary<string, string> extractResponse = new Dictionary<string, string>();

    var queryType = QueryTypeEnum.None;
    var branch = QueryTypeEnum.None;

    string[] offerPatterns = { "(KASKO SİGORTA)(.*)" };
    string[] zeyilPatterns = { "" };

    var read = PdfExtractText("pdf/atlas.pdf"); // pdfin konumu

    var text = read[0].ToString();

    Dictionary<RegexEnum, string> cascoRegex = new Dictionary<RegexEnum, string>() {
        {RegexEnum.NationalNumber,@"(?<=TCK/VERGİ DR. NO : )(.*)(?=)" },
        {RegexEnum.PolicyNo,@"" },
        //{RegexEnum.RenewNo,@"" },
        {RegexEnum.StartDate,@"" },
        {RegexEnum.EndDate, @""},
        {RegexEnum.Name,@"" },
        {RegexEnum.Firm,@"" },
        {RegexEnum.Brand,@"" },
        {RegexEnum.VehicleModel,@"" },
        {RegexEnum.ModelYear,@"" },
        {RegexEnum.UsingType, @""},
        {RegexEnum.VehicleType,@"" },
        {RegexEnum.BrandCode,@" "},
        {RegexEnum.FrameNumber,@"" },
        {RegexEnum.EngineNo,@"" },
        {RegexEnum.NetPremium,@"" },
        {RegexEnum.GrossPremium,@"" },
        {RegexEnum.RegisterNo,@"" },
        {RegexEnum.PlateNumber,"" },
        {RegexEnum.Branch,@"" },
        {RegexEnum.VknNo,@"" },
        {RegexEnum.IsPolicy,  @"" },
        {RegexEnum.IsOffer, @"" },
        {RegexEnum.QueryType, @"" },
        {RegexEnum.AcenteNo,@"" },
        {RegexEnum.ZeyilNo,@"" }
    };

    Dictionary<RegexEnum, string> trafficRegex = new Dictionary<RegexEnum, string>() {
        {RegexEnum.NationalNumber,@"" },
        {RegexEnum.PolicyNo,@"" },
        //{RegexEnum.RenewNo,@"" },
        {RegexEnum.StartDate,@"" },
        {RegexEnum.EndDate, @""},
        {RegexEnum.Name,@"" },
        {RegexEnum.Firm,@"" },
        {RegexEnum.Brand,@"" },
        {RegexEnum.VehicleModel,@"" },
        {RegexEnum.ModelYear,@"" },
        {RegexEnum.UsingType, @""},
        {RegexEnum.VehicleType,@"" },
        {RegexEnum.BrandCode,@" "},
        {RegexEnum.FrameNumber,@"" },
        {RegexEnum.EngineNo,@"" },
        {RegexEnum.NetPremium,@"" },
        {RegexEnum.GrossPremium,@"" },
        {RegexEnum.RegisterNo,@"" },
        {RegexEnum.PlateNumber,"" },
        {RegexEnum.Branch,@"" },
        {RegexEnum.VknNo,@"" },
        {RegexEnum.IsPolicy,  @"" },
        {RegexEnum.IsOffer, @"" },
        {RegexEnum.QueryType, @"" },
        {RegexEnum.AcenteNo,@"" },
        {RegexEnum.ZeyilNo,@"" }
    };

    Dictionary<RegexEnum, string> zeyilRegex = new Dictionary<RegexEnum, string>() {
        {RegexEnum.NationalNumber,@"" },
        {RegexEnum.PolicyNo,@"" },
        //{RegexEnum.RenewNo,@"" },
        {RegexEnum.StartDate,@"" },
        {RegexEnum.EndDate, @""},
        {RegexEnum.Name,@"" },
        {RegexEnum.Firm,@"" },
        {RegexEnum.Brand,@"" },
        {RegexEnum.VehicleModel,@"" },
        {RegexEnum.ModelYear,@"" },
        {RegexEnum.UsingType, @""},
        {RegexEnum.VehicleType,@"" },
        {RegexEnum.BrandCode,@" "},
        {RegexEnum.FrameNumber,@"" },
        {RegexEnum.EngineNo,@"" },
        {RegexEnum.NetPremium,@"" },
        {RegexEnum.GrossPremium,@"" },
        {RegexEnum.RegisterNo,@"" },
        {RegexEnum.PlateNumber,"" },
        {RegexEnum.Branch,@"" },
        {RegexEnum.VknNo,@"" },
        {RegexEnum.IsPolicy,  @"" },
        {RegexEnum.IsOffer, @"" },
        {RegexEnum.QueryType, @"" },
        {RegexEnum.AcenteNo,@"" },
        {RegexEnum.ZeyilNo,@"" }
    };


    foreach (var offers in offerPatterns)
    {
        Match match = Regex.Match(text, offers);
        if (match.Success)
        {
            if (match.Value.Contains("TEKLİFİ") || match.Value.Contains("Teklifi") || match.Value.Contains("TEKLİFNAMEDİR") || match.Value.Contains("TEKLİFİDİR"))
            {
                IsOffer = true;
                IsPolicy = false;
            }
            else
            {
                IsPolicy = true;
            }
            if (match.Value.Contains("Trafik") || match.Value.Contains("TRAFİK") || match.Value.Contains("MALİ") || match.Value.Contains("SORUMLULUK") || match.Value.Contains("(MALİ SORUMLULUK SİGORTA POLİÇESİ)") || match.Value.Contains("ZORUNLU MALİ SORUMLULUK"))
            {
                queryType = QueryTypeEnum.trafik;
                branch = QueryTypeEnum.trafik;
            }
            else
            {
                queryType = QueryTypeEnum.kasko;
                branch = QueryTypeEnum.kasko;
            }
        }
    }

    //foreach (var zeyil in zeyilPatterns) {
    //    Match match = Regex.Match(text, zeyil);
    //    if (match.Success) {
    //        queryType = QueryTypeEnum.ZEYIL;
    //        IsZeyil = true;
    //    }
    //}

    switch (queryType)
    {
        case QueryTypeEnum.None:
            break;
        case QueryTypeEnum.kasko:
            foreach (var cascoRegexItem in cascoRegex)
            {
                patternList.Add(new RegexModel()
                {
                    PatternName = cascoRegexItem.Key.ToString(),
                    PatternValue = cascoRegexItem.Value,
                });
            }

            foreach (var pattern in patternList)
            {
                Match match = Regex.Match(text, pattern.PatternValue);
                if (match.Success)
                {
                    Console.WriteLine($"{pattern.PatternName}: {match.Value}");
                    if (pattern.PatternName == "IsPolicy")
                    {
                        extractResponse.Add(pattern.PatternName, IsPolicy.ToString());
                    }
                    else if (pattern.PatternName == "IsOffer")
                    {
                        extractResponse.Add(pattern.PatternName, IsOffer.ToString());
                    }
                    else if (pattern.PatternName == "QueryType")
                    {
                        extractResponse.Add(pattern.PatternName, queryType.ToString());
                    }
                    else if (pattern.PatternName == "Firm")
                    {
                        extractResponse.Add(pattern.PatternName, "ANKARA");
                    }
                    else if (pattern.PatternName == "Branch")
                    {
                        extractResponse.Add(pattern.PatternName, branch.ToString());
                    }

                    var exits = extractResponse.Where(x => x.Key == pattern.PatternName).ToList();
                    if (exits.Count == 0)
                    {
                        extractResponse.Add(pattern.PatternName, match.Value.Trim().ToString());
                    }
                }
            }
            break;
        case QueryTypeEnum.trafik:

            break;
        case QueryTypeEnum.dask:
            break;
        case QueryTypeEnum.tamamlayici:
            break;
        case QueryTypeEnum.ZEYIL:
            break;
        default:
            break;
    }

    var serializeResponse = JsonConvert.SerializeObject(extractResponse);
    var execute = JsonConvert.DeserializeObject<PdfModel>(serializeResponse);

}
catch (Exception e)
{
    Console.WriteLine(e.ToString());
}

List<string> PdfExtractText(string path)
{
    List<string> pdfLine = new List<string>();
    string page = "";
    using (PdfReader reader = new PdfReader(path))
    {
        ITextExtractionStrategy Strategy = new LocationTextExtractionStrategy();
        /* for (int i = 1; i <= reader.NumberOfPages; i++) { TÜM SAYFALARI ALIR
          page += PdfTextExtractor.GetTextFromPage(reader, i, Strategy);
      } */
        for (int i = 1; i <= 1; i++)
        {
            page += PdfTextExtractor.GetTextFromPage(reader, i, Strategy);
        }
        pdfLine.Add(page);

        return pdfLine.ToList();
    }
}