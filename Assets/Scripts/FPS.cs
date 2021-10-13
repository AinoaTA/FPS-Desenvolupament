using UnityEngine;

public class FPS : MonoBehaviour
{
    private float m_Yaw;
    private float m_Pitch;

    public Transform m_PitchControllerTransform;
    public float m_YawRotationalSpeed = 360.0f;
    public float m_PitchRotationalSpeed = -180.0f;
    public float m_MinPitch = -80.0f;
    public float m_MaxPitch = 50.0f;
    public bool m_InvertedYaw = false;
    public bool m_InvertedPitch = true;

    public ShootGun ShootGun;
    public Camera PCamera;
    public GameObject PrefabParticle;
    private CharacterController m_CharacterController;

    public float m_Speed = 10.0f;
    public float m_FastSpeedMultiplier = 3f;
    public float m_JumpSpeed = 10.0f;
    public KeyCode m_LeftKeyCode = KeyCode.A;
    public KeyCode m_RightKeyCode = KeyCode.D;
    public KeyCode m_UpKeyCode = KeyCode.W;
    public KeyCode m_DownKeyCode = KeyCode.S;
    public KeyCode m_RunKeyCode = KeyCode.J;
    public KeyCode m_JumpKeyCode = KeyCode.Space;
    public KeyCode m_DebugLockAngleKeyCode = KeyCode.I;
    public KeyCode m_DebugLockKeyCode = KeyCode.O;

    public bool CanShootBool = true;

    private float touchingGroundValue = 0.5f;
    private float m_VerticalSpeed = 0.0f;
    private float touchingGround = 0.5f; //initial value
    private bool m_OnGround = false;

    private PlayerState playerState;

    public delegate void DelegateShoot();
    public static DelegateShoot delegateShoot;
    
    bool CanShoot => PlayerState.PlayerStateMode == PlayerState.PlayerMode.Idle;
    bool isChargerEmpty => ShootGun.CurrentBullets == 0;
    
    void Awake()
    {
        playerState = GetComponent<PlayerState>();
        m_Yaw = transform.rotation.eulerAngles.y;
        m_CharacterController = GetComponent<CharacterController>();
        m_Pitch = m_PitchControllerTransform.localRotation.eulerAngles.x;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        if (CanShoot)
        {
            if (Input.GetMouseButtonDown(0) && !isChargerEmpty)
                playerState.UpdateShoot(PlayerState.PlayerMode.Shooting);
            if (Input.GetKeyDown(KeyCode.R))
                playerState.UpdateShoot(PlayerState.PlayerMode.Charging);
        }

        PlayerCamera();
        PlayerMovement();
    }
    private void PlayerCamera()
    {
        float l_MouseAxisY = Input.GetAxis("Mouse Y");
        m_Pitch += l_MouseAxisY * m_PitchRotationalSpeed * Time.deltaTime * (m_InvertedPitch ? -1.0f : 1.0f);
        float l_MouseAxisX = Input.GetAxis("Mouse X");
        m_Yaw += l_MouseAxisX * m_YawRotationalSpeed * Time.deltaTime * (m_InvertedYaw ? -1.0f : 1.0f);
        m_Pitch = Mathf.Clamp(m_Pitch, m_MinPitch, m_MaxPitch);

        transform.rotation = Quaternion.Euler(0.0f, m_Yaw, 0.0f);
        m_PitchControllerTransform.localRotation = Quaternion.Euler(m_Pitch, 0.0f, 0.0f);
    }
    private void PlayerMovement()
    {
        float l_YawInRadians = m_Yaw * Mathf.Deg2Rad;
        float l_Yaw90InRadians = (m_Yaw + 90.0f) * Mathf.Deg2Rad;
        Vector3 l_Forward = new Vector3(Mathf.Sin(l_YawInRadians), 0.0f, Mathf.Cos(l_YawInRadians));
        Vector3 l_Right = new Vector3(Mathf.Sin(l_Yaw90InRadians), 0.0f, Mathf.Cos(l_Yaw90InRadians));
        Vector3 l_Movement = Vector3.zero;

        if (Input.GetKey(m_UpKeyCode))
            l_Movement = l_Forward;
        else if (Input.GetKey(m_DownKeyCode))
            l_Movement = -l_Forward;
        if (Input.GetKey(m_RightKeyCode))
            l_Movement += l_Right;
        else if (Input.GetKey(m_LeftKeyCode))
            l_Movement -= l_Right;


        float l_SpeedMultiplier = 1.0f;
        if (Input.GetKey(m_RunKeyCode))
            l_SpeedMultiplier = m_FastSpeedMultiplier;

        l_Movement.Normalize();
        l_Movement = l_Movement * Time.deltaTime * m_Speed * l_SpeedMultiplier;
        l_Movement.y = m_VerticalSpeed * Time.deltaTime;
        m_VerticalSpeed += Physics.gravity.y * Time.deltaTime;

        Gravity(l_Movement);
    }

    private void Gravity(Vector3 movement)
    {
        CollisionFlags l_CollisionFlags = m_CharacterController.Move(movement);
        if ((l_CollisionFlags & CollisionFlags.Below) != 0)
        {
            touchingGround += Time.deltaTime;
            m_OnGround = true;
            m_VerticalSpeed = 0.0f;
        }
        else
            m_OnGround = false;
        if ((l_CollisionFlags & CollisionFlags.Above) != 0 && m_VerticalSpeed > 0.0f)
            m_VerticalSpeed = 0.0f;

        if (touchingGround > touchingGroundValue && m_OnGround && Input.GetKeyDown(m_JumpKeyCode))
        {
            m_VerticalSpeed = m_JumpSpeed;
            touchingGround = 0f;
        }
    }
}
