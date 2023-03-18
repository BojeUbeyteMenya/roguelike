using System;
using System.Reflection.Metadata.Ecma335;
using System.Threading;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;

abstract class Factory
{
    public abstract Person GetMyPerson();
}

class GetKnight : Factory
{
    public override Person GetMyPerson()
    {
        Knight knight = new(100, 100, 100, 100, 100, "");
        return knight;
    }


}

class GerArcher : Factory
{
    public override Person GetMyPerson()
    {
        Archer archer = new(111, 111, 111, 111, 111, "");
        return archer;
    }


}

class GetThief : Factory
{
    public override Person GetMyPerson()
    {
        Thief thief = new(122, 122, 122, 122, 122, "");
        return thief;
    }


}
class Person
{
    private float _damage;
    private float _stamina;
    private float _health;
    private string _name;
    private float _armor;
    private int _WeaponId;
    private int _Money; // Дописать взаимодействие с деньгами

    public Person(float damage, float stamina, float health, float armor, int weapon, string name)
    {
        _damage = damage;
        _stamina = stamina;
        _health = health;
        _armor = armor;
        _name = name;
        _WeaponId = weapon;
    }

    public void ShowStats()
    {
        Console.WriteLine($"Вашего персонажа зовут: {_name}\nВаши характеристики:\nУрон = {_damage}\nБроня = " +
            $"{_armor}\nВыносливость = {_stamina}\nЗдоровье = {_health}");
    }

    public float Health => _health;
    public string Name => _name;
    public float Damage => _damage;



    public void TakeDamage(float EnemyDamage)
    {
        _health = _health - EnemyDamage * (_armor / 100);
    }

    public void PutOnWeapon(int ItemStats)
    {
        _damage = _damage + ItemStats;
        Console.WriteLine($"Ваш урон: {_damage}");
    }
    public void BlockDamage(float EnemyDamage)
    {
        _health = _health - (EnemyDamage * 0.5f -
        EnemyDamage * (_armor / 100));

    }
}

class Knight : Person
{
    public Knight(float damage, float stamina, float health, float armor, int weapon, string name) : base(damage, stamina, health, armor, weapon, name)
    {
    }
}

class Archer : Person
{
    public Archer(float damage, float stamina, float health, float armor, int weapon, string name) : base(damage, stamina, health, armor, weapon, name)
    {
    }
}

class Thief : Person
{
    public Thief(float damage, float stamina, float health, float armor, int weapon, string name) : base(damage, stamina, health, armor, weapon, name)
    {
    }
}

class Item //Класс предмета
{

    public int ItemStats; //статы предмета, если оружие, то + урон, если броня,то + броня, если зелье, то ну блять понятно 
    public string ItemName; //название предмета
    public int ItemCost; // цена на предмет
    public int GiveStats => ItemStats; // должно возвращать значение статистики, чтобы +/- к начальному
}
class Outfit : Item
{
    public int Id;  // Id сделан для того, чтобы в зависисмости от выбранного класс можно было бы надеть на перса или нет (принимает WeaponId)
    public int Type; // тип снаряжения 0-2 оружие (Меч, Лук, Нож), 3-5 броня (Тяжелая, Средняя, Легкая)
    public Outfit(int OutfitId, int OutfitType, int OutfitStats, string OutfitName, int OutfitCost)
    {
        Id = OutfitId;
        Type = OutfitType;
        ItemStats = OutfitStats;
        ItemName = OutfitName;
        ItemCost = OutfitCost;
    }


}

class Program
{
    private static Factory GetMyPerson(int Class_Selection) =>
        Class_Selection switch
        {
            1 => new GetKnight(),
            2 => new GerArcher(),
            3 => new GetThief(),
            _ => null
        };

    public static void Main(string[] args)
    {
        Random random = new Random();

        int rnd = random.Next(0, 3);

        Console.Write("Введите имя вашего персонажа: ");
        string name = Console.ReadLine();

        //  Person[] persons =
        //{
        //  new Person(20, 50, 100, 100, 1, name),
        //     new Person(33, 50, 70, 100, 2, name),
        //   new Person(20, 50, 100, 100, 3, name),
        //    };

        Console.WriteLine("Выберите класс вашего персонажа: \n 1. Рыцарь \n 2. Лучник \n 3. Вор\n");
        int Class_Selection = Convert.ToInt32(Console.ReadLine());

        Factory factory = GetMyPerson(Class_Selection);
        Person character = factory.GetMyPerson();
        character.ShowStats();

        // Person YourPerson = persons[Class_Selection];
        //YourPerson.ShowStats();

        // Outfit weapon = new Outfit(0, 2, 10, "Меч", 10);
        // Console.WriteLine($"Хотите надеть {weapon.ItemName}?");
        // string answer;
        // answer = Console.ReadLine();
        // if (answer == "да")
        // {
        //    Outfit ActiveWeapon = weapon;
        //   YourPerson.PutOnWeapon(ActiveWeapon.ItemStats);
        // }


        Person[] enemys =
        {
            new Person(5, 50, 50, 100, 1, "орк"),
            new Person(7, 60, 70, 100, 1, "троль"),
            new Person(3, 30, 30, 100, 1, "слайм"),
        };

        //Console.WriteLine($"\nВаше здоровье: {YourPerson.Health}\nЗдоровье противника {enemys[rnd].Name}: {enemys[rnd].Health}\n");

        //while (YourPerson.Health > 0 && enemys[rnd].Health > 0)
        //{
        //  enemys[rnd].TakeDamage(YourPerson.Damage);
        //YourPerson.TakeDamage(enemys[rnd].Damage);
        //    Console.WriteLine($"Ваше здоровье: {YourPerson.Health}\nЗдоровье противника {enemys[rnd].Name}: {enemys[rnd].Health}\n");
        //  if (YourPerson.Health <= 0)
        //{
        //        Console.WriteLine("Вы умерли");
        //  }
        //else if (enemys[rnd].Health <= 0)
        //    {
        //      Console.WriteLine("Вы победили");
        // }
        // }

        //   warrior.TakeDamage(10f);
        // warrior.ShowCharacteristics();

        Console.ReadKey();

    }
}
