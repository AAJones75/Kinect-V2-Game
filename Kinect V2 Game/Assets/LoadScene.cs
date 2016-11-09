using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{

    public string scene;
    public Button button;

    // Use this for initialization
    protected virtual void Start()
    {
        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(Load);
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected void Load()
    {
        SceneManager.LoadScene(scene);
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Game Character")
        {
            Load();
        }
    }
}
