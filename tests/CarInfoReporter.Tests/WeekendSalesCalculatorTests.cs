using CarInfoReporter.Models;
using CarInfoReporter.Services;

namespace CarInfoReporter.Tests;

public class WeekendSalesCalculatorTests
{
    private static readonly DateTime Friday = new(2010, 12, 3);
    private static readonly DateTime Saturday = new(2010, 12, 4);
    private static readonly DateTime Sunday = new(2010, 12, 5);
    private static readonly DateTime Monday = new(2010, 12, 6);

    [Fact]
    public void Sales_outside_the_weekend_are_ignored()
    {
        var sales = new[]
        {
            new CarSale("Škoda Fabia", Friday, 350_000, 20),
            new CarSale("Škoda Fabia", Monday, 350_000, 20),
        };

        Assert.Empty(WeekendSalesCalculator.SummarizeByModel(sales));
    }

    [Fact]
    public void Weekend_sales_are_summed_for_each_model()
    {
        var sales = new[]
        {
            new CarSale("Škoda Oktávia", Saturday, 500_000, 20),
            new CarSale("Škoda Fabia", Saturday, 350_000, 20),
            new CarSale("Škoda Oktávia", Sunday, 500_000, 20),
            new CarSale("Škoda Oktávia", Monday, 500_000, 20),
        };

        var summaries = WeekendSalesCalculator.SummarizeByModel(sales);

        Assert.Equal(
            [
                new ModelSummary("Škoda Fabia", 350_000, 420_000),
                new ModelSummary("Škoda Oktávia", 1_000_000, 1_200_000),
            ],
            summaries);
    }

    [Fact]
    public void Vat_is_calculated_for_each_sale_with_its_own_rate()
    {
        var sales = new[]
        {
            new CarSale("Škoda Felicia", Saturday, 210_000, 20),
            new CarSale("Škoda Felicia", Sunday, 210_000, 19),
        };

        var summary = Assert.Single(WeekendSalesCalculator.SummarizeByModel(sales));

        Assert.Equal(420_000, summary.PriceWithoutVat);
        Assert.Equal(252_000 + 249_900, summary.PriceWithVat);
    }
}
