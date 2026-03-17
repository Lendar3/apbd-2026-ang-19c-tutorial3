namespace Tutorial3.Models;

public class Rental
{
    public User RentedBy { get; set; }
    public Equipment RentedEquipment { get; set; }
    public DateTime RentalDate { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? ActualReturnDate { get; set; }
    public decimal PenaltyFee { get; set; }

    public bool IsOverdue => ActualReturnDate.HasValue 
        ? ActualReturnDate.Value > DueDate 
        : DateTime.Now > DueDate;
}