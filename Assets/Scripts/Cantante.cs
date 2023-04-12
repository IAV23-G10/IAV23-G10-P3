using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Cantante : MonoBehaviour
{
    // Segundos que estara cantando
    public double tiempoDeCanto;
    // Segundo en el que comezo a cantar
    private double tiempoComienzoCanto;
    // Segundos que esta descanasando
    public double tiempoDeDescanso;
    // Segundo en el que comezo a descansar
    private double tiempoComienzoDescanso;
    // Si esta capturada
    public bool capturada = false;
    public GameObject secuestrador = null;

    [Range(0, 180)]
    // Angulo de vision en horizontal
    public double anguloVistaHorizontal;
    // Distancia maxima de vision
    public double distanciaVista;
    // Objetivo al que ver"
    public Transform objetivo;

    // Segundos que puede estar merodeando
    public double tiempoDeMerodeo;
    // Segundo en el que comezo a merodear
    public double tiempoComienzoMerodeo = 0;
    // Distancia de merodeo
    public int distanciaDeMerodeo = 16;
    // Si canta o no
    public bool cantando = false;

    // Componente cacheado NavMeshAgent
    private NavMeshAgent agente;

    // Objetivos de su itinerario
    public Transform Escenario;
    public Transform Bambalinas;
    public Transform Celda;
    public Transform Vizconde;
    // La blackboard
    public GameBlackboard bb;
    bool encelda = false;

    public void Awake()
    {
        agente = GetComponent<NavMeshAgent>();
        
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "celda")
        {
            encelda = true;
        }
    }

    public void Start()
    {
        agente.updateRotation = false;
    }

    public void LateUpdate()
    {
        if (agente.velocity.sqrMagnitude > Mathf.Epsilon)
        {
            transform.rotation = Quaternion.LookRotation(agente.velocity.normalized);
        }
    }

    public void Secuestrada(GameObject _secuestrador)
    {
        capturada = true;
        secuestrador = _secuestrador;
        agente.enabled = false;
    }

    public void DeSecuestrada()
    {
        secuestrador = null;
        capturada= false;
        agente.enabled = true;
    }

    // Comienza a cantar, reseteando el temporizador
    public void Cantar()
    {
        tiempoComienzoCanto = 0;
        cantando = true;
    }

    // Comprueba si tiene que dejar de cantar
    public bool TerminaCantar()
    {
        tiempoComienzoCanto += Time.deltaTime;

        if (tiempoComienzoCanto >= tiempoDeCanto) { tiempoComienzoCanto = 0; return true; }
     
        return false;
    }

    // Comienza a descansar, reseteando el temporizador
    public void Descansar()
    {
        // IMPLEMENTAR
    }

    // Comprueba si tiene que dejar de descansar
    public bool TerminaDescansar()
    {
        tiempoComienzoDescanso += Time.deltaTime;

        if (tiempoComienzoDescanso >= tiempoDeDescanso) { tiempoComienzoDescanso = 0; return true; }

        return false;
    }

    // Comprueba si se encuentra en la celda
    public bool EstaEnCelda()
    {
        // IMPLEMENTAR
        return encelda;
    }

    // Comprueba si esta en un sitio desde el cual sabe llegar al escenario
    public bool ConozcoEsteSitio()
    {
        if (transform.position.y >= -0.3)
        {
            return true;
        }
        return false;
    }

    //Mira si ve al vizconde con un angulo de vision y una distancia maxima
    public bool Scan()
    {

        double angulo = Vector3.Angle(transform.forward, Vizconde.position - transform.position);
        double distancia = Vector3.Magnitude(transform.position - Vizconde.position);

        if (angulo < anguloVistaHorizontal &&  distancia<= distanciaVista)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vizconde.position - transform.position, out hit, Mathf.Infinity) && 
                hit.collider.gameObject.GetComponent<Player>())
            {
                if (agente.enabled)
                    agente.SetDestination(Vizconde.position);
                return true;
            };
        }
        return false;
    }

    // Genera una posicion aleatoria a cierta distancia dentro de las areas permitidas
    private Vector3 RandomNavSphere(float distance)
    {
        NavMeshHit hit;
        NavMesh.SamplePosition(transform.position + Random.insideUnitSphere * distanciaDeMerodeo, out hit, 
            distanciaDeMerodeo, NavMesh.AllAreas);
        return hit.position;
    }

    // Genera un nuevo punto de merodeo cada vez que agota su tiempo de merodeo actual
    public void IntentaMerodear()
    {
        // IMPLEMENTAR
        tiempoComienzoMerodeo += Time.deltaTime;

        if (tiempoComienzoMerodeo >= tiempoDeMerodeo)
        {
            tiempoComienzoMerodeo = 0;
            if (agente.enabled)
                agente.SetDestination(RandomNavSphere(distanciaDeMerodeo));
        }
    }

    public bool GetCapturada()
    {
        return capturada;
    }
    
    public GameObject getSecuestrador()
    {
        return secuestrador;
    }

    public void setCapturada(bool cap)
    {
        capturada = cap;
    }

    public GameObject sigueFantasma()
    {
        // IMPLEMENTAR
        return null;
    }

    public void sigueVizconde()
    {
        // IMPLEMENTAR
    }

    private void nuevoObjetivo(GameObject obj)
    {
        // IMPLEMENTAR
    }
}
