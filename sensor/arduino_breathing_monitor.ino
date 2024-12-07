const int num_LEDs = 5;   
const int LED_pins[] = {4,5,6,7,8};   

const int min_adjust_pin = A1;  
const int max_adjust_pin = A0;  
const int sensor_pin = A15;    

int start_time; 
//const int led_pin = 13;

   
int min_adjust_reading;   
int max_adjust_reading;   
int sensor_reading;       

int ADC_min;
int ADC_max; 

int delta;
int thresholds[num_LEDs];

unsigned long last_time = 0;

void setup() {  

  for(int pin=0; pin<num_LEDs; pin++){
    pinMode(LED_pins[pin],OUTPUT);  
  }

  Serial.begin(9600);
}

void loop() { 

  min_adjust_reading = analogRead(min_adjust_pin);   
  max_adjust_reading = analogRead(max_adjust_pin);   
  sensor_reading = analogRead(sensor_pin);     

  
  // Consider adjusting these ranges??

  ADC_min = map(min_adjust_reading,0,1023,200,400);
  ADC_max = map(max_adjust_reading,0,1023,300,500);

  
  delta = (ADC_max - ADC_min)/(num_LEDs+1);
  thresholds[0] = ADC_min + delta;
  for(int i=1; i<num_LEDs; i++){
    thresholds[i] = thresholds[i-1] + delta;   
  }
  
  // Compare sensor readings to the thresholds and turn on
  // the appropriate LEDs for each sensor.

  for(int i=0; i<num_LEDs; i++){
    if(sensor_reading > thresholds[i]){
      digitalWrite(LED_pins[i],HIGH);      
    }
    else{
      digitalWrite(LED_pins[i],LOW);
    }
    
  }

  // Print variables 

  if (millis() > last_time + 200){

  // Serial.print("Sensor: ");
  // Serial.print(sensor_reading);
  // Serial.print(" | ADC min: ");
  // Serial.print(ADC_min);
  // Serial.print(" | ADC max: ");
  // Serial.println(ADC_max);

  Serial.println(map(sensor_reading,ADC_min,ADC_max,0,100));

  last_time = millis();
  }
  
}
