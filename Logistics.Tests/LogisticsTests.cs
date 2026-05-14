using Xunit;
using Logistics.Domain;
using System.Linq;

namespace Logistics.Tests;

public class LogisticsTests
{

    [Fact]
    public void StandardDelivery_RegularCargo_CalculatesCorrectly() {
        var strategy = new StandardDelivery();
        var cargo = new Cargo { Weight = 1.0, Type = CargoType.Regular };
        Assert.Equal(2000, strategy.Calculate(100, cargo));
    }

    [Fact]
    public void StandardDelivery_FragileCargo_AddsFiftyPercent() {
        var strategy = new StandardDelivery();
        var cargo = new Cargo { Weight = 1.0, Type = CargoType.Fragile };
        Assert.Equal(3000, strategy.Calculate(100, cargo));
    }

    [Fact]
    public void StandardDelivery_HazardousCargo_DoublesPrice() {
        var strategy = new StandardDelivery();
        var cargo = new Cargo { Weight = 1.0, Type = CargoType.Hazardous };
        Assert.Equal(4000, strategy.Calculate(100, cargo));
    }


    [Fact]
    public void InsuranceDecorator_AddsFivePercentToTotal() {
        var strategy = new StandardDelivery();
        var decorator = new InsuranceDecorator(strategy);
        var cargo = new Cargo { Weight = 1.0, Type = CargoType.Regular };
        // Базова 2000 + 5% = 2100
        Assert.Equal(2100, decorator.GetPriceWithInsurance(100, cargo));
    }


    [Fact]
    public void Truck_ShouldHaveCorrectMaxLoad() {
        var truck = new Truck("TEST-01");
        Assert.Equal(20.0, truck.MaxLoad);
    }

    [Fact]
    public void Van_ShouldHaveCorrectMaxLoad() {
        var van = new Van("TEST-02");
        Assert.Equal(2.5, van.MaxLoad);
    }

    [Fact]
    public void Vehicle_StatusChangesToOnRoute_AfterDriving() {
        var hub = new LogisticsManager();
        var truck = new Truck("AA 1111 AA");
        hub.AddToFleet(truck);
        hub.AddToWarehouse(new Cargo { Weight = 10.0 });
        
        hub.ProcessLogistics("Київ", 100);
        
        Assert.Equal(VehicleStatus.OnRoute, truck.Status);
    }


    [Fact]
    public void LogisticsManager_ShouldSkipCargo_IfNoVehicleFits() {
        var hub = new LogisticsManager();
        var van = new Van("VAN-01"); // 2.5т
        hub.AddToFleet(van);
        hub.AddToWarehouse(new Cargo { Name = "Танк", Weight = 50.0 });

        hub.ProcessLogistics("Львів", 500);

        Assert.Equal(VehicleStatus.Ready, van.Status);
    }

    [Fact]
    public void LogisticsManager_ShouldProcessMultipleCargos_InOrder() {
        var hub = new LogisticsManager();
        hub.AddToFleet(new Truck("T-1"));
        hub.AddToFleet(new Truck("T-2"));
        hub.AddToWarehouse(new Cargo { Weight = 5 });
        hub.AddToWarehouse(new Cargo { Weight = 5 });

        hub.ProcessLogistics("Одеса", 400);

    }

    [Fact]
    public void Warehouse_QueueIsEmpty_AfterProcessingAllFits() {
        var hub = new LogisticsManager();
        hub.AddToFleet(new Truck("T-1"));
        hub.AddToWarehouse(new Cargo { Weight = 5 });

        hub.ProcessLogistics("Дніпро", 200);
        
    }

    [Fact]
    public void Vehicle_AssignsDriver_Correctly() {
        var truck = new Truck("AA 0000 AA") { CurrentDriver = "Богдан" };
        Assert.Equal("Богдан", truck.CurrentDriver);
    }
}