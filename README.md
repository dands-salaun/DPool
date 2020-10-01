# DPool
 Simple pool of GameObjects Unity (prefabs)

# Install

Open the Package Manager and add a new package like git URL.
Link: ```https://github.com/dands-salaun/DPool.git```

![Install-1](https://github.com/dands-salaun/DPool/blob/main/Documentation/Install%20-%201.png)
![Install-2](https://github.com/dands-salaun/DPool/blob/main/Documentation/Instal%20-%202.png)
![Install-3](https://github.com/dands-salaun/DPool/blob/main/Documentation/Install%20-%203.png)

### Or
Find *Packages/manifest.json* in your project and edit it to look like this:
```
{
  "dependencies": {
    "dands.dpool": "https://github.com/dands-salaun/DPool.git",
    ...
  },
}
```

# Usage
Create an *ScriptableObject* for each prefab you will use in DPool.

![Usage-1](https://github.com/dands-salaun/DPool/blob/main/Documentation/Usage%20-%201.png)

You must assign a tag for each item in the pool, a prefab, the number of items that will be created at startup and mark whether the pool of this object is spanish or not.

![Usage-2](https://github.com/dands-salaun/DPool/blob/main/Documentation/Usage%20-%202.png)

You will need to assign the DPool script to an empty object in the scene. And add the *ScriptableObject* already created.

![Usage-3](https://github.com/dands-salaun/DPool/blob/main/Documentation/Usage%20-%203.png)

## In the scripts

### ```GetObject``` 
To use an GameObject (prefab)
```csharp
public GameObject GetObject(string tagItem, Vector3 position, Quaternion rotation , bool active = true)
```
Usage
```csharp
    GameObject newObject = DPool.I.GetObject("TagItemDPool", transform.position.position, Quaternion.identity);
```

### ```ReturnObject``` 
Return an GameObject (prefab) to the pool.
```csharp
public void ReturnObject(GameObject toReturn)
```
Usage
```csharp
DPool.I.ReturnObject(gameObject);
```

## Utils

You can use an interface to create a pattern in the scripts of GameObjects.

```csharp
public interface IDPool
{
    void Init();
    void Return();
}   
```
