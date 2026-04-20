using UnityEngine.UI;
using UnityEngine;

namespace VoidLib2.Inventory
{
    public class ItemDefinition
    {
        public string Id { get; private set; }
        public Sprite? Image { get; private set; }
        public string Name { get; private set; }
        public ItemDefinition(string id, string name, Sprite? image = null)
        {
            Id = id;
            Name = name;
            Image = image;
        }
    }
}
