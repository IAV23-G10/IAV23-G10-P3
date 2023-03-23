
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

### A*

El pseudocódigo del algoritmo de A* utilizado es:
```python
 function pathfindAStar(graph: Graph,
 start: Node,
 end: Node,
 heuristic: Heuristic
 ) -> Connection[]:
 # This structure is used to keep track of the
 # information we need for each node.
 class NodeRecord:
node: Node
 connection: Connection
 costSoFar: float
 estimatedTotalCost: float

 # Initialize the record for the start node.
 startRecord = new NodeRecord()
 startRecord.node = start
 startRecord.connection = null
 startRecord.costSoFar = 0
 startRecord.estimatedTotalCost = heuristic.estimate(start)

 # Initialize the open and closed lists.
 open = new PathfindingList()
 Chapter 4 Pathfinding
 open += startRecord
 closed = new PathfindingList()

 # Iterate through processing each node.
 while length(open) > 0:
 # Find the smallest element in the open list (using the
 # estimatedTotalCost).
 current = open.smallestElement()

 # If it is the goal node, then terminate.
 if current.node == goal:
 break

 # Otherwise get its outgoing connections.
 connections = graph.getConnections(current)

 # Loop through each connection in turn.
 for connection in connections:
 # Get the cost estimate for the end node.
 endNode = connection.getToNode()
 endNodeCost = current.costSoFar + connection.getCost()

 # If the node is closed we may have to skip, or remove it
 # from the closed list.
 if closed.contains(endNode):
 # Here we find the record in the closed list
 # corresponding to the endNode.
 endNodeRecord = closed.find(endNode)

 # If we didn’t find a shorter route, skip.
 if endNodeRecord.costSoFar <= endNodeCost:
 continue

 # Otherwise remove it from the closed list.
 closed -= endNodeRecord

 # We can use the node’s old cost values to calculate
 # its heuristic without calling the possibly expensive
 # heuristic function.
 endNodeHeuristic = endNodeRecord.estimatedTotalCost -
 endNodeRecord.costSoFar

 # Skip if the node is open and we’ve not found a better
 # route.
 else if open.contains(endNode):
 # Here we find the record in the open list
 # corresponding to the endNode.
 endNodeRecord = open.find(endNode)

 # If our route is no better, then skip.
 if endNodeRecord.costSoFar <= endNodeCost:
 continue

 # Again, we can calculate its heuristic.
 endNodeHeuristic = endNodeRecord.cost -
 endNodeRecord.costSoFar

 # Otherwise we know we’ve got an unvisited node, so make a
 # record for it.
 else:
 endNodeRecord = new NodeRecord()
 endNodeRecord.node = endNode

 # We’ll need to calculate the heuristic value using
 # the function, since we don’t have an existing record
 # to use.
 endNodeHeuristic = heuristic.estimate(endNode)

 # We’re here if we need to update the node. Update the
 # cost, estimate and connection.
 endNodeRecord.cost = endNodeCost
 endNodeRecord.connection = connection
 endNodeRecord.estimatedTotalCost = endNodeCost +
endNodeHeuristic

 # And add it to the open list.
 if not open.contains(endNode):
 open += endNodeRecord

 # We’ve finished looking at the connections for the current
 # node, so add it to the closed list and remove it from the
 # open list.
 open -= current
 closed += current

 # We’re here if we’ve either found the goal, or if we’ve no more
 # nodes to search, find which.
 if current.node != goal:
 # We’ve run out of nodes without finding the goal, so there’s
 # no solution.
 return null

 else:
 # Compile the list of connections in the path.
 Chapter 4 Pathfinding
 path = []

 # Work back along the path, accumulating connections.
 while current.node != start:
 path += current.connection
 current = current.connection.getFromNode()

 # Reverse the path, and return it.
 return reverse(path)

```
 (Pag 240.AI for Games)
 
La funcion pathFindStar utiliza la clase NodeRecord para almacenar información útil en cada casilla para el calculo del path, con variables como el coste real hasta el origen, la estimación hasta la meta y la conexión con la casilla desde la que se ha calculado el camino. </br>
Utiliza 2 listas para el orden de prioridad de procesamiento de casillas, una para las que se va a procesar, y otra con las ya procesadas
Para el calculo del camino utilizara un bucle del que solo se podrá salir con la condición de que el nodo que se esta procesando sea el nodo meta.
Empezara procesando la casilla inicial, recorre sus conexiones y calcula los costes a esas casillas, entonces comprueba si la casilla ya ha sido procesada o todavia merece la pena seguir con esa casilla estimando su coste total, en caso de no valer la pena por el alto coste, se salta la casilla. Si el nodo es apto, se crea y se añade a la lista open. </br>
Una vez que se han recorrido todas las conexiones, se mete el Nodo en la lista de nodos cerrados (close).
Despues comprobar si hay mas nodos, si no hay mas y no se ha encontrado la salida, no hay solución (se devuelve null).

 ### Generador de laberinto

El pseudocódigo del algoritmo de generadorLaberinto es:

```python
function maze(level: Level, start: Location):
 # A stack of locations we can branch from.
 locations = [start]
 level.startAt(start)

 while locations:
 current = locations.top()

# Try to connect to a neighboring location.
 next = level.makeConnection(current)
 if next:
 # If successful, it will be our next iteration.
 locations.push(next)
 else:
 locations.pop()



 class Level:
 function startAt(location: Location)
function makeConnection(location: Location) -> Location

 class Location:
 x: int
 y: int

 class Connections:
 inMaze: bool = false
directions: bool[4] = [false, false, false, false]

class GridLevel:
 # dx, dy, and index into the Connections.directions array.
 NEIGHBORS = [(1, 0, 0), (0, 1, 1), (0, -1, 2), (-1, 0, 3)]

 width: int
 height: int
 cells: Connections[width][height]

 function startAt(location: Location):
 cells[location.x][location.y].inMaze = true

 function canPlaceCorridor(x: int, y: int, dirn :int) -> bool:
 # Must be in-bounds and not already part of the maze.
 return 0 <= x < width and
 0 <= y < height and
 not cells[x][y].inMaze

 function makeConnection(location: Location) -> Location:
 # Consider neighbors in a random order.
 neighbors = shuffle(NEIGHBORS)

 x = location.x
 y = location.y
 for (dx, dy, dirn) in neighbors:

 # Check if that location is valid.
 nx = x + dx
 ny = y + dy
 fromDirn = 3 - dirn
 if canPlaceCorridor(nx, ny, fromDirn):
 # Perform the connection.
 cells[x][y].directions[dirn] = true
 cells[nx][ny].inMaze = true
 cells[nx][ny].directions[fromDirn] = true
 return Location(nx, ny)

 # null of the neighbors were valid.
 return null


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

