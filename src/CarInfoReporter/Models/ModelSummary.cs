namespace CarInfoReporter.Models;

/// <summary>Total price of all weekend sales of one car model.</summary>
public record ModelSummary(string ModelName, decimal PriceWithoutVat, decimal PriceWithVat);
