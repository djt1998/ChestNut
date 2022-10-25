using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchSnowSystemRenderMode : MonoBehaviour
{
    public Material mat;

    // Start is called before the first frame update
    void Start()
    {
        if (GlobalData.needSwitchSnowRenderMode) {
            ParticleSystemRenderer pr = GetComponentInChildren<ParticleSystemRenderer>();
            pr.renderMode = ParticleSystemRenderMode.Billboard;
            pr.material = mat;
        }
    }
}
