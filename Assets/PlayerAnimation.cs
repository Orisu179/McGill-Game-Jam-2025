using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Rigidbody2D _rb;
    public Sprite[] run;
    float direction = 1;
    private float runCounter;
    private SpriteRenderer mySprite;
    private int spriteNum;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mySprite = gameObject.GetComponent<SpriteRenderer>();
        _rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal");

        if(inputX != 0){
            //running
            direction = inputX;

            if(runCounter > 0.06f){
                spriteNum += 1;

                if(spriteNum > run.Length - 1){
                    spriteNum = 0;
                }

                runCounter = 0;
            }

            mySprite.sprite = run[spriteNum];
        }

        runCounter += Time.deltaTime;

        if(direction == 1){
            mySprite.flipX = false;
        }
        else{
            mySprite.flipX = true;
        }
    }
}
