using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelect : MonoBehaviour
{
    public int[] selectIndex;
    public static int p1SelectedIndex;
    public static int p2SelectedIndex;

    // Start is called before the first frame update
    void Start()
    {
        p1SelectedIndex = 0;
        p2SelectedIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
    
    }
    //takes in char of "L" or "R" to check if it is the left or right button
    //takes in string of "p1" or "p2" to determine which player
    public void P1Left(){
        if(p1SelectedIndex == 0)
        {
            p1SelectedIndex = 4;
        }
        else{
            p1SelectedIndex -= 1;
        }
        Debug.Log(p1SelectedIndex);
    }

    public void P1Right(){
        if(p1SelectedIndex == 4)
        {
            p1SelectedIndex = 0;
        }
        else{
            p1SelectedIndex += 1;
        }
        Debug.Log(p1SelectedIndex);
    }

    public void P2Left(){
        if(p2SelectedIndex == 0)
        {
            p2SelectedIndex = 4;
        }
        else{
            p2SelectedIndex -= 1;
        }
    }

    public void P2Right(){
        if(p2SelectedIndex == 4)
        {
            p2SelectedIndex = 0;
        }
        else{
            p2SelectedIndex += 1;
        }
    }
}   
    
