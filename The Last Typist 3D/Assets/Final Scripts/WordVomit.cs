using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class WordVomit : MonoBehaviour{
    List<string> oneLetterWords;
    List<string> twoLetterWords;
    List<string> threeLetterWords;
    List<string> fourLetterWords;
    List<string> fiveLetterWords;
    List<string> sixLetterWords;
    List<string> sevenLetterWords;
    List<string> eightLetterWords;
    List<string> nineLetterWords;
    List<string> tenLetterWords;
    string readFromFilePath;
    public bool isInitialised = false;

    // Use this for initialization

    public void initialize()
    {
        if (!isInitialised)
        {
            readFromFilePath = Application.streamingAssetsPath + "/TextFiles/lengthOneEnglish.txt";
            oneLetterWords = File.ReadAllLines(readFromFilePath).ToList();
            readFromFilePath = Application.streamingAssetsPath + "/TextFiles/lengthTwoEnglish.txt";
            twoLetterWords = File.ReadAllLines(readFromFilePath).ToList();
            readFromFilePath = Application.streamingAssetsPath + "/TextFiles/lengthThreeEnglish.txt";
            threeLetterWords = File.ReadAllLines(readFromFilePath).ToList();
            readFromFilePath = Application.streamingAssetsPath + "/TextFiles/lengthFourEnglish.txt";
            fourLetterWords = File.ReadAllLines(readFromFilePath).ToList();
            readFromFilePath = Application.streamingAssetsPath + "/TextFiles/lengthFiveEnglish.txt";
            fiveLetterWords = File.ReadAllLines(readFromFilePath).ToList();
            readFromFilePath = Application.streamingAssetsPath + "/TextFiles/lengthSixEnglish.txt";
            sixLetterWords = File.ReadAllLines(readFromFilePath).ToList();
            readFromFilePath = Application.streamingAssetsPath + "/TextFiles/lengthSevenEnglish.txt";
            sevenLetterWords = File.ReadAllLines(readFromFilePath).ToList();
            readFromFilePath = Application.streamingAssetsPath + "/TextFiles/lengthEightEnglish.txt";
            eightLetterWords = File.ReadAllLines(readFromFilePath).ToList();
            readFromFilePath = Application.streamingAssetsPath + "/TextFiles/lengthNineEnglish.txt";
            nineLetterWords = File.ReadAllLines(readFromFilePath).ToList();
            readFromFilePath = Application.streamingAssetsPath + "/TextFiles/lengthTenEnglish.txt";
            tenLetterWords = File.ReadAllLines(readFromFilePath).ToList();
        }
       
    }
	

    public string getRandomWord(int length)
    {
    switch (length)
        {
            case 0:
                return "";
            case 1:
                return oneLetterWords[UnityEngine.Random.Range(1, oneLetterWords.Count)];
            case 2:
                return twoLetterWords[UnityEngine.Random.Range(1, twoLetterWords.Count)];
            case 3:
                return threeLetterWords[UnityEngine.Random.Range(1, threeLetterWords.Count)];
            case 4:
                return fourLetterWords[UnityEngine.Random.Range(1, fourLetterWords.Count)];
            case 5:
                return fiveLetterWords[UnityEngine.Random.Range(1, fiveLetterWords.Count)];
            case 6:
                return sixLetterWords[UnityEngine.Random.Range(1, sixLetterWords.Count)];
            case 7:
                return sevenLetterWords[UnityEngine.Random.Range(1, sevenLetterWords.Count)];
            case 8:
                return eightLetterWords[UnityEngine.Random.Range(1, eightLetterWords.Count)];
            case 9:
                return nineLetterWords[UnityEngine.Random.Range(1, nineLetterWords.Count)];
            case 10:
                return tenLetterWords[UnityEngine.Random.Range(1, tenLetterWords.Count)];
            default:  
                return "the";
        }
    }
}
