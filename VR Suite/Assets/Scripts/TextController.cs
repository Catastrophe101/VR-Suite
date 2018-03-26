using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.IO;
using UnityEngine.SceneManagement;
[AddComponentMenu("GoogleVR/UI/GvrReticle")]
[RequireComponent(typeof(Renderer))]


public class TextController : MonoBehaviour {
	private Text txt;//the string to be displayed in GUI Text element attached
	protected int lowBound=0;
	private Canvas CanvasObject;
	protected int upBound=0;

	 protected string completeText="";

 void Start(){	

		showInConsole ();


	}

	void Update(){
		if (Input.GetKeyDown ("w")) {
			SceneManager.UnloadScene ("text");
		}
		showInGuiText ();
		
	}
	void showInConsole(){

		StringReader reader = null;

		TextAsset Ebook = (TextAsset)Resources.Load ("Ebook", typeof(TextAsset));
		//Ebook.txt is a string containing  the whole file.TO read it line by line
		reader = new StringReader(Ebook.text);
		if (reader == null) {
			Debug.Log ("Ebook.txt not found or readable");

		} else {
			//Read Each line from file
			string txt;

			while((txt=reader.ReadLine())!=null)
			{
				completeText = completeText + txt + "\n";
			}
		}
		}

	void showInGuiText(){
		txt = GetComponent<Text> ();
	int length = completeText.Length;

			if (Input.GetKeyDown("d")) {
			lowBound = upBound;
			
			upBound = lowBound + 100;
			if (upBound>length)
				upBound=length;
			
			txt.text = completeText.Substring (lowBound, upBound);
	
			}
			if (Input.GetKeyDown("a")) {
			upBound = lowBound;
			
			lowBound = upBound - 100;
			if (lowBound<0)
				lowBound=0;
			
			txt.text = completeText.Substring (lowBound, upBound);
            
			}


	}
	}





