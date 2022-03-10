using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class MathCalculation : MonoBehaviour
{
    [SerializeField] TMP_InputField[,] texts = new TMP_InputField[4, 3];
    [SerializeField] float[] matrici = new float[4];
    public bool error;
    
    void Start()
    {

    }

    // Il metodo trasforma la matrice 4x3 fatta di InputField in float
    public float[,] ConfermaNumeri(TMP_InputField[,] t)
    {
        float[,] numbers = new float[4, 3];

        for (int i = 0; i < 4; i++)
        {
            for (int e = 0; e < 3; e++)
            {
                try
                {
                    numbers[i, e] = float.Parse(t[i, e].text);
                }
                catch (Exception)
                {
                    error = true;
                    Debug.LogWarning("Format not supported");
                    throw;
                }                 
            }
        }
        return numbers;
    }

    //Il metodo che effettua i calcoli
    public float[] CalcolaSistema(float[,] n)
    {
        //Dichiaro tutte le matrici, monodimensionali, bidimensionali e tridimensionali
        #region DefinizioneMatrici 
        float[] z = new float[3];
        float[,] a = new float[3, 3];
        float[, ,] b = new float[3, 3, 3];
        float[] valoriMezzi = new float[4];
        float[] valoriFiniti = new float[3];
        #endregion

        //Prerparo le matrici necessarie per il calcolo logico, 1 bidimensionale e 1 tridimensionale data dalla sovrapposizione, tramite script mio originale, delle precedenti bidimensionali
        #region For
        for (int i = 0; i < 3; i++)
        {
            z[i] = n[3, i];
        }

        for (int i = 0; i < 3; i++)
        {
            for (int e = 0; e < 3; e++)
            {
                a[i, e] = n[i, e];
            }
        }

        for (int i = 0; i < 3; i++)
        {
            for (int e = 0; e < 3; e++)
            {
                for (int f = 0; f < 3; f++)
                {
                    if (e == i)
                    {
                        b[e, f, i] = z[f];
                    }
                    else
                    {
                        b[e, f, i] = a[e, f];
                    }                  
                }
            }
        }

       // Infine chiamo i metodi, scritti da me, per decodificare le matrici e calcolarne la determinante

        valoriMezzi[0] = MatriceQuadrata(a);
        for (int i = 0; i < 3; i++)
        {
            valoriMezzi[i + 1] = MatriceQuadrata(DaCubicaAQuadrata(b, i));
        }

        for (int i = 0; i < 3; i++)
        {
            try
            {
                valoriFiniti[i] = valoriMezzi[i + 1] / valoriMezzi[0];
            }
            catch (Exception)
            {
                error = true;
                throw;
            }          
        }
        #endregion

        //Restituisco i valori finiti
        return valoriFiniti;
    }

    //Metodo per scomporre la radice cubica
    private float[,] DaCubicaAQuadrata(float[, ,] c, int y)
    {
        float[,] a = new float[3, 3];

        for (int i = 0; i < a.GetLength(0); i++)
        {
            for (int e = 0; e < a.GetLength(1); e++)
            {
                a[i, e] = c[i, e, y];
            }
        }

        return a;
    }

    //Calcola la matrice quadrata avvelendosi di un metodo, scritto successivamente, che compone una matrice 6x3 dalla quale risulta più semplice calcolare la matrice utilizzando il metodo a "scaletta"
    public float MatriceQuadrata(float[,] m)
    {
        float[] finiti = new float[6];

        float[,] n = MatriceRettangolareDaQuadrata(m);

        for (int i = 0; i < 3; i++)
        {
            finiti[i] = n[i, 0] * n[i + 1, 1] * n[i + 2, 2];
        }
        for (int i = 2; i < 5; i++)
        {
            finiti[i + 1] = n[i, 0] * n[i - 1, 1] * n[i - 2, 2];
        }

        return (finiti[0] + finiti[1] + finiti[2] - finiti[3] - finiti[4] - finiti[5]);
    }

    //Prepara le matrici rettangolari per il calcolo
    private float[,] MatriceRettangolareDaQuadrata(float[,] m)
    {
        float[,] n = new float[6, 3];
        for (int i = 0; i < 3; i++)
        {
            for (int e = 0; e < 3; e++)
            {
                n[i, e] = m[i, e];
            }
        }
        for (int i = 0; i < 3; i++)
        {
            for (int e = 0; e < 3; e++)
            {
                n[i + 3, e] = m[i, e];
            }
        }

        return n;

    }

    
}
