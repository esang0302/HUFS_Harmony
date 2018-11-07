#include "Ardunity.h"
#include "AnalogInput.h"

int fsrPin = 0;     // the FSR and 10K pulldown are connected to a0
int fsrReading;     // the analog reading from the FSR resistor divider

AnalogInput aInput0(0, A0);

void setup()
{
  ArdunityApp.attachController((ArdunityController*)&aInput0);
  ArdunityApp.resolution(256, 1024);
  ArdunityApp.timeout(5000);
  ArdunityApp.begin(115200);
}

void loop()
{
  fsrReading = analogRead(fsrPin);

  Serial.println(fsrReading);

  if (fsrReading < 10) {
    Serial.println(" - No pressure");
  } else if (fsrReading < 200) {
    Serial.println(" - Light touch");
  } else if (fsrReading < 500) {
    Serial.println(" - Light squeeze");
  } else if (fsrReading < 800) {
    Serial.println(" - Medium squeeze");
  } else {
    Serial.println(" - Big squeeze");
  }
  ArdunityApp.process();
  delay(100);
}
