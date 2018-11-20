#include <SoftwareSerial.h>
#include <String.h>

int fsrPin = A0;     // the FSR and 10K pulldown are connected to a0
int fsrvalue;     // the analog reading from the FSR resistor divider
int maximum = 0;
int count = 0;
int data = 0; // 처음인지 아닌지 확인하기위해
String send_data = "";
String unity = "";


float mapfloat(long x, long in_min, long in_max, long out_min, long out_max) // sensor 값을 0부터 1사이로 mapping해주는 함수
{
 return (float)(x - in_min) * (out_max - out_min) / (float)(in_max - in_min) + out_min;
}

void setup() {
  // put your setup code here, to run once:
  Serial.begin(9600);
}

void loop() {
  // put your main code here, to run repeatedly:
  fsrvalue = analogRead(fsrPin);
  if (count == 0){ // 처음 한번만 시작 메세지 보내기 위해 count 설정
    Serial.println(0); // 유니티에 통신 시작을 알리는 data 전송
    count = 1; // count =1 하여 다시 보내지 않도록
  }
  else if(count == 1){ // count == 1일때 유니티와의 통신 시작 > 센서값 보낸다.
    if(maximum <= fsrvalue){
            maximum = fsrvalue;
            Serial.flush();
          } 
          if (maximum > fsrvalue){
            Serial.println(maximum);  // 유니티로 압력센서의 받은 max 값을 보낸다.
            maximum = 0;
            Serial.flush();
            }
  }
   delay(150);
}
