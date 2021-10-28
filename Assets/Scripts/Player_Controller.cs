using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player_Controller : MonoBehaviour
{

    [SerializeField] ParticleSystem groundClickParticle = null;

    public Camera cam;
    public NavMeshAgent agent;

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                //Moving agent
                agent.SetDestination(hit.point);

                //Instantiate particle effect
                Vector3 mousePos = hit.point;
                var clone = Instantiate(groundClickParticle, mousePos, Quaternion.identity);
                groundClickParticle.Play();

            }
        }

        Destroy(GameObject.Find("GroundClick(Clone)"), 0.75f);

    }
}
