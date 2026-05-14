namespace Logistics.Domain;

public enum CargoType { Regular, Fragile, Hazardous }

public class Cargo {
    public string Name { get; set; } = "Товар";
    public double Weight { get; set; }
    public CargoType Type { get; set; }
}

public interface IDeliveryStrategy {
    double Calculate(double distance, Cargo cargo);
}

public class StandardDelivery : IDeliveryStrategy {
    public double Calculate(double dist, Cargo cargo) {
        double multiplier = cargo.Type switch {
            CargoType.Fragile => 1.5,
            CargoType.Hazardous => 2.0,
            _ => 1.0
        };
        return dist * 20 * multiplier;
    }
}

public class InsuranceDecorator {
    private readonly IDeliveryStrategy _strategy;
    public InsuranceDecorator(IDeliveryStrategy strategy) => _strategy = strategy;
    public double GetPriceWithInsurance(double dist, Cargo cargo) => _strategy.Calculate(dist, cargo) * 1.05;
}