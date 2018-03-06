using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Life : MonoBehaviour {
    public Image lifeIcon;
    private int life;
    // Use this for initialization
    private void Awake()
    {

        PlayerPrefs.SetInt("Player Life", 3);
    }
    void Start () {
        life = PlayerPrefs.GetInt("Player Life");
        for(int i = 0; i < life; i++)
        {
            Image lifeI = Instantiate(lifeIcon,transform);
            lifeI.transform.SetParent(gameObject.transform,false);
            lifeI.rectTransform.position = new Vector3(20 + 55 * i, 348, 0);
        }
        AddLife();
    }
	void AddLife()
    {
        int life = PlayerPrefs.GetInt("Player Life");
        if (life < 5)
        {
            PlayerPrefs.SetInt("Player Life", life+1);
            Image lifeI = Instantiate(lifeIcon, transform);
            lifeI.transform.SetParent(gameObject.transform, false);
            lifeI.rectTransform.position = new Vector3(20 + 55 * life, 348, 0);
        }
    }
    void RemoveLife()
    {
        int life = PlayerPrefs.GetInt("Player Life");
        if (life > 0)
        {
            PlayerPrefs.SetInt("Player Life", --life);
            Image[] vidas=gameObject.GetComponentsInChildren<Image>();
            Destroy(vidas[vidas.Length - 1]);
        }
       

    }
	// Update is called once per frame
	void Update () {
		
	}
}
