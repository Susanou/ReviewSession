using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    private Text message;
    [SerializeField] private float horizontalSpeed = 100;
    private float verticalSpeed = 100;

    // Start is called before the first frame update
    void Start()
    {
        message = gameObject.GetComponent<Text>();
        message.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.Find("Player");
        int currentHealth = player.GetComponent<PlayerCharacter>().getHealth();

        if (currentHealth <= 0)
        {
            message.text = "You are dead!";
        }


        this.transform.Translate(horizontalSpeed * Time.deltaTime, verticalSpeed * Time.deltaTime, 0);

    }
}
