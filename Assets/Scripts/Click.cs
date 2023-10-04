using UnityEngine;
using UnityEngine.UI;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Random = UnityEngine.Random;

public class Click : MonoBehaviour
{
    [SerializeField] private Sprite[] Sprites;
    [SerializeField] private Image img;
    private int weaponImgCounter;
    private int costUpMoney;
    private int costUpWeapon;
    private int infection;
    private int days;
    private int power;
    private int miningSpeed;
    private int money;
    private long counter;
    private long zombies;
    private long people;
    public Text Tdays;
    public Text Tmoney;
    public Text T_killed_z_counter;
    public Text T_alive_z_counter;
    public Text T_alive_p_counter;
    public Text T_costUpMoney;
    public Text T_costUpWeapon;

    void Start()
    {
        infection = Random.Range(1000, 10000);

        if (File.Exists(Application.persistentDataPath + "/MySaveData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/MySaveData.dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();
            weaponImgCounter = data.savedWeaponImgCounter;
            costUpMoney = data.savedCostUpMoney;
            costUpWeapon = data.savedCostUpWeapon;
            money = data.savedMoney;
            miningSpeed = data.savedMiningSpeed;
            days = data.savedDays;
            power = data.savedPower;
            counter = data.savedCounter;
            zombies = data.savedZombies;
            people = data.savedPeople;
        }
        else
        {
            weaponImgCounter = 0;
            costUpMoney = 10;
            costUpWeapon = 10;
            money = 0;
            miningSpeed = 1;
            days = 0;
            power = 1;
            people = 7137583692;
            zombies = 742257351;
            counter = 0;
        }

        T_killed_z_counter.text = "Убито: " + counter;
        T_alive_z_counter.text = "Зомби: " + zombies;
        T_alive_p_counter.text = "Людей: " + people;
        Tdays.text = "Прошло: " + days.ToString() + " дней";
        Tmoney.text = "Заработано: " + money.ToString() + " $";
        T_costUpMoney.text = costUpMoney.ToString() + " $";
        T_costUpWeapon.text = costUpWeapon.ToString() + " $";
        img.sprite = Sprites[weaponImgCounter];
    }

    public void clck()
    {
        counter += power;
        zombies -= power;

        if (zombies < 1)
        {
            T_killed_z_counter.text = "Убито: " + counter;
            T_alive_z_counter.text = "Зомби: 0";
            ResetData();
            GetComponent<UImanager>().Win();
        }
        else
        {
            T_killed_z_counter.text = "Убито: " + counter;
            T_alive_z_counter.text = "Зомби: " + zombies;
        }
    }

    public void save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/MySaveData.dat");
        SaveData data = new SaveData();
        data.savedWeaponImgCounter = weaponImgCounter;
        data.savedCostUpWeapon = costUpWeapon;
        data.savedCostUpMoney = costUpMoney;
        data.savedMoney = money;
        data.savedMiningSpeed = miningSpeed;
        data.savedDays = days;
        data.savedPower = power;
        data.savedCounter = counter;
        data.savedZombies = zombies;
        data.savedPeople = people;
        bf.Serialize(file, data);
        file.Close();
    }

    public void ResetData()
    {
        if (File.Exists(Application.persistentDataPath + "/MySaveData.dat"))
        {
            File.Delete(Application.persistentDataPath + "/MySaveData.dat");
        }
    }

    public void Days()
    {
        infection = Random.Range(1000, 10000);
        people -= infection;
        zombies += infection;
        days++;
        money += miningSpeed;
        if (people > 0)
        {
            T_alive_z_counter.text = "Зомби: " + zombies;
            T_alive_p_counter.text = "Людей: " + people;
            Tdays.text = "Прошло: " + days.ToString() + " дней";
            Tmoney.text = "Заработано: " + money.ToString() + " $";
        }
        else
        {
            T_alive_z_counter.text = "Зомби: " + zombies;
            T_alive_p_counter.text = "Людей: 0";
            Tdays.text = "Прошло: " + days.ToString() + " дней";
            Tmoney.text = "Заработано: " + money.ToString() + " $";
            ResetData();
            GetComponent<UImanager>().Loose();
        }

    }

    public void UpWeapon()
    {
        if (money >= costUpWeapon)
        {
            power++;
            money -= costUpWeapon;
            costUpWeapon += 10;
            T_costUpWeapon.text = costUpWeapon.ToString() + " $";
            Tmoney.text = "Заработано: " + money.ToString() + " $";
            if (weaponImgCounter < 6)
            {
                weaponImgCounter++;
                img.sprite = Sprites[weaponImgCounter];
            }
        }
    }

    public void UpMoney()
    {
        if (money >= costUpMoney)
        {
            miningSpeed*=2;
            money -= costUpMoney;
            costUpMoney += 10;
            T_costUpMoney.text = costUpMoney.ToString() + " $";
            Tmoney.text = "Заработано: " + money.ToString() + " $";
        }
    }
}

[Serializable]
class SaveData
{
    public int savedWeaponImgCounter;
    public int savedCostUpMoney;
    public int savedCostUpWeapon;
    public int savedMoney;
    public int savedMiningSpeed;
    public int savedDays;
    public int savedPower;
    public long savedCounter;
    public long savedZombies;
    public long savedPeople;
}