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
    public bool isAttack = false;
    #region Sense properties
    //public GameObject goPlayer;
    public GameObject[] goPlayers;  // Array to store multiple players
    private Transform closestPlayerTrans; // Store the closest player's transform

    public bool isVisible = false;
    public float detectionRate = 1.0f;
    private float elapsedTime = 0.0f;
    #endregion

    #region Sight properties
    public int FieldOfView = 45;
    public int ViewDistance = 10;
    private Transform playerTrans;
    private Vector3 rayDirection;
    #endregion

    [Header("Patrol Waypoints")]
    public bool isPatrol = false;
    PathFollowing pathFollowing;

    void Start()
    {
        //if (goPlayer == null)
        //{
        //    goPlayer = GameObject.FindGameObjectWithTag("Player");
        //}
        //playerTrans = goPlayer.transform;

        goPlayers = GameObject.FindGameObjectsWithTag("Player");
    }

    void Update()
    {
        NPCEnergy = this.gameObject.GetComponent<NPCEnergy>();
        energyLevel = NPCEnergy.energyLevel;
        idleStrength = NPCEnergy.idleStrength;
        patrolStrength = NPCEnergy.patrolStrength;
        attackStrength = NPCEnergy.attackStrength;

        fsm = this.gameObject.GetComponent<NPCStateMachine>();

        if (isAlert)
        {
            UpdateSense();
        }

        pathFollowing = this.gameObject.GetComponent<PathFollowing>();
        pathFollowing.enabled = isPatrol;

        if(isAttack)
        {
            MoveToPlayer();
        }

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

    //public void MoveToPlayer()
    //{
    //    Vector3 directionToPlayer = (playerTrans.position - transform.position).normalized;
    //    Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
    //    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 2f * Time.deltaTime);
    //    transform.Translate(directionToPlayer * attackStrength * 10f * Time.deltaTime, Space.World);
    //}

    //public void DetectAspect()
    //{
    //    if (playerTrans == null) return;

    //    rayDirection = (playerTrans.position - transform.position).normalized;

    //    if (Vector3.Angle(rayDirection, transform.forward) < FieldOfView)
    //    {
    //        RaycastHit hit;
    //        if (Physics.Raycast(transform.position, rayDirection, out hit, ViewDistance))
    //        {
    //            if (hit.collider.gameObject.CompareTag("Player"))
    //            {
    //                Debug.Log("Chasing Mode: ON");
    //                isVisible = true;
    //                fsm.ChangeState("Attack");
    //            }
    //        }
    //        else
    //        {
    //            Debug.Log("Chasing Mode: OFF");
    //            isVisible = false;
    //            if (energyLevel != 0)
    //            {
    //                if (energyLevel > 0.3f)
    //                {
    //                    fsm.ChangeState("Patrol");
    //                }
    //                else if (energyLevel <= 0.3f)
    //                {
    //                    fsm.ChangeState("Idle");
    //                }
    //            }
    //        }
    //    }
    //}
    public void MoveToPlayer()
    {
        if (closestPlayerTrans == null)
            return;

        Vector3 directionToPlayer = (closestPlayerTrans.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 2f * Time.deltaTime);
        if(attackStrength < 0.3)
        {
            transform.Translate(directionToPlayer * 3f * Time.deltaTime, Space.World);
        }
        else
        {
            transform.Translate(directionToPlayer * attackStrength * 10f * Time.deltaTime, Space.World);
        }

    }

    public void DetectAspect()
    {
        float closestDistance = float.MaxValue;
        closestPlayerTrans = null;

        foreach (GameObject player in goPlayers)
        {
            Transform playerTrans = player.transform;
            Vector3 rayDirection = (playerTrans.position - transform.position).normalized;

            if (Vector3.Angle(rayDirection, transform.forward) < FieldOfView)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, rayDirection, out hit, ViewDistance))
                {
                    if (hit.collider.gameObject.CompareTag("Player"))
                    {
                        float distance = Vector3.Distance(transform.position, playerTrans.position);
                        if (distance < closestDistance)
                        {
                            closestDistance = distance;
                            closestPlayerTrans = playerTrans; // Update closest player
                        }
                    }
                }
            }
        }

        if (closestPlayerTrans != null)
        {
            Debug.Log("Chasing Mode: ON - Closest Player Detected");
            isVisible = true;
            fsm.ChangeState("Attack");
        }
        else
        {
            Debug.Log("Chasing Mode: OFF - No Player Detected");
            isVisible = false;

            if (energyLevel > 0.3f)
            {
                fsm.ChangeState("Patrol");
            }
            else
            {
                fsm.ChangeState("Idle");
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (!Application.isEditor || goPlayers == null)
            return;

        foreach (GameObject player in goPlayers)
        {
            Vector3 playerPos = player.transform.position;
            Debug.DrawLine(transform.position, playerPos, Color.red);
        }
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
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            if (player.isProtected)
            {
                Destroy(this.gameObject);
            }
            else
            {
                player.health -= 10 * attackStrength;
            }
            
        }
    }

}
