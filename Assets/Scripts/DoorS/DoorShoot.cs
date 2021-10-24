using UnityEngine;

public class DoorShoot : MonoBehaviour
{
    private Animator animator;
    public bool Open=false;

    static DoorShoot Gate;

    static public DoorShoot GetGate() => Gate;
    private void OnEnable()
    {
        ShooterPoints.delegateGate += OpenGate;
    }

    private void OnDisable()
    {
        ShooterPoints.delegateGate -= OpenGate;
    }
    private void Awake()
    {
        animator = GetComponent<Animator>();
        Gate = this;
    }
    public void OpenGate()
    {
        Open = true;
        animator.SetBool("Open", true);
        MusicControllerFPS.GetMusicController().GateOpen();
    }

    public void ResetGate()
    {
        Open = false;
        animator.SetBool("Open", false);
        animator.SetBool("Close", true);
    }
}
