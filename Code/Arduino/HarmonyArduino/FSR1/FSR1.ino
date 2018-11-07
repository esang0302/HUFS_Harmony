#include "Ardunity.h"
#include "AnalogInput.h"

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
  ArdunityApp.process();
}
