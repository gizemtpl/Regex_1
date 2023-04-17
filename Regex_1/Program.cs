using iTextSharp.text.pdf.parser;
using iTextSharp.text.pdf;
using Newtonsoft.Json;
using Regex_1.Enums;
using Regex_1.Model;
using System.Text.RegularExpressions;
using System.Security.Authentication.ExtendedProtection;

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

    var read = PdfExtractText("pdf/andolukaskoteklif.pdf"); // pdfin konumu

    var text = read[0].ToString();
    //isim ,soyisim ,adres, fiyat, police,tc (text bak  alt satıra point koy)
    //(?<=\d{10}/\d{1} \d{2}/\d{2}/\d{4} - )(.*)(?= \d{10} /\d{1}\d{8} \d{9})bitiş tarihi
    //PolicyEnforcement not başlangıç bitiş
    Dictionary<RegexEnum, string> cascoRegex = new Dictionary<RegexEnum, string>() {
        {RegexEnum.NationalNumber,@"" },
        {RegexEnum.PolicyNo,@"" },
        //{RegexEnum.RenewNo,@"" },
        {RegexEnum.StartDate,@"" },
        {RegexEnum.EndDate, @""},
        {RegexEnum.Name,@"" },
        {RegexEnum.Firm,@"" },
        {RegexEnum.Brand,@"" },
        {RegexEnum.Address, @"--"},
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
        {RegexEnum.StartDate,@"(?<=Başlangıç-Bitiş Tarihi ve Saati Süresi Vadesi\n340148 0468997559 1 )(.*)(?=-15:05 - 10)" },
        {RegexEnum.EndDate, @"(?<=Bitiş Tarihi ve Saati Süresi Vadesi\n340148 0468997559 1 31/01/2023-15:05 - )(.*)(?= 31)"},
        {RegexEnum.Name,@"" },
        {RegexEnum.Firm,@"" },
        {RegexEnum.Address,@"" },
        {RegexEnum.Policypremium,@"" },
        {RegexEnum.Brand,@"(?<= Marka :)(.*)(?=)" },
        {RegexEnum.VehicleModel,@"(?<= Model : )(.*)(?=)" },
        {RegexEnum.ModelYear,@" Model Yılı : " },
        {RegexEnum.UsingType, @""},
        {RegexEnum.VehicleType,@"" },
        {RegexEnum.BrandCode,@" "},
        {RegexEnum.FrameNumber,@"(?<=Şasi No : )(.*)(?= M)" },
        {RegexEnum.EngineNo,@"" },
        {RegexEnum.NetPremium,@"(?<=Vergi Öncesi Prim )(.*)(?=)" },
        {RegexEnum.GrossPremium,@"(?<=Ödenecek Tutar )(.*)(?=)" },
        {RegexEnum.RegisterNo,@"" },
        {RegexEnum.PlateNumber,"\r\n(?<=Plaka No : )(.*)(?= Mar)" },
        {RegexEnum.Branch,@"" },
        {RegexEnum.VknNo,@"" },
        {RegexEnum.IsPolicy,  @"" },
        {RegexEnum.IsOffer, @"(?<=Süresi Vadesi\n340148 )(.*)(?= 1 )" },
        {RegexEnum.QueryType, @"" },
        {RegexEnum.AcenteNo,@"" },
        {RegexEnum.ZeyilNo,@"" }
    };
    /*
     void andolukaskoteklif()
    {

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

            var read = PdfExtractText("pdf/andolukaskoteklif.pdf"); // pdfin konumu

            var text = read[0].ToString();
            //isim ,soyisim ,adres, fiyat, police,tc (text bak  alt satıra point koy)
            //(?<=\d{10}/\d{1} \d{2}/\d{2}/\d{4} - )(.*)(?= \d{10} /\d{1}\d{8} \d{9})bitiş tarihi
            //PolicyEnforcement not başlangıç bitiş
            Dictionary<RegexEnum, string> cascoRegex = new Dictionary<RegexEnum, string>() {
        {RegexEnum.NationalNumber,@"" },
        {RegexEnum.PolicyNo,@"" },
        //{RegexEnum.RenewNo,@"" },
        {RegexEnum.StartDate,@"" },
        {RegexEnum.EndDate, @""},
        {RegexEnum.Name,@"" },
        {RegexEnum.Firm,@"" },
        {RegexEnum.Brand,@"" },
        {RegexEnum.Address, @"--"},
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
        {RegexEnum.StartDate,@"(?<=Başlangıç-Bitiş Tarihi ve Saati Süresi Vadesi\n340148 0468997559 1 )(.*)(?=-15:05 - 10)" },
        {RegexEnum.EndDate, @"(?<=Bitiş Tarihi ve Saati Süresi Vadesi\n340148 0468997559 1 31/01/2023-15:05 - )(.*)(?= 31)"},
        {RegexEnum.Name,@"" },
        {RegexEnum.Firm,@"" },
        {RegexEnum.Address,@"" },
        {RegexEnum.Policypremium,@"" },
        {RegexEnum.Brand,@"(?<= Marka :)(.*)(?=)" },
        {RegexEnum.VehicleModel,@"(?<= Model : )(.*)(?=)" },
        {RegexEnum.ModelYear,@" Model Yılı : " },
        {RegexEnum.UsingType, @""},
        {RegexEnum.VehicleType,@"" },
        {RegexEnum.BrandCode,@" "},
        {RegexEnum.FrameNumber,@"(?<=Şasi No : )(.*)(?= M)" },
        {RegexEnum.EngineNo,@"" },
        {RegexEnum.NetPremium,@"(?<=Vergi Öncesi Prim )(.*)(?=)" },
        {RegexEnum.GrossPremium,@"(?<=Ödenecek Tutar )(.*)(?=)" },
        {RegexEnum.RegisterNo,@"" },
        {RegexEnum.PlateNumber,"\r\n(?<=Plaka No : )(.*)(?= Mar)" },
        {RegexEnum.Branch,@"" },
        {RegexEnum.VknNo,@"" },
        {RegexEnum.IsPolicy,  @"" },
        {RegexEnum.IsOffer, @"(?<=Süresi Vadesi\n340148 )(.*)(?= 1 )" },
        {RegexEnum.QueryType, @"" },
        {RegexEnum.AcenteNo,@"" },
        {RegexEnum.ZeyilNo,@"" }
    };
        }
        void anadoludask()
    {
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

            var read = PdfExtractText("pdf/anadoludask.pdf"); // pdfin konumu

            var text = read[0].ToString();
            //isim ,soyisim ,adres, fiyat, police,tc (text bak  alt satıra point koy)
            //(?<=\d{10}/\d{1} \d{2}/\d{2}/\d{4} - )(.*)(?= \d{10} /\d{1}\d{8} \d{9})bitiş tarihi
            //PolicyEnforcement not başlangıç bitiş
            Dictionary<RegexEnum, string> cascoRegex = new Dictionary<RegexEnum, string>() {
        {RegexEnum.NationalNumber,@"" },
        {RegexEnum.PolicyNo,@"" },
        //{RegexEnum.RenewNo,@"" },
        {RegexEnum.StartDate,@"" },
        {RegexEnum.EndDate, @""},
        {RegexEnum.Name,@"" },
        {RegexEnum.Firm,@"" },
        {RegexEnum.Brand,@"" },
        {RegexEnum.Address, @"--"},
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
        {RegexEnum.NationalNumber,@"(?<=T.C Kimlik Numarası : )(.*)(?=)" },
        {RegexEnum.PolicyNo,@"(?<=Poliçe No Poliçe Vadesi DASK Seri No DASK Poliçe No Adres Kodu\n)(.*)(?=1 )" },
        //{RegexEnum.RenewNo,@"" },
        {RegexEnum.StartDate,@"(?<=\d{10}/\d{1})(.*)(?=\d{2}/\d{2}/\d{4})" },
        {RegexEnum.EndDate, @"(?<=\d{10}/\d{1} \d{2}/\d{2}/\d{4} - )(.*)(?= \d* \d*)"},
        {RegexEnum.Name,@"(?<=Unvanı : )(.*)(?=)" },
        {RegexEnum.Firm,@"" },
        {RegexEnum.Address,@"(?<=İSTANBUL)(.*)(?=)" },
        {RegexEnum.Policypremium,@"" },
        {RegexEnum.Brand,@"" },
        {RegexEnum.VehicleModel,@"" },
        {RegexEnum.ModelYear,@"" },
        {RegexEnum.UsingType, @""},
        {RegexEnum.VehicleType,@"" },
        {RegexEnum.BrandCode,@" "},
        {RegexEnum.FrameNumber,@"" },
        {RegexEnum.EngineNo,@"" },
        {RegexEnum.NetPremium,@"" },
        {RegexEnum.GrossPremium,@"(?<=Prim )(.*)(?=)" },
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
        }
    
  void AVEONKASKOTEKLİF()
  {

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

          var read = PdfExtractText("pdf/AVEONKASKOTEKLİF.pdf"); // pdfin konumu

          var text = read[0].ToString();
          //isim ,soyisim ,adres, fiyat, police,tc (text bak  alt satıra point koy)
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
      {RegexEnum.NationalNumber,@"(?<=T.C. Kimlik No:)(.*)(?=)" },
      {RegexEnum.PolicyNo,@"(?<=Yenileme No: )(.*)(?=)" },
      //{RegexEnum.RenewNo,@"" },
      {RegexEnum.StartDate,@"(?<=Başlama Tarihi:)(.*)(?= Bitiş T)" },
      {RegexEnum.EndDate, @"(?<=Bitiş Tarihi: )(.*)(?=)"},
      {RegexEnum.Name,@"(?<= Adı Soyadı )(.*)(?=)" },
      {RegexEnum.Firm,@"" },
      {RegexEnum.Address,@"(?<=Adresi :)(.*)(?=)" },
      {RegexEnum.Policypremium,@"" },
      {RegexEnum.Brand,@"(?<=Marka          :)(.*)(?= Tip)" },
      {RegexEnum.VehicleModel,@"(?<= Tip         :)(.*)(?=)" },
      {RegexEnum.ModelYear,@"(?<=Model Yılı     :)(.*)(?= Kişi )" },
      {RegexEnum.UsingType, @""},
      {RegexEnum.VehicleType,@"" },
      {RegexEnum.BrandCode,@" "},
      {RegexEnum.FrameNumber,@"(?<=Şasi No     :)(.*)(?=)" },
      {RegexEnum.EngineNo,@"" },
      {RegexEnum.NetPremium,@" (?<=Net Prim )(.*)(?= P)" },
      {RegexEnum.GrossPremium,@"(?<=Brüt Prim )(.*)(?= 2)" },
      {RegexEnum.RegisterNo,@"" },
      {RegexEnum.PlateNumber,"(?<= Plaka )(.*)(?=)" },
      {RegexEnum.Branch,@"" },
      {RegexEnum.VknNo,@"" },
      {RegexEnum.IsPolicy,  @"" },
      {RegexEnum.IsOffer, @"" },
      {RegexEnum.QueryType, @"" },
      {RegexEnum.AcenteNo,@"" },
      {RegexEnum.ZeyilNo,@"" }
  };
      }





   void korukaskoteklif()
     {
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

  var read = PdfExtractText("pdf/KORUKASKOTEKLİF.PDF"); // pdfin konumu

  var text = read[0].ToString();
  //isim ,soyisim ,adres, fiyat, police,tc (text bak  alt satıra point koy)
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
      {RegexEnum.NationalNumber,@"(?<= TC Kimlik Numarası )(.*)(?=)" },
      {RegexEnum.PolicyNo,@"(?<= Poliçe No )(.*)(?=)" },
      //{RegexEnum.RenewNo,@"" },
      {RegexEnum.StartDate,@"" },
      {RegexEnum.EndDate, @"--"},
      {RegexEnum.Name,@"(?<= Adı Soyadı )(.*)(?=)" },
      {RegexEnum.Firm,@"" },
      {RegexEnum.Address,@"" },
      {RegexEnum.Policypremium,@"" },
      {RegexEnum.Brand,@"(?<=Marka)(.*)(?= Önceki )" },
      {RegexEnum.VehicleModel,@"(?<=Tip)(.*)(?=)" },
      {RegexEnum.ModelYear,@"(?<=Model Yılı )(.*)(?= Önceki)" },
      {RegexEnum.UsingType, @""},
      {RegexEnum.VehicleType,@"" },
      {RegexEnum.BrandCode,@" "},
      {RegexEnum.FrameNumber,@"(?<=Şasi Numarası )(.*)(?= Servis )" },
      {RegexEnum.EngineNo,@"" },
      {RegexEnum.NetPremium,@" Rayiç Bedel Net Prim " },
      {RegexEnum.GrossPremium,@"(?<=Rayiç Bedel Brüt Prim )(.*)(?=)" },
      {RegexEnum.RegisterNo,@"" },
      {RegexEnum.PlateNumber,"(?<=Plaka)(.*)(?= Ön)" },
      {RegexEnum.Branch,@"" },
      {RegexEnum.VknNo,@"" },
      {RegexEnum.IsPolicy,  @"" },
      {RegexEnum.IsOffer, @"" },
      {RegexEnum.QueryType, @"" },
      {RegexEnum.AcenteNo,@"" },
      {RegexEnum.ZeyilNo,@"" }
  };
     }

     void anadoludaskprim()
     {
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

  var read = PdfExtractText("pdf/ANADOLUDASKPRİMLİZEYİL.pdf"); // pdfin konumu

  var text = read[0].ToString();
  //isim ,soyisim ,adres, fiyat, police,tc (text bak  alt satıra point koy)
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
      {RegexEnum.NationalNumber,@"(?<=Kimlik Numarası : )(.*)(?=)" },
      {RegexEnum.PolicyNo,@"--" },
      //{RegexEnum.RenewNo,@"" },
      {RegexEnum.StartDate,@"" },
      {RegexEnum.EndDate, @"--"},
      {RegexEnum.Name,@"" },
      {RegexEnum.Firm,@"(?<=Unvanı :)(.*)(?=)" },
      {RegexEnum.Address,@"(?<= ATA FİLTRE DIŞ TİCARET ANONİMŞİRKETİ\n)(.*)(?=)" },
      {RegexEnum.Policypremium,@"(?<=Prim )(.*)(?=)" },
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
   *  }
   *  
   * 

  void allianzdaskprimsizeyil()
  {
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

  var read = PdfExtractText("pdf/ALLİANZDASKPRİMSİZZEYİL.pdf"); // pdfin konumu

  var text = read[0].ToString();
  //isim ,soyisim ,adres, fiyat, police,tc (text bak  alt satıra point koy)
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
      {RegexEnum.NationalNumber,@"(?<=TC Kimlik No : )(.*)(?=)" },
      {RegexEnum.PolicyNo,@"(?<=DASK Poliçe No : )(.*)(?=)" },
      //{RegexEnum.RenewNo,@"" },
      {RegexEnum.StartDate,@"(?<=Başlangıç-Bitiş :)(.*)(?=-31)" },
      {RegexEnum.EndDate, @"(?<=2022-)(.*)(?=)"},
      {RegexEnum.Name,@"(?<=Adı Soyadı : )(.*)(?= Sıfatı : )" },
      {RegexEnum.Firm,@"(?<=Acente Adı : )(.*)(?=)" },
      {RegexEnum.Address,@"(?<=Adres : )(.*)(?=)" },
      {RegexEnum.Policypremium,@"(?<=Poliçe Primi : )(.*)(?=)" },
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
  }
  */
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

    foreach (var zeyil in zeyilPatterns)
    {
        Match match = Regex.Match(text, zeyil);
        if (match.Success)
        {
            queryType = QueryTypeEnum.ZEYIL;
            IsZeyil = true;
        }
    }

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
            foreach (var zeyilRegexItem in zeyilRegex)
            {
                patternList.Add(new RegexModel()
                {
                    PatternName = zeyilRegexItem.Key.ToString(),
                    PatternValue = zeyilRegexItem.Value,
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
        for (int i = 1; i <= reader.NumberOfPages; i++)
        {
            page += PdfTextExtractor.GetTextFromPage(reader, i, Strategy);
        }
        pdfLine.Add(page);

        return pdfLine.ToList();
    }
}