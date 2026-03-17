namespace Tutorial3.services;

using Models;

public class PenaltyCalculator
{
    private const decimal DailyPenaltyRate = 10.0m;

    public decimal CalculatePenalty(Rental rental, DateTime returnDate)
    {
        if (returnDate <= rental.DueDate) return 0;
        
        int daysLate = (returnDate - rental.DueDate).Days;
        return daysLate * DailyPenaltyRate;
    }
}