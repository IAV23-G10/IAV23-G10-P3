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
using BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject;
using System.Numerics;

/*
 * Accion de seguir a la cantante, cuando la alcanza devuelve Success
 */

public class GhostChaseAction : Action
{
    float captureRange = 3;

    [SerializeField]
    NavMeshAgent agent;
    [SerializeField]
    GameObject singer;
    [SerializeField]
    GameBlackboard gameBlackboard;

    public override void OnAwake()
    {
        singer = GameObject.FindGameObjectWithTag("Cantante");
    }
    public override void OnStart()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.isStopped = false;
    }

    public override TaskStatus OnUpdate()
    {
        int a = agent.areaMask, b = 1 << NavMesh.GetAreaFromName("Escenario");
        agent.areaMask = (a | b);


        //Si el piano esta roto vamos a por el
        if (gameBlackboard.pianoRoto())
        {
            return TaskStatus.Failure;
        }

        // Si esta a distancia lo suficientemente cercana como para capturar a la cantante, o ya esta capturada
        if (TargetReached(singer.transform.position, captureRange))
        {
            if(agent.enabled)
            agent.isStopped = true;

            Cantante singerScript = singer.GetComponent<Cantante>();
            singerScript.Secuestrada(gameObject);

            return TaskStatus.Success;
        }
        else
        {
            if (agent.enabled)
                agent.isStopped = false;
            if(agent.enabled)
            agent.SetDestination(singer.transform.position);

            return TaskStatus.Running;
        }
    }
}
