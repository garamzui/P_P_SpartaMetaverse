

//// 메인 컨트롤러
//class GameManager
//{
//    PlayerController player;
//    void StartGame();
//    void EndOfWave();
//    void GameOver();
//}

//// 플레이어 본체
//class PlayerController
//{
//    int hp;
//    Inventory inventory;
//    void Init(GameManager gm);
//    void Move();
//    void Interact();
//    void TakeDamage(int dmg);
//}

//// 인벤토리 시스템
//class Inventory
//{
//    List<Item> items;
//    void AddItem(Item item);
//    void RemoveItem(Item item);
//}

//// 아이템 베이스
//class Item
//{
//    string name;
//    string description;
//    void Use();
//}

//// 맵 제어 (씬 관리 or 필드 관리)
//class MapManager
//{
//    void LoadScene(string sceneName);
//    void MoveToNextArea();
//}

//// NPC 상호작용
//class NPC
//{
//    string npcName;
//    void Talk();
//    void GiveQuest();
//}

//// 전투형 미니게임 관리
//class CombatManager
//{
//    List<Monster> enemies;
//    void StartCombat();
//    void EndCombat();
//}

//// 몬스터 베이스
//class Monster
//{
//    int hp;
//    void TakeDamage(int dmg);
//    void Attack(PlayerController player);
//}

//// UI 제어
//class UIManager
//{
//    void ShowHPBar();
//    void ShowInventory();
//    void ShowDialogue(string text);
//}
