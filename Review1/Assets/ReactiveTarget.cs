using UnityEngine;
using System.Collections;

public class ReactiveTarget : MonoBehaviour {

    [SerializeField] GameObject tombstonePrefab;
    private float angle = 90;
    private GameObject tombstone;
	public void ReactToHit() {
		WanderingAI behavior = GetComponent<WanderingAI>();
		if (behavior != null) {
			behavior.SetAlive(false);
		}
		StartCoroutine(Die());
	}

	private IEnumerator Die() {
        // save starting rotation position
        Quaternion startRotation = transform.rotation;
        var toAngle = Quaternion.Euler(transform.eulerAngles + new Vector3(-90, 0, 0));

        // rotate until reaching angle
        for (float t = 0f; t < 1; t += Time.deltaTime)
        {

            transform.rotation = Quaternion.Lerp(startRotation, toAngle, t);

            yield return null;
        }

        // delay here
        yield return new WaitForSeconds(1);

        Destroy(this.gameObject);

        tombstone = Instantiate(tombstonePrefab) as GameObject;
        tombstone.transform.position = this.transform.position;

        GameObject.Find("Controller").GetComponent<SceneController>().death();
    }
}
