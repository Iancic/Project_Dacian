using TMPro;
using UnityEngine;

public class RoundController : MonoBehaviour
{
    public TMP_Text text;

    void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.SetText("Round " + EnemySpawner.rounds.ToString());
    }
}
