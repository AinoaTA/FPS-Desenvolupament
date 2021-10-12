using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    private Animator door;
    private void Awake()
    {
        door = GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            door.SetBool("Open", true);
    }

    private void OnTriggerExit(Collider other)
    {
          door.SetBool("Open", false);
    }
}
