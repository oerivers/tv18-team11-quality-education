﻿namespace GoogleVR.HelloVR {
	using UnityEngine;

	[RequireComponent(typeof(Collider))]
	public class ObjController : MonoBehaviour {
		private Vector3 startingPosition;
		private Renderer renderer;

		public Material inactiveMaterial;
		public Material gazedAtMaterial;

		void Start() {
			startingPosition = transform.localPosition;
			renderer = GetComponent<Renderer>();
			SetGazedAt(false);
		}

		public void SetGazedAt(bool gazedAt) {
			if (inactiveMaterial != null && gazedAtMaterial != null) {
				renderer.material = gazedAt ? gazedAtMaterial : inactiveMaterial;
				return;
			}
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
