const int damonFileSize = 916;
extern unsigned char damonBinary[damonFileSize];

void message(string txt);
void shopDamon();
void stockDamon();
void loadDamonBinary();

//MLT: No return value
void createInventoryItem(int startByte);
string readNameString(int stringOffset);


