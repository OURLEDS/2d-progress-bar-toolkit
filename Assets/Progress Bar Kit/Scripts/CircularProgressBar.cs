using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircularProgressBar : MonoBehaviour {
	public Color mainColor = Color.white;
	public Color fillColor = Color.green;
	public int numberOfSegments = 5;
	public float startAngle = 40;
	public float endAngle = 320;
	public float sizeOfNotch = 5;
	public float fillAmount = 0.0f;

	private Image image;
	private List<Image> progressToFill = new List<Image> ();
	private float sizeOfSegment;

	public void Awake() {
		// get images
		image = GetComponentInChildren<Image>();
		image.color = mainColor;
		image.gameObject.SetActive(false);

		float startNormalAngle = NormalizeAngle(startAngle);
		float endNormalAngle = NormalizeAngle(360 - endAngle);
		float notchesNormalAngle = (numberOfSegments - 1) * NormalizeAngle(sizeOfNotch);
		float allSegmentsAngleArea = 1 - startNormalAngle - endNormalAngle - notchesNormalAngle;
		
		// count size of segments
		sizeOfSegment = allSegmentsAngleArea / numberOfSegments;
		for (int i = 0; i < numberOfSegments; i++) {
			GameObject currentSegment = Instantiate(image.gameObject, transform.position, Quaternion.identity, transform);
			currentSegment.SetActive(true);

			Image segmentImage = currentSegment.GetComponent<Image>();
			segmentImage.fillAmount = sizeOfSegment;

			Image segmentFillImage = segmentImage.transform.GetChild (0).GetComponent<Image> ();
			segmentFillImage.color = fillColor;
			progressToFill.Add (segmentFillImage);

			float zRot = startAngle + i * ConvertCircleFragmentToAngle(sizeOfSegment) + i * sizeOfNotch;
			segmentImage.transform.rotation = Quaternion.Euler(0,0, -zRot);
		}
	}

	public void Update() {
		for (int i = 0; i < numberOfSegments; i++) {
			progressToFill [i].fillAmount = (fillAmount * ((endAngle-startAngle)/360)) - sizeOfSegment * i;
		}
	}

	private float NormalizeAngle(float angle) {
		return Mathf.Clamp01(angle / 360f);
	}

	private float ConvertCircleFragmentToAngle(float fragment) {
		return 360 * fragment;
	}
}
