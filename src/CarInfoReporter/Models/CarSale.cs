namespace CarInfoReporter.Models;

/// <summary>One sold car = one row of the input XML file.</summary>
/// <param name="ModelName">Name of the car model, e.g. "Škoda Fabia".</param>
/// <param name="SaleDate">Day the car was sold.</param>
/// <param name="Price">Price without VAT.</param>
/// <param name="VatRate">VAT rate in percent, e.g. 20.</param>
public record CarSale(string ModelName, DateTime SaleDate, decimal Price, decimal VatRate)
{
    public decimal PriceWithVat => Price * (1 + VatRate / 100);

    public bool IsWeekendSale => SaleDate.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday;
}
