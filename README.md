Asignacion grupal para el laboratorio de Desarrollo de Software I

Mandato:
Desarrolle un programa en C# que implemente el juego Master Mind.

El programa deberá generar de forma aleatoria una clave secreta compuesta por 4 números diferentes en un rango del 1 al 9. El jugador dispondrá de 10 intentos para descubrir la combinación correcta.

REQUISITOS

La clave debe generarse de manera aleatoria.
Ningún número de la clave puede repetirse.
El usuario deberá ingresar 4 números separados por espacios.
El programa debe validar que:
Se ingresen exactamente 4 números.
Los números estén entre 1 y 9.
No existan números repetidos en la entrada.
La entrada sea numérica.
Después de cada intento, el programa mostrará pistas según las siguientes reglas:
C: número correcto en la posición correcta.
F: número correcto, pero en una posición incorrecta.
X: número que no pertenece a la clave.
Si el usuario adivina los 4 números en sus posiciones correctas, gana la partida.
La puntuación se calculará en función de los intentos utilizados.
Si el usuario no adivina la clave después de los 10 intentos, el programa deberá mostrar la combinación secreta.
El programa deberá implementar el uso de clases.
El programa deberá manejar adecuadamente las excepciones que puedan producirse durante la entrada y validación de datos.
EJEMPLO

Clave secreta:
4 6 3 5

Entrada del usuario:
1 6 5 2

Salida:
X C F X

Explicación:

El número 6 está en la posición correcta → C
El número 5 pertenece a la clave, pero está en una posición incorrecta → F
Los números 1 y 2 no pertenecen a la clave → X
