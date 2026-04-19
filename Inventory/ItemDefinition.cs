using UnityEngine.UI;

namespace VoidLib2.Inventory
{
    public class ItemDefinition
    {
        public string Id { get; private set; }
        public Image? Image { get; private set; }
        public string Name { get; private set; }
        public ItemDefinition(string id, string name, Image? image = null)
        {
            Id = id;
            Name = name;
            Image = image;
        }
    }
}
