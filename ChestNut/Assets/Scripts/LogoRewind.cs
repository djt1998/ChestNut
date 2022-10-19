using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoRewind : MonoBehaviour
{
    public GameObject generateEffect;

    // Start is called before the first frame update
    void Start()
    {
        if (GlobalData.FirstTimeEnterMenu) {
            GlobalData.FirstTimeEnterMenu = false;
            GetComponent<MeshRenderer>().enabled = false;
            StartCoroutine(generateLogo(2));
        }
        else {
            generateEffect.SetActive(false);
        }
    }

    IEnumerator generateLogo(int countDown) {
        // Instantiate(formationEffect, transform.position + new Vector3(0, 0, -2), transform.rotation);
        // Instantiate(generateEffect, transform.position + new Vector3(0, 0, -2), transform.rotation);
        yield return new WaitForSeconds(countDown);
        GetComponent<MeshRenderer>().enabled = true;
    }
}
