using System;
using System.Threading;

class Person
{
    private float _damage;
    private float _stamina;
    private float _health;
    private string _name;
    private float _armor;
    private int _WeaponId;

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

    public float Health
    {
        get
        {
            return _health;
        }
    }
    public string Name
    {
        get
        {
            return _name;
        }
    }
    public float Damage
    {
        get { return _damage; }
    }


    public void TakeDamage(float EnemyDamage)
    {
        _health = _health - EnemyDamage * (_armor / 100);
    }
    public void BlockDamage(float EnemyDamage)
    {
        _health = _health - (EnemyDamage * 0.5f -
        EnemyDamage * (_armor / 100));
    }
}

class Program
{
    public static void Main(string[] args)
    {
        Random random = new Random();

        int rnd = random.Next(0, 3);

        Console.Write("Введите имя вашего персонажа: ");
        string name = Console.ReadLine();

        Person[] persons =
        {
            new Person(20, 50, 100, 100, 1, name),
            new Person(33, 50, 70, 100, 2, name),
            new Person(20, 50, 100, 100, 3, name),
        };

        Console.WriteLine("Выберите класс вашего персонажа: \n 1. Воин \n 2. Лучник \n 3. Вор\n");
        int Class_Selection = Convert.ToInt32(Console.ReadLine()) - 1;

        Person YourPerson = persons[Class_Selection];
        YourPerson.ShowStats();

        Person[] enemys =
        {
            new Person(5, 50, 50, 100, 1, "орк"),
            new Person(7, 60, 70, 100, 1, "троль"),
            new Person(3, 30, 30, 100, 1, "слайм"),
        };

        Console.WriteLine($"\nВаше здоровье: {YourPerson.Health}\nЗдоровье противника {enemys[rnd].Name}: {enemys[rnd].Health}\n");

        while (YourPerson.Health > 0 && enemys[rnd].Health > 0)
        {
            enemys[rnd].TakeDamage(YourPerson.Damage);
            YourPerson.TakeDamage(enemys[rnd].Damage);
            Console.WriteLine($"Ваше здоровье: {YourPerson.Health}\nЗдоровье противника {enemys[rnd].Name}: {enemys[rnd].Health}\n");
            if (YourPerson.Health <= 0)
            {
                Console.WriteLine("Вы умерли");
            }
            else if (enemys[rnd].Health <= 0)
            {
                Console.WriteLine("Вы победили");
            }
        }

        //   warrior.TakeDamage(10f);
        // warrior.ShowCharacteristics();

        Console.ReadKey();

    }
}
