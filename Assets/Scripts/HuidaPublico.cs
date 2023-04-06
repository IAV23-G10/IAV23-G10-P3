using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class HuidaPublico : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject destino;
    public GameObject origen;
    NavMeshAgent agent;
    void Start()
    {
        agent=GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GetComponent<Publico>().getLuces())
        {
            agent.SetDestination(destino.transform.position);
        }
        else
        {
            agent.SetDestination(origen.transform.position);
        }
    }
}
