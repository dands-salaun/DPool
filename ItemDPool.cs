using UnityEngine;

namespace Dands.Pool
{
    [CreateAssetMenu(fileName = "Item Pool", menuName = "DPool/ItemPool", order = 1)]
    public class ItemDPool : ScriptableObject
    {
        public string tagItem;
        public GameObject prefab;
        public int amount;
        public bool isExpandable;
    }    

}

