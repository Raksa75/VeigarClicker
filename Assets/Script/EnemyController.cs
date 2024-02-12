using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    // Texte de l'ennemi
    public TMP_Text HpText;

    // Statistiques initiales de l'ennemi
    private float initialHealth;
    private float initialMoney;

    // Points de vie de l'ennemi
    public float health = 100f;
    public float money = 100f;

    // R�f�rence au prefab de l'ennemi pour la r�apparition
    public GameObject enemyPrefab;

    // M�thode appel�e lorsqu'un clic gauche est d�tect� sur l'ennemi
    private void OnMouseDown()
    {
        // V�rifiez si le bouton gauche de la souris est enfonc�
        if (Input.GetMouseButtonDown(0))
        {
            // Obtenez le script Veigar attach� � l'objet Veigar
            Veigar veigar = FindObjectOfType<Veigar>();

            // Si un script Veigar est trouv�
            if (veigar != null)
            {
                // Faites subir des d�g�ts � l'ennemi �quivalents aux d�g�ts d'attaque de Veigar
                TakeDamage(veigar.attackDamage);
            }
        }
    }

    // M�thode pour infliger des d�g�ts � l'ennemi
    public void TakeDamage(float damageAmount)
    {
        // Soustraire les d�g�ts des points de vie de l'ennemi
        health -= damageAmount;

        // V�rifier si l'ennemi est mort
        if (health <= 0)
        {
            Die(); // Si l'ennemi est mort, appelez la m�thode Die()
        }
    }

    // M�thode appel�e lorsque l'ennemi est mort
    private void Die()
    {
        // D�sactiver le texte de l'ennemi
        HpText.enabled = false;

        // Attendre 0.5 seconde avant de faire r�appara�tre un nouvel ennemi
        StartCoroutine(RespawnEnemy(0.5f));

        // Gagner de l'argent lorsque l'ennemi meurt
        Veigar veigar = FindObjectOfType<Veigar>();
        if (veigar != null)
        {
            veigar.EarnMoney(money);
        }
    }

    // Coroutine pour la r�apparition de l'ennemi apr�s un d�lai
    private IEnumerator RespawnEnemy(float delay)
    {
        // Attendre pendant le d�lai sp�cifi�
        yield return new WaitForSeconds(delay);

        // Activer le texte de l'ennemi
        HpText.enabled = true;

        // Instancier un nouvel ennemi � la position actuelle
        GameObject newEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);

        // R�cup�rer les statistiques initiales du nouvel ennemi
        EnemyController newEnemyController = newEnemy.GetComponent<EnemyController>();
        newEnemyController.health = initialHealth;
        newEnemyController.money = initialMoney;
    }

    private void Start()
    {
        // Sauvegarder les statistiques initiales de l'ennemi
        initialHealth = health;
        initialMoney = money;

        UpdateEnemyHUD();
    }

    private void UpdateEnemyHUD()
    {
        // Mettre � jour le texte de chaque objet texte avec les valeurs correspondantes
        HpText.text = "HP :" + health.ToString();
    }
}
