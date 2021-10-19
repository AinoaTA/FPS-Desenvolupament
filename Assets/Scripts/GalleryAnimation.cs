using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalleryAnimation : MonoBehaviour
{

    private Animation animation;
    public AnimationClip Idle, Shot, Fall;

    private float timer;
    public enum AnimationStates
    {
        Idle,
        Shot,
        Fall,
    }

    //algunas se moverán y otras no.
    public AnimationStates m_DefaultState; //aquí setearemos cual de ellas
    private AnimationStates m_CurrentState;

    private void Awake()
    {
        animation = GetComponent<Animation>();
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
                UpdateFall();
                break;
            default:
                break;
        }
        print(gameObject.name + " "+m_CurrentState);
    } 

    //SETS
    public void SetIdle() {m_CurrentState = AnimationStates.Idle; }
    public void SetShot() 
    { 
        m_CurrentState = AnimationStates.Shot; 
       
    }
    public void SetFall() { m_CurrentState = AnimationStates.Fall; }


    //UPDATES
    private void UpdateIdle() { animation.CrossFade("Idle"); }
    private void UpdateShot() 
    {
        print("a");
        animation.CrossFade("Shot");
        timer += Time.deltaTime;
        if (timer > 2)
            animation.CrossFadeQueued("Fall");
    }
    private void UpdateFall() { animation.CrossFade("Fall"); }

}
