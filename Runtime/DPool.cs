using System.Collections.Generic;
using UnityEngine;

namespace Dands.Pool
{
public class DPool: MonoBehaviour
{
    #region Singlenton

    private static DPool _instance;
    public static DPool I {get { return _instance; }}

    #endregion
    
    public List<ItemDPool> itemsPool;
    private Dictionary <string, Queue<GameObject>> poolDictionary;

    private void Awake()
    {
        #region Singleton

        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }

        #endregion

        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (ItemDPool item in itemsPool)
        {
            if (item != null)
            {
                Queue<GameObject> pool = new Queue<GameObject>();
                GameObject miniPool = new GameObject(item.tagItem);
                miniPool.transform.SetParent(transform);
            
                for (int i = 0; i < item.amount; i++)
                {
                    GameObject itemPool = SpawnItem(item);
                    itemPool.transform.SetParent(miniPool.transform);
                    pool.Enqueue(itemPool);
                }
            
                poolDictionary.Add(item.tagItem, pool);
            }
        }
    }

    private GameObject SpawnItem(ItemDPool item)
    {
        GameObject itemPool = Instantiate(item.prefab, transform, true);

        itemPool.AddComponent<TagPoolerObject>();
        itemPool.GetComponent<TagPoolerObject>().tagItem = item.tagItem;
        itemPool.SetActive(false);

        return itemPool;
    }

    public GameObject GetObject(string tagItem, Vector3 position, Quaternion rotation , bool active = true)
    {
        if (!poolDictionary.ContainsKey(tagItem))
        {
            Debug.LogWarning("Pool with tag " + tagItem + " doens't exist.");
            return null;
        }
        
        GameObject objPooled;
        
        if (poolDictionary[tagItem].Count > 0)
        {
            objPooled = poolDictionary[tagItem].Dequeue();
            objPooled.transform.position = position;
            objPooled.transform.rotation = rotation;
            
            objPooled.SetActive(active);
            return objPooled;
        }
        else
        {
            ItemDPool itemPool = GetItemPool(tagItem);

            if (itemPool != null)
            {
                if (itemPool.isExpandable)
                {
                    Debug.Log("Expanding pool <color=red>" + tagItem + "</color>");
                    
                    
                    GameObject newItemPool = SpawnItem(itemPool);
                    newItemPool.transform.position = position;
                    newItemPool.transform.rotation = rotation;
                    newItemPool.SetActive(active);
                    
                    
                    Transform[] children = gameObject.GetComponentsInChildren<Transform>();
                    Transform parent = null;
                    
                    foreach (Transform trans in children)
                    {
                        if (trans.name == tagItem)
                        {
                            parent = trans;
                            break;
                        }
                    }

                    if (parent == null)
                    {
                        parent = new GameObject(itemPool.tagItem).transform;
                        parent.transform.SetParent(transform);
                    }
                    
                    newItemPool.transform.SetParent(parent);
                    return newItemPool;
                }
                else
                {
                    Debug.LogWarning("Object isn't expandable.");
                    return null;
                }
            }
            else
            {
                Debug.LogWarning("Pool with tag " + tagItem + "doens't exist.");
                return null;
            }
        }
    }

    public void ReturnObject(GameObject toReturn)
    {
        TagPoolerObject tagPoolerObject = toReturn.GetComponent<TagPoolerObject>();

        if (tagPoolerObject != null)
        {
            if (poolDictionary.ContainsKey(tagPoolerObject.tagItem))
            {
                toReturn.SetActive(false);
                poolDictionary[tagPoolerObject.tagItem].Enqueue(toReturn);
            }
            else
            {
                Debug.LogWarning(toReturn.name + " was returned to a pool it wasn't spawned from! Destroying.");
                Destroy(toReturn);
            }
        }
        else
        {
            Debug.LogWarning(toReturn.name + " was returned to a pool it wasn't spawned from! Destroying.");
            Destroy(toReturn);
        }
    }

    private ItemDPool GetItemPool(string tagItem)
    {
        ItemDPool item = null;
        
        foreach (ItemDPool itemTemp in itemsPool)
        {
            if (itemTemp != null)
            {
                if (tagItem.Equals(itemTemp.tagItem))
                {
                    item = itemTemp;
                    break;
                }
            }
        }

        return item;
    }
    
}    
    public class TagPoolerObject: MonoBehaviour
    {
        public string tagItem;
    }
}

