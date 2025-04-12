using UnityEngine;

public class DungeonEnemy : MonoBehaviour
{
    public float health = 100f;
    public float attack = 10f;
    public float range = 5f;
    public float moveSpeed = 2.5f;

    private Transform player;
    private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player").transform;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //insert AI/bot
    }
    void AttackPlayer() {

    }
    void takeDamage(float takeDamage) 
    {
        health -= takeDamage;
        if(health <= 0) {
            Die();
        }
    }
    void Die() {
        Destroy(gameObject);
    }
}
