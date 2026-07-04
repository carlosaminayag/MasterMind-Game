namespace MasterMind_terminado;

class Program
    {
        static void Main(String[] args)
        {
            bool jugarDeNuevo = true;
            while (jugarDeNuevo)
            {
               Random numeroaleatorio = new Random();
            bool confirmar = false;
            List<int> claveSecreta = new List<int>();
            Console.Clear();
            Console.WriteLine("Bienvenido a Master Mind!\n");
            Console.WriteLine("Este juego se trata de encontrar la combinación clave de 4 digitos");
            Console.WriteLine("Tendras 10 intentos para encontrar dicha clave, de lo contrario habrás perdido");
            Console.WriteLine("\nX = si el digito no esta dentro de la combinación");
            Console.WriteLine("C = si el digito esta en la combinación y en la posición correcta.");
            Console.WriteLine("F = si el digito esta en la combinación pero no en la posición correcta.");
            Console.WriteLine("\nRecuerda que los digitos no se pueden repetir y deben estar entre 1 y 9");
            Console.WriteLine("\n¡Buena suerte!\n");
            while (claveSecreta.Count < 4)
            {
                int numeroCandidato = numeroaleatorio.Next(1, 10);
                bool existeRepetido = false;

                foreach (int elemento in claveSecreta)
                {
                    if (elemento == numeroCandidato)
                    {
                        existeRepetido = true;
                    }
                }

                if (existeRepetido == false)
                {
                    claveSecreta.Add(numeroCandidato);
                }
            }

            int intentosMax = 10;
            int intento = 1;
            Console.WriteLine();

            while (intento <= intentosMax && !confirmar)
            {
                try
                {
                    Console.Write($"Intento {intento}: Ingresa 4 números separados por espacio: ");
                    string entrada = Console.ReadLine() ?? "";

                    string[] partes = entrada.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                    bool entradaValida = true;
                    List<int> numeros = new List<int>();

                    if (partes.Length != 4)
                    {
                        Console.WriteLine("Entrada inválida. Debes ingresar exactamente 4 números.");
                        entradaValida = false;
                    }
                    else
                    {
                        foreach (string p in partes)
                        {
                            if (entradaValida)
                            {
                                if (int.TryParse(p, out int valor))
                                {
                                    bool repetido = false;

                                    foreach (int numero in numeros)
                                    {
                                        if (numero == valor)
                                        {
                                            repetido = true;
                                        }
                                    }

                                    if (valor < 1 || valor > 9 || repetido)
                                    {
                                        Console.WriteLine("Los números deben estar entre 1 y 9 y no pueden repetirse.");
                                        entradaValida = false;
                                    }
                                    else
                                    {
                                        numeros.Add(valor);
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Solo se permiten números.");
                                    entradaValida = false;
                                }
                            }
                        }

                        if (!entradaValida)
                        {
                            continue;
                        }

                        int[] clave = claveSecreta.ToArray();
                        int[] intentoJugador = numeros.ToArray();

                        char[] pistas = MasterMindAyuda.GetClues(clave, intentoJugador);

                        Console.WriteLine(string.Join(" ", pistas));

                        confirmar = MasterMindAyuda.JugadorGano(pistas);

                        if (confirmar)
                        {
                            Console.WriteLine("¡Felicidades! Has encontrado la combinación secreta.");
                            int puntuacion = SistemaDePuntuacion.CalcularPuntuacion(intento, confirmar);
                            Console.WriteLine($"Su puntuacion fue de: {puntuacion} puntos");
                            break;
                        }

                        if (Finaldeljuego.ShouldRevealCode(intento, confirmar))
                        {
                            int[] claveArray = claveSecreta.ToArray();
                            Finaldeljuego.RevelaCodigoSecreto(claveArray);
                        }
                    }
                    intento++;
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"Ocurrio un error inesperado: {ex.Message}");
                }
            }
            Console.Write("\n¿Deseas jugar de nuevo? (s/n): ");
            string respuesta = Console.ReadLine();
            if (respuesta.ToLower() != "s")
            {
                Console.WriteLine("Gracias por jugar Master Mind. ¡Hasta la próxima!");
                jugarDeNuevo = false; 
            }
            
            }
            Console.ReadKey();
        }

        public static class MasterMindAyuda
        {
            public static char[] GetClues(int[] secretCode, int[] guess)
            {
                char[] clues = new char[4];

                for (int i = 0; i < guess.Length; i++)
                {
                    if (guess[i] == secretCode[i])
                    {
                        clues[i] = 'C';
                    }
                    else
                    {
                        bool existeEnClave = false;

                        foreach (int numeroClave in secretCode)
                        {
                            if (numeroClave == guess[i])
                            {
                                existeEnClave = true;
                            }
                        }

                        if (existeEnClave)
                        {
                            clues[i] = 'F';
                        }
                        else
                        {
                            clues[i] = 'X';
                        }
                    }
                }

                return clues;
            }

            public static bool JugadorGano(char[] clues)
            {
                bool todasCorrectas = true;

                foreach (char c in clues)
                {
                    if (c != 'C')
                    {
                        todasCorrectas = false;
                    }
                }

                return todasCorrectas;
            }
        }

        public class SistemaDePuntuacion 
        {
            public static int CalcularPuntuacion(int intentos, bool ganador)
            {
                if (!ganador)
                {
                    return 0;
                }

                int puntuacionBase = 100;
                int intentosRestantes = 10 - intentos;
                int bonus = intentosRestantes * 10;

                int puntuacionFinal = puntuacionBase + bonus;

                return puntuacionFinal;
            }
        }

        public class Finaldeljuego
        {
            public static void RevelaCodigoSecreto(int[] secretCode)
            {
                Console.WriteLine("\n" + new string('=', 50));
                Console.WriteLine("¡SE ACABARON LOS INTENTOS!");
                Console.WriteLine(new string('=', 50));
                Console.WriteLine("\nLo siento, no lograste adivinar esta vez...");

                string claveFormato = string.Join(" ", secretCode);

                Console.WriteLine($"\nLa clave secreta era: {claveFormato}");
                Console.WriteLine("\n¡Pero no te desanimes! Intenta otra partida.");
                Console.WriteLine(new string('=', 50) + "\n");
            }
            public static bool ShouldRevealCode(int intentos, bool ganador)
            {
                bool seAcabaronLosIntentos = (intentos >= 10);
                bool noGano = !ganador;

                if (seAcabaronLosIntentos && noGano)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

    }
