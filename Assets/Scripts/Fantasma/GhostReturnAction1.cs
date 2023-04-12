/*    
   Copyright (C) 2020-2023 Federico Peinado
   http://www.federicopeinado.com
   Este fichero forma parte del material de la asignatura Inteligencia Artificial para Videojuegos.
   Esta asignatura se imparte en la Facultad de Informática de la Universidad Complutense de Madrid (España).
   Autor: Federico Peinado 
   Contacto: email@federicopeinado.com
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

/*
 * Accion de ir a la sala de musica, cuando llega devuelve Success
 */

public class GhostReturnAction_ : Action
{   
    NavMeshAgent agent;
    public GameObject musicRoom;
    float captureRange = 1.5f;

    Cantante cantante;

    public override void OnAwake()
    {
        // IMPLEMENTAR

        agent = GetComponent<NavMeshAgent>();

        cantante = GameObject.FindObjectOfType<Cantante>();
    }

    public override TaskStatus OnUpdate()
    {
        //Debug.Log("cantante.cantando = " +Vector3.Distance(transform.position, musicRoom.transform.position));

        if (TargetReached(musicRoom.transform.position, captureRange))
        {
            agent.SetDestination(musicRoom.transform.position);

            //if (agent.enabled)
            //    agent.isStopped = true;

            Debug.Log("TARGET REACHED");

            // Solo dejara de estar enganchado al piano si oye a la cantante
            if (cantante.cantando)
                return TaskStatus.Success;
            else
                return TaskStatus.Running;
        }
        else
        {
            if (agent.enabled)
                agent.isStopped = false;
            if (agent.enabled)
                agent.SetDestination(musicRoom.transform.position);

            return TaskStatus.Running;
        }
    }
}