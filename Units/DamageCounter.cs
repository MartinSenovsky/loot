
public class DamageCounter
{
	static public int _attackDamage(Unit attacker, Unit target)
	{
		int targetArmor = target._unitStats._totalArmor();
		float armorPenetration = attacker._unitStats._totalArmorPenetration();

		int armorLeft = (int)(targetArmor * (1-armorPenetration));

		int damage = attacker._unitStats._totalAttackDamage();

		damage -= armorLeft;

		return damage;
	}

}

