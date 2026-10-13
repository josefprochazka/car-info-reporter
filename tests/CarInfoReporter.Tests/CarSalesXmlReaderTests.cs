using CarInfoReporter.Models;
using CarInfoReporter.Services;

namespace CarInfoReporter.Tests;

public class CarSalesXmlReaderTests
{
    // The sample file is copied to the output folder by the application project.
    private static readonly string SampleFile = Path.Combine(AppContext.BaseDirectory, "data", "car-sales.xml");

    [Fact]
    public void Sample_file_is_read()
    {
        var sales = CarSalesXmlReader.Read(SampleFile);

        Assert.Equal(13, sales.Count);
        Assert.Equal(new CarSale("Škoda Oktávia", new DateTime(2010, 12, 2), 500_000, 20), sales[0]);
    }

    [Fact]
    public void Sample_file_gives_the_expected_weekend_summary()
    {
        var summaries = WeekendSalesCalculator.SummarizeByModel(CarSalesXmlReader.Read(SampleFile));

        Assert.Equal(
            [
                new ModelSummary("Škoda Fabia", 700_000, 840_000),
                new ModelSummary("Škoda Felicia", 630_000, 751_800),
                new ModelSummary("Škoda Oktávia", 1_000_000, 1_200_000),
            ],
            summaries);
    }

    [Theory]
    [InlineData("this is not xml")]
    [InlineData("<Cars />")]
    [InlineData("<CarSales><CarSale><ModelName>Škoda Fabia</ModelName></CarSale></CarSales>")]
    [InlineData("<CarSales><CarSale><ModelName>Škoda Fabia</ModelName><SaleDate>yesterday</SaleDate><Price>1</Price><VatRate>20</VatRate></CarSale></CarSales>")]
    public void Invalid_file_is_rejected(string content)
    {
        var filePath = Path.GetTempFileName();
        File.WriteAllText(filePath, content);

        try
        {
            Assert.Throws<InvalidDataException>(() => CarSalesXmlReader.Read(filePath));
        }
        finally
        {
            File.Delete(filePath);
        }
    }
}
