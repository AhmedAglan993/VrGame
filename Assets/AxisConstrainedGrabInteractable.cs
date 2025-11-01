using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
public class AxisConstrainedGrabInteractable : XRGrabInteractable
{
    public enum Axis { X, Y, Z }
    [Header("Constraint Settings")]
    public bool constrainPosition = true;
    public Axis movementAxis = Axis.Y;
    private Vector3 initialGrabOffset;

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        // Record offset between object and interactor at grab time
        initialGrabOffset = transform.position - args.interactorObject.GetAttachTransform(this).position;

    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);

        if (isSelected && constrainPosition)
        {
            Transform interactor = firstInteractorSelecting.GetAttachTransform(this);
            Vector3 targetPos = interactor.position + initialGrabOffset;

            Vector3 current = transform.position;

            switch (movementAxis)
            {
                case Axis.X:
                    targetPos.y = current.y;
                    targetPos.z = current.z;
                    break;
                case Axis.Y:
                    targetPos.x = current.x;
                    targetPos.z = current.z;
                    break;
                case Axis.Z:
                    targetPos.x = current.x;
                    targetPos.y = current.y;
                    break;
            }

            transform.position = targetPos;
        }
    }
}
