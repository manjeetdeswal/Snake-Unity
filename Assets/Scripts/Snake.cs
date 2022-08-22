using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Snake : MonoBehaviour
{
    private Vector2 direction =Vector2.right;
    private List<Transform> seagments = new List<Transform>();
    public Transform prefab;
    public int size = 2;

    public Button play ,high;
    private Text text,HighScore;
    private AudioSource source;
    private int score = 0;
    public AudioClip[] clips;
    private GameObject player;
    private GameObject food;
    void Start()
    {
        player = GameObject.Find("Snake");
        food = GameObject.Find("Food");
        source = GetComponent<AudioSource>();
        play = GameObject.Find("Play").GetComponent<Button>();
        high = GameObject.Find("HighScore").GetComponent<Button>();
        text = GameObject.Find("Score").GetComponent<Text>();
        HighScore = GameObject.Find("High").GetComponent<Text>();
        HighScore.gameObject.SetActive(false);
        player.SetActive(false);
        food.SetActive(false);
        Time.timeScale = 0f;
        // ResetState();
    }


    private void Grow()
    {
        
        Transform newSegment = Instantiate(prefab);
        newSegment.position = seagments[seagments.Count - 1].position;
        seagments.Add(newSegment);

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Food")
        {
            
            Grow();
            source.PlayOneShot(clips[0]);
            score++; 
            text.text = score + "";
        }

         if(collision.CompareTag("Obs")) {
            clips[1].LoadAudioData();
            if (clips[1].isReadyToPlay)
            {
                source.PlayOneShot(clips[1]);
            }
            
            showUI();
         
        }
    }

    public  void showUI()
    {

        
        
        player.SetActive(false);
        food.SetActive(false);
        if (PlayerPrefs.GetInt("score") < score)
        {
            PlayerPrefs.SetInt("score", score);
        }
        Time.timeScale = 0f;
        score = 0;
        text.text = "0";
        for (int i = 1; i < seagments.Count; i++)
        {
            Destroy(seagments[i].gameObject);
        }
        seagments.Clear();
        seagments.Add(transform);
        for (int x = 1; x < size; x++)
        {
            seagments.Add(Instantiate(prefab));
        }
        transform.position = Vector3.zero;

        high.gameObject.SetActive(true);

        play.gameObject.SetActive(true);

    }
    public void HighShow()
    {

        HighScore.gameObject.SetActive(true);
        HighScore.text = "Highest Score is : " + PlayerPrefs.GetInt("score");
    }
    public void ResetState()
    { Time.timeScale = 1f;

        HighScore.gameObject.SetActive(false);
        high.gameObject.SetActive(false);

        play.gameObject.SetActive(false);

        player.SetActive(true);
        food.SetActive(true);
        if (PlayerPrefs.GetInt("score") < score)
        {
            PlayerPrefs.SetInt("score", score);
        }
    
        for (int i = 1; i < seagments.Count; i++)
        {
            Destroy(seagments[i].gameObject);
        }
            seagments.Clear();
            seagments.Add(transform); 
            for(int x =1; x<size; x++)
            {
                seagments.Add(Instantiate(prefab));
            }
            transform.position = Vector3.zero;
        
    }

    void Update()
    {
      //  text.text = PlayerPrefs.GetInt("score", 0) + "";
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) )
        {
            direction = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            direction = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            direction = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.D ) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            direction = Vector2.right;
        }
    }
    private void FixedUpdate()
    {

        for(int i =seagments.Count-1; i> 0; i--)
        {
            seagments[i].position = seagments[i - 1].position;
        }
        transform.position = new Vector3(Mathf.Round( transform.position.x + direction.x)
            ,Mathf.Round( transform.position.y + direction.y),
            0f
            );


    }
}
