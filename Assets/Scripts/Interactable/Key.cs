public class Key : Interactable
{
    public PuzlePanel Puzle;
    public override void Interact()
    {
        Puzle.ShowPanel();
    }
}