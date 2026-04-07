namespace MyImmo.App.Dtos;

public class Incomes
{
    public required int Id { get; set; }
    public List<Income>? Income { get; set; }
}