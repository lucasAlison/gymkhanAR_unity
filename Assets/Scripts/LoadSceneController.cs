using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System;
using UnityEngine;
using TMPro;
using DataHelpers;
#if PLATFORM_ANDROID
using UnityEngine.Android;
#endif

public class LoadSceneController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text = null;

	private static DataHelper dataHelper = null;
    
    void Start()
    {
        #if PLATFORM_ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.Camera)) {
            Permission.RequestUserPermission(Permission.Camera);
        }
        #endif
        if (dataHelper == null)
        {               
            Application.deepLinkActivated += onDeepLinkActivated;
            if (!String.IsNullOrEmpty(Application.absoluteURL))
            {
                onDeepLinkActivated(Application.absoluteURL);
            }else{
                //onDeepLinkActivated(Application.absoluteURL);
                text.text = "Não foi possivel carregar a cena!!!! :( \nToken não informado";
			}
            DontDestroyOnLoad(gameObject);
        }
        else
        {
			loadScene();
            Destroy(gameObject);
        }
		Debug.Log(text.text);
    }
 
    private void onDeepLinkActivated(string url)
    {
		dataHelper = DataHelper.getInstance();
        dataHelper.setToken(url.Split("?token="[0])[1].Split("="[0])[1]);
        //dataHelper.setToken("fdc26fa3-d648-4905-8825-1e984dd5b79d");        
        loadScene();
    }

	private void loadScene()
	{		
		try 
        {
	       	SceneManager.LoadScene(dataHelper.getLevel().getNextScene());
        }
        catch (Exception e)
        {
            text.text = "Não foi possivel carregar a cena!!!! :( \n\n"+e;
        }
	}
}
