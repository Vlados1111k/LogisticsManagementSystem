namespace Logistics.Domain;

public enum VehicleStatus { Ready, OnRoute, Maintenance }

public abstract class Vehicle {
    public string LicensePlate { get; set; }
    public string Model { get; set; }
    public double MaxLoad { get; set; }
    public VehicleStatus Status { get; set; } = VehicleStatus.Ready;
    public string? CurrentDriver { get; set; }

    protected Vehicle(string plate, string model, double maxLoad) {
        LicensePlate = plate;
        Model = model;
        MaxLoad = maxLoad;
    }

    public abstract void Drive(string destination);
}

public class Truck : Vehicle {
    public Truck(string plate) : base(plate, "Volvo FH16 (Фура)", 20.0) { }
    public override void Drive(string dest) => Console.WriteLine($"[РЕЙС]: Фура {LicensePlate} виїхала до: {dest}");
}

public class Van : Vehicle {
    public Van(string plate) : base(plate, "Mercedes Sprinter (Бус)", 2.5) { }
    public override void Drive(string dest) => Console.WriteLine($"[РЕЙС]: Бус {LicensePlate} маневрує містом до: {dest}");
}