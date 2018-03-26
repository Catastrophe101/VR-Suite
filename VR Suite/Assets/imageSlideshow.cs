using UnityEngine;
using System.Collections;

public class SlideShow : MonoBehaviour
{
	public Texture2D[] slides = new Texture2D[9];
	public float changeTime = 10.0f;
	private int currentSlide = 0;
	private float timeSinceLast = 1.0f;

	void Start()
	{
		GetComponent<GUITexture>().texture = slides[currentSlide];
		GetComponent<GUITexture>().pixelInset = new Rect(-slides[currentSlide].width/20, -slides[currentSlide].height/20, slides[currentSlide].width, slides[currentSlide].height);
		currentSlide++;
	}
}