namespace Assets.Scripts.Common
{
    public interface IDamageable
    {
        bool IsDead { get; set; }

        void Hit(int hpDamage);
    }
}
