using System.Collections;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    private float destroyTime = 3f;
    void Start()
    {
        StartCoroutine(DestroyObjectOnTimeFun());
    }

    IEnumerator DestroyObjectOnTimeFun()
    {
        yield return new WaitForSeconds(destroyTime);
        GameObject.Destroy(gameObject);
    }
}
