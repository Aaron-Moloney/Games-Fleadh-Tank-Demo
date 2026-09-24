using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Hitbox : MonoBehaviour
{
    public TextMeshProUGUI textHP;
    public int health = 100;
    public GameObject parentRef;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //health = 100000;
        if (health <= 0)
        {
            textHP.text = "Dead!";
            StartCoroutine(deadTimer());
        }
        else
        {
            textHP.text = "HP: " + health;
        }
    }
    IEnumerator deadTimer()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            health -= 10;
        }
        if (collision.CompareTag("Infantry"))
        {
            health -= 1;
        }
        if (collision.CompareTag("EnemyTank"))
        {
            health -= 25;
        }
        if (collision.CompareTag("EnemySpeed"))
        {
            health -= 5;
        }
        if (collision.CompareTag("Collectable"))
        {
            Destroy(collision.gameObject);
            parentRef.GetComponent<Pivot>().addAmmo();
        }
    }
}
