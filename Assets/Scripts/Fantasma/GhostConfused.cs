/*    
   Copyright (C) 2020-2023 Federico Peinado
   http://www.federicopeinado.com
   Este fichero forma parte del material de la asignatura Inteligencia Artificial para Videojuegos.
   Esta asignatura se imparte en la Facultad de Inform�tica de la Universidad Complutense de Madrid (Espa�a).
   Autor: Federico Peinado 
   Contacto: email@federicopeinado.com
*/

using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostConfused : Conditional
{
    GameBlackboard bb;

    [SerializeField]
    NavMeshAgent agent;

    // Devuelve true si el fantasma se esta despertando (dejando de estar confundido)
    bool awakening;

    public override void OnAwake()
    {
        bb = GameObject.FindObjectOfType<GameBlackboard>();
    }

    public override TaskStatus OnUpdate()
    {
        if (bb.isGhostConfused)
        {
            // Si ya se esta intentado despertar, no intentar despertar otra vez
            if (!awakening)
            {
                awakening = true;
                StartCoroutine(StopConfusion());
            }
            // Quedarse parado en el sitio
            agent.SetDestination(transform.position);
            return TaskStatus.Running;
        }
        else 
            return TaskStatus.Success;
    }

    IEnumerator StopConfusion()
    {
        yield return new WaitForSeconds(2);
        bb.isGhostConfused = false;
        awakening = false;
    }
}
