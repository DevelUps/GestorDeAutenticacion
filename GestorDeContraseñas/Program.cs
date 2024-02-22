using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorDeContraseñas
{
    internal class Program
   
    {


        static void Main(string[] args)
        {
            string nombreUsuario, opcion, contraseña;
            (bool contraseñaValida, string mensajeError) verificarContraseña;
            Contraseña contraseña2 = new Contraseña(); // Declarar contraseña2 fuera del switch

            // Imprimir encabezado para el registro
            Console.WriteLine("\t\tRegistro\n\n");

            Console.Write("Ingrese un nombre de usuario:  ");
            nombreUsuario = Console.ReadLine();

            Console.Write("¿Desea que le generemos una contraseña segura si/no ?  ");
            opcion = Console.ReadLine();
            opcion = opcion.ToLower();

            switch (opcion)
            {
                case "si":
                    // Generar una contraseña segura usando la clase Contraseña
                    Contraseña contraseña1 = new Contraseña();
                    contraseña = contraseña1.GenerarContraseña();

                    // Mostrar la contraseña generada al usuario
                    Console.WriteLine($"Esta es la contraseña generada para ti, guárdala en un lugar seguro: {contraseña}");

                    // Indicar al usuario que presione cualquier tecla para terminar el registro
                    Console.WriteLine("\nPresiona cualquier tecla para terminar tu registro ");
                    Console.ReadKey();
                    Console.Clear();

                    // Mostrar al usuario sus datos de acceso
                    Console.WriteLine($"\nTus datos de acceso son \n\tUsuario: {nombreUsuario} \n\tContraseña: {contraseña}");
                    break;

                case "no":
                    // Pedir al usuario que ingrese una contraseña segura hasta que sea válida
                    do
                    {
                        Console.Write("\nIngrese una contraseña segura (la contraseña debe contener entre 8-20 caracteres, incluido un número, una mayúscula, una minúscula y ninguno de los siguientes caracteres. $%&!?): ");
                        contraseña = Console.ReadLine();
                        verificarContraseña = contraseña2.ComprobarContraseña(contraseña);

                        // Si la contraseña no es válida, mostrar mensaje de error
                        if (!verificarContraseña.contraseñaValida)
                        {
                            Console.WriteLine(verificarContraseña.mensajeError);
                        }
                    } while (!verificarContraseña.contraseñaValida); // Repetir hasta que la contraseña sea válida

                    // Indicar al usuario que presione cualquier tecla para terminar el registro
                    Console.WriteLine("\nPresiona cualquier tecla para terminar tu registro ");
                    Console.ReadKey();
                    Console.Clear();

                    // Mostrar al usuario sus datos de acceso
                    Console.WriteLine($"\nTus datos de acceso son \n\tUsuario: {nombreUsuario} \n\tContraseña: {contraseña}");
                    break;

                default:
                    // En caso de que la opción ingresada no sea válida
                    Console.WriteLine("Opción no válida. Por favor, seleccione 'si' o 'no'.");
                    break;
            }
        }
    }

    // Clase para generar y validar contraseñas
    class Contraseña
    {
        // Campos: colecciones de caracteres para generar contraseñas
        string numeros = "0123456789";
        string letrasMin = "abcdefghijklmnopqrstuvwxyz";
        string letrasMay = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string caracterEspecial = "$%#&!?";

        // Contadores para elegir y verificar el número de caracteres de cada grupo
        int numContiene = 0, minContiene = 0, mayContiene = 0, espContiene = 0;

        // Método para generar una contraseña segura
        public string GenerarContraseña()
        {
            string contraseñaGenerada = "";
            Random random = new Random();
            int longitudContraseña = random.Next(8, 21);

            double numTener = longitudContraseña * 0.15;
            double minTener = longitudContraseña * 0.35;
            double mayTener = longitudContraseña * 0.35;
            double espTener = longitudContraseña * 0.15;

            char caracterEscogido;

            while (contraseñaGenerada.Length < longitudContraseña)
            {
                switch (random.Next(0, 4))
                {
                    case 0:
                        if (numContiene < numTener)
                        {
                            caracterEscogido = numeros[random.Next(numeros.Length)];
                            contraseñaGenerada += caracterEscogido;
                            numContiene++;
                        }
                        break;

                    case 1:
                        if (minContiene < minTener)
                        {
                            caracterEscogido = letrasMin[random.Next(letrasMin.Length)];
                            contraseñaGenerada += caracterEscogido;
                            minContiene++;
                        }
                        break;

                    case 2:
                        if (mayContiene < mayTener)
                        {
                            caracterEscogido = letrasMay[random.Next(letrasMay.Length)];
                            contraseñaGenerada += caracterEscogido;
                            mayContiene++;
                        }
                        break;

                    case 3:
                        if (espContiene < espTener)
                        {
                            caracterEscogido = caracterEspecial[random.Next(caracterEspecial.Length)];
                            contraseñaGenerada += caracterEscogido;
                            espContiene++;
                        }
                        break;
                }
            }
            return contraseñaGenerada;
        }

        // Método para validar una contraseña ingresada por el usuario
        public (bool, string) ComprobarContraseña(string contraseñaPa)
        {
            // Declarar variables para cada criterio de validación
            bool contraseñaValida = false;
            bool hayNumero = false, hayMinuscula = false, hayMayuscula = false, hayEspecial = false;
            string mensajeError = "";

            // Verificar que la contraseña cumpla con la longitud requerida
            if (contraseñaPa.Length >= 8 && contraseñaPa.Length <= 20)
            {
                // Verificar si la contraseña contiene al menos un número
                foreach (char elemento in numeros)
                {
                    if (contraseñaPa.IndexOf(elemento) >= 0)
                    {
                        hayNumero = true;
                        break;
                    }
                }
                if (hayNumero)
                {
                    // Verificar si la contraseña contiene al menos una letra minúscula
                    foreach (char elemento in letrasMin)
                    {
                        if (contraseñaPa.IndexOf(elemento) >= 0)
                        {
                            hayMinuscula = true;
                            break;
                        }
                    }
                    if (hayMinuscula)
                    {
                        // Verificar si la contraseña contiene al menos una letra mayúscula
                        foreach (char elemento in letrasMay)
                        {
                            if (contraseñaPa.IndexOf(elemento) >= 0)
                            {
                                hayMayuscula = true;
                                break;
                            }
                        }
                        if (hayMayuscula)
                        {
                            // Verificar si la contraseña contiene al menos un carácter especial
                            foreach (char elemento in caracterEspecial)
                            {
                                if (contraseñaPa.IndexOf(elemento) >= 0)
                                {
                                    hayEspecial = true;
                                    break;
                                }
                            }
                        }
                    }
                }

                // Si la contraseña cumple con todos los criterios, se considera válida
                if (hayNumero && hayMinuscula && hayMayuscula && hayEspecial)
                {
                    contraseñaValida = true;
                }
                else
                {
                    // Si no cumple con todos los criterios, se asigna un mensaje de error adecuado
                    if (!hayNumero)
                    {
                        mensajeError = "La contraseña debe tener al menos un número. ";
                    }
                    if (!hayMinuscula)
                    {
                        mensajeError += "La contraseña debe tener al menos una letra minúscula. ";
                    }
                    if (!hayMayuscula)
                    {
                        mensajeError += "La contraseña debe tener al menos una letra mayúscula. ";
                    }
                    if (!hayEspecial)
                    {
                        mensajeError += "La contraseña debe tener al menos un carácter especial ($%&#!?). ";
                    }
                }
            }
            else
            {
                // Si la contraseña no cumple con la longitud requerida, se asigna un mensaje de error adecuado
                mensajeError = "La contraseña debe tener entre 8 y 20 caracteres. ";
            }

            // Devolver una tupla con la validez de la contraseña y el mensaje de error correspondiente
            return (contraseñaValida, mensajeError);
        }
    }
}