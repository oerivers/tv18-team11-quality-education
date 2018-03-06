namespace GoogleVR.HelloVR {
	using UnityEngine;
	using UnityEngine.SceneManagement;
	using System.Collections;

	[RequireComponent(typeof(Collider))]
	public class ObjController : MonoBehaviour {
		private Vector3 startingPosition;
		private Renderer renderer;

		public Material inactiveMaterial;
		public Material gazedAtMaterial;
		public string scene;

		private IEnumerator coroutine;
		private bool gazedAt;
		private int time;

		void Start() {
			startingPosition = transform.localPosition;
			renderer = GetComponent<Renderer>();
			gazedAt = false;
			SetGazedAt(gazedAt);
			coroutine = Selection ();
			StartCoroutine (coroutine);
		}

		public void SetGazedAt(bool gazed) {
			this.time = 0;
			this.gazedAt = gazed;
			Debug.Log ("enter");
			if (inactiveMaterial != null && gazedAtMaterial != null) {
				renderer.material = this.gazedAt ? gazedAtMaterial : inactiveMaterial;
				return;
			}
		
		}

		private IEnumerator Selection(){
			while (true) {
				if(this.gazedAt){
					this.time++;
					if (this.time > 20) {
						renderer.material = inactiveMaterial;
						changeScene ();
					}
				}
				yield return new WaitForSeconds(.1f);
			}
		}

		private void changeScene(){
			SceneManager.LoadScene (scene, LoadSceneMode.Single);
		}

		public void Recenter() {
			#if !UNITY_EDITOR
			GvrCardboardHelpers.Recenter();
			#else
			if (GvrEditorEmulator.Instance != null) {
				GvrEditorEmulator.Instance.Recenter();
			}
			#endif  // !UNITY_EDITOR
		}

		public void TeleportRandomly() {
			// Pick a random sibling, move them somewhere random, activate them,
			// deactivate ourself.
			int sibIdx = transform.GetSiblingIndex();
			int numSibs = transform.parent.childCount;
			sibIdx = (sibIdx + Random.Range(1, numSibs)) % numSibs;
			GameObject randomSib = transform.parent.GetChild(sibIdx).gameObject;

			// Move to random new location ±100º horzontal.
			Vector3 direction = Quaternion.Euler(
				0,
				Random.Range(-90, 90),
				0) * Vector3.forward;
			// New location between 1.5m and 3.5m.
			float distance = 2 * Random.value + 1.5f;
			Vector3 newPos = direction * distance;
			// Limit vertical position to be fully in the room.
			newPos.y = Mathf.Clamp(newPos.y, -1.2f, 4f);
			randomSib.transform.localPosition = newPos;

			randomSib.SetActive(true);
			gameObject.SetActive(false);
			SetGazedAt(false);
		}
	}
}
