const int citySmithyFileSize = 1691;
extern unsigned char citySmithyBinary[citySmithyFileSize];

void loadCitySmithyBinary();

void shopSmithy();
int getSmithyNo();
void stockSmithyWares();
void smithyDisplayUpdate();

//TODO: MLT No return value
void createCitySmithyInventoryItem(int startByte);
string readSmithyItemString(int stringOffset);
