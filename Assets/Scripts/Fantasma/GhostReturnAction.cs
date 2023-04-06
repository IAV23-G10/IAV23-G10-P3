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

public class GhostReturnAction : Action
{
    NavMeshAgent agent;
    public GameObject musicRoom;
    float captureRange = 3;
    public override void OnAwake()
    {
        // IMPLEMENTAR
    
        agent = GetComponent<NavMeshAgent>(); 
    }

    public override TaskStatus OnUpdate()
    {
        if (TargetReached(musicRoom.transform.position, captureRange))
        {
            if (agent.enabled)
                agent.isStopped = true;
         
            
            return TaskStatus.Success;
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