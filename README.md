
# IAV23-G10-P3
# IAV - Base para la Práctica 3


## AUTORES
- [Miriam Martin Sanchez](https://github.com/miriam-m-s)
- [Diego Rol Sanchez](https://github.com/DiegoRol69)
- [Ignacio del Castillo](https://github.com/NachoDelCastillo)

## PROPUESTA

La práctica consiste en desarrollar un prototipo de IA para Videojuegos, dentro de un entorno virtual que represente la ópera de París, con un agente inteligente (el fantasma) que decide, se mueve y actúa según lo que encuentra en sus diferentes estancias, otros agentes más simples como la cantante y el público, y un avatar, el vizconde -némesis del fantasma-, controlado por el jugador.  </br>


Esquema con la topología de las distintas estancias de la ópera. Los nodos del grafo representan estancias identificadas por una abreviatura, las aristas dirigidas representan visibilidad entre ellas, e incluso navegabilidad si las aristas son negras.
El punto de partida propuesto para esta práctica, con la documentación e implementación (código y recursos audiovisuales) necesaria, se encuentra en este repositorio de GitHub: IAV-Decision.

[Enunciado oficial](https://narratech.com/es/inteligencia-artificial-para-videojuegos/decision/historias-de-fantasmas/)

### ESTANCIAS DEL ENTORNO Y COMPORTAMMIENTO</br>

Patio de butacas (P). Es la estancia inicial del público,  dividido en dos bloques (este y oeste). Cada bloque chilla a la mínima que vea al fantasma sobre el escenario, pero permanecerá en su sitio salvo que caiga la lámpara gigante del techo correspondiente (hay lámpara este y oeste), en cuyo caso ese lado del patio de butacas se oscurecerá y los espectadores huirán despavoridos al vestíbulo. No regresarán hasta que no se vuelva a colocar su lámpara. Esta estancia está conectada con el escenario (visibilidad y navegabilidad, como indica la arista negra), con el vestíbulo, y es visible desde los palcos este y oeste, aunque el público no puede alcanzar a ver bien si hay alguien en los palcos, ni aunque estén las dos lámparas encendidas.</br>

Vestíbulo (V). Es la zona más externa de la ópera, donde van los bloques de público cuando se asustan. Simplemente conecta con el patio de butacas.</br>

Escenario (E). Es la estancia inicial de la cantante, donde se dedica a su oficio, aunque lo intercala (cada pocos segundos) con un descanso que realiza tras las bambalinas, una estancia contigua. Además también conecta con el patio de butacas y los palcos, y es posible dejarse caer por una trampilla al sótano oeste, aunque no sea posible regresar. El fantasma no pisará ninguna estancia como esta mientras haya público mirando. Eso sí, tanto en esta estancia como en otras puede “capturar” (coger al hombro) a la cantante, incluso compartiendo estancia con el vizconde y llevársela a donde quiera, soltándola por voluntad propia o porque se sienta intimidado por el “choque” con nuestro héroe. Si la cantante acaba en una estancia que no esté conectada con el escenario, se sentirá confusa y merodeará (pasando de estancia a estancia aleatoriamente), dejándose “rescatar” por el vizconde en caso de que lo vea cerca, con la esperanza de que la lleve hasta una estancia que conozca, para poder retomar así su ritmo normal de trabajo.</br>

Bambalinas (B). Estancia donde suele descansar la cantante y que conecta con el escenario, el sótano oeste y que permite deslizarse por una rampa algo oculta al sótano este, sin posibilidad de regresar.</br>

Palco oeste (Po). Estancia inicial del vizconde, personaje que controla el jugador y que gusta disfrutar desde aquí de la función. El palco tiene una palanca que se puede usar para dejar caer la lámpara oeste del patio de butacas. Conecta con el escenario, con el sótano oeste y permite ver el patio de butacas, aunque debido a la altura no existe visibilidad en el sentido opuesto. El vizconde puede moverse con libertad, como el fantasma, también sobre las barcas cercanas. Puede usar palancas y chocar contra el fantasma, intimidándole y haciendo que retroceda durante unos pocos segundos (y suelte a la cantante si la llevaba). Puede golpear muebles, como los de la sala de música, haciendo un ruido tremendo que se escuchará en toda la zona subterránea. Puede interactuar con una lámpara caída, para arreglarla automáticamente (colocándola en su sitio), y también con la cantante, cogiéndola en brazos automáticamente y llevándola consigo, o dejándola en el suelo a voluntad (interactuando sin que haya una palanca delante ni otra cosa así).</br>

Palco este (Po). Estancia similar al palco oeste, con una palanca que se puede usar para dejar caer la lámpara este del patio de butacas. Conecta con el escenario, con el sótano este y permite ver el patio de butacas, aunque sin visibilidad en el otro sentido. </br>

Sótano oeste (So). Estancia que conecta con el palco oeste, con las bambalinas y con el sótano norte, aunque para recorrer esta conexión hace falta subirse a una barca. Sólo una persona (tal vez con otra al hombro o en brazos) puede montarse sobre la barca a la vez y sólo si esta se encuentra atracada en la orilla de esa estancia. Por defecto, la barca que se necesita aquí comienza atracada en la otra orilla, en la del sótano norte, y aunque en todas las orillas siempre hay una palanca que permite acercarla, el proceso de “recuperar” la barca es algo lento (mayor coste). Se puede llegar a esta estancia desde el escenario, pero no al revés.</br>

Sótano este (Se). Estancia que conecta con el palco este, y tanto con el sótano norte como con la sala de música donde compone su obra el fantasma, aunque para recorrer estas dos últimas conexiones hacen falta barcas. Por defecto, la barca que lleva al sótano norte sí está en esta orilla, pero la que lleva a la sala de música está en la orilla contraria. Aunque se puede llegar a esta estancia desde las bambalinas, por una trampilla, desde aquí no se conecta con las bambalinas.</br>

Celda (C). Estancia donde el fantasma deja a la cantante para completar su secuestro con éxito, usando una palanca que activa unas rejas que la impiden salir (y que por supuesto el vizconde podrá desactivar). Conecta con el sótano norte.</br>

Sótano norte (Sn). Estancia que conecta con la celda, además de con la sala de música, el sótano este y el sótano oeste a través de sus correspondientes tres barcas.</br>

Sala de música (M). Estancia inicial del fantasma, donde le gusta pasar tiempo componiendo su ópera. Conecta mediante una barca con el sótano este, y con otra con el sótano norte. El fantasma tiene el objetivo principal de secuestrar a la cantante, para lo que intentará buscarla en las bambalinas, en el escenario o si no logra dar con ella, explorando las demás estancias meticulosamente por si estuviera “perdida” por allí. No puede acceder al escenario si hay público mirando, de modo que, como objetivo secundario, necesita tirar las dos lámparas del techo para vaciar del todo el patio de butacas. Sea como sea, una vez atrapada la cantante, la llevará consigo hasta la celda, intentando usar siempre el camino con menor coste (recordando la última posición de las barcas y del vizconde que conoce, y eligiendo la ruta con menor coste, la que tenga más barcas a su favor y que evite al héroe de esta historia). Cuando llega hasta la celda la soltará allí, activará las rejas e irá hasta la sala de música, permaneciendo allí indefinidamente. Lo único que desconcentra al fantasma cuando está componiendo es escuchar a su musa cantar de nuevo en el escenario, reavivando sus deseos de secuestrarla y encerrarla otra vez en su celda. Por otro lado, si el fantasma llega a percibir el ruido de los golpes del vizconde a su piano, abortará la misión que esté haciendo (soltando a la cantante) y correrá enfurecido hasta allí para dedicar unos segundos a arreglar semejante estropicio.</br>

### CARACTERÍSTICAS PRINCIPALES DEL PROTOTIPO</br>

**A.** Hay un mundo virtual (la casa de la ópera), con un esquema de división de malla de navegación proporcionado por Unity, donde se ubican todos los elementos descritos anteriormente. El vizconde es controlado por el jugador mediante el ratón y un único clic derecho para interactuar con otros elementos. Hay cámaras que siguen a cada uno de los personajes, incluyendo una que nos dé la vista general del entorno.

**B.** Cada mitad del público huye tras la caída de su lámpara, y regresa en cuanto esta ha sido arreglada. Esto se hace con una navegación y un movimiento triviales, sin apenas decisión.

**C.** La cantante es un agente inteligente basado en una máquina de estados que pasa del escenario a las bambalinas cuando toca, que puede ser “llevada” por los otros dos personajes hasta otra estancia, que merodea desorientada cuando está en las estancias subterráneas, y que se deja llevar por el vizconde, con la esperanza de reencontrar el escenario y continuar su rutina allí. Tiene navegación, movimiento y percepción sencillos, y decisión mediante máquina de estados

**D.** El fantasma funciona mediante un árbol de comportamiento complejo, para buscar a la cantante, tirar las lámparas para deshacerse del público, capturar a la chica, llevarla a la celda, activar las rejas para encerrarla, etc.

**E.** El fantasma también cuenta con un sistema de gestión sensorial para reaccionar a lo que realmente ve (en la propia estancia o en otras que son visibles para él) y oye (el canto de su musa, el grito del público, el ruido de la sala de música…), sin tener que recurrir a información privilegiada sino únicamente recordando lo que ha ido viendo por el mundo.

## PUNTO DE PARTIDA
Se parte de un proyecto base de Unity proporcionado por el profesor aquí:
(https://github.com/Narratech/IAV-Decision)

Se siguen usando el ComportamientoAgente y Agente ya mencionados en la [PRÁCTICA 1](https://github.com/IAV23-G10/IAV23-G10-P1/tree/main/IAV-P1-main)</br>

En la escena de apertura se puede observar un mapa que muestra todas las habitaciones previamente mencionadas junto con sus respectivos pasillos. También se pueden ver modelos de los espectadores, el vizconde, el fantasma y la cantante, así como objetos interactivos como el piano, las palancas y las barcas. En cuanto a los scripts, hay varias clases, entre ellas:</br>
- **Game Blackboard** : contiene las referencias a las habitaciones del mapa así como a todas las variables del mundo. También permite hacer saber al fantasma la palanca que tiene que tocar y la elección de la habitación en la que tiene que buscar a la cantante. 

- **Player** : permite controlar las acciones del vizconde, tanto moverlo como interactuar con el resto de elementos.</br>

- **CameraManager**: permite controlar el cambio de cámara para visualizar distintas estancias del mapa.</br>

- **SiguePersonaje** : permite que la cantante persiga a quien se le asigne.</br>

- **SotanosTrigger** : permite avisar al BlackBoard cuando el fantasma está en el sótano.</br>

- **BarcaTrigger** : permite que el fantasma o el vizconde puedan pasar al otro lado si está la barca, si no lo está se llama a la barca para que cambie de lado y poder transportarlos.</br>

- **Publico** : permite controlar si el público debería huir o quedarse en el patio de butacas.</br>

- **Accion** : permite ver si se ha interactuado con un piano,palanca o puerta.</br>

- **PalancaPuerta** : permite abrir y cerrar la puerta de la celda, actualizando BlackBoard.</br>

- **CantanteCondition** : permite al fantasma saber si la cantante está cantando en el escenario.</br>

- **ControlPiano** : hace sonar el piano si este colisiona con otro objeto.</br>

- **GhostArreglaPianoAction** : permiter al fantasma arreglar el piano.</br>

- **GhostCloseDoorAction** : acción de cerrar la puerta de la celda.</br>

- **GhostChaseAction** : acción de seguir a la cantante para capturarla.</br>

- **GhostLlevarCantante** : acción de ir a por la cantante para raptarla.</br>

- **GhostBreakLights** : acción de romper las luces.</br>

- **GhostWaitsInMusicRoom** : acción de esperar indefinidamente hasta volver a por la cantante.</br>

- **NoLightCondition** : avisa de si hay público.</br>

- **GhostSearchRandomAction** : permite al fantasma ir a una sala random establecida en BlackBoard.</br>

- **ImprisonedCondition** : permite al fantasma saber si la cantante está en la celda.</br>

- **Cantante** : toma de decisiones de la cantante a través de la máquina de estados, entre ellas cuando tiene que cantar y descansar, quien la secuestra, el merodeo  si está perdida y el seguimiento al vizconde si lo ve estando perdida.</br>

- **GhostBehavior** : toma de decisiones del fantasma a través de árbol de comportamiento.</br>

## DISEÑO DE LA SOLUCIÓN
Lo que vamos a realizar para resolver esta práctica es implementar el comportamiento de los diferentes Scripts:

**PLANTEAMINETO INICIAL**: </br>

- **SiguePersonaje:** Usaremos este comportamiento para mover tanto a la cantante como al fantasma, ya sea para que persigan otro personaje o una posicion concreta del mapa (Ej: El fantasma va a una palanca / La cantante va a las bambalinas)

Para el manejo de los comportamientos de la **cantante** usaremos una maquina de estados programada en c# haciendo uso de enumeradores para los nombres de los estados

Para el manejo de los comportamientos del **fantasma** haremos uso de la herramienta de "Behaviour tree" para controlar los diferentes estados


## MÁQUINA DE ESTADOS CANTANTE

<table>
  <tr>
    <th>BOCETO INICIAL</th>
    <th>RESULTADO FINAL</th>
  </tr>
  <tr>
    <td><img src="https://github.com/IAV23-G10/IAV23-G10-P3/blob/main/Assets/ImagesForReadme/image.png" /></td>
    <td><img src="https://github.com/IAV23-G10/IAV23-G10-P3/blob/main/Assets/ImagesForReadme/MaquinaEstados.png"/></td>
  </tr>
</table>

 ## ÁRBOL DE COMPORTAMIENTO FANTASMA

<table>
  <tr>
    <th>BOCETO INICIAL</th>
    <th>RESULTADO FINAL</th>
  </tr>
  <tr>
    <td><img src="https://github.com/IAV23-G10/IAV23-G10-P3/blob/main/Assets/ImagesForReadme/ArbolComportamiento.png"/></td>
    <td><img src="https://github.com/IAV23-G10/IAV23-G10-P3/blob/main/Assets/ImagesForReadme/Fantasma_MaquinaEstados.png"/></td>
  </tr>

## Pruebas y métricas

[Vídeo con la batería de pruebas](https://youtu.be/G2lqGw3u8RQ) </br>

En el video se pueden ver las siguientes pruebas:

- **Apartado A**: </br>
- **Apartado B**:</br>
- **Apartado C**:</br>
- **Apartado D**: </br>
- **Apartado E**: </br>

## Ampliaciones

Parte estética:

  - Cambio en la textura del público
  - Cambio en la textura de las paredes
  - Un miembro del público tiene la cabeza cuadrada



## Producción

Las tareas se han realizado y el esfuerzo ha sido repartido entre los autores.

| Estado  |  Tarea  |  Fecha  |  
|:-:|:--|:-:|
| ✔ | Diseño: Primer borrador | 23-3-2023 |
| ✔ | Característica A: movimiento con click izquierdo del ratón y click derecho para interactuar | 31-3-2023 |
| ✔ | Característica B: Caida de la lampara y movimiento espectadores| 9-4-2023 |
| ✔ | Característica C: Maquina de estados cantante| 11-4-2023 |
| ✔ | Característica D: Árbol de comportamiento fantasma| 12-4-2023 |
| ✔ | Característica E: Sistema sensorial fantasma| 11-4-2023 |
| ✔ | Característica E: personaje navega y se mueve automáticamente en dirección a la baldosa| 8-4-2023 |
|  | OPCIONAL |  |
| ✔ |   Éstetica proyecto | 30-3-2023 |



## Referencias

Los recursos de terceros utilizados son de uso público.

- *AI for Games*, Ian Millington.


