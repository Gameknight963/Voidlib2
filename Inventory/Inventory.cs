namespace VoidLib2.Inventory
{
    public class Inventory
    {
        public List<InventoryItem> Items = new();

        public void AddItem(ItemDefinition def, int quantity = 1)
        {
            Items.Add(new InventoryItem(def, quantity));
        }
        public void RemoveItem(string id, int quantity = 1)
        {
            InventoryItem? item = Items.FirstOrDefault(i => i.Definition.Id == id) 
                ?? throw new InvalidOperationException($"Item '{id}' not found in inventory.");
            item.Quantity -= quantity;

            if (item.Quantity <= 0)
                Items.Remove(item);
        }
        public InventoryItem GetItem(string id)
        {
            return Items.FirstOrDefault(i => i.Definition.Id == id)
                ?? throw new InvalidOperationException($"Item '{id}' not found in inventory.");
        }

        public bool HasItem(string id, int quantity = 1)
        {
            InventoryItem? item = Items.FirstOrDefault(i => i.Definition.Id == id);
            return item != null && item.Quantity >= quantity;
        }
    }
}
