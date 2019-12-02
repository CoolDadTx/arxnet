/* DWARVEN SMITHY.H
 *
 * TODO:

 *
 */

const int dwarvenFileSize = 459;
extern unsigned char dwarvenBinary[dwarvenFileSize];

void loadDwarvenBinary();

void runDwarvenSmithy();
void processDwarvenMenuInput();
void displayDwarvenModuleText();
void calculateSaleItemValue(int itemRef);
void processPayment();
void chooseDwarvenSmithyItem();
void buildSmithyMenuOptions();
int calculateGemsAndJewelsTotal();
void deductGems(int totalGems);
//MLT: No return value
void createDwarvenInventoryItem(int startByte);
std::string readDwarvenNameString(int stringOffset);
void addDwarvenSmithyToMap();
void makeCustomWeaponOffer();
void createCustomWeapon();
void calculateForgeBonus(int additionalGemsOffered);
