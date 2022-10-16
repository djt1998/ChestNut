using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Networking;

public class DataSenderController : MonoBehaviour
{
    // URL
    [SerializeField] private string URL;    // = "https://docs.google.com/forms/d/e/1FAIpQLSePz3EsxIRK0KUICpWOA31I30ossPnruJ_Zai7Nz78bydreAA/formResponse";
    public string[] entryList;

    // singleton instance
    public static DataSenderController instacne;

    private void Awake() {
        if (instacne == null) {
            DontDestroyOnLoad(gameObject);
            instacne = this;
        }
        else if (instacne != this) {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    public void Send(string _tag, string _time) {
        StartCoroutine(post(_tag, _time));
    }

    private IEnumerator post(string _tag, string _time) {
        WWWForm form = new WWWForm();
		form.AddField(entryList[0], GlobalData.SESSION_ID);
		form.AddField(entryList[1], _tag);
		form.AddField(entryList[2], _time);

		// Send responses and verify result
		using (UnityWebRequest www = UnityWebRequest.Post(URL, form))
		{
		    yield return www.SendWebRequest();

		    if (www.result != UnityWebRequest.Result.Success)
		    {
		        Debug.Log(www.error);
		    }
		    else
		    {
		        Debug.Log("Form upload complete!");
		    }
		}
    }
}
