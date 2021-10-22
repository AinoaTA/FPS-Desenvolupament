using System.Collections;
using UnityEngine;

public class MusicControllerFPS : MonoBehaviour
{
    static MusicControllerFPS m_MusicController;
    public AudioSource Player;
    public AudioSource Gate;
    
   //public AudioSource[] Drone;

    public AudioClip ShootClip;
    public AudioClip ShootFallClip;
    public AudioClip DoorGateClip;

    public AudioClip DroneClip;

    private void Awake()
    {
        m_MusicController = this;
    }
    static public MusicControllerFPS GetMusicController()
    {
        return m_MusicController;
    }

    public void GateOpen()
    {
        Gate.clip = DoorGateClip;
        Gate.PlayOneShot(Gate.clip);
    }

    public void PlayerShoot()
    {
        Player.clip = ShootClip;
        Player.Play();
        StartCoroutine(GetFallBullet());
    }

    private IEnumerator GetFallBullet()
    {
        yield return new WaitForSeconds(0.5f);

        Player.clip = ShootFallClip;
        Player.Play();
    }

}
