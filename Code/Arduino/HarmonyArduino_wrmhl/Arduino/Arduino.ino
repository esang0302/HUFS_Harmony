// This simple code allow you to send data from Arduino to Unity3D.

// uncomment "NATIVE_USB" if you're using ARM CPU (Arduino DUE, Arduino M0, ..)
//#define NATIVE_USB

// uncomment "SERIAL_USB" if you're using non ARM CPU (Arduino Uno, Arduino Mega, ..)
#define SERIAL_USB

int fsrPin = A0;     // the FSR and 10K pulldown are connected to a0
int fsrvalue;     // the analog reading from the FSR resistor divider
int maximum = 0;
int count = 0;
String initial = "0";

void setup() {
  #ifdef NATIVE_USB
    SerialUSB.begin(1); //Baudrate is irevelant for Native USB
  #endif

  #ifdef SERIAL_USB
    Serial.begin(250000); // You can choose any baudrate, just need to also change it in Unity.
    while (!Serial); // wait for Leonardo enumeration, others continue immediately
  #endif
  //Serial.println(0);
}

// Run forever
void loop() {
  fsrvalue = analogRead(fsrPin);
  //sendData("hey");
  
  /*if (count == 0){ // 처음 한번만 시작 메세지 보내기 위해 count 설정
    sendData(initial); // 유니티에 통신 시작을 알리는 data 전송
    count = 1; // count =1 하여 다시 보내지 않도록
  }
  
   else if(count == 1){ // count == 1일때 유니티와의 통신 시작 > 센서값 보낸다.
      if(maximum <= fsrvalue){
          maximum = fsrvalue;
        } 
        if (maximum > fsrvalue){
          sendData(String(maximum));  // 유니티로 압력센서의 받은 max 값을 보낸다.
          maximum = 0;
          }
    }*/
    if(maximum <= fsrvalue){
          maximum = fsrvalue;
        } 
    if (maximum > fsrvalue){
      sendData(String(maximum));  // 유니티로 압력센서의 받은 max 값을 보낸다.
      maximum = 0;
    }
   delay(100);
}

void sendData(String data){
   #ifdef NATIVE_USB
    SerialUSB.println(data); // need a end-line because wrmlh.csharp use readLine method to receive data 
  #endif

  #ifdef SERIAL_USB
    Serial.println(data); // need a end-line because wrmlh.csharp use readLine method to receive data
  #endif
}
