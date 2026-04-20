namespace VoidLib2.Inventory
{
    public class InventoryManager
    {
        public static InventoryManager Instance { get; private set; } = new();
        public Inventory PlayerInventory { get; } = new();

        public Dictionary<string, ItemDefinition> Items = new();
    }
}
