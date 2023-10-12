namespace QueryBuilder.Test;

public class TestClass : ITableTranslator
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int? Age { get; set; }
    public DateTime Timespan { get; set; }
}