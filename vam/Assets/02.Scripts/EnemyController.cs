using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody theRB;
    public float moveSpeed;
    Transform target;

    public float damage;

    public float hitWaitTime = 1f;
    float hitCounter;

    public float health = 5f;

    public float KnockBackTime = 0.5f;
    public float KnockBackCounter;

    public int expToGive = 1;

    public int CoinValue = 1;
    public float coninDropRate = 0.5f;

    Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = Player_controller.instance.transform;
        target = player;
    }

    // Update is called once per frame
    void Update()
    {
        if(player.gameObject.activeSelf == true)
        {
            if(KnockBackCounter > 0)
            {
                KnockBackCounter -= Time.deltaTime;

                if(moveSpeed > 0)
                {
                    moveSpeed = -moveSpeed * 2f;
                }

                if(KnockBackCounter < 0)
                {
                    moveSpeed = Mathf.Abs(moveSpeed * 0.5f);
                }
            }

            if(Vector3.Distance(target.position, transform.position) > 1.0f)
            {
                theRB.velocity = (target.position - transform.position).normalized * moveSpeed;
            }

            if(hitCounter > 0f)
            {
                hitCounter -= Time.deltaTime;
            }
        }
        else
        {
            theRB.velocity = Vector3.zero;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "Player" && hitCounter <= 0f)
        {
            //PlayerHealthController.instance.TackDamage(damage);
            hitCounter = hitWaitTime;
        }
    }

    public void TakeDamage(float damageToTake)
    {
        health -= damageToTake;

        if(health <= 0f)
        {
            Destroy(gameObject);
            //TODO ExerienceLevelController 구현
            
        }
        else
        {
            //TODO SFXManager
        }

        //TODO DamageNumberController 구현
    }
    public void TakeDamage(float damageToTake, bool shouldKnockback)
    {
        TakeDamage(damageToTake);

        if(shouldKnockback == true)
        {
            KnockBackCounter = KnockBackTime;
        }
    }
}
