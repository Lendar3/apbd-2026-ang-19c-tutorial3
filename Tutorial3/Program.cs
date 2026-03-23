using Tutorial3.Models;
using Tutorial3.services;

var penaltyCalculator = new PenaltyCalculator();
var rentalService = new RentalService(penaltyCalculator);

var laptop = new Laptop("Dell XPS", 16, "i7");
var projector = new Projector("Epson X", "1080p", 3000);
var camera = new Camera("Canon EOS", 24, true);

var student = new Student("John", "Doe");
var employee = new Employee("Jane", "Smith");

Console.WriteLine("--- System Initialized ---");

try
{
    Console.WriteLine($"\nRenting {laptop.Name} to {student.FirstName}...");
    rentalService.RentEquipment(student, laptop, 5);
    Console.WriteLine("Success!");

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
    rentalService.RentEquipment(student, projector, 2);
    Console.WriteLine($"\nRented {projector.Name} to {student.FirstName}");
    
    Console.WriteLine($"Trying to rent a 3rd item ({camera.Name}) to student...");
    rentalService.RentEquipment(student, camera, 2); 
}
catch (Exception ex)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($"Error: {ex.Message}");
    Console.ResetColor();
}


Console.WriteLine("\n--- Processing Returns ---");
decimal fee1 = rentalService.ReturnEquipment(projector, DateTime.Now); 
Console.WriteLine($"Returned {projector.Name} on time. Penalty: ${fee1}");


decimal fee2 = rentalService.ReturnEquipment(laptop, DateTime.Now.AddDays(8)); 
Console.WriteLine($"Returned {laptop.Name} late. Penalty: ${fee2}");


Console.WriteLine("\n--- Final System Report ---");
foreach (var rental in rentalService.GetAllRentals())
{
    string status = rental.ActualReturnDate == null ? "Active" : "Returned";
    Console.WriteLine($"- {rental.RentedEquipment.Name} rented by {rental.RentedBy.FirstName}. Status: {status}. Penalty: ${rental.PenaltyFee}");
}