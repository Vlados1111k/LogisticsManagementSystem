using Logistics.Domain;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;
var hub = new LogisticsManager();

hub.AddToFleet(new Truck("AA 1111 AA") { CurrentDriver = "Петро" });
hub.AddToFleet(new Van("BK 5555 BK") { CurrentDriver = "Олег" });
hub.AddToFleet(new Van("BK 5532 BТ") { CurrentDriver = "Максим" });
hub.AddToFleet(new Van("СВ 1232 ВМ") { CurrentDriver = "Андрій" });
hub.AddToFleet(new Van("BН 7632 BС") { CurrentDriver = "Владислав" });

while (true) {
    Console.WriteLine("\n Керування логістикою та автопарком:");
    Console.WriteLine("1. Стан автопарку");
    Console.WriteLine("2. Додати вантаж на склад");
    Console.WriteLine("3. Відправити машини в рейс (Логістика)");
    Console.WriteLine("4. Вихід");
    Console.Write("Ваш вибір: ");

    switch (Console.ReadLine()) {
        case "1": hub.ShowFleetReport(); break;
        case "2":
            Console.Write("Назва товару: "); string n = Console.ReadLine() ?? "Товар";
            Console.Write("Вага (т): "); double.TryParse(Console.ReadLine(), out double w);
            hub.AddToWarehouse(new Cargo { Name = n, Weight = w });
            Console.WriteLine("Додано"); break;
        case "3":
            hub.ProcessLogistics("Львів", 540); break;
        case "4":
            hub.ReturnAllVehicles();
            Console.WriteLine("Натисніть клавішу...");
            Console.ReadKey();
            Console.WriteLine("Вихід...");
            return;
    }
}