using iTextSharp.text.pdf.parser;
using iTextSharp.text.pdf;
using System.Text.RegularExpressions;
using regex_2;

using (PdfReader reader = new PdfReader("pdf/anadolu.pdf"))
{
    string page = "";
    List<Modell> regex = new List<Modell>();
    ITextExtractionStrategy Strategy = new LocationTextExtractionStrategy();
    for (int i = 1; i <= 9; i++)
    {
        page += PdfTextExtractor.GetTextFromPage(reader, i, Strategy);
    }
    regex.Add(new Modell()
    {
        Plaka = @"(?<=Plaka No : )(.*)(?= Ma)",
        Tc = @"(?<=Müşteri Numarası :)(.*)(?=)",
        Sasi = @"(?<=Şasi No : )(.*)(?=Mod)",
        BaslamaTarihi = "",
        BitisTarihi = "(?<=\\d{2}/\\d{2}/\\d{4}-\\d{2}:\\d{2} - )(.*)(?= \\d{2}/\\d{2}/\\d{4}-\\d{2}:\\d{2} \\d{3} \\w{3} \\d{2}/\\d{2}/\\d{4} - \\d{2}/\\d{2}/\\d{4})",
        Police = "",
        Marka = "(?<=Marka : )(.*)(?=)",
        ModelAd = "(?<= Model :)(.*)(?=)",
        ModelYili = "(?<=Model Yılı : )(.*)(?=)",
        ToplamNetPrim = "(?<=Vergi Öncesi Prim )(.*)(?=)",
        BrutPrim = "(?<=Ödenecek Tutar )(.*)(?=)",
    });

    foreach (var item in regex)
    {
        Match tc = Regex.Match(page, item.Tc);
        Match sasi = Regex.Match(page, item.Sasi);
        Match plaka = Regex.Match(page, item.Plaka);
        Match baslamatarihi = Regex.Match(page, item.BaslamaTarihi);
        Match bitistarihi = Regex.Match(page, item.BitisTarihi);
        Match police = Regex.Match(page, item.Police);
        Match marka = Regex.Match(page, item.Marka);
        Match modelad = Regex.Match(page, item.ModelAd);
        Match modelyili = Regex.Match(page, item.ModelYili);
        Match brtprim = Regex.Match(page, item.BrutPrim);
        Match netprim = Regex.Match(page, item.ToplamNetPrim);

        Console.WriteLine($" Musteri No : {tc.Value}, Plaka : {plaka.Value}, Sasi : {sasi.Value},  Baslamatarihi : {baslamatarihi.Value}, Bitistarihi : {bitistarihi.Value}, Police:{police.Value}{tc.Value}, Marka : {marka.Value},ModelAd : {modelad.Value}, ModelYılı : {modelyili.Value},  Netprim : {netprim.Value}, ÖdenecekTutar : {brtprim.Value}");
    }

    //tc kimlik , varsa doğum tarihi, plaka, belge seri no (ruhsat no), başlangıç ve bitiş, poliçe veya teklif no, araç marka, 
    // model, model yılı, kullanım tarzı, net prim, brüt 
}



/*
void zurich()
{
    using (PdfReader reader = new PdfReader("pdf/zurich.pdf"))
    {
        string page = "";
        List<Model> regex = new List<Model>();
        ITextExtractionStrategy Strategy = new LocationTextExtractionStrategy();
        for (int i = 1; i <= 3; i++)
        {
            page += PdfTextExtractor.GetTextFromPage(reader, i, Strategy);
        }

        regex.Add(new Model()
        {
            Plaka = @"(?<=PLAKA\n:)(.*)(?=)",
            Tc = @"(?<=VERGİ NO )(.*)(?=)",
            Sasi = @"(?<=ŞASİ NO :)(.*)(?= TR)",
            BaslamaTarihi = "",
            BitisTarihi = "",
            Police = "",
            Marka = "(?<=TİPİ : )(.*)(?=500)",
            ModelAd = "(?<=TİPİ : )(.*)(?=)",
            ModelYili = "(?<=MODEL\n)(.*)(?=ÖNC)",
            ToplamNetPrim = "",
            BrutPrim = "",
        });

        foreach (var item in regex)
        {
            Match tc = Regex.Match(page, item.Tc);
            Match sasi = Regex.Match(page, item.Sasi);
            Match plaka = Regex.Match(page, item.Plaka);
            Match baslamatarihi = Regex.Match(page, item.BaslamaTarihi);
            Match bitistarihi = Regex.Match(page, item.BitisTarihi);
            Match police = Regex.Match(page, item.Police);
            Match marka = Regex.Match(page, item.Marka);
            Match modelad = Regex.Match(page, item.ModelAd);
            Match modelyili = Regex.Match(page, item.ModelYili);
            Match brtprim = Regex.Match(page, item.BrutPrim);
            Match netprim = Regex.Match(page, item.ToplamNetPrim);

            Console.WriteLine($" Tc : {tc.Value}, Plaka : {plaka.Value}, Sasi : {sasi.Value},  Baslamatarihi : {baslamatarihi.Value}, Bitistarihi : {bitistarihi.Value}, Police:{police.Value}{tc.Value}, Marka : {marka.Value},ModelAd : {modelad.Value}, ModelYılı : {modelyili.Value},  Netprim : {netprim.Value}, ÖdenecekTutar : {brtprim.Value}");
        }

        //tc kimlik , varsa doğum tarihi, plaka, belge seri no (ruhsat no), başlangıç ve bitiş, poliçe veya teklif no, araç marka, 
        // model, model yılı, kullanım tarzı, net prim, brüt 
    }
}

void unico()
{
    using (PdfReader reader = new PdfReader("pdf/unico.pdf"))
    {
        string page = "";
        List<Model> regex = new List<Model>();
        ITextExtractionStrategy Strategy = new LocationTextExtractionStrategy();
        for (int i = 1; i <= 2; i++)
        {
            page += PdfTextExtractor.GetTextFromPage(reader, i, Strategy);
        }

        regex.Add(new Model()
        {

            Plaka = @"(?<=TESCİL TARİHİ\n)(.*)(?=)",
            Tc = @"(?<=VERGİ DR. NO\n:\n)(.*)(?=)",
            Sasi = @"(?<=MOTOR NO\n)(.*)(?=)",
            BaslamaTarihi = "(?<=BAŞLAMA TARİHİ )(.*)(?= SÜRE )",
            BitisTarihi = "(?<=BİTİŞ TARİHİ )(.*)(?= ACE)",
            Police = "(?<=YENİLEME NO )(.*)(?= 0 )",
            Marka = "(?<=MODEL Ö.ŞİRKET\n)(.*)(?= : )",
            ModelAd = "(?<=TİP :)(.*)(?=Ö.P)",
            ModelYili = "(?<=KULLANIM TARZI HASARSIZLIK İND\n)(.*)(?=: )",
            ToplamNetPrim = "(?<=TOPLAM NET PRİM )(.*)(?=)",
            BrutPrim = "(?<=BRÜT PRİM )(.*)(?=)",
        });

        foreach (var item in regex)
        {
            Match tc = Regex.Match(page, item.Tc);
            Match sasi = Regex.Match(page, item.Sasi);
            Match plaka = Regex.Match(page, item.Plaka);
            Match baslamatarihi = Regex.Match(page, item.BaslamaTarihi);
            Match bitistarihi = Regex.Match(page, item.BitisTarihi);
            Match police = Regex.Match(page, item.Police);
            Match marka = Regex.Match(page, item.Marka);
            Match modelad = Regex.Match(page, item.ModelAd);
            Match modelyili = Regex.Match(page, item.ModelYili);
            Match brtprim = Regex.Match(page, item.BrutPrim);
            Match netprim = Regex.Match(page, item.ToplamNetPrim);

            Console.WriteLine($" Tc : {tc.Value}, Plaka : {plaka.Value}, Sasi : {sasi.Value},  Baslamatarihi : {baslamatarihi.Value}, Bitistarihi : {bitistarihi.Value}, Police:{police.Value}{tc.Value}, Marka : {marka.Value},ModelAd : {modelad.Value}, ModelYılı : {modelyili.Value},  Netprim : {netprim.Value}, ÖdenecekTutar : {brtprim.Value}");
        }

        //tc kimlik , varsa doğum tarihi, plaka, belge seri no (ruhsat no), başlangıç ve bitiş, poliçe veya teklif no, araç marka, 
        // model, model yılı, kullanım tarzı, net prim, brüt 
    }
}

void turkiye()
{
    using (PdfReader reader = new PdfReader("pdf/turkiye.pdf"))
    {
        string page = "";
        List<Model> regex = new List<Model>();
        ITextExtractionStrategy Strategy = new LocationTextExtractionStrategy();
        for (int i = 1; i <= 2; i++)
        {
            page += PdfTextExtractor.GetTextFromPage(reader, i, Strategy);
        }

        regex.Add(new Model()
        {
            Plaka = @"(?<=Plaka )(.*)(?=Ara)",
            Tc = @"(?<= Vergi No )(.*)(?=)",
            Sasi = @"(?<=Araç Tipi Sasi numarasi )(.*)(?=)",
            BaslamaTarihi = "",
            BitisTarihi = "",
            Police = "(?<=Poliçe No)(.*)(?= 2)",
            Marka = "(?<=Araç Markası )(.*)(?=Mo)",
            ModelAd = "(?<=87\n)(.*)(?=)",
            ModelYili = "(?<=Araç Modeli)(.*)(?=)",
            ToplamNetPrim = "(?<=Net Prim )(.*)(?=)",
            BrutPrim = "(?<=Yalnız : AltıBinDörtYüzDört Türk Lirası OnDört Kuruş Toplam )(.*)(?=)",
        });

        foreach (var item in regex)
        {
            Match tc = Regex.Match(page, item.Tc);
            Match sasi = Regex.Match(page, item.Sasi);
            Match plaka = Regex.Match(page, item.Plaka);
            Match baslamatarihi = Regex.Match(page, item.BaslamaTarihi);
            Match bitistarihi = Regex.Match(page, item.BitisTarihi);
            Match police = Regex.Match(page, item.Police);
            Match marka = Regex.Match(page, item.Marka);
            Match modelad = Regex.Match(page, item.ModelAd);
            Match modelyili = Regex.Match(page, item.ModelYili);
            Match brtprim = Regex.Match(page, item.BrutPrim);
            Match netprim = Regex.Match(page, item.ToplamNetPrim);

            Console.WriteLine($" Tc : {tc.Value}, Plaka : {plaka.Value}, Sasi : {sasi.Value},  Baslamatarihi : {baslamatarihi.Value}, Bitistarihi : {bitistarihi.Value}, Police:{police.Value}{tc.Value}, Marka : {marka.Value},ModelAd : {modelad.Value}, ModelYılı : {modelyili.Value},  Netprim : {netprim.Value}, ÖdenecekTutar : {brtprim.Value}");
        }

        //tc kimlik , varsa doğum tarihi, plaka, belge seri no (ruhsat no), başlangıç ve bitiş, poliçe veya teklif no, araç marka, 
        // model, model yılı, kullanım tarzı, net prim, brüt 
    }
}

void tmt()
{
    using (PdfReader reader = new PdfReader("pdf/tmt.pdf"))
    {
        string page = "";
        List<Model> regex = new List<Model>();
        ITextExtractionStrategy Strategy = new LocationTextExtractionStrategy();
        for (int i = 1; i <= 2; i++)
        {
            page += PdfTextExtractor.GetTextFromPage(reader, i, Strategy);
        }

        regex.Add(new Model()
        {
            Plaka = @"(?<=PLAKA :)(.*)(?=)",
            Tc = @"(?<=VERGİ DR. NO)(.*)(?=)",
            Sasi = @"(?<=ŞASİ NO : )(.*)(?=)",
            BaslamaTarihi = "(?<=BAŞLAMA TARİHİ : )(.*)(?= S)",
            BitisTarihi = "(?<=BİTİŞ TARİHİ :)(.*)(?=ACE)",
            Police = "(?<=YENİLEME NO )(.*)(?= 0)",
            Marka = "(?<=MARKA : )(.*)(?=)",
            ModelAd = "(?<=MODEL : )(.*)(?=)",
            ModelYili = "(?<=MODEL YILI : )(.*)(?=)",
            ToplamNetPrim = "(?<=TOPLAM NET PRİM )(.*)(?=)",
            BrutPrim = "(?<=BRÜT PRİM )(.*)(?=)",
        });

        foreach (var item in regex)
        {
            Match tc = Regex.Match(page, item.Tc);
            Match sasi = Regex.Match(page, item.Sasi);
            Match plaka = Regex.Match(page, item.Plaka);
            Match baslamatarihi = Regex.Match(page, item.BaslamaTarihi);
            Match bitistarihi = Regex.Match(page, item.BitisTarihi);
            Match police = Regex.Match(page, item.Police);
            Match marka = Regex.Match(page, item.Marka);
            Match modelad = Regex.Match(page, item.ModelAd);
            Match modelyili = Regex.Match(page, item.ModelYili);
            Match brtprim = Regex.Match(page, item.BrutPrim);
            Match netprim = Regex.Match(page, item.ToplamNetPrim);

            Console.WriteLine($" Tc : {tc.Value}, Plaka : {plaka.Value}, Sasi : {sasi.Value},  Baslamatarihi : {baslamatarihi.Value}, Bitistarihi : {bitistarihi.Value}, Police:{police.Value}{tc.Value}, Marka : {marka.Value},ModelAd : {modelad.Value}, ModelYılı : {modelyili.Value},  Netprim : {netprim.Value}, ÖdenecekTutar : {brtprim.Value}");
        }

        //tc kimlik , varsa doğum tarihi, plaka, belge seri no (ruhsat no), başlangıç ve bitiş, poliçe veya teklif no, araç marka, 
        // model, model yılı, kullanım tarzı, net prim, brüt 
    }
}

void sompo()
{
    using (PdfReader reader = new PdfReader("pdf/sompo.pdf"))
    {
        string page = "";
        List<Model> regex = new List<Model>();
        ITextExtractionStrategy Strategy = new LocationTextExtractionStrategy();
        for (int i = 1; i <= 2; i++)
        {
            page += PdfTextExtractor.GetTextFromPage(reader, i, Strategy);
        }

        regex.Add(new Model()
        {
            Plaka = @"(?<=Plaka No : )(.*)(?=)",
            Tc = @"(?<=TC Kimlik Numarası : )(.*)(?=)",
            Sasi = @"(?<=Şasi No : )(.*)(?= Firma)",
            BaslamaTarihi = "",
            BitisTarihi = "",
            Police = "",
            Marka = "(?<=Marka :)(.*)(?=:)",
            ModelAd = "(?<=Tip : )(.*)(?=OKU)",
            ModelYili = "(?<=Model : )(.*)(?= Tr)",
            ToplamNetPrim = "(?<=NET PRİM )(.*)(?=)",
            BrutPrim = "(?<=BRÜT PRİM )(.*)(?=)",
        });

        foreach (var item in regex)
        {
            Match tc = Regex.Match(page, item.Tc);
            Match sasi = Regex.Match(page, item.Sasi);
            Match plaka = Regex.Match(page, item.Plaka);
            Match baslamatarihi = Regex.Match(page, item.BaslamaTarihi);
            Match bitistarihi = Regex.Match(page, item.BitisTarihi);
            Match police = Regex.Match(page, item.Police);
            Match marka = Regex.Match(page, item.Marka);
            Match modelad = Regex.Match(page, item.ModelAd);
            Match modelyili = Regex.Match(page, item.ModelYili);
            Match brtprim = Regex.Match(page, item.BrutPrim);
            Match netprim = Regex.Match(page, item.ToplamNetPrim);

            Console.WriteLine($" Tc : {tc.Value}, Plaka : {plaka.Value}, Sasi : {sasi.Value},  Baslamatarihi : {baslamatarihi.Value}, Bitistarihi : {bitistarihi.Value}, Police:{police.Value}{tc.Value}, Marka : {marka.Value},ModelAd : {modelad.Value}, ModelYılı : {modelyili.Value},  Netprim : {netprim.Value}, ÖdenecekTutar : {brtprim.Value}");
        }

        //tc kimlik , varsa doğum tarihi, plaka, belge seri no (ruhsat no), başlangıç ve bitiş, poliçe veya teklif no, araç marka, 
        // model, model yılı, kullanım tarzı, net prim, brüt 
    }
}

void ray()
{
    using (PdfReader reader = new PdfReader("pdf/ray.pdf"))
    {
        string page = "";
        List<Model> regex = new List<Model>();
        ITextExtractionStrategy Strategy = new LocationTextExtractionStrategy();
        for (int i = 1; i <= 2; i++)
        {
            page += PdfTextExtractor.GetTextFromPage(reader, i, Strategy);
        }

        regex.Add(new Model()
        {
            Plaka = @"(?<=PLAKA NO :)(.*)(?= K)",
            Tc = @"(?<=T.C. KİMLİK NO : )(.*)(?=)",
            Sasi = @"(?<=ŞASİ NO : )(.*)(?=)",
            BaslamaTarihi = "(?<=BAŞLANGIÇ TARİHİ : )(.*)(?= B)",
            BitisTarihi = "(?<=BİTİŞ TARİHİ : )(.*)(?=)",
            Police = "(?<=YENİLEME NO :)(.*)(?= )",
            Marka = "(?<=TİPİ :)(.*)(?=SP)",
            ModelAd = "(?<=TİPİ :)(.*)(?=MO)",
            ModelYili = "(?<=MODEL : )(.*)(?= TAR)",
            ToplamNetPrim = "(?<=TOPLAM NET PRİM :)(.*)(?=)",
            BrutPrim = "(?<=BRÜT PRİM : )(.*)(?=)",
        });

        foreach (var item in regex)
        {
            Match tc = Regex.Match(page, item.Tc);
            Match sasi = Regex.Match(page, item.Sasi);
            Match plaka = Regex.Match(page, item.Plaka);
            Match baslamatarihi = Regex.Match(page, item.BaslamaTarihi);
            Match bitistarihi = Regex.Match(page, item.BitisTarihi);
            Match police = Regex.Match(page, item.Police);
            Match marka = Regex.Match(page, item.Marka);
            Match modelad = Regex.Match(page, item.ModelAd);
            Match modelyili = Regex.Match(page, item.ModelYili);
            Match brtprim = Regex.Match(page, item.BrutPrim);
            Match netprim = Regex.Match(page, item.ToplamNetPrim);

            Console.WriteLine($" Tc : {tc.Value}, Plaka : {plaka.Value}, Sasi : {sasi.Value},  Baslamatarihi : {baslamatarihi.Value}, Bitistarihi : {bitistarihi.Value}, Police:{police.Value}{tc.Value}, Marka : {marka.Value},ModelAd : {modelad.Value}, ModelYılı : {modelyili.Value},  Netprim : {netprim.Value}, ÖdenecekTutar : {brtprim.Value}");
        }

        //tc kimlik , varsa doğum tarihi, plaka, belge seri no (ruhsat no), başlangıç ve bitiş, poliçe veya teklif no, araç marka, 
        // model, model yılı, kullanım tarzı, net prim, brüt 
    }
}

void orient()
{

    using (PdfReader reader = new PdfReader("pdf/orient.pdf"))
    {
        string page = "";
        List<Model> regex = new List<Model>();
        ITextExtractionStrategy Strategy = new LocationTextExtractionStrategy();
        for (int i = 1; i <= 2; i++)
        {
            page += PdfTextExtractor.GetTextFromPage(reader, i, Strategy);
        }

        regex.Add(new Model()
        {
            Plaka = @"(?<=PLAKA : )(.*)(?=)",
            Tc = @"(?<=VERGİ DR. NO)(.*)(?=)",
            Sasi = @"(?<=MODEL YILI :)(.*)(?=ŞA)",
            BaslamaTarihi = "(?<=BAŞLAMA TARİHİ )(.*)(?= SÜRE 3)",
            BitisTarihi = "(?<=BİTİŞ TARİHİ )(.*)(?= ACE)",
            Police = "(?<=YENİLEME NO )(.*)(?= ÜRÜN)",
            Marka = "(?<=MARKA : )(.*)(?= Ö) ",
            ModelAd = "(?<=MODEL :)(.*)(?=KULL)",
            ModelYili = "(?<=MODEL YILI :)(.*)(?= ŞA)",
            ToplamNetPrim = "(?<=TOPLAM NET PRİM)(.*)(?=)",
            BrutPrim = "(?<=BRÜT PRİM )(.*)(?=) ",
        });

        foreach (var item in regex)
        {
            Match tc = Regex.Match(page, item.Tc);
            Match sasi = Regex.Match(page, item.Sasi);
            Match plaka = Regex.Match(page, item.Plaka);
            Match baslamatarihi = Regex.Match(page, item.BaslamaTarihi);
            Match bitistarihi = Regex.Match(page, item.BitisTarihi);
            Match police = Regex.Match(page, item.Police);
            Match marka = Regex.Match(page, item.Marka);
            Match modelad = Regex.Match(page, item.ModelAd);
            Match modelyili = Regex.Match(page, item.ModelYili);
            Match brtprim = Regex.Match(page, item.BrutPrim);
            Match netprim = Regex.Match(page, item.ToplamNetPrim);

            Console.WriteLine($" Tc : {tc.Value}, Plaka : {plaka.Value}, Sasi : {sasi.Value},  Baslamatarihi : {baslamatarihi.Value}, Bitistarihi : {bitistarihi.Value}, Police:{police.Value}{tc.Value}, Marka : {marka.Value},ModelAd : {modelad.Value}, ModelYılı : {modelyili.Value},  Netprim : {netprim.Value}, ÖdenecekTutar : {brtprim.Value}");
        }

        //tc kimlik , varsa doğum tarihi, plaka, belge seri no (ruhsat no), başlangıç ve bitiş, poliçe veya teklif no, araç marka, 
        // model, model yılı, kullanım tarzı, net prim, brüt 
    }
}

void neova()
{
    using (PdfReader reader = new PdfReader("pdf/neova.pdf"))
    {
        string page = "";
        List<Model> regex = new List<Model>();
        ITextExtractionStrategy Strategy = new LocationTextExtractionStrategy();
        for (int i = 1; i <= 2; i++)
        {
            page += PdfTextExtractor.GetTextFromPage(reader, i, Strategy);
        }

        regex.Add(new Model()
        {
            Plaka = @"(?<=PLAKA : )(.*)(?=)",
            Tc = @"(?<=VERGİ DR. NO :)(.*)(?=)",
            Sasi = @"(?<=ŞASİ NO : )(.*)(?=)",
            BaslamaTarihi = "(?<=BAŞLAMA TARİHİ)(.*)(?= ACEN)",
            BitisTarihi = "(?<=BİTİŞ TARİHİ :)(.*)(?=)",
            Police = "(?<=POLİÇE NO : )(.*)(?= 0 M)",
            Marka = "(?<=MARKA :)(.*)(?=)",
            ModelAd = "(?<=TİP : )(.*)(?=)",
            ModelYili = "(?<=MODEL :)(.*)(?=)",
            ToplamNetPrim = "(?<=TOPLAM NET KATKI PRİMİ : )(.*)(?=)",
            BrutPrim = "(?<=BRÜT KATKI PRİMİ :)(.*)(?=)",
        });

        foreach (var item in regex)
        {
            Match tc = Regex.Match(page, item.Tc);
            Match sasi = Regex.Match(page, item.Sasi);
            Match plaka = Regex.Match(page, item.Plaka);
            Match baslamatarihi = Regex.Match(page, item.BaslamaTarihi);
            Match bitistarihi = Regex.Match(page, item.BitisTarihi);
            Match police = Regex.Match(page, item.Police);
            Match marka = Regex.Match(page, item.Marka);
            Match modelad = Regex.Match(page, item.ModelAd);
            Match modelyili = Regex.Match(page, item.ModelYili);
            Match brtprim = Regex.Match(page, item.BrutPrim);
            Match netprim = Regex.Match(page, item.ToplamNetPrim);

            Console.WriteLine($" Tc : {tc.Value}, Plaka : {plaka.Value}, Sasi : {sasi.Value},  Baslamatarihi : {baslamatarihi.Value}, Bitistarihi : {bitistarihi.Value}, Police:{police.Value}{tc.Value}, Marka : {marka.Value},ModelAd : {modelad.Value}, ModelYılı : {modelyili.Value},  Netprim : {netprim.Value}, ÖdenecekTutar : {brtprim.Value}");
        }

        //tc kimlik , varsa doğum tarihi, plaka, belge seri no (ruhsat no), başlangıç ve bitiş, poliçe veya teklif no, araç marka, 
        // model, model yılı, kullanım tarzı, net prim, brüt 
    }
}

void mapfre()
{
    using (PdfReader reader = new PdfReader("pdf/mapfre.pdf"))
    {
        string page = "";
        List<Model> regex = new List<Model>();
        ITextExtractionStrategy Strategy = new LocationTextExtractionStrategy();
        for (int i = 1; i <= 2; i++)
        {
            page += PdfTextExtractor.GetTextFromPage(reader, i, Strategy);
        }

        regex.Add(new Model()
        {
            Plaka = @"(?<=PLAKA )(.*)(?= Y)",
            Tc = @"(?<=Kimlik No )(.*)(?=)",
            Sasi = @"(?<=AS0 NO )(.*)(?= M)",
            BaslamaTarihi = "",
            BitisTarihi = "",
            Police = "",
            Marka = "(?<=MARKA )(.*)(?=)",
            ModelAd = "(?<=T0P )(.*)(?= 2022)",
            ModelYili = "(?<=.MODEL)(.*)(?=)",
            ToplamNetPrim = "(?<=Net Prim )(.*)(?=)",
            BrutPrim = "(?<=Ödenecek Tutar )(.*)(?=)",
        });

        foreach (var item in regex)
        {
            Match tc = Regex.Match(page, item.Tc);
            Match sasi = Regex.Match(page, item.Sasi);
            Match plaka = Regex.Match(page, item.Plaka);
            Match baslamatarihi = Regex.Match(page, item.BaslamaTarihi);
            Match bitistarihi = Regex.Match(page, item.BitisTarihi);
            Match police = Regex.Match(page, item.Police);
            Match marka = Regex.Match(page, item.Marka);
            Match modelad = Regex.Match(page, item.ModelAd);
            Match modelyili = Regex.Match(page, item.ModelYili);
            Match brtprim = Regex.Match(page, item.BrutPrim);
            Match netprim = Regex.Match(page, item.ToplamNetPrim);

            Console.WriteLine($" Tc : {tc.Value}, Plaka : {plaka.Value}, Sasi : {sasi.Value},  Baslamatarihi : {baslamatarihi.Value}, Bitistarihi : {bitistarihi.Value}, Police:{police.Value}{tc.Value}, Marka : {marka.Value},ModelAd : {modelad.Value}, ModelYılı : {modelyili.Value},  Netprim : {netprim.Value}, ÖdenecekTutar : {brtprim.Value}");
        }

        //tc kimlik , varsa doğum tarihi, plaka, belge seri no (ruhsat no), başlangıç ve bitiş, poliçe veya teklif no, araç marka, 
        // model, model yılı, kullanım tarzı, net prim, brüt 
    }
}

void koru()
{
    using (PdfReader reader = new PdfReader("pdf/koru.pdf"))
    {
        string page = "";
        List<Model> regex = new List<Model>();
        ITextExtractionStrategy Strategy = new LocationTextExtractionStrategy();
        for (int i = 1; i <= 2; i++)
        {
            page += PdfTextExtractor.GetTextFromPage(reader, i, Strategy);
        }

        regex.Add(new Model()
        {
            Plaka = @"(?<= Plaka 34 )(.*)(?= Önceki Y)",
            Tc = @"(?<=TC Kimlik Numarası)(.*)(?=)",
            Sasi = @"(?<=Şasi Numarası)(.*)(?= Servis )",
            BaslamaTarihi = "",
            BitisTarihi = "",
            Police = "(?<= Önceki Poliçe No )(.*)(?=)",
            Marka = "(?<=Marka )(.*)(?=Önceki Ş)",
            ModelAd = "(?<=Tip)(.*)(?= Önceki A)",
            ModelYili = "(?<=Model Yılı )(.*)(?= Önceki P)",
            ToplamNetPrim = "(?<=Net Prim )(.*)(?=)",
            BrutPrim = "(?<= Brüt Prim )(.*)(?=)",
        });

        foreach (var item in regex)
        {
            Match tc = Regex.Match(page, item.Tc);
            Match sasi = Regex.Match(page, item.Sasi);
            Match plaka = Regex.Match(page, item.Plaka);
            Match baslamatarihi = Regex.Match(page, item.BaslamaTarihi);
            Match bitistarihi = Regex.Match(page, item.BitisTarihi);
            Match police = Regex.Match(page, item.Police);
            Match marka = Regex.Match(page, item.Marka);
            Match modelad = Regex.Match(page, item.ModelAd);
            Match modelyili = Regex.Match(page, item.ModelYili);
            Match brtprim = Regex.Match(page, item.BrutPrim);
            Match netprim = Regex.Match(page, item.ToplamNetPrim);

            Console.WriteLine($" Tc : {tc.Value}, Plaka : {plaka.Value}, Sasi : {sasi.Value},  Baslamatarihi : {baslamatarihi.Value}, Bitistarihi : {bitistarihi.Value}, ÖncekiPolice:{police.Value}{tc.Value}, Marka : {marka.Value},ModelAd : {modelad.Value}, ModelYılı : {modelyili.Value}, Burutprim : {brtprim.Value}, Netprim : {netprim.Value}");
        }

        //tc kimlik , varsa doğum tarihi, plaka, belge seri no (ruhsat no), başlangıç ve bitiş, poliçe veya teklif no, araç marka, 
        // model, model yılı, kullanım tarzı, net prim, brüt 
    }

}

void hepiyi()
{
    using (PdfReader reader = new PdfReader("pdf/hepiyi.pdf"))
    {
        string page = "";
        List<Model> regex = new List<Model>();
        ITextExtractionStrategy Strategy = new LocationTextExtractionStrategy();
        for (int i = 1; i <= 2; i++)
        {
            page += PdfTextExtractor.GetTextFromPage(reader, i, Strategy);
        }

        regex.Add(new Model()
        {
            Plaka = @"(?<=Plaka :)(.*)(?=Marka :)",
            Tc = @"(?<=Kimlik Numarası :)(.*)(?=)",
            Sasi = @"(?<=Şasi No :)(.*)(?= Model Yılı)",
            BaslamaTarihi = "",
            BitisTarihi = "",
            Police = "",
            Marka = "(?<=Marka :)(.*)(?=)",
            ModelAd = "(?<=Tipi :)(.*)(?=)",
            ModelYili = "(?<=Model Yılı :)(.*)(?=)",
            ToplamNetPrim = "(?<= Net Prim : )(.*)(?= Ödeme P)",
            BrutPrim = "(?<= Brüt prim : )(.*)(?=)",
        });

        foreach (var item in regex)
        {
            Match tc = Regex.Match(page, item.Tc);
            Match sasi = Regex.Match(page, item.Sasi);
            Match plaka = Regex.Match(page, item.Plaka);
            Match baslamatarihi = Regex.Match(page, item.BaslamaTarihi);
            Match bitistarihi = Regex.Match(page, item.BitisTarihi);
            Match police = Regex.Match(page, item.Police);
            Match marka = Regex.Match(page, item.Marka);
            Match modelad = Regex.Match(page, item.ModelAd);
            Match modelyili = Regex.Match(page, item.ModelYili);
            Match brtprim = Regex.Match(page, item.BrutPrim);
            Match netprim = Regex.Match(page, item.ToplamNetPrim);

            Console.WriteLine($" Tc : {tc.Value}, Plaka : {plaka.Value}, Sasi : {sasi.Value},  Baslamatarihi : {baslamatarihi.Value}, Bitistarihi : {bitistarihi.Value}, Police:{police.Value}{tc.Value}, Marka : {marka.Value},Modelad : {modelad.Value}, ModelYılı : {modelyili.Value}, Burutprim : {brtprim.Value}, Netprim : {netprim.Value}");
        }

        //tc kimlik , varsa doğum tarihi, plaka, belge seri no (ruhsat no), başlangıç ve bitiş, poliçe veya teklif no, araç marka, 
        // model, model yılı, kullanım tarzı, net prim, brüt 
    }
}

void hdi()
{
    using (PdfReader reader = new PdfReader("pdf/hdi.pdf"))
    {
        string page = "";
        List<Model> regex = new List<Model>();
        ITextExtractionStrategy Strategy = new LocationTextExtractionStrategy();
        for (int i = 1; i <= 2; i++)
        {
            page += PdfTextExtractor.GetTextFromPage(reader, i, Strategy);
        }

        regex.Add(new Model()
        {
            Plaka = @"(?<=İkametgah İli : İSTANBUL)(.*)(?=)",
            Tc = @"(?<=T.C Kimlik No   :)(.*)(?=  Vergi Numarası  :)",
            Sasi = @"(?<=Şasi No         :)(.*)(?=                             Hasarsızlık   : )",
            BaslamaTarihi = "(?<=Başlangıç Tarihi :)(.*)(?=                                    Düzenleme Tarihi )",
            BitisTarihi = "(?<=Bitiş Tarihi     :)(.*)(?=                                               Saati : 15:54)",
            Police = "(?<=Poliçe No :)(.*)(?=                            Acente No : 7401)",
            Marka = "(?<=Markası         :)(.*)(?=)",
            ModelAd = "(?<=Tipi            :)(.*)(?=)",
            ModelYili = "(?<=Modeli          :)(.*)(?=)",
            ToplamNetPrim = "(?<=  Net Prim        :       )(.*)(?=)",
            BrutPrim = "(?<= Ödenecek Tutar  :       )(.*)(?=)",
        });

        foreach (var item in regex)
        {
            Match tc = Regex.Match(page, item.Tc);
            Match sasi = Regex.Match(page, item.Sasi);
            Match plaka = Regex.Match(page, item.Plaka);
            Match baslamatarihi = Regex.Match(page, item.BaslamaTarihi);
            Match bitistarihi = Regex.Match(page, item.BitisTarihi);
            Match police = Regex.Match(page, item.Police);
            Match marka = Regex.Match(page, item.Marka);
            Match modelad = Regex.Match(page, item.ModelAd);
            Match modelyili = Regex.Match(page, item.ModelYili);
            Match brtprim = Regex.Match(page, item.BrutPrim);
            Match netprim = Regex.Match(page, item.ToplamNetPrim);

            Console.WriteLine($" Tc : {tc.Value}, Plaka : {plaka.Value}, Sasi : {sasi.Value},  Baslamatarihi : {baslamatarihi.Value}, Bitistarihi : {bitistarihi.Value}, Police:{police.Value}{tc.Value}, Marka : {marka.Value},Modelad : {modelad.Value}, ModelYılı : {modelyili.Value}, Burutprim : {brtprim.Value}, Netprim : {netprim.Value}");
        }

        //tc kimlik , varsa doğum tarihi, plaka, belge seri no (ruhsat no), başlangıç ve bitiş, poliçe veya teklif no, araç marka, 
        // model, model yılı, kullanım tarzı, net prim, brüt prim

    }
}

void gulf()
{
    using (PdfReader reader = new PdfReader("pdf/gulf.pdf"))
    {
        string page = "";
        List<Model> regex = new List<Model>();
        ITextExtractionStrategy Strategy = new LocationTextExtractionStrategy();
        for (int i = 1; i <= 2; i++)
        {
            page += PdfTextExtractor.GetTextFromPage(reader, i, Strategy);
        }

        regex.Add(new Model()
        {
            Plaka = @"(?<=Araç Plakası : )(.*)(?=)",
            Tc = @"(?<=TC Kimlik No )(.*)(?=)",
            Sasi = @"(?<=Şasi No :)(.*)(?=)",
            BaslamaTarihi = "(?<=Başlangıç Tarihi)(.*)(?= Bitiş Tarihi)",
            BitisTarihi = "(?<= Bitiş Tarihi)(.*)(?=Sigorta Sü)",
            Police = "(?<=Poliçe No :)(.*)(?=)",
            Marka = "(?<=ARAÇ MARKASI )(.*)(?= İKAME )",
            ModelAd = "(?<=ARAÇ TİPİ :)(.*)(?=)",
            ModelYili = "(?<=Model Yılı :)(.*)(?=)",
            ToplamNetPrim = "(?<=TOPLAM NET PRİM)(.*)(?=)",
            BrutPrim = "(?<=GİDER VERGİSİ 857.55\n)(.*)(?=)",
        });

        foreach (var item in regex)
        {
            Match tc = Regex.Match(page, item.Tc);
            Match sasi = Regex.Match(page, item.Sasi);
            Match plaka = Regex.Match(page, item.Plaka);
            Match baslamatarihi = Regex.Match(page, item.BaslamaTarihi);
            Match bitistarihi = Regex.Match(page, item.BitisTarihi);
            Match police = Regex.Match(page, item.Police);
            Match marka = Regex.Match(page, item.Marka);
            Match modelad = Regex.Match(page, item.ModelAd);
            Match modelyili = Regex.Match(page, item.ModelYili);
            Match brtprim = Regex.Match(page, item.BrutPrim);
            Match netprim = Regex.Match(page, item.ToplamNetPrim);

            Console.WriteLine($" Tc : {tc.Value}, Plaka : {plaka.Value}, Sasi : {sasi.Value},  Baslamatarihi : {baslamatarihi.Value}, Bitistarihi : {bitistarihi.Value}, Police:{police.Value}{tc.Value}, Marka : {marka.Value},Modelad : {modelad.Value}, ModelYılı : {modelyili.Value}, Brutprim : {brtprim.Value}, Netprim : {netprim.Value}");
        }

        //tc kimlik , varsa doğum tarihi, plaka, belge seri no (ruhsat no), başlangıç ve bitiş, poliçe veya teklif no, araç marka, 
        // model, model yılı, kullanım tarzı, net prim, brüt prim

    }
}

void groupama()
{
    using (PdfReader reader = new PdfReader("pdf/groupama.pdf"))
    {
        string page = "";
        List<Model> regex = new List<Model>();
        ITextExtractionStrategy Strategy = new LocationTextExtractionStrategy();
        for (int i = 1; i <= 2; i++)
        {
            page += PdfTextExtractor.GetTextFromPage(reader, i, Strategy);
        }

        regex.Add(new Model()
        {
            Plaka = @"(?<=PLAKA :)(.*)(?= TRONIC)",
            Tc = @"(?<=TC Kimlik No )(.*)(?=)",
            Sasi = @"(?<=ŞASİ NO :)(.*)(?=SÜRÜCÜ)",
            BaslamaTarihi_BitisTarihi = "(?<=177778 79180152 0 )(.*)(?=)",

            Police = "(?<= Poliçe No Zeyil No Tanzim Tarihi Poliçe Başlangıç-Bitiş Tarihi\r\n00AJL42 177778 )(.*)(?=0 27/01/2023-14:23:3)",
            Marka = "(?<=ARAÇ MARKASI )(.*)(?= İKAME )",
            ModelAd = "(?<=ARAÇ TİPİ :)(.*)(?=)",
            ModelYili = "(?<=MODEL YILI :)(.*)(?=)",
            ToplamNetPrim = "(?<=TOPLAM NET PRİM)(.*)(?=)",
            BrutPrim = "(?<=GİDER VERGİSİ 857.55\n)(.*)(?=)",
        });

        foreach (var item in regex)
        {
            Match tc = Regex.Match(page, item.Tc);
            Match sasi = Regex.Match(page, item.Sasi);
            Match plaka = Regex.Match(page, item.Plaka);
            Match baslamatarihi_bitis = Regex.Match(page, item.BaslamaTarihi_BitisTarihi);
            // Match bitistarihi = Regex.Match(page, item.BitisTarihi);
            Match police = Regex.Match(page, item.Police);
            Match marka = Regex.Match(page, item.Marka);
            Match modelad = Regex.Match(page, item.ModelAd);
            Match modelyili = Regex.Match(page, item.ModelYili);
            Match brtprim = Regex.Match(page, item.BrutPrim);
            Match netprim = Regex.Match(page, item.ToplamNetPrim);

            Console.WriteLine($" Tc : {tc.Value}, Plaka : {plaka.Value}, Sasi : {sasi.Value}, Baslamatarihi/BitişTarihi : {baslamatarihi_bitis.Value}, Police:{police.Value}{tc.Value}, Marka : {marka.Value},Modelad : {modelad.Value}, ModelYılı : {modelyili.Value}, Brutprim : {brtprim.Value}, Netprim : {netprim.Value}");
        }

        //tc kimlik , varsa doğum tarihi, plaka, belge seri no (ruhsat no), başlangıç ve bitiş, poliçe veya teklif no, araç marka, 
        // model, model yılı, kullanım tarzı, net prim, brüt prim

    }
}

void gri()
{
    using (PdfReader reader = new PdfReader("pdf/gri.pdf"))
    {
        string page = "";
        List<Model> regex = new List<Model>();
        ITextExtractionStrategy Strategy = new LocationTextExtractionStrategy();
        for (int i = 1; i <= 2; i++)
        {
            page += PdfTextExtractor.GetTextFromPage(reader, i, Strategy);
        }

        regex.Add(new Model()
        {
            Plaka = @"(?<=Plaka       :)(.*)(?=)",
            Tc = @"(?<=T.C. Kimlik No:)(.*)(?=)",
            Sasi = @"(?<=Şasi No)(.*)(?=)",
            BaslamaTarihi = "(?<=Başlama Tarihi:)(.*)(?= ­  12:00 Bitiş Tarihi: 09.07.2023)",
            BitisTarihi = "(?<=Bitiş Tarihi:)(.*)(?= ­  12:00  Süre: 365)",
            Police = "(?<=Poliçe / Zeyil/ Yenileme No:)(.*)(?=Müşteri No: 1791406)",
            Marka = "(?<=Marka          :)(.*)(?= Tip         :ASTRA SEDAN 1.6 115 BUSINESS)",
            ModelAd = "(?<=Tip         :)(.*)(?= )",
            ModelYili = "(?<=Model Yılı     :)(.*)(?= Kişi Sayısı)",
            ToplamNetPrim = "(?<=Net Prim )(.*)(?=P 07.07.2022 1,155.01)",
            BrutPrim = "(?<=Brüt Prim )(.*)(?=2 07.09.2022 1,155.00)",
        });

        foreach (var item in regex)
        {
            Match tc = Regex.Match(page, item.Tc);
            Match sasi = Regex.Match(page, item.Sasi);
            Match plaka = Regex.Match(page, item.Plaka);
            Match baslamatarihi = Regex.Match(page, item.BaslamaTarihi);
            Match bitistarihi = Regex.Match(page, item.BitisTarihi);
            Match police = Regex.Match(page, item.Police);
            Match marka = Regex.Match(page, item.Marka);
            Match modelad = Regex.Match(page, item.ModelAd);
            Match modelyili = Regex.Match(page, item.ModelYili);
            Match brtprim = Regex.Match(page, item.BrutPrim);
            Match netprim = Regex.Match(page, item.ToplamNetPrim);

            Console.WriteLine($" Tc : {tc.Value}, Plaka : {plaka.Value}, Sasi : {sasi.Value}, Baslamatarihi : {baslamatarihi.Value}, Bitistarihi : {bitistarihi.Value},Police:{police.Value}{tc.Value}, Marka : {marka.Value},Modelad : {modelad.Value}, ModelYılı : {modelyili.Value}, Brutprim : {brtprim.Value}, Netprim : {netprim.Value}");
        }
    }
}

void doga()
{
    using (PdfReader reader = new PdfReader("pdf/doga.pdf"))
    {
        string page = "";
        List<Model> regex = new List<Model>();
        ITextExtractionStrategy Strategy = new LocationTextExtractionStrategy();
        for (int i = 1; i <= 2; i++)
        {
            page += PdfTextExtractor.GetTextFromPage(reader, i, Strategy);
        }

        regex.Add(new Model()
        {
            Plaka = @"(?<=Plaka)(.*)(?=)",
            Tc = @"(?<=T.C. Kimlik No:)(.*)(?=)",
            Sasi = @"(?<=Şasi No)(.*)(?=)",
            BaslamaTarihi = "(?<=Başlama Tarihi:)(.*)(?=Bitiş Tarihi: 17.06.2023 )",
            BitisTarihi = "(?<=Bitiş Tarihi:)(.*)(?= Süre: 365)",
            Police = "(?<=Poliçe / Yenileme No: )(.*)(?=/ 0 Zeyil No: 0)",
            Marka = "(?<=Marka)(.*)(?=)",
            ModelAd = "(?<=Tip)(.*)(?=)",
            ModelYili = "(?<=Model Yılı)(.*)(?= Kişi)",
            ToplamNetPrim = "(?<=Net Prim )(.*)(?=)",
            BrutPrim = "(?<=Brüt Prim )(.*)(?=)",
        });

        foreach (var item in regex)
        {
            Match tc = Regex.Match(page, item.Tc);
            Match sasi = Regex.Match(page, item.Sasi);
            Match plaka = Regex.Match(page, item.Plaka);
            Match baslamatarihi = Regex.Match(page, item.BaslamaTarihi);
            Match bitistarihi = Regex.Match(page, item.BitisTarihi);
            Match police = Regex.Match(page, item.Police);
            Match marka = Regex.Match(page, item.Marka);
            Match modelad = Regex.Match(page, item.ModelAd);
            Match modelyili = Regex.Match(page, item.ModelYili);
            Match brtprim = Regex.Match(page, item.BrutPrim);
            Match netprim = Regex.Match(page, item.ToplamNetPrim);

            Console.WriteLine($" Tc : {tc.Value}, Plaka : {plaka.Value}, Sasi : {sasi.Value}, Baslamatarihi : {baslamatarihi.Value}, Bitistarihi : {bitistarihi.Value},Police:{police.Value}{tc.Value}, Marka : {marka.Value},Modelad : {modelad.Value}, ModelYılı : {modelyili.Value}, Brutprim : {brtprim.Value}, Netprim : {netprim.Value}");
        }
    }
}

void aveon()
{
    using (PdfReader reader = new PdfReader("pdf/Aveon.PDF"))
    {
        string page = "";
        List<Model> regex = new List<Model>();
        ITextExtractionStrategy Strategy = new LocationTextExtractionStrategy();
        for (int i = 1; i <= 2; i++)
        {
            page += PdfTextExtractor.GetTextFromPage(reader, i, Strategy);
        }

        regex.Add(new Model()
        {
            Plaka = @"(?<=Plaka       :)(.*)(?=)",
            Tc = @"(?<=T.C. Kimlik No:)(.*)(?=)",
            Sasi = @"(?<=Şasi No     :)(.*)(?=)",
            BaslamaTarihi = "(?<=Başlama Tarihi: )(.*)(?=Bitiş Tarihi)",
            BitisTarihi = "(?<=Bitiş Tarihi: )(.*)(?=Süre)",
            Police = "(?<=Poliçe / Yenileme No: )(.*)(?=/ 0 Zeyil No: 0)",
            Marka = "(?<=Marka)(.*)(?=)",
            // ModelAd = "(?<=Tipi )(.*)(?=)",
            ModelYili = "(?<=Model Yılı)(.*)(?= Kişi)",
            ToplamNetPrim = "(?<=Net Prim )(.*)(?=P 31.01.2023)",
            BrutPrim = "(?<=Brüt Prim)(.*)(?=2)",

        });

        foreach (var item in regex)
        {
            Match tc = Regex.Match(page, item.Tc);
            Match sasi = Regex.Match(page, item.Sasi);
            Match plaka = Regex.Match(page, item.Plaka);
            Match baslamatarihi = Regex.Match(page, item.BaslamaTarihi);
            Match bitistarihi = Regex.Match(page, item.BitisTarihi);
            Match police = Regex.Match(page, item.Police);
            Match marka = Regex.Match(page, item.Marka);
            // Match modelad = Regex.Match(page, item.ModelAd);
            Match modelyili = Regex.Match(page, item.ModelYili);

            Match brtprim = Regex.Match(page, item.BrutPrim);
            Match netprim = Regex.Match(page, item.ToplamNetPrim);

            Console.WriteLine($" Tc : {tc.Value}, Plaka : {plaka.Value}, Sasi : {sasi.Value}, Baslamatarihi : {baslamatarihi.Value}, Bitistarihi : {bitistarihi.Value},Police:{police.Value}{tc.Value}, Marka : {marka.Value}, ModelYılı : {modelyili.Value}, Brutprim : {brtprim.Value}, Netprim : {netprim.Value}");
        }
    }
}

void arex()
{
    using (PdfReader reader = new PdfReader("pdf/arex.pdf"))
    {
        string page = "";
        List<Model> regex = new List<Model>();
        ITextExtractionStrategy Strategy = new LocationTextExtractionStrategy();
        for (int i = 1; i <= 2; i++)
        {
            page += PdfTextExtractor.GetTextFromPage(reader, i, Strategy);
        }

        regex.Add(new Model()
        {
            Plaka = @"(?<=Plaka No )(.*)(?=Kullanım)",
            Tc = @"(?<=T.C.KİMLİK NO : )(.*)(?=)",
            Sasi = @"(?<=Şasi No )(.*)(?=)",
            BaslamaTarihi = "(?<=BAŞLAMA TARİHİ : )(.*)(?=)",
            BitisTarihi = "(?<= BİTİŞ TARİHİ : )(.*)(?=)",
            Police = "(?<=POLİÇE / YENİLEME NO : )(.*)(?=)",
            Marka = "(?<=Marka )(.*)(?=)",
            ModelAd = "(?<=Tipi )(.*)(?=)",
            ModelYili = "(?<=Model )(.*)(?=)",

            ToplamNetPrim = "(?<=PRİM BİLGİLERİ PRİM ÖDEME TABLOSU\n)(.*)(?=)",
            BrutPrim = "(?<=BSMV :\n\n)(.*)(?=)",

        });

        foreach (var item in regex)
        {
            Match tc = Regex.Match(page, item.Tc);
            Match sasi = Regex.Match(page, item.Sasi);
            Match plaka = Regex.Match(page, item.Plaka);
            Match baslamatarihi = Regex.Match(page, item.BaslamaTarihi);
            Match bitistarihi = Regex.Match(page, item.BitisTarihi);
            Match police = Regex.Match(page, item.Police);
            Match marka = Regex.Match(page, item.Marka);
            Match modelad = Regex.Match(page, item.ModelAd);
            Match modelyili = Regex.Match(page, item.ModelYili);

            Match brtprim = Regex.Match(page, item.BrutPrim);
            Match netprim = Regex.Match(page, item.ToplamNetPrim);




            Console.WriteLine($" Tc : {tc.Value}, Plaka : {plaka.Value}, Sasi : {sasi.Value}, Baslamatarihi : {baslamatarihi.Value}, Bitistarihi : {bitistarihi.Value},Police:{police.Value}{tc.Value}, Marka : {marka.Value}, Modelad : {modelad.Value}, ModelYılı : {modelyili.Value}, Brutprim : {brtprim.Value}, Netprim : {netprim.Value}");
        }
    }
}

void ak()
{
    using (PdfReader reader = new PdfReader("pdf/ak.pdf"))
    {
        string page = "";
        List<Model> regex = new List<Model>();
        ITextExtractionStrategy Strategy = new LocationTextExtractionStrategy();
        for (int i = 1; i <= 2; i++)
        {
            page += PdfTextExtractor.GetTextFromPage(reader, i, Strategy);
        }

        regex.Add(new Model()
        {
            Plaka = @"(?<=Plaka : )(.*)(?=)",
            Tc = @"(?<=TC Kimlik No : )(.*)(?=)",
            Sasi = @"(?<=Şasi No : )(.*)(?=)",
            BaslamaTarihi = "(?<=BAŞLANGIÇ TARİHİ )(.*)(?=)",
            BitisTarihi = "(?<=BİTİŞ TARİHİ )(.*)(?=)",
            Police = "(?<=POLİÇE NO )(.*)(?=)",
            Marka = "(?<=Marka/Tip : )(.*)(?=)",
            KullanimTarzi = "(?<=Kullanım Şekli :)(.*)(?=)",
            ToplamNetPrim = "(?<=Net Prim : )(.*)(?=)",
            OdenecekPrim = "(?<=ÖDENECEK PRİM : )(.*)(?=)",

        });

        foreach (var item in regex)
        {
            Match tc = Regex.Match(page, item.Tc);
            Match sasi = Regex.Match(page, item.Sasi);
            Match plaka = Regex.Match(page, item.Plaka);
            Match baslamatarihi = Regex.Match(page, item.BaslamaTarihi);
            Match bitistarihi = Regex.Match(page, item.BitisTarihi);
            Match police = Regex.Match(page, item.Police);
            Match marka = Regex.Match(page, item.Marka);

            Match kullanimTarzi = Regex.Match(page, item.KullanimTarzi);
            Match toplamnetprim = Regex.Match(page, item.ToplamNetPrim);
            Match netprim = Regex.Match(page, item.ToplamNetPrim);




            Console.WriteLine($" Tc : {tc.Value}, Plaka : {plaka.Value}, Sasi : {sasi.Value}, Baslamatarihi : {baslamatarihi.Value}, Bitistarihi : {bitistarihi.Value}" +
                $",Police:{police.Value}{tc.Value}, Marka : {marka.Value},  KullanımTarzı : {kullanimTarzi.Value}" +
                $", Brutprim : {toplamnetprim.Value}, Netprim : {netprim.Value}");
        }

        

    }
}

void atlas()
{
    using (PdfReader reader = new PdfReader("pdf/atlas.pdf"))
    {
        string page = "";
        List<Model> regex = new List<Model>();
        ITextExtractionStrategy Strategy = new LocationTextExtractionStrategy();
        for (int i = 1; i <= 2; i++)
        {
            page += PdfTextExtractor.GetTextFromPage(reader, i, Strategy);
        }

        regex.Add(new Model()
        {
            Plaka = @"(?<=ŞASİ NO : )(.*)(?=)",
            Tc = @"(?<=TCK/VERGİ DR. NO : )(.*)(?=)",
            Sasi = @"(?<=ŞASİ NO : )(.*)(?=)",
            BaslamaTarihi = "(?<=BAŞLAMA TARİHİ : )(.*)(?=)",
            BitisTarihi = "(?<=BİTİŞ TARİHİ : )(.*)(?=)",
            Police = "(?<=POLİÇE / YENİLEME NO : )(.*)(?=)",
            Marka = "(?<=MARKA : )(.*)(?=)",
            ModelAd = "(?<=MODEL :)(.*)(?=)",
            ModelYili = "(?<=MODEL YILI : )(.*)(?=)",
            KullanimTarzi = "(?<=KULLANIM TIPI : )(.*)(?=)",
            ToplamNetPrim = "(?<=TOPLAM NET PRİM_+: )(.*)(?=)",
            BrütPrim = "(?<=BRÜT PRİM : )(.*)(?=)",

        });

        foreach (var item in regex)
        {
            Match tc = Regex.Match(page, item.Tc);
            Match sasi = Regex.Match(page, item.Sasi);
            Match plaka = Regex.Match(page, item.Plaka);
            Match baslamatarihi = Regex.Match(page, item.BaslamaTarihi);
            Match bitistarihi = Regex.Match(page, item.BitisTarihi);
            Match police = Regex.Match(page, item.Police);
            Match marka = Regex.Match(page, item.Marka);
            Match modelad = Regex.Match(page, item.ModelAd);
            Match modelyili = Regex.Match(page, item.ModelYili);
            Match kullanimTarzi = Regex.Match(page, item.KullanimTarzi);
            Match brtprim = Regex.Match(page, item.BrütPrim);
            Match netprim = Regex.Match(page, item.ToplamNetPrim);




            Console.WriteLine($" Tc : {tc.Value}, Plaka : {plaka.Value}, Sasi : {sasi.Value}, Baslamatarihi : {baslamatarihi.Value}, Bitistarihi : {bitistarihi.Value},Police:{police.Value}{tc.Value}, Marka : {marka.Value}, Modelad : {modelad.Value}, ModelYılı : {modelyili.Value}, KullanımTarzı : {kullanimTarzi.Value}, Brutprim : {brtprim.Value}, Netprim : {netprim.Value}");
        }

        //tc kimlik , varsa doğum tarihi, plaka, belge seri no (ruhsat no), başlangıç ve bitiş, poliçe veya teklif no, araç marka, 
        // model, model yılı, kullanım tarzı, net prim, brüt prim

    }
}


    }
}
*/