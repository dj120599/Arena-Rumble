  const int pingPin = 2; // Trigger Pin of Ultrasonic Sensor
const int echoPin = 3; // Echo Pin of Ultrasonic Sensor

const int button_up = 10; //mete o valor button_up para 10
const int button_down = 7; //mete o valor button_down para 7
const int button_left = 9; //mete o valor button_left para 9
const int button_right = 8; //mete o valor button_right para 8

const int button_atk = 5; //mete o valor button_atk para 5
const int button_spc = 6; //mete o valor button_spc para 6

bool special = false;

void setup() {
  // put your setup code here, to run once:
 Serial.begin(9600); // Starting Serial Terminal
 
 pinMode(pingPin, OUTPUT);
 pinMode(echoPin, INPUT);

 pinMode(button_up, INPUT);
 pinMode(button_down, INPUT);
 pinMode(button_left, INPUT);
 pinMode(button_right, INPUT);

 pinMode(button_atk, INPUT);
 pinMode(button_spc, INPUT);
}

void loop() {

  //se o bool special for true, o ultra som será ligado
  if(special == true)
  {
  long duration, inches, cm;
   digitalWrite(pingPin, LOW);
   delayMicroseconds(2);
   digitalWrite(pingPin, HIGH);
   delayMicroseconds(10);
   digitalWrite(pingPin, LOW); 
   duration = pulseIn(echoPin, HIGH);
   cm = microsecondsToCentimeters(duration);
   if(cm > 100)
   {
    cm = 100;
   }else if(cm < 6)
   {
    
    cm = 6;
    special = false;
   }
   //manda o valor de cm para o unity
   Serial.write(cm);
   delay(100);
  }

    //se o butão button_up estiver carregado, manda o valor 1 para o unity
    if(digitalRead(button_up)==LOW){
    Serial.write(1); // Unity will read this value 
    Serial.flush();
    delay(20); 
    }

    //se o butão button_down estiver carregado, manda o valor 2 para o unity
     if(digitalRead(button_down)==LOW){
    Serial.write(2); // Unity will read this value 
    Serial.flush();
    delay(20); 
    }
    
    //se o butão button_left estiver carregado, manda o valor 3 para o unity
     if(digitalRead(button_left)==LOW){
    Serial.write(3); // Unity will read this value 
    Serial.flush();
    delay(20); 
    }

    //se o butão button_right estiver carregado, manda o valor 4 para o unity
     if(digitalRead(button_right)==LOW){
    Serial.write(4); // Unity will read this value 
    Serial.flush();
    delay(20); 
    }
    
    //se o butão button_atk estiver carregado, manda o valor 5 para o unity
     if(digitalRead(button_atk)==LOW){
    Serial.write(5); // Unity will read this value 
    Serial.flush();
    delay(20); 
    }

   //se o butão button_spc estiver carregado, o bool special  fica true
     if(digitalRead(button_spc)==LOW){
    special = true;
    }

    //se nenhum butão estever carregado
    if((digitalRead(button_spc))&&(digitalRead(button_atk))&&(digitalRead(button_left))
    &&(digitalRead(button_right))&&(digitalRead(button_down))&&(digitalRead(button_up))==HIGH)
    {
     Serial.write(0); // Unity will read this value 
    Serial.flush();
    delay(20); 
    }

}

long microsecondsToCentimeters(long microseconds) {
   return microseconds / 29 / 2;
}
