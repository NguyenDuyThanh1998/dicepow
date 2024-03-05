using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyArsen", menuName = "ScriptableObjects/Skill/Aspect/Arsenal")]
public class Arsenal : SkillData
{
    [SerializeField] Bullet[] bullets;
    [SerializeField] Pattern[] patterns;
    Dictionary<Bullet, Pattern> WeaponDictionary;

    public int Count => WeaponDictionary.Count;

    public void Initialize()
    {
        PopulateDictionary(bullets, patterns);
    }

    public Dictionary<Bullet, Pattern> PopulateDictionary(Bullet[] _bullets, Pattern[] _patterns)
    {
        WeaponDictionary = new Dictionary<Bullet, Pattern>();

        for (int i = 0; i < _bullets.Length; i++)
        {
            //Debug.Log("PatternType: " + _patterns[i].GetType().Name);
            var patternInstance = CreateInstance(_patterns[i].GetType().Name) as Pattern; //ScriptableObject.CreateInstance()
            patternInstance.Initialize();
            WeaponDictionary.Add(_bullets[i], patternInstance);
        }

        return WeaponDictionary;
    }

    public virtual KeyValuePair<Bullet, Pattern> Equip()
    {
        return WeaponDictionary.ElementAt(0);
    }

    public virtual void Equip(ref Bullet _bullet, ref Pattern _pattern)
    {
        if (WeaponDictionary.Count < 1)
        {
            Debug.LogError($"{this.DataName} missing Bullet-Pattern KeyValuePair");
        }

        var currentWeapon = WeaponDictionary.ElementAt(0);
        _bullet = currentWeapon.Key;
        _pattern = currentWeapon.Value;
    }

    public virtual KeyValuePair<Bullet, Pattern> Swap(int index)
    {
        if (index >= WeaponDictionary.Count)
        {
            index = 0;
        }

        return WeaponDictionary.ElementAt(index);
    }

    public virtual KeyValuePair<Bullet, Pattern> Swap(Bullet _currentBullet)
    {
        int index = 0;
        for (int i = 0; i < WeaponDictionary.Count - 1; i++)
        {
            if (WeaponDictionary.ElementAt(i).Key == _currentBullet)
            {
                index = i + 1;
            }
        }
        return WeaponDictionary.ElementAt(index);
    }

    public virtual void Swap(ref Bullet _currentBullet, ref Pattern _currentPattern)
    {
        int index = 0;
        for (int i = 0; i < WeaponDictionary.Count - 1; i++)
        {
            if (WeaponDictionary.ElementAt(i).Key == _currentBullet)
            {
                index = i + 1;
            }
        }

        var currentWeapon = WeaponDictionary.ElementAt(index);
        _currentBullet = currentWeapon.Key;
        _currentPattern = currentWeapon.Value;
    }
}
