// Copyright 2014 Google Inc. All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace GoogleVR.HelloVR {
  using UnityEngine;

  [RequireComponent(typeof(Collider))]
  public class LetterController : MonoBehaviour {
	public char letter = 'A';
    private Vector3 startingPosition;
    private Renderer renderer;

    public Material inactiveMaterial;
    public Material gazedAtMaterial;

    void Start() {
      	startingPosition = transform.localPosition;
    	renderer = GetComponent<Renderer>();
  		SetGazedAt(false);
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
		transform.localPosition = newPos;
    }

    public void SetGazedAt(bool gazedAt) {
    }

    public void Reset() {
      int sibIdx = transform.GetSiblingIndex();
      int numSibs = transform.parent.childCount;
      for (int i=0; i<numSibs; i++) {
        GameObject sib = transform.parent.GetChild(i).gameObject;
        sib.transform.localPosition = startingPosition;
        sib.SetActive(i == sibIdx);
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
		gameObject.SetActive(false);
		print ("Whushhh: " + letter);
      // Pick a random sibling, move them somewhere random, activate them,
      // deactivate ourself.
//      int sibIdx = transform.GetSiblingIndex();
//      int numSibs = transform.parent.childCount;
//      sibIdx = (sibIdx + Random.Range(1, numSibs)) % numSibs;
//      GameObject randomSib = transform.parent.GetChild(sibIdx).gameObject;
//
//      // Move to random new location ±100º horzontal.
//      Vector3 direction = Quaternion.Euler(
//          0,
//          Random.Range(-90, 90),
//          0) * Vector3.forward;
//      // New location between 1.5m and 3.5m.
//      float distance = 2 * Random.value + 1.5f;
//      Vector3 newPos = direction * distance;
//      // Limit vertical position to be fully in the room.
//      newPos.y = Mathf.Clamp(newPos.y, -1.2f, 4f);
//      randomSib.transform.localPosition = newPos;
//      SetGazedAt(false);
    }
  }
}
