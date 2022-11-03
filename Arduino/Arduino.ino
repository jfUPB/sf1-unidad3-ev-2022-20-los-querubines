#include "I2Cdev.h"
#include "MPU6050_6Axis_MotionApps20.h"
#if I2CDEV_IMPLEMENTATION == I2CDEV_ARDUINO_WIRE
    #include "Wire.h"
#endif

MPU6050 mpu;

Quaternion q;
uint8_t Quack[16] = {0};

bool dmpReady = false;
uint8_t mpuIntStatus;
uint8_t devStatus;
uint16_t packetSize;
uint16_t fifoCount;  
uint8_t fifoBuffer[64];

void setup() {

    #if I2CDEV_IMPLEMENTATION == I2CDEV_ARDUINO_WIRE
        Wire.begin();
        Wire.setClock(400000);
    #elif I2CDEV_IMPLEMENTATION == I2CDEV_BUILTIN_FASTWIRE
        Fastwire::setup(400, true);
    #endif

    Serial.begin(115200);

    mpu.initialize();
    devStatus = mpu.dmpInitialize();
    
    mpu.setXGyroOffset(220);
    mpu.setYGyroOffset(76);
    mpu.setZGyroOffset(-85);
    mpu.setZAccelOffset(1788);

    if (devStatus == 0) {
        mpu.CalibrateAccel(6);
        mpu.CalibrateGyro(6);
        mpu.PrintActiveOffsets();
        mpu.setDMPEnabled(true);
        dmpReady = true;
        packetSize = mpu.dmpGetFIFOPacketSize();
    }
}

void loop(){

  if(!dmpReady) return;
  
  if(Serial.available()){
      if (mpu.dmpGetCurrentFIFOPacket(fifoBuffer)) {
            mpu.dmpGetQuaternion(&q, fifoBuffer);
            memcpy(Quack, (uint8_t *)&q.w, 4);
            memcpy(Quack, (uint8_t *)&q.x, 4);
            memcpy(Quack, (uint8_t *)&q.y, 4);
            memcpy(Quack, (uint8_t *)&q.z, 4);
    }
      if(Serial.available() > 0){
        Serial.write(Quack, 16);
      }
  }
}
