// This simple code allow you to send data from Arduino to Unity3D.

// uncomment "NATIVE_USB" if you're using ARM CPU (Arduino DUE, Arduino M0, ..)
//#define NATIVE_USB

// uncomment "SERIAL_USB" if you're using non ARM CPU (Arduino Uno, Arduino Mega, ..)
#define SERIAL_USB

int fsrPin = A0;     // the FSR and 10K pulldown are connected to a0
int fsrvalue;     // the analog reading from the FSR resistor divider
int maximum = 0;
int count = 0;
String data = "A";
void setup() {
  #ifdef NATIVE_USB
    SerialUSB.begin(1); //Baudrate is irevelant for Native USB
  #endif

  #ifdef SERIAL_USB
    Serial.begin(9600); // You can choose any baudrate, just need to also change it in Unity.
    while (!Serial); // wait for Leonardo enumeration, others continue immediately
  #endif
}

// Run forever
void loop() {
   fsrvalue = analogRead(fsrPin);

    sendData(String(fsrvalue));
          
  //sendData("Hello World!");
  delay(150); // Choose your delay having in mind your ReadTimeout in Unity3D
}

void sendData(String data){
   #ifdef NATIVE_USB
    SerialUSB.println(data); // need a end-line because wrmlh.csharp use readLine method to receive data 
  #endif

  #ifdef SERIAL_USB
    Serial.println(data); // need a end-line because wrmlh.csharp use readLine method to receive data
  #endif
}
