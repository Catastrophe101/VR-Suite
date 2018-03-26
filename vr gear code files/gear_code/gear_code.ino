#include <MeetAndroid.h>
#include <Wire.h>
MeetAndroid meetAndroid;

long accelX, accelY, accelZ;
float gForceX, gForceY, gForceZ;
float gX[3], gY[3], gZ[3];
long gyroX, gyroY, gyroZ;
float rotX, rotY, rotZ;
int controllerData[16]; // array to store data from 16 clock cycles

// Setup
void setup() 
{
  // set baud rate to 57.6k
  Serial.begin(57600);
  Wire.begin();
  setupMPU();
}
void setupMPU() {
  Wire.beginTransmission(0b1101000); //This is the I2C address of the MPU (b1101000/b1101001 for AC0 low/high datasheet sec. 9.2)
  Wire.write(0x6B); //Accessing the register 6B - Power Management (Sec. 4.28)
  Wire.write(0b00000000); //Setting SLEEP register to 0. (Required; see Note on p. 9)
  Wire.endTransmission();
  Wire.beginTransmission(0b1101000); //I2C address of the MPU
  Wire.write(0x1B); //Accessing the register 1B - Gyroscope Configuration (Sec. 4.4)
  Wire.write(0x00000000); //Setting the gyro to full scale +/- 250deg./s
  Wire.endTransmission();
  Wire.beginTransmission(0b1101000); //I2C address of the MPU
  Wire.write(0x1C); //Accessing the register 1C - Acccelerometer Configuration (Sec. 4.5)
  Wire.write(0b00000000); //Setting the accel to +/- 2g
  Wire.endTransmission();
}


void recordAccelRegisters() {
  Wire.beginTransmission(0b1101000); //I2C address of the MPU
  Wire.write(0x3B); //Starting register for Accel Readings
  Wire.endTransmission();
  Wire.requestFrom(0b1101000, 6); //Request Accel Registers (3B - 40)
  while (Wire.available() < 6);
  accelX = Wire.read() << 8 | Wire.read(); //Store first two bytes into accelX
  accelY = Wire.read() << 8 | Wire.read(); //Store middle two bytes into accelY
  accelZ = Wire.read() << 8 | Wire.read(); //Store last two bytes into accelZ
  processAccelData();
}
void processAccelData() {
  gForceX = accelX / 16384.0;
  gForceY = accelY / 16384.0;
  gForceZ = accelZ / 16384.0;
}


// Button Reference
// 01111111 11111111 - B
// 10111111 11111111 - Y
// 11011111 11111111 - Select
// 11101111 11111111 - Start
// 11110111 11111111 - Up
// 11111011 11111111 - Down
// 11111101 11111111 - Left
// 11111110 11111111 - Right
// 11111111 01111111 - A
// 11111111 10111111 - X
// 11111111 11011111 - L
// 11111111 11101111 - R

// Main loop
void loop() 
{
  delay(100);
  recordAccelRegisters();
  String Out = "";
  int length = 1;
  // UP
  if(gForceX > 0.5 && gForceY < 0.3 )
  {
    if (Out != "")
    {
      Out = Out + ",";
      length = length + 1;
    }
    Out = Out + "W";
    length = length + 1;
    //Serial.print("W");
  }
  
  // DOWN
  else if (gForceX < 0.3 && gForceY < 0.3 && gForceZ > 0.7)
  {
    if (Out != "")
    {
      Out = Out + ",";
      length = length + 1;
    }
    Out = Out + "S";
    length = length + 1;
    //Serial.print("S");
  }
  
  // RIGHT
  else if (gForceY > 0.5 && gForceX < 0.3)
  {
    if (Out != "")
    {
      Out = Out + ",";
      length = length + 1;
    }
    Out = Out + "D";
    length = length + 1;
    //Serial.print("D");
  }
  
  // LEFT
  else if (gForceY < -0.5 && gForceX < 0.3)
  {
    if (Out != "")
    {
      Out = Out + ",";
      length = length + 1;
    }
    Out = Out + "A";
    length = length + 1;
    //Serial.print("A");
  }
  else if (gForceX < -0.5 && gForceY < 0.3)
  {
    if (Out != "")
    {
      Out = Out + ",";
      length = length + 1;
    }
    Out = Out + "R";
    length = length + 1;
    //Serial.print("S");
  }
  
  char outData[length];
  Out.toCharArray(outData, length);
  meetAndroid.send(outData);
  delay(16);
 /* Serial.print(" X=");
  Serial.print(gForceX);
  Serial.print(" Y=");
  Serial.print(gForceY);
  Serial.print(" Z=");
  Serial.println(gForceZ);*/
} 

