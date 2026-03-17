using Tutorial3.Models;
using Tutorial3.services;

// Инициализация сервисов
var penaltyCalculator = new PenaltyCalculator();
var rentalService = new RentalService(penaltyCalculator);

// 11-12. Добавляем технику и пользователей
var laptop = new Laptop("Dell XPS", 16, "i7");
var projector = new Projector("Epson X", "1080p", 3000);
var camera = new Camera("Canon EOS", 24, true);

var student = new Student("John", "Doe");
var employee = new Employee("Jane", "Smith");

Console.WriteLine("--- System Initialized ---");

try
{
    // 13. Успешная аренда
    Console.WriteLine($"\nRenting {laptop.Name} to {student.FirstName}...");
    rentalService.RentEquipment(student, laptop, 5);
    Console.WriteLine("Success!");

    // 14. Попытка невалидной операции (аренда недоступного)
    Console.WriteLine($"\nTrying to rent already rented {laptop.Name} to {employee.FirstName}...");
    rentalService.RentEquipment(employee, laptop, 2);
}
catch (Exception ex)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($"Error: {ex.Message}");
    Console.ResetColor();
}

try
{
    // 14. Попытка превысить лимит (студенту можно только 2)
    rentalService.RentEquipment(student, projector, 2);
    Console.WriteLine($"\nRented {projector.Name} to {student.FirstName}");
    
    Console.WriteLine($"Trying to rent a 3rd item ({camera.Name}) to student...");
    rentalService.RentEquipment(student, camera, 2); // Тут выбросит исключение
}
catch (Exception ex)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($"Error: {ex.Message}");
    Console.ResetColor();
}

// 15-16. Возврат оборудования
Console.WriteLine("\n--- Processing Returns ---");
decimal fee1 = rentalService.ReturnEquipment(projector, DateTime.Now); // Вовремя
Console.WriteLine($"Returned {projector.Name} on time. Penalty: ${fee1}");

// Симулируем возврат с опозданием на 3 дня
decimal fee2 = rentalService.ReturnEquipment(laptop, DateTime.Now.AddDays(8)); 
Console.WriteLine($"Returned {laptop.Name} late. Penalty: ${fee2}");

// 17. Финальный отчет
Console.WriteLine("\n--- Final System Report ---");
foreach (var rental in rentalService.GetAllRentals())
{
    string status = rental.ActualReturnDate == null ? "Active" : "Returned";
    Console.WriteLine($"- {rental.RentedEquipment.Name} rented by {rental.RentedBy.FirstName}. Status: {status}. Penalty: ${rental.PenaltyFee}");
}