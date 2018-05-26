public interface IHasStats
{
    void AddStatModifer(StatModifer modifer , AttributeEnum attributeType );
    void RemoveStatModifer(StatModifer modifer , AttributeEnum attributeType );
}