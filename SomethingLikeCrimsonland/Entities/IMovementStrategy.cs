namespace Entities
{
    public interface IMovementStrategy
    {
        void Move(EnemyObject enemy, GameObject player);
    }
}