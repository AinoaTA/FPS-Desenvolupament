using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    public List<GameObject> m_Drops;
    private int nDrop;
    private int nNumber;
    public GameObject Destroy;
    
    public void DropItem()
    {
        if (RandomDrop())
        {
            GameObject dropInstance = Instantiate(m_Drops[nDrop], transform.position, Quaternion.identity, Destroy.transform);
            dropInstance.GetComponent<Rigidbody>().AddForce(Vector3.right, ForceMode.Impulse);
        }
    }

    private bool RandomDrop()
    {
        nDrop = Random.Range(0, m_Drops.Count);
        nNumber = Random.Range(0, 100);

        if (nNumber <= 75)
            return true;
        else
            return false;
    }
}
