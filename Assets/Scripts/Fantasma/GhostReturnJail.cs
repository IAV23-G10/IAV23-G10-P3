using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

// Este script parte de que se tiene en hombros a la cantante.
/// Su funcionamiento consiste en llevar a la cantante a la celda y encerrarla,
/// Esto incluye abrir/cerrar la puerta de la celda cuando sea necesario
public class GhostReturnJail : Action
{
    [SerializeField]
    GameBlackboard gameBlackboard;

    float destinationRange = 3;

    [SerializeField]
    Transform jail;
    [SerializeField]
    Transform jailLever;

    [SerializeField]
    NavMeshAgent agent;
    [SerializeField]
    Cantante singer;

    public override void OnAwake()
    {
        agent.isStopped = false;
    }

    public override TaskStatus OnUpdate()
    {
       

        //Si el piano esta roto vamos a por el
        if (gameBlackboard.pianoRoto())
        {
            // Dejar de ser su secuestrador
            singer.DeSecuestrada();
            return TaskStatus.Failure;
        }

        Debug.Log("singer.secuestrador = " + singer.secuestrador);
        Debug.Log("blackboard.gate = " + gameBlackboard.gate);


        // Va a moverse
        if(agent.enabled)
        agent.isStopped = false;

        // Comprobar si ya se ha encerrado a la cantante o no
        if (gameBlackboard.singerTrapped)
        {
            // Si ya se ha encerrado a la cantante

            // Cerrar la puerta si no lo esta ya
            if (gameBlackboard.gate&&agent.enabled)
                agent.SetDestination(jailLever.position);
            else
                // Si la cantante esta dentro de la carcel, y la puerta esta cerrada
                // Se puede dar por satisfecha esta accion
                return TaskStatus.Success;
        }
        else
        {
            // Si la cantante no esta dentro de la carcel todavia, tiene que llevarla alli

            // Comprobar si la carcel esta abierta o cerrada
            if (gameBlackboard.gate)
            {
                // Si esta abierto, ir al interior de la celda

                // Comprobar si ya se ha llegado al interior de la celda
                if (TargetReached(jail.position, destinationRange))
                {
                    // Si se ha llegado a dentro de la carcel, dejar a la cantante

                    // Dejar de ser su secuestrador
                    singer.DeSecuestrada();
                    // Notificar al juego de que la cantante esta encerrada en la carcel
                    gameBlackboard.singerTrapped = true;
                }

                else
                {
                    // Si todavia no se ha llegado al interior de la celda, ir
                    if(agent.enabled)
                    agent.SetDestination(jail.position);
                }
            }

            else
            {
                // Si esta cerrado, abrirlo yendo a la palanca
                if(agent.enabled)
                agent.SetDestination(jailLever.position);
            }
        }

        return TaskStatus.Running;
    }
}
