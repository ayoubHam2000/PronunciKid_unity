using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controller : MonoBehaviour
{
    //about resaurces and type of the display letters
    [SerializeField] int            letterPositionType  = 3;
    [SerializeField] GameObject[]   positionTypes       = null;
    [SerializeField] GameObject[]   lettersGameObject   = null;

    [HideInInspector] public LetterInfo  rightLetter = null;

    [SerializeField]  private float timeBetweenClick = 1;
    [HideInInspector] public float waite = 0;

    private sceneManager sceneManager;

    private void Start()
    {
        sceneManager = FindObjectOfType<sceneManager>();
        makeLevel();
    }

    private void Update()
    {
        if (waite > 0)
        {
            waite -= Time.deltaTime;
        }
    }

    public void rightAnswer()
    {
        waite = 2;
        StopAllCoroutines();
        Invoke("nextLevel", 2);
    }

    public void falseAnswer()
    {
        waite = timeBetweenClick;
    }

    public void win()
    {
        sceneManager.congratulation();
    }

    //-----------------------------------------------
    //private

    private void nextLevel()
    {
        letter[] instantiatedLetters = FindObjectsOfType<letter>();
        int i = 0;
        while(i < instantiatedLetters.Length)
        {
            Destroy(instantiatedLetters[i].gameObject);
            i++;
        }
        makeLevel();
    }

    //--------------------------------------------------------------------
    //methods
    //--------------------------------------------------------------------

    private void makeLevel()
    {
        int i = 0;
        List<int> randomValues;
        List<Transform> positions;

        int positionsLength = positionTypes[letterPositionType].transform.childCount;
        randomValues = pickDistinctRandomValue(positionsLength, lettersGameObject.Length);
        positions = getPositions();
        for(i = 0; i < positionsLength; i++)
        {
            Instantiate(
                lettersGameObject[randomValues[i]], 
                positions[i].position, 
                Quaternion.identity);
        }

        //pick random letter as right letter
        int random = randomValues[Random.Range(0, randomValues.Count)];
        rightLetter = lettersGameObject[random].transform.GetChild(0).GetComponent<LetterInfo>();
        Debug.Log(rightLetter.letterName);
        waite = 0;
        StartCoroutine(playSoundContunialy(rightLetter.audioClip));
    }

    private IEnumerator playSoundContunialy(AudioClip theLetterSound)
    {
        while (true)
        {
            playSound(theLetterSound);
            yield return new WaitForSeconds(1.5f);
        }
    }

    private List<int> pickDistinctRandomValue(int nb, int end)
    {
        //nb is the number of distinct random values
        //range is 0...end | end excluded
        List<int> randomValues = new List<int>();
        List<int> list         = new List<int>();
        int i               = 0;
        int random          = 0;

        if(end < nb)
        {
            Debug.Log("Error nb smaller then end");
            return randomValues;
        }

        for (i = 0; i < end; i++)
            list.Add(i);
        while(randomValues.Count < nb)
        {
            random = Random.Range(0, list.Count);
            randomValues.Add(list[random]);
            list.RemoveAt(random);
        }

        return randomValues;
    }

    private List<Transform> getPositions()
    {
        List<Transform> positions = new List<Transform>();
        int i;

        GameObject positionType = positionTypes[letterPositionType];
        int size = positionType.transform.childCount;
        for (i = 0; i < size; i++)
        {
            positions.Add(positionType.transform.GetChild(i).transform);
        }

        return positions;
    }

    private void playSound(AudioClip theSound)
    {
        if (theSound != null)
            AudioSource.PlayClipAtPoint(theSound, Camera.main.transform.position);
    }

}
