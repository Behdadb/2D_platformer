using UnityEngine;
using UnitySampleAssets.CrossPlatformInput;

namespace UnitySampleAssets._2D
{

    [RequireComponent(typeof (PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
        private PlatformerCharacter2D character;
        private bool jump;
		private bool kick;
		private Vector3 kickDirection;

        private void Awake()
        {
            character = GetComponent<PlatformerCharacter2D>();
        }

        private void Update()
        {
            if (!jump) {
				// Read the jump input in Update so button presses aren't missed.
				jump = CrossPlatformInputManager.GetButtonDown ("Jump");
			}

			if (!kick) {
				kick = CrossPlatformInputManager.GetButtonDown ("Fire1");
				kickDirection = Camera.main.ScreenToWorldPoint (Input.mousePosition) - transform.position;
				kickDirection.z = 0f;
				kickDirection.Normalize ();
			}
        }

        private void FixedUpdate()
        {
            // Read the inputs.
            bool crouch = Input.GetKey(KeyCode.LeftControl);
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            // Pass all parameters to the character control script.
			character.Move(h, crouch, jump, kick, kickDirection);
			jump = false;
			kick = false;
        }
    }
}