using System.Collections.Generic;
using UnityEngine;


namespace LuckyGamezLib {
    public static class CentralObjectPooler {


        private static Dictionary<string, Queue<GameObject>> objectPoolsDictionary = new Dictionary<string, Queue<GameObject>>();


        /// <summary>
        /// Creates a new pool to use for objects
        /// </summary>
        /// <param name="poolId">The id of the pool you want to use</param>
        /// <param name="objectsToPool"></param>
        public static void CreateNewPool(string poolId, GameObject[] objectsToPool) {
            if (objectPoolsDictionary.ContainsKey(poolId)) {
                // Return if the poolDictionary already has this pool
                Debug.LogWarning("There's already a object pool with this id! poolId: " + poolId);

                return;
            }

            Queue<GameObject> objectPoolQueue = new Queue<GameObject>();

            foreach (GameObject obj in objectsToPool) {
                // Enqueue the object to later use
                objectPoolQueue.Enqueue(obj);

                // Set the object inactive so that it isn't visible in-game
                obj.SetActive(false);
            }

            // Add the key and gameObjects to the dictionary it self
            objectPoolsDictionary.Add(poolId, objectPoolQueue);
        }

        /// <summary>
        /// Adds object(s) to a given object pool queue
        /// </summary>
        /// <param name="poolId">The id of the pool you want to use</param>
        /// <param name="objectsToAdd"></param>
        public static void AddObjectsToPool(string poolId, GameObject[] objectsToAdd) {
            if (!objectPoolsDictionary.ContainsKey(poolId)) {
                // Return if the dictionary with this id doesn't yet exist
                Debug.LogWarning("You're trying to add objects to a pool which doesn't exist! Pool id: " + poolId);

                return;
            }

            Queue<GameObject> objectPoolQueue = objectPoolsDictionary[poolId];

            foreach (GameObject obj in objectsToAdd) {
                // Enqueue the object for later use
                objectPoolQueue.Enqueue(obj);

                // Set the object inactive so that it isn't visible in-game
                obj.SetActive(false);
            }
        }

        /// <summary>
        /// Use an item from the given queue to use in-game
        /// </summary>
        /// <param name="poolId">The id of the pool you want to use</param>
        /// <param name="positionToUse"></param>
        /// <param name="rotationToUse"></param>
        public static GameObject UseObjectFromPool(string poolId, Vector3 positionToUse, Quaternion rotationToUse) {
            if (!objectPoolsDictionary.ContainsKey(poolId)) {
                // Return if the pool dictionary doens't have a pool with the given id
                Debug.LogWarning("You're trying to get and use an object from a pool which doesn't exist Pool id: " + poolId);

                return null;
            }

            Queue<GameObject> objectPoolQueue = objectPoolsDictionary[poolId];

            if (objectPoolQueue.Count == 0) {
                // Return if the pool doesn't have any objects in it
                Debug.LogWarning("There were no objects in this pool to use! Pool id:" + poolId);

                return null;
            }

            // Remove the object that needs to be used from the queue
            GameObject dequeuedObject = objectPoolQueue.Dequeue();

            // Set the object that we dequeued active so it's visible in-game
            dequeuedObject.SetActive(true);
            dequeuedObject.transform.position = positionToUse;
            dequeuedObject.transform.rotation = rotationToUse;

            return dequeuedObject;
        }

        /// <summary>
        /// Return a object that was used from a queue
        /// </summary>
        /// <param name="poolId">The id of the pool you want to use</param>
        /// <param name="gameObjectToReturn">The copy of the object you want to return</param>
        public static void ReturnObjectToPool(string poolId, GameObject gameObjectToReturn) {
            if (!objectPoolsDictionary.ContainsKey(poolId)) {
                // Return if the pool dictionary doen't have a pool with the given id
                Debug.LogWarning("You're trying to add an object back to a non existing pool! Pool id: " + poolId);

                return;
            }

            Queue<GameObject> objectPoolQueue = objectPoolsDictionary[poolId];

            // Add the object back to the queue
            objectPoolQueue.Enqueue(gameObjectToReturn);

            // Deactivate the object so it's not visible in-game
            gameObjectToReturn.SetActive(false);
        }

        /// <summary>
        /// Removes a certain amount of object from the wanted pool. NOTE: This function DOES destroy these objects aswell
        /// </summary>
        /// <param name="poolId">The id of the pool you want to use</param>
        /// <param name="amountToRemove">The amount of objects you want to remove from this pool type</param>
        public static void RemoveObjectsFromPool(string poolId, int amountToRemove) {
            if (!objectPoolsDictionary.ContainsKey(poolId)) {
                // Return if the pool doesn't exist with the given id
                Debug.LogWarning("The pool you're trying to remove objects from doesn't exist with the given key! Given key: " + poolId);

                return;
            }

            Queue<GameObject> objectPoolQueue = objectPoolsDictionary[poolId];

            if (objectPoolQueue.Count == 0) {
                // Return if the pool doesn't have any objects in it
                Debug.LogWarning("There were no objects in this pool to remove! Pool id:" + poolId);

                return;
            }

            // Check if the user is trying to remove more than what's available
            if (amountToRemove > objectPoolQueue.Count) {
                Debug.LogWarning("You're trying to remove more objects than this pool has. Removing everything from this pool");

                amountToRemove = objectPoolQueue.Count;
            }

            // Loop the index amount to destroy and dequeue the objects from the queue
            for (int i = amountToRemove - 1; i >= 0; i--) {
                GameObject dequeuedObject = objectPoolQueue.Dequeue();

                UnityEngine.Object.Destroy(dequeuedObject);
            }

            // No need for re-adding the queue to the dictionary because we're getting a copy
        }

        public static bool DoesObjectPoolExist(string poolId) {
            return objectPoolsDictionary.ContainsKey(poolId);
        }

        public static int GetObjectPoolCountFromId(string poolId) {
            Queue<GameObject> objectPoolQueue = objectPoolsDictionary[poolId];

            if (objectPoolQueue.Count == 0) {
                Debug.LogWarning("You're trying to get the count of a object pool which is non-existent! Pool id: " + poolId);
            }

            return objectPoolQueue.Count;
        }

        public static void DestroyObjectPool(string poolId) {
            if (!objectPoolsDictionary.ContainsKey(poolId)) {
                // Return if the pool doesn't exist
                Debug.LogWarning("The pool you're tyring to acces and delete doesn't exist");

                return;
            }

            Queue<GameObject> objectPoolQueue = objectPoolsDictionary[poolId];

            for (int i = objectPoolQueue.Count - 1; i >= 0; i--) {
                GameObject dequeuedObject = objectPoolQueue.Dequeue();

                UnityEngine.Object.Destroy(dequeuedObject);
            }

            objectPoolsDictionary.Remove(poolId);
        }
    }
}