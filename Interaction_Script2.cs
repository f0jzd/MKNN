using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Interaction_Script2 : MonoBehaviour
{


   
    public GameObject hugShower;
    public GameObject Jerry;
    public SpriteRenderer jerryFace;

    public GameObject player;
    public GameObject heartsplosion;
    public GameObject brokenHearts;
    public Button backArrow;

    public AudioClip[] audioList;


    private bool hugCheck;
    private bool hugging;
    private bool fadein = false;
    private bool fadeout = true;
    private bool jeeryHappy = true;

    public LayerMask hitLayer;
    public SpriteRenderer emotion;
    private float randomNumber = 30;
    private float aValue = 1;
    private float t = 2;
    float alpha;
    public Sprite[] spriteList;
    private Color color;
    private int spriteindex = 1;



    

    // Start is called before the first frame update
    void Start()
    {
        brokenHearts.SetActive(false);  // Disable particle effect
        heartsplosion.SetActive(false); // Disable particle effect
        
        emotion.color = new Color(1, 1, 1, 1);// Sets a new color
        alpha = emotion.color.a;
        hugShower.SetActive(true);
        Debug.Log(emotion.color.a);

        //heartsplosion.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        

        //hugging = InteractionScript.hugging;
        //Check if the player is clicking on an NPC, if it hits the NPC a coroutine starts which changes the NPCs face over the timer the player holds down the mouse
        if (hugCheck == true) 
        {
            
            Vector3 mouse = Input.mousePosition;
            Ray castPoint = Camera.main.ScreenPointToRay(mouse);
            RaycastHit hit;

            if (Physics.Raycast(castPoint, out hit))
            {
                if (hit.collider.tag == "NPC" && (Input.GetMouseButton(0)))
                {

                    Debug.Log("hugging npc");
                    StartCoroutine(FadeOut());
                    StartCoroutine(FadeTo());

                    hugging = true;

                }
                if (hit.collider.tag != "NPC" || Input.GetMouseButtonUp(0))
                {
                    hugging = false;


                }
            }
        }


            // Renders different colliders
            if (hugging == true) 
        {
            Debug.Log("huggin in interaction script2 true");
            hugShower.SetActive(true);
            Jerry.SetActive(false);
            //player.SetActive(false);
            
            //Debug.Log(emotion.color.a);
        }

        //Changes the sprites 
        if (hugging == false)
        {
            hugShower.SetActive(false);
            Jerry.SetActive(true);
            //player.SetActive(true);
        }
        if (spriteindex == spriteList.Length)
        {

            fadeout = false;

        }
        if (emotion.sprite.name == "Jerry Face Happy")
        {
            heartsplosion.SetActive(true);
            Debug.Log("JerryHappy");
        }
        if (emotion.sprite.name == "Jeryy Face angry")
        {
            brokenHearts.SetActive(true);

        }
        if (emotion.sprite.name != "Jeryy Face angry" || emotion.sprite.name != "Jerry Face Happy")
        {
            
        }

        jerryFace.sprite = emotion.sprite;


    }//Plays audio on collision
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            hugCheck = true;
            GetComponent<AudioSource>().Play();
        }
    }
    public void OnButtonClick()
    {
        hugCheck = false;
    }






    IEnumerator FadeTo() // Fading to script, there are probably easier ways but since I was new I could not find any easier nor had the time
    {
       
        Debug.Log("starting fadeto");



        while (t < 2f & fadein == true & hugging == true)
        {
            t += Time.deltaTime / randomNumber;
            Color newColor = new Color(1, 1, 1, Mathf.SmoothStep(0, aValue, t));
            color = newColor;
            emotion.color = new Color(1, 1, 1, color.a);

            if (t >= 2f)
            {

                fadeout = true;
                fadein = false;
                yield return null;


            }
            yield return null;


        }

        yield return null;

    }
    IEnumerator FadeOut()
    {
        
        Debug.Log("starting fadeout");



        while (t > 0f & fadeout == true & hugging == true)
        {
            t -= Time.deltaTime / randomNumber;
            Color newColor = new Color(1, 1, 1, Mathf.SmoothStep(0, aValue, t));
            //transform.renderer.material.color = newColor;
            color = newColor;
            emotion.color = new Color(1, 1, 1, color.a);

            if (t < 0.0f)
            {
                
                GetComponent<AudioSource>().clip = audioList[spriteindex];
                GetComponent<AudioSource>().Play();
                emotion.sprite = spriteList[spriteindex];
                spriteindex = spriteindex + 1;
                fadeout = false;
                fadein = true;
                yield return null;


            }
            yield return null;


        }

        yield return null;
    }




}
