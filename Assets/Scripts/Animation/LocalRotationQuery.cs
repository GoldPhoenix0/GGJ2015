using UnityEngine;
using System.Collections;

public class LocalRotationQuery : MonoBehaviour {

	public Transform observed;

	public Quaternion GetLocalRotation() {
		return observed.localRotation;
	}
}
