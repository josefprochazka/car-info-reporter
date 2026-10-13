using CarInfoReporter.Models;

namespace CarInfoReporter.Services;

public static class WeekendSalesCalculator
{
    /// <summary>
    /// Takes only the cars sold on Saturday or Sunday and sums their prices for each model.
    /// VAT is calculated per sale, because the same model can be sold with different VAT rates.
    /// </summary>
    public static List<ModelSummary> SummarizeByModel(IEnumerable<CarSale> sales)
    {
        return sales
            .Where(sale => sale.IsWeekendSale)
            .GroupBy(sale => sale.ModelName)
            .Select(model => new ModelSummary(
                ModelName: model.Key,
                PriceWithoutVat: model.Sum(sale => sale.Price),
                PriceWithVat: model.Sum(sale => sale.PriceWithVat)))
            .OrderBy(summary => summary.ModelName)
            .ToList();
    }
}
