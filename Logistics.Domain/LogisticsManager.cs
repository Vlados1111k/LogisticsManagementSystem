namespace Logistics.Domain;

public class LogisticsManager {
    private List<Vehicle> _fleet = new();
    private Queue<Cargo> _warehouse = new();

    public void AddToFleet(Vehicle v) => _fleet.Add(v);
    public void AddToWarehouse(Cargo c) => _warehouse.Enqueue(c);

    public void ProcessLogistics(string destination, double distance) {
        Console.WriteLine($"\n Логістика ({destination})");
        if (_warehouse.Count == 0) { Console.WriteLine("Склад порожній."); return; }

        while (_warehouse.Count > 0) {
            var cargo = _warehouse.Peek();
            var vehicle = _fleet.FirstOrDefault(v => v.Status == VehicleStatus.Ready && v.MaxLoad >= cargo.Weight);

            if (vehicle != null) {
                _warehouse.Dequeue();
                vehicle.Status = VehicleStatus.OnRoute;
                double price = new InsuranceDecorator(new StandardDelivery()).GetPriceWithInsurance(distance, cargo);
                
                Console.WriteLine($"Вантаж '{cargo.Name}' ({cargo.Weight}т) -> {vehicle.Model}");
                Console.WriteLine($"Ціна: {price:F2} грн");
                vehicle.Drive(destination);
            } else {
                Console.WriteLine($"Немає вільного авто для вантажу: {cargo.Name} ({cargo.Weight}т)");
                break;
            }
        }
    }

    public void ShowFleetReport() {
    Console.WriteLine("\nМоніторинг автопарку:");
    
    var readyVehicles = _fleet.Where(v => v.Status == VehicleStatus.Ready).ToList();
    var busyVehicles = _fleet.Where(v => v.Status == VehicleStatus.OnRoute).ToList();

    Console.WriteLine($"Вільні ({readyVehicles.Count})");
    foreach (var v in readyVehicles) 
        Console.WriteLine($"  ID: {v.LicensePlate} | {v.Model} | Вантажність: {v.MaxLoad}т");

    Console.WriteLine($"У рейсі ({busyVehicles.Count})");
    foreach (var v in busyVehicles) 
        Console.WriteLine($"  ID: {v.LicensePlate} | {v.Model} | Прямує до мети...");
    }
public void ReturnAllVehicles() {
    int count = 0;
    foreach (var v in _fleet.Where(v => v.Status == VehicleStatus.OnRoute)) {
        v.Status = VehicleStatus.Ready;
        count++;
    }
    Console.WriteLine($"{count} машин повернулися в гараж і знову готові до роботи!");
    }
}
