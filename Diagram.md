classDiagram
    %% Класи автопарку
    class Vehicle {
        <<abstract>>
        +String LicensePlate
        +String Model
        +Double MaxLoad
        +VehicleStatus Status
        +String CurrentDriver
        +Drive(destination)*
    }
    class Truck {
        +Drive(destination)
    }
    class Van {
        +Drive(destination)
    }

    %% Класи логістики
    class Cargo {
        +String Name
        +Double Weight
        +CargoType Type
    }
    
    class Route {
        +String From
        +String To
        +Double Distance
        +Double EstimatedTime
    }

    %% Патерни проектування
    class IDeliveryStrategy {
        <<interface>>
        +Calculate(dist, cargo)
    }
    class StandardDelivery {
        +Calculate(dist, cargo)
    }
    class InsuranceDecorator {
        -IDeliveryStrategy _strategy
        +GetPriceWithInsurance(dist, cargo)
    }

    %% Менеджмент
    class LogisticsManager {
        -List~Vehicle~ _fleet
        -Queue~Cargo~ _warehouse
        +AddToFleet(v)
        +AddToWarehouse(c)
        +ProcessLogistics(dest, dist)
        +ShowFleet()
        +ReturnAllVehicles()
    }

    %% Перерахування
    class VehicleStatus {
        <<enumeration>>
        Ready
        OnRoute
        Maintenance
    }

    %% Зв'язки
    Vehicle <|-- Truck
    Vehicle <|-- Van
    Vehicle --> VehicleStatus
    LogisticsManager "1" o-- "*" Vehicle : керує
    LogisticsManager "1" o-- "*" Cargo : зберігає в черзі
    LogisticsManager ..> IDeliveryStrategy : використовує
    InsuranceDecorator ..> IDeliveryStrategy : обгортає (Decorator)
    StandardDelivery ..|> IDeliveryStrategy : реалізує (Strategy)