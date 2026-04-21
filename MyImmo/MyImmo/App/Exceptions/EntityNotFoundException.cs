namespace MyImmo.App.Exceptions;

public class EntityNotFoundException : Exception
{
    public string Entity { get; }
    public EntityNotFoundException(string entity)
    {
        Entity = $"Entity with id={entity} could not be found in the data base.";
    }
}