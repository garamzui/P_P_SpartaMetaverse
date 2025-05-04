

//// ���� ��Ʈ�ѷ�
//class GameManager
//{
//    PlayerController player;
//    void StartGame();
//    void EndOfWave();
//    void GameOver();
//}

//// �÷��̾� ��ü
//class PlayerController
//{
//    int hp;
//    Inventory inventory;
//    void Init(GameManager gm);
//    void Move();
//    void Interact();
//    void TakeDamage(int dmg);
//}

//// �κ��丮 �ý���
//class Inventory
//{
//    List<Item> items;
//    void AddItem(Item item);
//    void RemoveItem(Item item);
//}

//// ������ ���̽�
//class Item
//{
//    string name;
//    string description;
//    void Use();
//}

//// �� ���� (�� ���� or �ʵ� ����)
//class MapManager
//{
//    void LoadScene(string sceneName);
//    void MoveToNextArea();
//}

//// NPC ��ȣ�ۿ�
//class NPC
//{
//    string npcName;
//    void Talk();
//    void GiveQuest();
//}

//// ������ �̴ϰ��� ����
//class CombatManager
//{
//    List<Monster> enemies;
//    void StartCombat();
//    void EndCombat();
//}

//// ���� ���̽�
//class Monster
//{
//    int hp;
//    void TakeDamage(int dmg);
//    void Attack(PlayerController player);
//}

//// UI ����
//class UIManager
//{
//    void ShowHPBar();
//    void ShowInventory();
//    void ShowDialogue(string text);
//}
