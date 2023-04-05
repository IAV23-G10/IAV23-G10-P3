using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;


/*
 * Devuelve Success cuando no hay publico mirando
 */


public class NoLightCondition : Conditional
{
    GameBlackboard gameBlackboard;

    public override void OnAwake()
    {
        gameBlackboard = GameObject.FindObjectOfType<GameBlackboard>();
    }

    public override TaskStatus OnUpdate()
    {
        bool leftLightFunctional = gameBlackboard.GetLight(GameBlackboard.Side.left);
        bool rightLightFunctional = gameBlackboard.GetLight(GameBlackboard.Side.right);

        // Si las dos luces funcionan, no puede secuestrar a la cantante
        if (leftLightFunctional && rightLightFunctional)
            return TaskStatus.Failure;

        // Si una de los dos luces o las dos estan rotas, entonces si puede secuestrar a la cantante
        else
            return TaskStatus.Success;
    }
}
