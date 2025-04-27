using UnityEngine;
using System.Collections.Generic;

namespace PlayerSystem
{
    public class PlayerInventory : MonoBehaviour
    {
        private static PlayerInventory _instance;
        public static PlayerInventory Instance => _instance;

        private List<string> _items = new List<string>();

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }
            _instance = this;
        }

        private void Start()
        {
            _items = InventoryDatabase.LoadInventory();
        }

        private void OnDestroy()
        {
            if (_instance == this)
            {
                InventoryDatabase.SaveInventory(_items);
            }
        }

        public void AddItem(string itemName)
        {
            if (!_items.Contains(itemName))
            {
                _items.Add(itemName);
                Debug.Log($"Item added: {itemName}");
            }
            else
            {
                Debug.Log($"Already have item: {itemName}");
            }
        }

        public void RemoveItem(string itemName)
        {
            if (_items.Contains(itemName))
            {
                _items.Remove(itemName);
                Debug.Log($"Item removed: {itemName}");
            }
            else
            {
                Debug.Log($"Item not found: {itemName}");
            }
        }

        public bool HasItem(string itemName)
        {
            return _items.Contains(itemName);
        }

        public List<string> GetAllItems()
        {
            return new List<string>(_items);
        }
    }
}