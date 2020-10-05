using System.Collections;
using UnityEngine;

namespace Dands.Pool.Demo
{
    public class DemoDPool : MonoBehaviour
    {
        public float delayCreateNewObject = 1f;
        public Transform positionCreateNewObject;
        public string[] tagOfObjectsInThePool;

        private void Start()
        {
            StartCoroutine(SpawnObject());
        }

        private IEnumerator SpawnObject()
        {
            yield return new WaitForSeconds(delayCreateNewObject);

            GameObject newObject =
                DPool.I.GetObject(RaffleObject(), positionCreateNewObject.position, Quaternion.identity);

            StartCoroutine(SpawnObject());

        }

        private string RaffleObject()
        {
            string raffleTag = "";

            if (tagOfObjectsInThePool.Length > 0)
            {
                raffleTag = tagOfObjectsInThePool[Random.Range(0, tagOfObjectsInThePool.Length)];
            }

            return raffleTag;
        }
    }
}