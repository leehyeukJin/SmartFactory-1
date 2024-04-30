using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button btn;
    public TMP_InputField inputField;
    public Toggle toggle;
    public Slider slider;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnBtnClkEvent()
    {
        print(inputField.text);
        print(toggle.isOn);

        if(toggle.isOn)
        {
            // 시계 작동
        }
        print(slider.value);
        if(slider.value > 0.5f)
        {
            // 시계의 속도를 50% 빠르게 만든다.
        }
    }
}
