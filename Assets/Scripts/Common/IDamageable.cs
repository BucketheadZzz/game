namespace Assets.Scripts.Common
{
    public interface IDamageable
    {
        bool IsDead { get; }

        void Hit(int hpDamage);
    }
}
