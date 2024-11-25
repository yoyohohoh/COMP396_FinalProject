using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class NPCController : MonoBehaviour
{
    #region FSM properties
    [Header("Energy")]
    public NPCEnergy NPCEnergy;
    [SerializeField] public float energyLevel;
    [SerializeField] public float idleStrength;
    [SerializeField] public float patrolStrength;
    [SerializeField] public float attackStrength;
    NPCStateMachine fsm;
    public bool isAlert = false;
    #endregion

    [Header("Sensor")]
    #region Sense properties
    public GameObject goPlayer;
    public bool isVisible = false;
    public float detectionRate = 1.0f;
    private float elapsedTime = 0.0f;
    #endregion

    #region Sight properties
    public int FieldOfView = 45;
    public int ViewDistance = 5;
    private Transform playerTrans;
    private Vector3 rayDirection;
    #endregion

    [Header("Patrol Waypoints")]
    public bool isPatrol = false;
    PathFollowing pathFollowing;

    void Start()
    {
        if (goPlayer == null)
        {
            goPlayer = GameObject.FindGameObjectWithTag("Player");
        }
        playerTrans = goPlayer.transform;
        
        
    }

    void Update()
    {
        NPCEnergy = this.gameObject.GetComponent<NPCEnergy>();
        energyLevel = NPCEnergy.energyLevel;
        idleStrength = NPCEnergy.idleStrength;
        patrolStrength = NPCEnergy.patrolStrength;
        attackStrength = NPCEnergy.attackStrength;

        fsm = this.gameObject.GetComponent<NPCStateMachine>();
        if (!isVisible)
        {
            if (energyLevel != 0)
            {
                if (energyLevel > 0.3f)
                {
                    fsm.ChangeState("Patrol");
                }
                else if (energyLevel <= 0.3f)
                {
                    fsm.ChangeState("Idle");
                }
            }
        }

        if (isAlert)
        {
            UpdateSense();
        }

        pathFollowing = this.gameObject.GetComponent<PathFollowing>();
        pathFollowing.enabled = isPatrol;

    }

    public void UpdateSense()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= detectionRate)
        {
            DetectAspect();
            elapsedTime = 0.0f;
        }
    }
    public void DetectAspect()
    {
        if (playerTrans == null) return;

        rayDirection = (playerTrans.position - transform.position).normalized;

        if (Vector3.Angle(rayDirection, transform.forward) < FieldOfView)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, rayDirection, out hit, ViewDistance))
            {
                if (hit.collider.gameObject.CompareTag("Player"))
                {
                    Debug.Log("Player Detected");
                    isVisible = true;
                    fsm.ChangeState("Attack");
                }
            }
            else
            {
                Debug.Log("Player Undetected");
                isVisible = false;
            }
        }
    }
    private void OnDrawGizmos()
    {
        if (!Application.isEditor || playerTrans == null)
            return;

        Debug.DrawLine(transform.position, playerTrans.position, Color.red);
        Vector3 frontRayPoint = transform.position + (transform.forward * ViewDistance);

        Vector3 leftRayPoint = Quaternion.Euler(0, FieldOfView * 0.5f, 0) * frontRayPoint;
        Vector3 rightRayPoint = Quaternion.Euler(0, -FieldOfView * 0.5f, 0) * frontRayPoint;

        Debug.DrawLine(transform.position, frontRayPoint, Color.blue);
        Debug.DrawLine(transform.position, leftRayPoint, Color.green);
        Debug.DrawLine(transform.position, rightRayPoint, Color.green);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Caught Player");
            other.gameObject.GetComponent<PlayerController>().health -= 100 * attackStrength;
        }
    }

}
