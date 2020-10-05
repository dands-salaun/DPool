using System.Collections;
using UnityEngine;

namespace Dands.Pool.Demo
{
    public class AutoDestroy : MonoBehaviour
    {
        private float timeDestroy = 5f;

        private void OnEnable()
        {
            StartCoroutine(DelayDestroy());
        }

        private IEnumerator DelayDestroy()
        {
            yield return new WaitForSeconds(timeDestroy);
            DPool.I.ReturnObject(gameObject);
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }
    }
}