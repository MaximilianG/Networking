using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Ennemy : MonoBehaviour
{
    [SerializeField] Transform waypoint1, waypoint2; // On a un peu modifié l'ennemy on met les waypoints dans l'inspector maintenant
    public Vector3 position1; // et on instancie plus l'ennemy depuis le roommanager mais on le met directement dans le level, plus simple
    public Vector3 position2;
    private PhotonView PV;

    Vector3 currentTargetDestination;

    public float distanceTolerance = 0.5f; //you can change the tolerance to whatever you need it to be

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    void Start()
    {

        position1 = waypoint1.position;
        position2 = waypoint2.position;
        transform.position = position1; //set the initial position
        currentTargetDestination = position2;
    }

    void Update()
    {
        if (PhotonNetwork.IsMasterClient == false)
            return;
        transform.position = Vector3.MoveTowards(transform.position, currentTargetDestination, 5 * Time.deltaTime);

        if (Vector3.Distance(transform.position, currentTargetDestination) <= distanceTolerance)
        {
            //once we reach the current destination, set the other location as our new destination
            if (currentTargetDestination == position1)
            {
                currentTargetDestination = position2;
            }
            else
            {
                currentTargetDestination = position1;
            }
        }
    }
}