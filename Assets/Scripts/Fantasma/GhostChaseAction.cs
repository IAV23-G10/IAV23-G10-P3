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



    public override void OnAwake()
    {
        // IMPLEMENTAR 
        agent= GetComponent<NavMeshAgent>();
        singer = GameObject.FindGameObjectWithTag("Cantante");
    }

    public override TaskStatus OnUpdate()
    {
        // Comprobar si hay publico usando el blackboard


        // Si esta a distancia lo suficientemente cercana como para capturar a la cantante, o ya esta capturada
        if (Vector3.Distance(transform.position, singer.transform.position) < captureRange)
        {
            agent.isStopped = true;

            Cantante singerScript = singer.GetComponent<Cantante>();
            singerScript.Secuestrada(gameObject);

            return TaskStatus.Success;
        }
        else
        {
            agent.isStopped = false;
            agent.SetDestination(singer.transform.position);

            return TaskStatus.Running;
        }
    }
}
