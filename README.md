# object-pool
This is a small utility for Unity games: 
[Object Pool](https://en.wikipedia.org/wiki/Object_pool_pattern), which can spawn and retrieve prefab instances.

## usage
1. Add `ObjectPool` component to any object in your scene.
2. Use `ObjectPool.Rent(...)` methods to spawn new pooled instances of prefabs.
3. Use `ObjectPool.Return(...)` methods to return them to the pool once they are no longer in use.
4. If you happen to forget (3), pooled instances will auto-return themselves after the time you set in (2).
