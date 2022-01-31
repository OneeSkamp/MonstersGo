using UnityEngine;

public class MonsterController : MonoBehaviour {
    public int type;

    private float timer = 0;
    private float duration = 3f;

    private void Update() {
        timer += Time.deltaTime;

        if (timer > duration) {
            Destroy(gameObject);
        }
    }
}
