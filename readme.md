Using LookAt

• LookAt rotates the object so that its forward vector points at the target. The forward vector is indicated by the blue vector in the Unity scene view. If objects are facing the way you expect, check that their forward vector is aligned with the facing of the model.

Rigidbody and Child Colliders

• If you add a rigidbody to a parent object that has more than one child objects which have collider components, all those colliders will be combined in the rigidbody and the parent will be provided to any OnCollisionEnter methods as the collider argument.