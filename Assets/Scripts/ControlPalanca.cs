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
using UnityEditor.Experimental.GraphView;
using UnityEngine;

/*
 * Sube o baja los candelabros cuando un objeto se colisiona con este, además avisando al público de este evento
 */

public class ControlPalanca : MonoBehaviour
{
    [SerializeField]
    GameBlackboard.Side side;

    GameBlackboard gameBlackboard;

    public GameObject candelabro;
    public GameObject publico;
    public float step;
    float altura;
    public bool caido = false;

    public ControlPalanca otroControl;

    private void Start()
    {
        gameBlackboard = FindObjectOfType<GameBlackboard>();

        altura = candelabro.transform.position.y;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Cantante>() || other.gameObject.GetComponent<Player>()) return;
        caido = !caido;
        Interact();
    }

    public void Interact()
    {
        publico.GetComponent<Collider>().enabled = !caido && !otroControl.caido;

        // Informar al GameBlackboard
        gameBlackboard.ChangeLight(side, !caido);

        if (caido)
        {
            candelabro.GetComponent<Rigidbody>().useGravity = true;
            for (int i = 0; i < publico.transform.childCount; ++i)
            {
                publico.transform.GetChild(i).GetComponent<Publico>().apagaLuz();
            }
            
        }
        else
        {
            candelabro.GetComponent<Rigidbody>().useGravity = false;
            for (int i = 0; i < publico.transform.childCount; ++i)
            {
                publico.transform.GetChild(i).GetComponent<Publico>().enciendeLuz();
            }
        }
    }

    private void FixedUpdate()
    {
        if (!caido && candelabro.transform.position.y < altura)
        {
            candelabro.transform.Translate(new Vector3(0, step, 0));
        }
    }
}
