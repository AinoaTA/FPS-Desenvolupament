using UnityEngine;

public class DoorShoot : MonoBehaviour
{
    private Animator animator;
    private void OnEnable()
    {
        ShooterPoints.delegateGate += OpenGate;
    }

    private void OnDisable()
    {
        ShooterPoints.delegateGate -= OpenGate;
    }
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void OpenGate()
    {
        animator.SetBool("Open", true);
    }
}
