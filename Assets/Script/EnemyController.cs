using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Points de vie de l'ennemi
    public float health = 100f;

    // Méthode appelée lorsqu'un clic gauche est détecté sur l'ennemi
    private void OnMouseDown()
    {
        // Vérifiez si le bouton gauche de la souris est enfoncé
        if (Input.GetMouseButtonDown(0))
        {
            // Obtenez le script Veigar attaché à l'objet Veigar
            Veigar veigar = FindObjectOfType<Veigar>();

            // Si un script Veigar est trouvé
            if (veigar != null)
            {
                // Faites subir des dégâts à l'ennemi équivalents aux dégâts d'attaque de Veigar
                TakeDamage(veigar.attackDamage);
            }
        }
    }

    // Méthode pour infliger des dégâts à l'ennemi
    public void TakeDamage(float damageAmount)
    {
        // Soustraire les dégâts des points de vie de l'ennemi
        health -= damageAmount;

        // Vérifier si l'ennemi est mort
        if (health <= 0)
        {
            Die(); // Si l'ennemi est mort, appelez la méthode Die()
        }
    }

    // Méthode appelée lorsque l'ennemi est mort
    private void Die()
    {
        // Obtenez le script Veigar attaché à l'objet Veigar
        Veigar veigar = FindObjectOfType<Veigar>();

        // Si un script Veigar est trouvé
        if (veigar != null)
        {
            // Relâchez de l'argent (vous pouvez ajuster la quantité d'argent relâchée selon vos besoins)
            veigar.EarnMoney(veigar.money);

            // Détruisez l'objet ennemi
            Destroy(gameObject);
        }
    }
}
