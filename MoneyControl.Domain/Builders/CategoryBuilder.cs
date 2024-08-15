namespace MoneyControl.Domain.Builders;
public class CategoryBuilder
{
    private readonly Category MyCategory = new() { Id = 0 };

    public CategoryBuilder() { }
    public CategoryBuilder(Category cat) => MyCategory = cat;
    public Category Build() => MyCategory;
    public CategoryBuilder WithId(int value)
    {
        MyCategory.Id = value;
        return this;
    }
    public CategoryBuilder WithName(string value) { MyCategory.Name = value; return this; }
    public CategoryBuilder WithType(string value) { MyCategory.Type = value; return this; }
}
