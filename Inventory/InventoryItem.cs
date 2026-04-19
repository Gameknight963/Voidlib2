namespace VoidLib2.Inventory
{
    public class InventoryItem
    {
        public ItemDefinition Definition;
        public int Quantity;
        
        public InventoryItem(ItemDefinition definition, int quantity)
        {
            Definition = definition;
            Quantity = quantity;
        }
    }
}
