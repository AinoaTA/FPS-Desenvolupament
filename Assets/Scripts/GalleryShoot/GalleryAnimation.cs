using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalleryAnimation : MonoBehaviour
{
    private Animator anim;

    private float timer;
    private float timerRevive;

    public delegate void DelegateStartGallery();
    public static DelegateStartGallery startGallery;

    public int m_Points;
    public enum AnimationStates
    {
        Idle,
        Shot,
        Fall,
        Up,
        Moved,
    }

    //algunas se moverán y otras no.
    public AnimationStates m_DefaultState; //aquí setearemos cual de ellas
    private AnimationStates m_CurrentState;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        m_CurrentState = AnimationStates.Idle;

        if (m_DefaultState != m_CurrentState)
            m_CurrentState = m_DefaultState;
    }

    private void Update()
    {
        UpdateAnimation();
    }
    private void UpdateAnimation()
    {

        switch (m_CurrentState)
        {
            case AnimationStates.Idle:
                UpdateIdle();
                break;
            case AnimationStates.Shot:
                UpdateShot();
                break;
            case AnimationStates.Fall:
                StartCoroutine(UpdateFall());
                break;
            case AnimationStates.Up:
                StartCoroutine(UpdateRevive());
                break;
            case AnimationStates.Moved:
                UpdateMoved();
                break;
            default:
                break;
        }

    }

    //SETS
    public void SetIdle() { m_CurrentState = AnimationStates.Idle; }
    public void SetShot()
    {
        startGallery?.Invoke();
        m_CurrentState = AnimationStates.Shot;

    }
    public void SetFall() { m_CurrentState = AnimationStates.Fall; }
    public void SetRevive() { m_CurrentState = AnimationStates.Up; }
    public void SetMoved() { m_CurrentState = AnimationStates.Moved; }


    //UPDATES
    private void UpdateIdle()
    {
        anim.SetBool("Idle", true);
        if (m_DefaultState != AnimationStates.Idle)
            SetMoved();

    }
    private void UpdateMoved()
    {
        anim.SetBool("Idle", false);
        anim.SetBool("Moved", true);
    }
    private void UpdateShot()
    {
        anim.SetBool("Idle", false);
        anim.SetBool("Shot", true);
        anim.SetBool("Moved", false);
        timer += Time.deltaTime;

        if (timer > 2)
        {
            anim.SetBool("Shot", false);
            SetFall();
            timer = 0f;
        }
    }
    private IEnumerator UpdateFall()
    {
        anim.SetBool("FallBool", true);
        yield return new WaitForSeconds(2f);
        anim.SetBool("FallBool", false);
        SetRevive();
    }

    private IEnumerator UpdateRevive()
    {
        timerRevive += Time.deltaTime;

        if (timerRevive > 5)
        {
            anim.SetBool("UpBool", true);
            timerRevive = 0f;
            yield return new WaitForSeconds(1f);
            anim.SetBool("UpBool", false);
            SetIdle();

        }
    }
}
