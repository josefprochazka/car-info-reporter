using System.IO;
using System.Xml;
using System.Xml.Linq;
using CarInfoReporter.Models;

namespace CarInfoReporter.Services;

/// <summary>Reads car sales from an XML file. See data/car-sales.xml for the format.</summary>
public static class CarSalesXmlReader
{
    /// <exception cref="InvalidDataException">The file is not a valid car sales XML file.</exception>
    public static List<CarSale> Read(string filePath)
    {
        XDocument document;
        try
        {
            document = XDocument.Load(filePath, LoadOptions.SetLineInfo);
        }
        catch (XmlException ex)
        {
            throw new InvalidDataException($"The file is not a valid XML file. {ex.Message}", ex);
        }

        if (document.Root?.Name != "CarSales")
        {
            throw new InvalidDataException("The root element of the file must be <CarSales>.");
        }

        return document.Root.Elements("CarSale").Select(ReadCarSale).ToList();
    }

    private static CarSale ReadCarSale(XElement carSale)
    {
        // The explicit XElement conversions parse values in the XML (culture independent) format.
        try
        {
            return new CarSale(
                ModelName: (string)RequiredElement(carSale, "ModelName"),
                SaleDate: (DateTime)RequiredElement(carSale, "SaleDate"),
                Price: (decimal)RequiredElement(carSale, "Price"),
                VatRate: (decimal)RequiredElement(carSale, "VatRate"));
        }
        catch (FormatException ex)
        {
            throw new InvalidDataException($"<CarSale> on line {LineNumber(carSale)} contains an invalid value. {ex.Message}", ex);
        }
    }

    private static XElement RequiredElement(XElement carSale, string name)
    {
        return carSale.Element(name)
            ?? throw new InvalidDataException($"<CarSale> on line {LineNumber(carSale)} is missing the <{name}> element.");
    }

    private static int LineNumber(XElement element) => ((IXmlLineInfo)element).LineNumber;
}
