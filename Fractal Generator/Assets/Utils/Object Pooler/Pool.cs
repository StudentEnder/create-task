using System.Collections.Generic;
using UnityEngine;

// Inspired by SebLague's ObjectPooler: https://github.com/SebLague/Super-Chore-Man/blob/main/Assets/Scripts/Pool.cs
// and Unity's pool functionality

/// <summary>
/// Object Pooler. Used for more efficiently re-using objects by enabling and disabling them instead than instantiating and destroying new objects each time.
/// </summary>
/// <typeparam name="T"></typeparam>
public class Pool<T> where T: PooledItem
{
    /// <summary>
    /// Queue list of objects available to be (re)used from the pool. <br />
    /// Queues use first-in, first out structure, meaning the first/oldest object enqueued (first-in) is the first object dequeued (first-out) <br />
    /// </summary>
    private Queue<T> availableObjects;

    /// <summary>
    /// Whether or not there are objects available in the pool
    /// </summary>
    public bool IsAvailable
    {
        get
        {
            return availableObjects.Count > 0;
        }
    }

    /// <summary>
    /// the holder gameobject contains all pooled items as children
    /// </summary>
    private GameObject holder;

    /// <summary>
    /// The gameobject prefab of PooledItem, for instantiating new objects.
    /// </summary>
    private T prefab;

    /// <summary>
    /// Total amount of PoolItem objects that have been pooled (available or in use).
    /// </summary>
    private int poolSize;

    /// <summary>
    /// Creates a new Object Pool, storing re-usable, instantiated <see cref="PooledItem"/>s.
    /// </summary>
    /// <param name="prefab">The prefab to instantiate from. Must be <see cref="PooledItem"/></param>
    /// <param name="poolSize">The amount of objects to store in the pool.</param>
    public Pool(T prefab, int poolSize)
    {
        this.prefab = prefab;
        this.poolSize = 0; // set to 0 at first because ExpandPool adds the poolSize

        availableObjects = new Queue<T>();
        holder = new GameObject($"Pool ({typeof(T)})");

        ExpandPool(poolSize);
    }

    /// <summary>
    /// Gets an object from the pool after <see cref="PooledItem.Init">initializing</see> it.
    /// </summary>
    public T Get()
    {
        T entity = availableObjects.Dequeue(); // removes the object from the available queue.
        entity.Init(); // initializes the object with values (defined at PooledItem definition of Init)

        return entity;
    }

    /// <summary>
    /// Expands the pool, instantiating new PooledItems into the pool. <br />
    /// WARNING: This is a computationally costly action. If possible, determine max amount of pooled objects ahead of time (in Pool construction) and cycle between those.
    /// </summary>
    /// <param name="AmountOfObjectsToAdd">Amount of PooledItem objects to add to the pool.</param>
    public void ExpandPool(int AmountOfObjectsToAdd)
    {
        poolSize += AmountOfObjectsToAdd;
        for (int i = 0; i < AmountOfObjectsToAdd; i++)
        {
            // Instantiates a new PooledItem, placing it into the pool with pooling functionality (it is disabled, and can enter and exit pool)
            T entity = GameObject.Instantiate(prefab);
            entity.transform.SetParent(holder.transform);
            entity.OnRelease += (x) => availableObjects.Enqueue(x as T);
            availableObjects.Enqueue(entity);
            entity.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Reduces the pool, destroying available and stored PooledItems. <br />
    /// WARNING: This is a computationally costly action. It is likely better to leave the available pool at its current size, unless memory is extremely limited.
    /// </summary>
    /// <param name="AmountOfObjectsToDestroy">Amount of PooledItem objects to destroy from the available pool</param>
    public void ReducePool(int AmountOfObjectsToDestroy)
    {
        poolSize -= AmountOfObjectsToDestroy;
        if (poolSize < 0)
        {
            Debug.LogWarning($"{this}: You are trying to reduce the pool past the available PooledItems! poolSize: {poolSize}");
        }

        for (int i = 0; i < AmountOfObjectsToDestroy; i++)
        {
            T entity = availableObjects.Dequeue();
            GameObject.Destroy(entity);
        }
    }

}

public abstract class PooledItem : MonoBehaviour
{
    public event System.Action<PooledItem> OnRelease;

    /// <summary>
    /// Initialization function, called when object exits available pool and is used.
    /// </summary>
    public abstract void Init();

    /// <summary>
    /// Reset function, called when object enters available pool, to reset values.
    /// </summary>
    protected abstract void Reset(); 

    /// <summary>
    /// Releases the object back into the pool. Do this instead of gameobject destruction.
    /// </summary>
    protected void ReleasePooled()
    {
        Reset(); // reset values of object so they don't persist when re-used.
        gameObject.SetActive(false); // disable gameobject for storage
        OnRelease?.Invoke(this); // invokes OnRelease event, to tell when this PooledObject should return to the available pool.
    }
}
