using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelected : MonoBehaviour
{
    public Animator anim;

    public void Selected()
    {
        anim.SetBool("Selected", true);
    }

    public void DeSelected()
    {
        anim.SetBool("Selected", false);
    }
}
