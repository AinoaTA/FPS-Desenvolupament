using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    public GameObject PrefabToDrop;
    private int nDrop;
    
    public void DropItem()
    {
        if (RandomDrop())
        {
            GameObject dropInstance = Instantiate(PrefabToDrop, transform.position, Quaternion.identity);
            dropInstance.GetComponent<Rigidbody>().AddForce(Vector3.right, ForceMode.Impulse);
        }
    }

    private bool RandomDrop()
    {
        nDrop = Random.Range(0, 100);
        if (nDrop <= 75)
            return true;
        else
            return false;
    }
}
