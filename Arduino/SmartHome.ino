/*
  AUTHOR:   Fahim Faisal (embeddedfahim)
  DATE:     Jan 20, 2019
  LICENSE:  Apache 2.0 (Public Domain)
  CONTACT:  embeddedfahim@gmail.com
*/

#include<dht.h>
#include<SoftwareSerial.h>       // library for emulating serial communication protocol on non-dedicated digital pins..

dht DHT;
String str;                      // string for storing data coming from android..
int intensity;
int dataPin = 10;
SoftwareSerial BT(11, 12);       // soft RX, TX..

void setup() {
  BT.begin(38400);                // beginning software serial communication..
  Serial.begin(9600);
  pinMode(6, OUTPUT);
  pinMode(9, OUTPUT);            // declaring pin as an output pin..
  pinMode(13, OUTPUT);           // declaring pin as an output pin..
  pinMode(dataPin, INPUT);
}

void loop() {
  while(BT.available()) {        // checking whether data is available over software serial..
    delay(10);                   // delay for making things stable..
    char c = intensity = BT.read();
    str += c;                    // characters are being put together to form words..
  }
  
  if(str.length() > 0) {
    if(intensity == 102 || intensity == 110 || intensity == 114 || intensity == 118)
    {
      // do nothing
    }
    else
    {
      analogWrite(6, map(intensity, 0, 255, 255, 0));
    }
    if(str == "light on") {
      digitalWrite(9, HIGH);     // pulling pin high..
      digitalWrite(13, HIGH);    // pulling pin high..
    }
    else if(str == "light off") { 
      digitalWrite(9, LOW);      // pulling pin low..
      digitalWrite(13, LOW);     // pulling pin low..
    }
    else if(str == "weather") {
      int readData = DHT.read11(dataPin);
      int temp = DHT.temperature;
      int hum = DHT.humidity;
      BT.print("Temperature: ");
      BT.print(temp);
      BT.print(" °C");
      BT.print(" Humidity: ");
      BT.print(hum);
      BT.println("%");
    }

    str = "";                    // resetting the string variable for storing new data..
  }
}
