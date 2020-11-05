
namespace RPG.Control
{
    public interface IRayCastable
    {
        CursorType GetCursorType();
        bool HanleRaycast(PlayerController callingController);
    }
}
