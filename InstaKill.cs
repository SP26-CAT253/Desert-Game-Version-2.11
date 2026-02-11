using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstaKill : MonoBehaviour

{
    public int health;
    public int maxHealth = 1;
    private bool isDead;

    public GameManaScript gameMana;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //Empty just like my head
    }//

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0 && !isDead)
        {
            isDead = true;
            Destroy(gameObject);
            gameMana.gameOver();
        }
    }

    public void healUp(int amount)
    {
        health += amount;
        if (health >= 1 && !isDead)
        {
            isDead = true;
            Destroy(gameObject);
            gameMana.gameWin();
        }
    }
}
