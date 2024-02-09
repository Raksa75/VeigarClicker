using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Points de vie de l'ennemi
    public float health = 100f;

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
        // Obtenez le script Veigar attach� � l'objet Veigar
        Veigar veigar = FindObjectOfType<Veigar>();

        // Si un script Veigar est trouv�
        if (veigar != null)
        {
            // Rel�chez de l'argent (vous pouvez ajuster la quantit� d'argent rel�ch�e selon vos besoins)
            veigar.EarnMoney(veigar.money);

            // D�truisez l'objet ennemi
            Destroy(gameObject);
        }
    }
}
