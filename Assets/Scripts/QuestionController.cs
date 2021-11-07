using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DataHelpers;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System.Net;

public class QuestionController : MonoBehaviour
{
    private static int OPTIONS_LENGTH = 3;
    [SerializeField] private TextMeshProUGUI text = null;
    [SerializeField] private TextMeshProUGUI[] optionsTexts = new TextMeshProUGUI[OPTIONS_LENGTH];
    [SerializeField] private Button[] optionsButtons = new Button[OPTIONS_LENGTH];
    
    private bool[] addedCorrections = new bool[OPTIONS_LENGTH];

    void Awake()
    {
        Question question = DataHelper.getInstance().getQuestion();

        text.text = question.text;        

        for (int i = 0; i < OPTIONS_LENGTH; i++)
        {
            int index = i;
            addedCorrections[index] = false;
            QuestionOption option = question.options[index];
            optionsTexts[index].text = option.text;
            if (option.isCorrect)
            {                
                if (!question.respondeu)
                {
                    question.respondeu = true;
                    optionsButtons[index].onClick.AddListener(() =>
                    {
                        optionsTexts[index].text += "\n" + option.correction;
                        addedCorrections[index] = true;
                        optionsButtons[index].enabled = false;
                        StartCoroutine(LoadLevelAfterDelay(1));
                    });
                }
            }
            else 
            {
                optionsButtons[index].onClick.AddListener(() => {
                    if (!addedCorrections[index])
                    {
                        optionsTexts[index].text += "\n" + option.correction;
                        optionsTexts[index].color = new Color32(255, 0, 0, 255);
                        addedCorrections[index] = true;
                    } 
                });
            }
        }
    }
    
    private IEnumerator LoadLevelAfterDelay(float delay)
    {
        string teamActivityId = DataHelper.getInstance().getToken();
        var requisicaoWeb = WebRequest.CreateHttp("http://167.99.237.34/team/activities/perform/" + teamActivityId);
        requisicaoWeb.Method = "POST";
        using (var resposta = requisicaoWeb.GetResponse())
        {
            resposta.Close();
        }
        yield return new WaitForSeconds(delay);
        //SceneManager.LoadScene("Main");
        Application.Quit();
    }
}
