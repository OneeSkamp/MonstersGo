using UnityEngine;
using UnityEngine.SceneManagement;

public enum Rarity {
    Rare,
    Epic,
    Legendary
}

public class MonsterController : MonoBehaviour {
    public Rarity rarity;
    public Sprite icon;

    private float timer = 0;
    private float duration = 4f;

    private void Update() {
        if (SceneManager.GetActiveScene().name != "SampleScene") return;

        timer += Time.deltaTime;

        if (timer > duration) {
            Destroy(gameObject);
        }
    }

    public void Spawn(Transform monsterPos) {
        Instantiate(gameObject, monsterPos.position, monsterPos.rotation);
    }
}
