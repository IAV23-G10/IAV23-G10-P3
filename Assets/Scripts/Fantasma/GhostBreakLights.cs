using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject;
using System.Numerics;

public class GhostBreakLights : Action
{
    [SerializeField]
    NavMeshAgent agent;

    GameBlackboard gameBlackboard;


    public override void OnAwake()
    {
        // IMPLEMENTAR 
       
    }

    public override void OnStart()
    {
        agent = GetComponent<NavMeshAgent>();
        gameBlackboard = GameObject.FindObjectOfType<GameBlackboard>();
    }

   
    public override TaskStatus OnUpdate()
    {
        // Tiene que romper una lampara para poder secuestrar a la cantante

        // Comprobar si se ha llegado al interruptor


        // 
        if (gameBlackboard.FunctionalLights())
        {
            // Si las dos luces estan encendidas, ir a apagar la que tenga mas cerca

            UnityEngine.Vector3 nearestLeverPosition = gameBlackboard.nearestLever(gameObject).transform.position;
            agent.SetDestination(nearestLeverPosition);

            return TaskStatus.Running;
        }
        else
        {
            // Si una o las dos luces estan rotas, mision cumplida
            return TaskStatus.Success;
        }
    }
}