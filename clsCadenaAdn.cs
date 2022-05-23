using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pjunoDatos
{
    public class clsCadenaAdn
    {

        public  bool IsMutant(string[] cadenaAdn)
        {
            bool esMutante = false;
            bool esCuadrada = true;
            bool cumpleBase = true;
            byte[,] matrizAdn;
          
            try
            {
                matrizAdn = validarCadenaAdn(cadenaAdn, ref esCuadrada, ref cumpleBase);
                if (esCuadrada && cumpleBase)
                {
                    esMutante = evaluarMutante(matrizAdn);
                }
                //pendiente manejo para reportar que la cadenaADN no es valida. 
                return esMutante;
            }
            catch
            {
                return esMutante;
            }

        }




        private bool evaluarMutante(byte[,] cadenaAdn)
        {
            int secuencia = 0;
            int secuenciaVertical = 0;
            int secuenciaOblicua = 0;
            bool esMutante = false;

            try
            {

                secuenciaOblicua = 1;

                for (int fila = 0; fila < cadenaAdn.GetLength(0); fila++)
                {
                    secuencia = 1;
                    secuenciaVertical = 1;

                    for (int columna = 0; columna < cadenaAdn.GetLength(0); columna++)
                    {


                        int posNext = columna + 1;
                        if (posNext < cadenaAdn.GetLength(0))
                        {
                            /*validación horizontal*/
                            if (cadenaAdn[fila, columna] == cadenaAdn[fila, columna + 1])
                            {
                                secuencia = secuencia + 1;
                            }
                            else if (secuencia > 1)
                            {
                                secuencia = secuencia - 1;
                            }


                            /*validación vertical*/
                            if (cadenaAdn[columna, fila] == cadenaAdn[columna + 1, fila])
                            {
                                secuenciaVertical = secuenciaVertical + 1;
                            }


                            /*validación oblicua*/
                            if (fila == columna)
                            {
                                if (cadenaAdn[fila, columna] == cadenaAdn[fila + 1, columna + 1])
                                {
                                    secuenciaOblicua = secuenciaOblicua + 1;
                                }
                                else if (secuenciaOblicua > 1)
                                {
                                    secuenciaOblicua = secuenciaOblicua - 1;
                                }
                            }

                        }

                        if (secuencia == 4)
                        {
                            esMutante = true;
                            return esMutante;
                        }
                    }

                    if (secuenciaVertical == 4)
                    {
                        esMutante = true;
                        return esMutante;
                    }

                    if (secuenciaOblicua == 4)
                    {
                        esMutante = true;
                        return esMutante;
                    }
                }

                return esMutante;
            }
            catch
            {
                return false;
            }
        }

        private  byte[,] validarCadenaAdn(string[] cadenaAdn, ref bool esCuadrada, ref bool cumpleBase)
        {

            int longitudCadenaAdn = cadenaAdn.Length;
            string secuencia;
            byte[] valoresASCII;
            byte letra;

            byte[,] matrizAdn = new byte[longitudCadenaAdn, longitudCadenaAdn];

            try
            {
                for (int fila = 0; fila <= cadenaAdn.Length - 1; fila++)
                {
                    secuencia = cadenaAdn[fila];
                    if (secuencia.Length == longitudCadenaAdn)
                    {
                        valoresASCII = Encoding.ASCII.GetBytes(secuencia);

                        for (int col = 0; col <= secuencia.Length - 1; col++)
                        {
                            letra = valoresASCII[col];
                            if (letra == 65 || letra == 67 || letra == 71 || letra == 84 || letra == 97 || letra == 99 || letra == 103 || letra == 116)
                            {
                                matrizAdn[fila, col] = letra;
                            }
                            else
                            {
                                cumpleBase = false;
                                break;
                            }
                        }
                    }
                    else
                    {
                        esCuadrada = false;
                        break;
                    }
                }
                return matrizAdn;
            }
            catch
            {
                return matrizAdn;
            }          
        }
    }
}
