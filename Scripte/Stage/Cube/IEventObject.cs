public interface IEventObject
{
    public void PlayEvent(Agent owner);
    public bool EventEnd();
    public void Stop();
}
