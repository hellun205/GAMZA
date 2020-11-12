using GAMJA.Entity;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using static GAMJA.Game.ConsoleFunc;
using static System.Console;
using static System.ConsoleKey;

namespace GAMJA.Game.Profile
{
  class Profile
  {
    public static void SaveProfile(Player player)
    {
      CreateDirectory(player.Name);

      Stream ws = new FileStream($"users/{player.Name}/player.dat", FileMode.OpenOrCreate);
      BinaryFormatter serializer = new BinaryFormatter();

      serializer.Serialize(ws, player);
      ws.Close();
    }

    public static Player LoadProfile(string name)
    {
      CreateDirectory(name);

      string path = $"users/{name}/player.dat";
      if (!File.Exists(path)) return null;
      Stream ws = new FileStream(path, FileMode.OpenOrCreate);
      BinaryFormatter deserializer = new BinaryFormatter();

      object profile = deserializer.Deserialize(ws);
      ws.Close();

      if (new Player().GetType().IsInstanceOfType(profile))
        return (Player)profile;
      else
        return null;
    }

    private static void CreateDirectory(string name)
    {
      Directory.CreateDirectory("users");
      Directory.CreateDirectory($"users/{name}");
    }

    public static Player GetPlayer()
    {
      string name = ReadTextScreen("불러올 플레이어의 이름을 입력하시오").Trim();

      if (name == "" || name == null) return null;

      Player player = LoadProfile(name);

      if (player == null)
      {
        WriteLineColor("플레이어를 찾을 수 없습니다.");
        ReadKey();
        return null;
      }
      else
      {
        return player;
      }
    }

    public static Player CreateUser()
    {
      string name = ReadTextScreen("만들 캐릭터의 이름을 입력하시오.").Trim();
      if (name == "" || name == null) return null;
      if (IsExistUserName(name))
      {
        switch (SelectScreen("캐릭터가 이미 존재합니다. 불러오시겠습니까?", new string[] { "불러오기\n", "취소\n" }))
        {
          case D1:
            return LoadProfile(name);
          case D2:
            return null;
          default:
            return null;
        }
      }
      Player player = new Player()
      {
        InitialMaxHp = 50,
        InitialMaxMp = 30,
        InitialAt = 5,
        InitialDef = 3,
        Name = name,
      };
      player.Inventory = new Inventory.Inven(6, 8, player);

      SaveProfile(player);
      return player;
    }

    public static bool IsExistUserName(string name)
    {
      return Directory.Exists($"users/{name}");
    }
  }
}
