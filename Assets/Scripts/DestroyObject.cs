using System.Collections;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    private float destroyTime = 3f;
    static public DestroyObject m_DestroyObject;

    void Start()
    {
        StartCoroutine(DestroyObjectOnTimeFun());
    }


    public void Restart()
    {
        StartCoroutine(DestroyObjectOnTimeFun());

       
    }
    IEnumerator DestroyObjectOnTimeFun()
    {
        yield return new WaitForSeconds(destroyTime);
        GameObject.Destroy(gameObject);
    }
}
