namespace ReservationSystem.Models;

public class Table
{
    public int Id { get; set; } // Primary Key
    public string Area { get; set; } // Dining area (Main Dining, Outside, Balcony)
    public string Number { get; set; } // Table number or code string at the moment as M1 D1 are table numbers
                                       // could be int and then seperate catagory for section
    public int Capacity { get; set; } // Maximum number of guests the table can accommodate
}
