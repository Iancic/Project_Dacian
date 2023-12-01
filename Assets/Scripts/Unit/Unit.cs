using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(fileName ="New Unit")]
public class Unit : ScriptableObject
{
    public string unitName;
    public float movementSpeed;

    public Sprite baseSprite;

    public AnimatorController animatorController;

    public string Class;
    //"Warrior"

    public int recruitValue;
    public int hitpoints;
    public float attackCooldown;

    public int damagePower;
}
