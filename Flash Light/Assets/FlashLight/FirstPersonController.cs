using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GabieStudio
{
    public class FirstPersonController : MonoBehaviour
    {
        [Tooltip("The Speed The Camera Moves")]
        public float moveSpeed;
        [SerializeField][Tooltip("Mouse Movement Speed")] float speedH;
        float yaw = 0;
        float yaw2 = 0;
        private static readonly string _InputX = "Mouse X";
        private static readonly string _InputY = "Mouse Y";
        private static readonly string _MouseLeft = "Fire1"; //Set This To name in Input Manager (Left Mouse Click)
        private static readonly string _MouseRight = "Fire2"; //Set this to name in Input Manager (Right Mouse Click)

        
        private void Update()
        {
            MoveFunction();
        }
        void MoveFunction()
        {
            float lr = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
            float fb = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
            transform.Translate(lr, 0, fb);
            yaw += speedH * Input.GetAxis(_InputX);
            yaw2 += speedH * Input.GetAxis(_InputY);
            transform.eulerAngles = new Vector3(-yaw2, yaw, 0);
        }
    }

   
}

