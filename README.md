
# IAV23-G10-P3
# IAV - Base para la Práctica 3


## Autores
- Miriam Martin Sanchez /https://github.com/miriam-m-s
- Diego Rol Sanchez /https://github.com/DiegoRol69
- Ignacio del Castillo /https://github.com/NachoDelCastillo
- VIDEO PRUEBAS https://youtu.be/zTtAYfi_qis

## Propuesta
La práctica consiste en desarrollar un prototipo de IA para Videojuegos, dentro de un entorno virtual que represente la ópera de París, con un agente inteligente (el fantasma) que decide, se mueve y actúa según lo que encuentra en sus diferentes estancias, otros agentes más simples como la cantante y el público, y un avatar, el vizconde -némesis del fantasma-, controlado por el jugador.  </br>


Esquema con la topología de las distintas estancias de la ópera. Los nodos del grafo representan estancias identificadas por una abreviatura, las aristas dirigidas representan visibilidad entre ellas, e incluso navegabilidad si las aristas son negras.
El punto de partida propuesto para esta práctica, con la documentación e implementación (código y recursos audiovisuales) necesaria, se encuentra en este repositorio de GitHub: IAV-Decision.

En el entorno virtual encontramos las siguientes estancias, describiendo también los elementos más relevantes que podemos encontrar en ellas, y su comportamiento:</br>

Patio de butacas (P). Es la estancia inicial del público,  dividido en dos bloques (este y oeste). Cada bloque chilla a la mínima que vea al fantasma sobre el escenario, pero permanecerá en su sitio salvo que caiga la lámpara gigante del techo correspondiente (hay lámpara este y oeste), en cuyo caso ese lado del patio de butacas se oscurecerá y los espectadores huirán despavoridos al vestíbulo. No regresarán hasta que no se vuelva a colocar su lámpara. Esta estancia está conectada con el escenario (visibilidad y navegabilidad, como indica la arista negra), con el vestíbulo, y es visible desde los palcos este y oeste, aunque el público no puede alcanzar a ver bien si hay alguien en los palcos, ni aunque estén las dos lámparas encendidas.</br></br>
Vestíbulo (V). Es la zona más externa de la ópera, donde van los bloques de público cuando se asustan. Simplemente conecta con el patio de butacas.
Escenario (E). Es la estancia inicial de la cantante, donde se dedica a su oficio, aunque lo intercala (cada pocos segundos) con un descanso que realiza tras las bambalinas, una estancia contigua. Además también conecta con el patio de butacas y los palcos, y es posible dejarse caer por una trampilla al sótano oeste, aunque no sea posible regresar. El fantasma no pisará ninguna estancia como esta mientras haya público mirando. Eso sí, tanto en esta estancia como en otras puede “capturar” (coger al hombro) a la cantante, incluso compartiendo estancia con el vizconde y llevársela a donde quiera, soltándola por voluntad propia o porque se sienta intimidado por el “choque” con nuestro héroe. Si la cantante acaba en una estancia que no esté conectada con el escenario, se sentirá confusa y merodeará (pasando de estancia a estancia aleatoriamente), dejándose “rescatar” por el vizconde en caso de que lo vea cerca, con la esperanza de que la lleve hasta una estancia que conozca, para poder retomar así su ritmo normal de trabajo.</br></br>
Bambalinas (B). Estancia donde suele descansar la cantante y que conecta con el escenario, el sótano oeste y que permite deslizarse por una rampa algo oculta al sótano este, sin posibilidad de regresar.</br>
Palco oeste (Po). Estancia inicial del vizconde, personaje que controla el jugador y que gusta disfrutar desde aquí de la función. El palco tiene una palanca que se puede usar para dejar caer la lámpara oeste del patio de butacas. Conecta con el escenario, con el sótano oeste y permite ver el patio de butacas, aunque debido a la altura no existe visibilidad en el sentido opuesto. El vizconde puede moverse con libertad, como el fantasma, también sobre las barcas cercanas. Puede usar palancas y chocar contra el fantasma, intimidándole y haciendo que retroceda durante unos pocos segundos (y suelte a la cantante si la llevaba). Puede golpear muebles, como los de la sala de música, haciendo un ruido tremendo que se escuchará en toda la zona subterránea. Puede interactuar con una lámpara caída, para arreglarla automáticamente (colocándola en su sitio), y también con la cantante, cogiéndola en brazos automáticamente y llevándola consigo, o dejándola en el suelo a voluntad (interactuando sin que haya una palanca delante ni otra cosa así).</br></br>
Palco este (Po). Estancia similar al palco oeste, con una palanca que se puede usar para dejar caer la lámpara este del patio de butacas. Conecta con el escenario, con el sótano este y permite ver el patio de butacas, aunque sin visibilidad en el otro sentido. </br></br>
Sótano oeste (So). Estancia que conecta con el palco oeste, con las bambalinas y con el sótano norte, aunque para recorrer esta conexión hace falta subirse a una barca. Sólo una persona (tal vez con otra al hombro o en brazos) puede montarse sobre la barca a la vez y sólo si esta se encuentra atracada en la orilla de esa estancia. Por defecto, la barca que se necesita aquí comienza atracada en la otra orilla, en la del sótano norte, y aunque en todas las orillas siempre hay una palanca que permite acercarla, el proceso de “recuperar” la barca es algo lento (mayor coste). Se puede llegar a esta estancia desde el escenario, pero no al revés.
Sótano este (Se). Estancia que conecta con el palco este, y tanto con el sótano norte como con la sala de música donde compone su obra el fantasma, aunque para recorrer estas dos últimas conexiones hacen falta barcas. Por defecto, la barca que lleva al sótano norte sí está en esta orilla, pero la que lleva a la sala de música está en la orilla contraria. Aunque se puede llegar a esta estancia desde las bambalinas, por una trampilla, desde aquí no se conecta con las bambalinas.</br></br>
Celda (C). Estancia donde el fantasma deja a la cantante para completar su secuestro con éxito, usando una palanca que activa unas rejas que la impiden salir (y que por supuesto el vizconde podrá desactivar). Conecta con el sótano norte.</br></br>
Sótano norte (Sn). Estancia que conecta con la celda, además de con la sala de música, el sótano este y el sótano oeste a través de sus correspondientes tres barcas.
Sala de música (M). Estancia inicial del fantasma, donde le gusta pasar tiempo componiendo su ópera. Conecta mediante una barca con el sótano este, y con otra con el sótano norte. El fantasma tiene el objetivo principal de secuestrar a la cantante, para lo que intentará buscarla en las bambalinas, en el escenario o si no logra dar con ella, explorando las demás estancias meticulosamente por si estuviera “perdida” por allí. No puede acceder al escenario si hay público mirando, de modo que, como objetivo secundario, necesita tirar las dos lámparas del techo para vaciar del todo el patio de butacas. Sea como sea, una vez atrapada la cantante, la llevará consigo hasta la celda, intentando usar siempre el camino con menor coste (recordando la última posición de las barcas y del vizconde que conoce, y eligiendo la ruta con menor coste, la que tenga más barcas a su favor y que evite al héroe de esta historia). Cuando llega hasta la celda la soltará allí, activará las rejas e irá hasta la sala de música, permaneciendo allí indefinidamente. Lo único que desconcentra al fantasma cuando está componiendo es escuchar a su musa cantar de nuevo en el escenario, reavivando sus deseos de secuestrarla y encerrarla otra vez en su celda. Por otro lado, si el fantasma llega a percibir el ruido de los golpes del vizconde a su piano, abortará la misión que esté haciendo (soltando a la cantante) y correrá enfurecido hasta allí para dedicar unos segundos a arreglar semejante estropicio. 
Las características principales del prototipo son:</br></br>

**A.** Hay un mundo virtual (la casa de la ópera), con un esquema de división de malla de navegación proporcionado por Unity, donde se ubican todos los elementos descritos anteriormente. El vizconde es controlado por el jugador mediante el ratón y un único clic derecho para interactuar con otros elementos. Hay cámaras que siguen a cada uno de los personajes, incluyendo una que nos dé la vista general del entorno.

**B.** Cada mitad del público huye tras la caída de su lámpara, y regresa en cuanto esta ha sido arreglada. Esto se hace con una navegación y un movimiento triviales, sin apenas decisión.

**C.** La cantante es un agente inteligente basado en una máquina de estados que pasa del escenario a las bambalinas cuando toca, que puede ser “llevada” por los otros dos personajes hasta otra estancia, que merodea desorientada cuando está en las estancias subterráneas, y que se deja llevar por el vizconde, con la esperanza de reencontrar el escenario y continuar su rutina allí. Tiene navegación, movimiento y percepción sencillos, y decisión mediante máquina de estados

**D.** El fantasma funciona mediante un árbol de comportamiento complejo, para buscar a la cantante, tirar las lámparas para deshacerse del público, capturar a la chica, llevarla a la celda, activar las rejas para encerrarla, etc.

**E.** El fantasma también cuenta con un sistema de gestión sensorial para reaccionar a lo que realmente ve (en la propia estancia o en otras que son visibles para él) y oye (el canto de su musa, el grito del público, el ruido de la sala de música…), sin tener que recurrir a información privilegiada sino únicamente recordando lo que ha ido viendo por el mundo.

## Punto de partida
Se parte de un proyecto base de Unity proporcionado por el profesor aquí:
(https://github.com/Narratech/IAV-Navegacion/)

En el proyecto inicial, el jugador controla a un personaje 3D con la habilidad de moverse en el plano XZ de forma uniforme, para ello se utilizan los inputs WASD o las flechas. También la escena dispone del agente minotauro que demomento no hace nada.<br />

- **Llegada:** Lo que hace es ralentizar al agente que lo posee hasta pararse cuando este va hacia el target.

- **Slow:** Se usa para que cuando el jugador entre en el radio de acción este se ralentice.

- **MinoCollision:** Reinicia la escena si colisiona con el jugador (pierde).

- **MinoEvader:** Evita que el minotauro detecte la colsion con otras entidades distintas al jugador.

- **MinoManager:** Instancia minotauros dentro de casillas válidas.

- **SegirCamino:** Sigue el camino elegido por A*.

- **ControlJugador:** Coge el input para mover al jugador.

- **Teseo:** Combina los componentes de ControlJugador y SeguirCamino para poder moverlo y hacerlo que siga el camino. También puede activar y desactivar el hilo.

- **Graph:** Esta clase sirve para implementar un grafo. Tiene el método principal de la práctica GetPathAStar() que habrá que definir haciendo uso de otros métodos como la Heurítica, GetNeighbours para recorrer adyacentes o BuildPath que reconstruye el path final desde el destino hasta el origen.

- **GraphGrid:** Genera un mapa a partir de un archiv .map.

- **Vertex:** Operaciones de los vértices del grafo para poder compararlos a la hora de construir el camino más óptimo.

- **BinaryHeap:** Template una cola de prioridad.

- **Node:** Operaciones de los nodos del grafo para poder compararlos a la hora de construir el camino más óptimo.

- **Componente ComportamientoAgente:** El minotauro y el jugador porseen este componente.Esta clase se encarga de actualizar la posicion segun la direccion.Para determinar la dirección tiene en cuenta los boolenaos combinar por peso y por prioridad.<br /><br />
            &emsp; **1. `Combinar por peso`**: actualiza la direccion modificando el modulo.<br />
            &emsp; **2. `Combinar por prioridad`**: modifica la direccion dependiendo del factor de prioridad.<br /><br />
            
- **Componente Agente:**  El minotauro y el jugador porseen este componente. Por un lado, se encarga de aplicar el movimiento a los objetos dinámicos (FixedUpdate) y cinemáticos (Update), limitando en ambos las velocidades lineal y angular y la aceleración en los dinámicos.

## Diseño de la solución
Lo que vamos a realizar para resolver esta práctica es implementar el comportamiento de los diferentes Scripts:

**PLANTEAMINETO INICIAL**: </br>
- **Merodear:** Perteneciente únicamente al minotauro para que tenga un movimiento errático y desordenado. </br>
- **Persecución:** Perteneciente únicamente al minotauro para que persiga al jugador con predicción. </br>
- **Minotauro Control:** Perteneciente únicamente al minotauro para que pueda cambiar entre el estado de merodeo y persecución. </br>
- **Jugador Control:** Perteneciente únimamente al jugador para moverlo con el ratón. </br>
- **A-star:** Perteneciente al jugador para que puedar encontrar de forma automática la salida al laberinto. </br>
            
**PLANTEAMIENTO FINAL**: De forma opcional y final nos gustaria añadir lo siguiente </br> 
- **Merodear:** El merodeo del minotauro pasaría a funcionar de manera que se elija una casilla aleatoria en un rango cerca de la posicion del minotauro, el minotauro reusara entonces el algoritmo de A* para llegar a esa casilla. Cuando llegue a su destino, espera unos segundos antes de elegir de nuevo una casilla aleatoria. </br> 
- **Persecución:** La persecución reusaria tambien el algoritmo A* para seguir al jugador encontrando el mejor camino, sin colisionar ni hacer raycast con las paredes. </br>
- **Jugador Control:** Al hacer click en una casilla que no sea una pared, el jugador usa el algoritmo A* para encontrar el camino mas corto hacia su casilla destino, una vez llega, se queda esperando. </br>

### MÁQUINA DE ESTADOS CANTANTE

El pseudocódigo del algoritmo de MÁQINA DE ESTADOS CANTANTE utilizado es:
```python
class MyFSM:
            # Define the names for each state.
            enum State:
                 PATROL
                 DEFEND
                 SLEEP

            # The current state.
            myState: State

            function update():
                        # Find the correct state.
                        if myState == PATROL:
                                    # Example transitions.
                                    if canSeePlayer():
                                                myState = DEFEND
                                    else if tired():
                                                myState = SLEEP

                        else if myState == DEFEND:
                                    # Example transitions.
                                    if not canSeePlayer():
                                                myState = PATROL

                        else if myState == SLEEP:
                                    # Example transitions.
                                    if not tired():
                                                myState = PATROL

            function notifyNoiseHeard(volume: float):
            if myState == SLEEP and volume > 10:
            myState = DEFEND

```
 (Pag 342.AI for Games)

 ### ÁRBOL DE COMPORTAMIENTO 
 
El pseudocódigo del algoritmo de generadorLaberinto es:

```python

class DecisionTreeNode:
# Recursively walk through the tree.
function makeDecision() -> DecisionTreeNode

class Action extends DecisionTreeNode:
function makeDecision() -> DecisionTreeNode:
return this

class Decision extends DecisionTreeNode:
trueNode: DecisionTreeNode
falseNode: DecisionTreeNode

# Defined in subclasses, with the appropriate type.
function testValue() -> any

# Perform the test.
function getBranch() -> DecisionTreeNode

# Recursively walk through the tree.
function makeDecision() -> DecisionTreeNode

class FloatDecision extends Decision:
minValue: float
maxValue: float

function testValue() -> float

function getBranch() -> DecisionTreeNode:
if maxValue >= testValue() >= minValue:
return trueNode
else:
return falseNode

```

### SEGUIMIENTO

El pseudocódigo del algoritmo de seguimiento es:

```python
class KinematicSeek:
 character: Static
 target: Static

 maxSpeed: float

 function getSteering() -> KinematicSteeringOutput:
 result = new KinematicSteeringOutput()

 # Get the direction to the target.
 result.velocity = target.position - character.position

 # The velocity is along this direction, at full speed.
 result.velocity.normalize()
 result.velocity *= maxSpeed

 # Face in the direction we want to move.
 character.orientation = newOrientation(
 character.orientation,
 result.velocity)

 result.rotation = 0
 return result
```
El comportamiento de seguimiento necesita los datos del personaje y los datos de su objetivo. Los datos tienen estos dos parámetros.
La clase Direction del proyecto de unity:
 Vector 3 direction
 float angular.
Se calcula el vector director del personaje al objetivo y solicita una velocidad a lo largo de esta línea. Los valores de orientación generalmente se ignoran, aunque podemos usar newOrientation arriba para mirar en la dirección en la que nos estamos moviendo.

Para empezar se declaran las variables que se utilizarán en la función, el objeto del character, el target y la máxima velocidad del character al perseguirlo.
En la función getSteering(), se calcula la dirección en la cual el character tiene que moverse para perseguir al target mediante una resta entre las dos posiciones, y se almacena en la velocidad del parámetro de salida “result”.
Después se utiliza un .normalized() para normalizar la dirección, y que haya una velocidad constante sin tener en cuenta cómo de lejos está el target.
Se multiplica por la variable maxSpeed para obtener la velocidad intencionada para el carácter.
La orientación del carácter es modificada para que mire en la dirección que acabamos de calcular (almacenada en la variable result).
Por último, ignora la rotación del resultado y lo devuelve como output de la función.

El resultado de esta función es el parámetro “Result”, el cual almacena la distancia que se tiene que mover el character para acercarse a la posición del target.(Pag 72.AI for Games)

### MERODEAR

El pseudocódigo del algoritmo de merodear es:

```python
class KinematicWander:

    character: Static
    maxSpeed: float
    
    timer: float    # 
    maxTime: float  # how many seconds before selecting a new direction

    # The maximum rotation speed we’d like, probably should be smaller
    # than the maximum possible, for a leisurely change in direction.
    maxRotation: float
    
    function getSteering() -> KinematicSteeringOutput:
        result = new KinematicSteeringOutput()

        # Get velocity from the vector form of the orientation.
        result.velocity = maxSpeed * character.orientation.asVector()

        if timer > maxTime:
            # Change our orientation randomly.
            result.rotation = randomBinomial() * maxRotation
            timer = 0;
        
        else:
            timer += Time.deltaTime

     return result
```

Para empezar se declaran las variables que se utilizarán en la función, el objeto del character, la máxima velocidad, y la máxima rotación del character al avanzar.
La función get Steering() toma como parámetro de output una variable de tipo Kinematic Steering Output
La velocidad del resultado se calcula con la máxima velocidad multiplicado por la orientación actual del character como un vector.
Después se randomiza la rotación con un randomBinomial().

Se utiliza un randomBinomial() para generar la rotación.Esta función devuelve valores comprendidos entre -1 y 1 dando prioridad a los valores más cercanos al 0. Esto significa que nuestro personaje tendrá más probabilidad de moverse en su misma dirección con una poca rotación. Con esta función binomial evitamos que el personaje gire bruscamente, aunque esto puede llegar a ser posible si se da la probabilidad.(Pag 76.AI for Games)



## Pruebas y métricas

- [Vídeo con la batería de pruebas](https://youtu.be/zTtAYfi_qis)
En el video se pueden ver las siguientes pruebas:

- **Apartado A**: </br>
- **Apartado B**:</br>
- **Apartado C y E**:</br>
- **Apartado D**: </br>

## Ampliaciones

Se han realizado las siguiente ampliaciones:

- Movimiento manual del jugador con A* 
- Merodeo del Minotauro con A*
- Interfaz de botones que permite: </br>
            - Reestablecer la escena </br>
            - Activar y desactivar el smooth del camino </br>
            - Cambiar la heurística </br>
            - Ver el merodeo del minotauro </br>
- El hilo no se dibuja si no hay salida y suena un sonido frustante

## Producción

Las tareas se han realizado y el esfuerzo ha sido repartido entre los autores.

| Estado  |  Tarea  |  Fecha  |  
|:-:|:--|:-:|
| ✔ | Diseño: Primer borrador | 28-2-2023 |
| :x: | Característica A: movimiento con click izquierdo del ratón | - |
| :x: | Característica B: Minotauro merodeo| - |
| :x: | Característica B: Minotauro seguimiento cuando detecta al jugador| - |
| :x: | Característica C: Camino más corto a la baldosa de salida calculado mediante A*| - |
| :x: | Característica D: suavizar o no el camino generado| - |
| :x: | Característica E: personaje navega y se mueve automáticamente en dirección a la baldosa| - |
|   | ... | |
|  | OPCIONAL |  |
| :x: | Generador del laberinto | - |
| :x: |  | - |
| :x: |  | - |


## Referencias

Los recursos de terceros utilizados son de uso público.

- *AI for Games*, Ian Millington.
- [Kaykit Medieval Builder Pack](https://kaylousberg.itch.io/kaykit-medieval-builder-pack)
- [Kaykit Dungeon](https://kaylousberg.itch.io/kaykit-dungeon)
- [Kaykit Animations](https://kaylousberg.itch.io/kaykit-animations)

