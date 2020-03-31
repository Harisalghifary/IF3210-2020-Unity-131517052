using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private int health = 100;

    private Material matWhite;
    private Material matDefault;
    SpriteRenderer spriteRenderer;
    private UnityEngine.Object explosionRef;
    private UnityEngine.Object enemyRef;

    [SerializeField]
    float delayBeforeDestroy = 5;

    Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        enemyRef = Resources.Load("Enemies");
        spriteRenderer = GetComponent<SpriteRenderer>();
        matWhite = Resources.Load("whiteFlash", typeof(Material)) as Material;
        matDefault = spriteRenderer.material;
        explosionRef = Resources.Load("Explosion");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            health-=25;
            spriteRenderer.material = matWhite;
            if (health<=0)
            {
                KillSelf();
            }
            else
            {
                Invoke("ResetMaterial", .1f);
            }
        }
    }

    void ResetMaterial()
    {
        spriteRenderer.material = matDefault;
    }

    private void KillSelf()
    {
        GameObject explosion = (GameObject)Instantiate(explosionRef);
        explosion.transform.position = new Vector3(transform.position.x, transform.position.y + 3, transform.position.z);
        //Destroy(gameObject);

        //spriteRenderer.enabled = false;
        gameObject.SetActive(false);
        Invoke("Respwan", delayBeforeDestroy);
    }

    void Respwan()  
    {
        GameObject enemyClone = (GameObject)Instantiate(enemyRef);
        enemyClone.transform.position = new Vector3(UnityEngine.Random.Range(startPos.x - 3, startPos.x + 3), startPos.y, startPos.z);


        Destroy(gameObject);
    }
}
