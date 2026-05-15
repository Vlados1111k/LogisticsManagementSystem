mermaid{
classDiagram
    class LogisticsManager {
        -List~Vehicle~ _fleet
        -Queue~Cargo~ _warehouse
        +AddToFleet(Vehicle vehicle)
        +AddToWarehouse(Cargo cargo)
        +ProcessDeliveries(IDeliveryStrategy strategy)
    }
    class Vehicle {
        <<abstract>>
        +String LicensePlate
        +double Capacity
        +VehicleStatus Status
        +Drive()*
    }
    class Truck {
        +Drive()
    }
    class Van {
        +Drive()
    }
    class IDeliveryStrategy {
        <<interface>>
        +Calculate(double distance, Cargo cargo)
    }
    class StandardDelivery {
        +Calculate()
    }
    class InsuranceDecorator {
        -IDeliveryStrategy _innerStrategy
        +Calculate()
    }
    Vehicle <|-- Truck : Спадкування
    Vehicle <|-- Van : Спадкування
    IDeliveryStrategy <|.. StandardDelivery : Реалізація
    IDeliveryStrategy <|.. InsuranceDecorator : Декорує
    LogisticsManager --> Vehicle : Керує
    LogisticsManager --> IDeliveryStrategy : Використовує
    InsuranceDecorator o-- IDeliveryStrategy : Обгортає




}
