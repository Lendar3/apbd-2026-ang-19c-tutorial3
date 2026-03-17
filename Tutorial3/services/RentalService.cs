namespace Tutorial3.services;
using Models;
public class RentalService
{
    private readonly List<Rental> _rentals = new();
    private readonly PenaltyCalculator _penaltyCalculator;

    public RentalService(PenaltyCalculator penaltyCalculator)
    {
        _penaltyCalculator = penaltyCalculator;
    }

    public IReadOnlyList<Rental> GetAllRentals() => _rentals;

    public void RentEquipment(User user, Equipment equipment, int days)
    {
        if (!equipment.IsAvailable)
            throw new InvalidOperationException($"Equipment {equipment.Name} is not available.");

        int activeRentals = _rentals.Count(r => r.RentedBy.Id == user.Id && r.ActualReturnDate == null);
        if (activeRentals >= user.MaxRentals)
            throw new InvalidOperationException($"{user.FirstName} has reached the maximum limit of {user.MaxRentals} rentals.");

        equipment.IsAvailable = false;
        _rentals.Add(new Rental
        {
            RentedBy = user,
            RentedEquipment = equipment,
            RentalDate = DateTime.Now,
            DueDate = DateTime.Now.AddDays(days)
        });
    }

    public decimal ReturnEquipment(Equipment equipment, DateTime returnDate)
    {
        var rental = _rentals.FirstOrDefault(r => r.RentedEquipment.Id == equipment.Id && r.ActualReturnDate == null);
        if (rental == null) throw new InvalidOperationException("Active rental not found.");

        rental.ActualReturnDate = returnDate;
        rental.RentedEquipment.IsAvailable = true;
        
        rental.PenaltyFee = _penaltyCalculator.CalculatePenalty(rental, returnDate);
        return rental.PenaltyFee;
    }
}