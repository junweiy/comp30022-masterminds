using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StartButtonUIController : MonoBehaviour {


    private static float CoolDown = 0.5f;
    private float currentCoolDown = 0.5f;
    private Image startImage;
    private bool increaseCoolDown = false;

	// Use this for initialization
	void Start () {
        startImage = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
        Color temp = startImage.color;
        temp.a = currentCoolDown / CoolDown;
        startImage.color = temp;
        if (currentCoolDown > 0 && !increaseCoolDown)
        {
            currentCoolDown -= Time.deltaTime;
        }
        else if (currentCoolDown <= 0 && !increaseCoolDown)
        {
            currentCoolDown += Time.deltaTime;
            increaseCoolDown = true;
        }
        else if (currentCoolDown < CoolDown+0.3f && increaseCoolDown)
        {
            currentCoolDown += Time.deltaTime;
        }
        else
        {
            currentCoolDown -= Time.deltaTime;
            increaseCoolDown = false;
        }
	}

    public void ToMainMenu()
    {
        StateController.SwitchToMainMenu();
    }
}
