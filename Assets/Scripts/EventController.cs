using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EventController : MonoBehaviour
{
    [SerializeField] TMP_InputField[] text0 = new TMP_InputField[4];
    [SerializeField] TMP_InputField[] text1 = new TMP_InputField[4];
    [SerializeField] TMP_InputField[] text2 = new TMP_InputField[4];
    [SerializeField] TMP_InputField[,] textA = new TMP_InputField[4, 3];
    [SerializeField] TMP_InputField[] textR = new TMP_InputField[3];
    MathCalculation mathCalculation;
    public float[] risultati = new float[3];
    public float[,] test = new float[4, 3];

    void Start()
    {
        mathCalculation = GetComponent<MathCalculation>();
    }

    public void OnButtonClick()
    {
        AssemblaText();
        test = mathCalculation.ConfermaNumeri(textA);
        risultati = mathCalculation.CalcolaSistema(test);
        for (int i = 0; i < 3; i++)
        {
           textR[i].text = risultati[i].ToString();
        }
        float a = float.Parse(text0[0].text);
        Debug.Log(a);
    }

    void AssemblaText()
    {
        for (int i = 0; i < 4; i++)
        {
            textA[i, 0] = text0[i];
        }
        for (int i = 0; i < 4; i++)
        {
            textA[i, 1] = text1[i];
        }
        for (int i = 0; i < 4; i++)
        {
            textA[i, 2] = text2[i];
        }
    }
}
