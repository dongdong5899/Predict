using UnityEngine;

public class AgentInteractionAction : AgentAction
{
    public override void Stop()
    {
        base.Stop();

        currentInteractedObject?.Stop();
    }

    private IEventObject currentInteractedObject;

    public override void Action()
    {
        base.Action();

        float duration = 0.2f;
        Vector3 targetPos = _owner.transform.position + _owner.transform.forward;

        Collider[] forwardObject = Physics.OverlapSphere(targetPos + Vector3.up * 0.5f, 0.1f, 1 << LayerMask.NameToLayer("Obstacle"));

        if (forwardObject.Length == 0)
        {
            GameManager.Instance.DelaySecCallback(duration, () => actionEnd = true);
            return;
        }

        if (forwardObject[0].transform.TryGetComponent(out IEventObject cubeObject))
        {
            currentInteractedObject = cubeObject;
            (cubeObject as CubeObject).OnAgent();
            cubeObject.PlayEvent(_owner);
            GameManager.Instance.WaitCallback(cubeObject.EventEnd, () => actionEnd = true);
            return;
        }

        GameManager.Instance.DelaySecCallback(duration, () => actionEnd = true);
    }
}
