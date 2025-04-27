using System.Collections.Generic;
using System.Diagnostics;

namespace PlayerSystem
{
    public static class InventoryDatabase
    {
        private static List<string> _savedItems = new List<string>();

        public static void SaveInventory(List<string> items)
        {
            _savedItems = new List<string>(items);
        }

        public static List<string> LoadInventory()
        {
            foreach (var item in _savedItems)
            {
                Debug.WriteLine(item);
            }
            return new List<string>(_savedItems);
        }
    }
}
