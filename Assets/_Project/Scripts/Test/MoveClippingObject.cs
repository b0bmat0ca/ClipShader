using System.Collections;
using UnityEngine;

namespace b0bmat0ca.Test
{
    public class MoveClippingObject : MonoBehaviour
    {
        private void Start()
        {
            StartCoroutine(MoveCoroutine());
        }
        
        private IEnumerator MoveCoroutine()
        {
            Vector3 startPos = new Vector3(0, 0, -1f);
            Vector3 endPos = startPos + new Vector3(0, 0, 1f);
            float duration = 8f;
            float elapsed = 0f;

            while (elapsed < duration)
            {
                transform.position = Vector3.Lerp(startPos, endPos, elapsed / duration);
                elapsed += Time.deltaTime;
                yield return null;
            }

            transform.position = endPos;

            elapsed = 0f;
            while (elapsed < duration)
            {
                transform.position = Vector3.Lerp(endPos, startPos, elapsed / duration);
                elapsed += Time.deltaTime;
                yield return null;
            }

            transform.position = startPos;

            StartCoroutine(MoveCoroutine());
        }
    }
}
