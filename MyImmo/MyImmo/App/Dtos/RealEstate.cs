namespace MyImmo.App.Dtos;

public class RealEstate
{
    public required int Id { get; set; }
    public List<Income>? Income { get; set; }
}