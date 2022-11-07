using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SettingButtonClick : MonoBehaviour, IPointerDownHandler
{
    public bool buttonPressed;
 
    public void OnPointerDown(PointerEventData eventData){
        buttonPressed = true;
    }
}
