using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TestTextReveal : MonoBehaviour
{
   public TMP_Text text;

   void Start() 
    {
        StartCoroutine(RevealText());
	}

    IEnumerator RevealText() 
    { 
        var originalString = text.text;
		text.text = "";

		var numCharsRevealed = 0;
		while (numCharsRevealed < originalString.Length)
		{
			while (originalString[numCharsRevealed] == ' ')
				++numCharsRevealed;

			++numCharsRevealed;

			text.text = originalString.Substring(0, numCharsRevealed);

			yield return new WaitForSeconds(0.16f);
		}
	}
}
