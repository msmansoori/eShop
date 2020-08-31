namespace eShop.ProductEntities.Entities.HelperFields
{
    public class LibraryField : HelperField
    {
        public string Name { get; set; }

        public override string Display => Name;
    }
}
