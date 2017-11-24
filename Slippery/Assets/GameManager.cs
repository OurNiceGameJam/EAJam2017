using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    
    public GameObject Player1;
    public GameObject Player2;

    Vector3[] m_InitialPos;

    public Text Player1Score;
    public Text Player2Score;
    public Text TimeInSeconds;

    int[] score;

    // Use this for initialization
    void Start () {
        score = new int[2];

        m_InitialPos = new Vector3[2];

        m_InitialPos[0] = Player1.transform.position;
        m_InitialPos[1] = Player2.transform.position;
	}
	
    public void SetNewRound(int loser)
    {
        switch (loser)
        {
            case 1:
                Player1.transform.position = m_InitialPos[0];
                Player1.GetComponent<Rigidbody>().velocity = Vector3.zero;
                score[1]++;
                break;
            case 2:
                Player2.transform.position = m_InitialPos[1];
                Player2.GetComponent<Rigidbody>().velocity = Vector3.zero;
                score[0]++;
                break;
            default:
                Debug.LogError("Something is fucked up");
                break;
        }

        if (score[0] == 5|| score[1] == 5)
        {
            EndGame();
        }

        Player1Score.text = score[0].ToString();
        Player2Score.text = score[1].ToString();
    }

    void EndGame()
    {
        SceneManager.LoadScene("main");
    }
}
