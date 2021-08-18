using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour {
	public Color mainColor = Color.white;
	public Color fillColor = Color.green;
	public int numberOfSegments = 5;
	public float sizeOfNotch = 5;
	public float fillAmount = 0.0f;

	private RectTransform rectTransform;
	private Image image;
	private List<Image> progressToFill = new List<Image> ();
	private float sizeOfSegment;

	public void Awake() {
		// get rect transform
		rectTransform = GetComponent<RectTransform> ();
		
		// get image
		image = GetComponentInChildren<Image>();
		image.color = mainColor;
		image.gameObject.SetActive(false);

		// count size of segments
		sizeOfSegment = rectTransform.sizeDelta.x / numberOfSegments;		
		for (int i = 0; i < numberOfSegments; i++) {
			GameObject currentSegment = Instantiate(image.gameObject, transform.position, Quaternion.identity, transform);
			currentSegment.SetActive(true);

			Image segmentImage = currentSegment.GetComponent<Image>();
			segmentImage.fillAmount = sizeOfSegment;
			segmentImage.transform.GetComponent<RectTransform> ().sizeDelta = new Vector2(sizeOfSegment, segmentImage.GetComponent<RectTransform> ().sizeDelta.y);
			segmentImage.transform.position += (Vector3.right * i * sizeOfSegment) - (Vector3.right * (rectTransform.sizeDelta.x)/ 2) + (Vector3.right * i * sizeOfNotch);

			Image segmentFillImage = segmentImage.transform.GetChild (0).GetComponent<Image> ();
			segmentFillImage.color = fillColor;
			progressToFill.Add (segmentFillImage);
			segmentFillImage.transform.GetComponent<RectTransform> ().sizeDelta = new Vector2(sizeOfSegment, segmentImage.GetComponent<RectTransform> ().sizeDelta.y);
		}
	}

	public void Update() {
		for (int i = 0; i < numberOfSegments; i++) {
			progressToFill [i].fillAmount = fillAmount - i;
		}
	}

	private float ConvertFragmentToWidth(float fragment) {
		return rectTransform.sizeDelta.x * fragment;
	}
}
