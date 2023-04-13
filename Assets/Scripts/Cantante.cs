using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject;
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

    Player player;
    BehaviorTree ghostTree;

    public void Awake()
    {
        agente = GetComponent<NavMeshAgent>();

        player = FindObjectOfType<Player>();

        ghostTree = GameObject.FindWithTag("Ghost").GetComponent<BehaviorTree>();
    }

    public void Start()
    {
        agente.updateRotation = false;
    }

    private void Update()
    {

        // Si estaba secuestrada por el fantasma, y el player se acerca, ser libre y confundir al fantasma

        if (secuestrador != null && secuestrador.gameObject.tag == "Ghost" 
            && Vector3.Distance(transform.position, player.transform.position) < 2)
        {
            bb.isGhostConfused = true;
            DeSecuestrada();

            ghostTree.enabled = false;

            // Resetear el fantasma
            Invoke("ResetGhost", .1f);
        }
    }
    // Resetear el arbol del fantasma, para que deje de hacer su tarea actual y se confunda
    void ResetGhost()
    { ghostTree.enabled = true; }

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
        cantando = false;
    }

    // Comprueba si tiene que dejar de descansar
    public bool TerminaDescansar()
    {
        tiempoComienzoDescanso += Time.deltaTime;

        if (tiempoComienzoDescanso >= tiempoDeDescanso) { tiempoComienzoDescanso = 0; return true; }

        return false;
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
}
