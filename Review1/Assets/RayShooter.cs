using UnityEngine;
using System.Collections;
using UnityEngine.UI; /* Required for controlling Canvas UI system */


public class RayShooter : MonoBehaviour {
	private Camera _camera;
	[SerializeField] private GameObject reticle;

	[SerializeField] private GameObject hp;

	private bool gameOver = true;
	private float dx = 1;
	private float dy = 1;

	void Start() {
		_camera = GetComponent<Camera>();

		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;

		reticle = GameObject.Find("Reticle");
		reticle.GetComponent<Text>().text = "-*-";
		reticle.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
		reticle.GetComponent<RectTransform>().position =
            new Vector3(_camera.pixelWidth / 2.0f - reticle.GetComponent<Text>().fontSize / 4.0f,
                        _camera.pixelHeight / 2.0f - reticle.GetComponent<Text>().fontSize / 2.0f,
                        0.0f);
	}

    /** Deprecated in Unity 2018 
	void OnGUI() {
		int size = 12;
		float posX = _camera.pixelWidth/2 - size/4;
		float posY = _camera.pixelHeight/2 - size/2;
		GUI.Label(new Rect(posX, posY, size, size), "*");
	}
    **/
    

	void Update() {
		if (Input.GetMouseButtonDown(0)) {
			Vector3 point = new Vector3(_camera.pixelWidth/2, _camera.pixelHeight/2, 0);
			Ray ray = _camera.ScreenPointToRay(point);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit)) {
				GameObject hitObject = hit.transform.gameObject;
				ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();
				if (target != null) {
					target.ReactToHit();
				} else {
					StartCoroutine(SphereIndicator(hit.point));
				}
			}
		}

		if(gameOver){
			StartCoroutine(floatBounce(hp.GetComponent<RectTransform>()));
		}
	}

	private IEnumerator SphereIndicator(Vector3 pos) {
		GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		sphere.transform.position = pos;

		yield return new WaitForSeconds(1);

		Destroy(sphere);
	}

	IEnumerator floatBounce(RectTransform pos)
	{
		if (pos.transform.position.x > Screen.width - hp.GetComponent<Text>().preferredWidth/2)
		{
			dx = -1; 
		}
		if (pos.transform.position.x < 0 + hp.GetComponent<Text>().preferredWidth / 2)
		{
			dx = +1; 
		}

		if (pos.transform.position.y > Screen.height - hp.GetComponent<Text>().preferredHeight/2)
		{
			dy = -1; 
		}
		if (pos.transform.position.y < 0 + hp.GetComponent<Text>().preferredHeight / 2)
		{
			dy = +1; 
		}


		pos.Translate(new Vector3(dx, dy, 0));


		Debug.Log(Screen.width + " " + Screen.height);

		

		yield return null;

	}

}