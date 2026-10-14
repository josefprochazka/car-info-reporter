namespace CarInfoReporter.Models;

/// <summary>Total price of all weekend sales of one car model.</summary>
/// <remarks>Created by WeekendSalesCalculator, shown in the "Weekend sales by model" table in the window.</remarks>
public record ModelSummary(string ModelName, decimal PriceWithoutVat, decimal PriceWithVat);
