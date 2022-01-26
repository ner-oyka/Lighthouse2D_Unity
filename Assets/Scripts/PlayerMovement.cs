using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using EventBusSystem;

public class PlayerMovement : MonoBehaviour, IPlayerInputHandler
{
    [Header("Movement")]
    public float WalkSpeed = 5f;
    public float SprintSpeed = 10f;
    public float RotateSpeed = 7.0f;

    [Header("Camera Offset")]
    public Cinemachine.CinemachineVirtualCamera VirtualCamera;
    public float DistanceToCameraOffset = 5.0f;
    public float MaxDistanceCameraOffset = 7.0f;
    public float SpeedCameraOffset = 5.0f;

    private float m_cachedDistanceToCamera;

    private Rigidbody2D m_Rigidbody2D;
    private Camera m_mainCamera;
    private float m_CurrentSpeed;

    private Vector2 m_MovementDirection;
    private Vector2 m_MousePosition;

    private Cinemachine.CinemachineTransposer m_CameraTransposer;

    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_mainCamera = Camera.main;
        m_CurrentSpeed = WalkSpeed;

        m_CameraTransposer = VirtualCamera.GetCinemachineComponent<Cinemachine.CinemachineTransposer>();
        m_cachedDistanceToCamera = 999;
    }

    private void OnEnable()
    {
        EventBus.Subscribe(this);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe(this);
    }

    private void FixedUpdate()
    {
        m_MovementDirection = PlayerInputManager.instance.MovementDirection;
        m_MousePosition = PlayerInputManager.instance.MousePosition;

        var _mousePosition = m_mainCamera.ScreenToWorldPoint(m_MousePosition);
        _mousePosition.z = 0;

        //m_Rigidbody2D.velocity = m_MovementDirection * m_CurrentSpeed * Time.fixedDeltaTime;
        m_Rigidbody2D.velocity = (m_mainCamera.transform.right * m_MovementDirection.x + m_mainCamera.transform.up * m_MovementDirection.y) * m_CurrentSpeed * Time.fixedDeltaTime;


        Quaternion _targetRotation = Quaternion.LookRotation(Vector3.forward, _mousePosition - transform.position);


        m_cachedDistanceToCamera = DistanceToCameraOffset;

        if (m_Rigidbody2D.velocity.magnitude > 3.1f)
        {
            _targetRotation = Quaternion.LookRotation(Vector3.forward, m_MovementDirection);
            m_cachedDistanceToCamera = 999;
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, _targetRotation, Time.fixedDeltaTime * RotateSpeed);

        //Camera offset
        if (Vector3.Distance(transform.position, _mousePosition) > m_cachedDistanceToCamera)
        {
            Vector3 _camPosTarget = (transform.position - (transform.up * m_cachedDistanceToCamera) + _mousePosition) * 0.5f;
            m_CameraTransposer.m_FollowOffset = Vector3.Lerp(m_CameraTransposer.m_FollowOffset,
                Vector3.ClampMagnitude(_camPosTarget - transform.position, MaxDistanceCameraOffset), Time.deltaTime * SpeedCameraOffset);
        }
        else
        {
            m_CameraTransposer.m_FollowOffset = Vector3.Lerp(m_CameraTransposer.m_FollowOffset, Vector3.zero, Time.deltaTime * SpeedCameraOffset);
        }
        m_CameraTransposer.m_FollowOffset.z = -100;
    }

    private void StartSprint()
    {
        m_CurrentSpeed = SprintSpeed;
    }

    private void StopSprint()
    {
        m_CurrentSpeed = WalkSpeed;
    }

    public void OnStartSprint()
    {
        StartSprint();
    }

    public void OnStopSprint()
    {
        StopSprint();
    }

    public void OnInventory()
    {

    }

    public void OnFlashlight()
    {

    }
}
