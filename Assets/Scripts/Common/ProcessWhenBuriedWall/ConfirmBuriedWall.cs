using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmBuriedWall : MonoBehaviour
{
    public bool isTouchingWall { private set; get; }    // •Ç‚ÉG‚ê‚Ä‚¢‚é‚©‚Ç‚¤‚©
    public Vector3 hitPosition { private set; get; }    // Õ“Ë‚µ‚½êŠ

    // Start is called before the first frame update
    void Start()
    {
        isTouchingWall = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider other)
    {
        //•Ç‚ÉG‚ê‚Ä‚¢‚é‚±‚Æ‚ğ‹L‰¯‚·‚é
        isTouchingWall = true;

        //Õ“ËˆÊ’u‚Ìæ“¾
        //hitPosition = other.
    }

    void OnTriggerExit(Collider other)
    {
        //•Ç‚©‚ç—£‚ê‚½‚±‚Æ‚ğ‹L‰¯‚·‚é
        isTouchingWall = false;
    }


}
