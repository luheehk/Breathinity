const int num_LEDs = 5;   
const int LED_pins[] = {4,5,6,7,8};     

const int min_adjust_pin = A1;  
const int max_adjust_pin = A0;  
const int sensor_pin = A15;      
   
int min_adjust_reading;   
int max_adjust_reading;   
int sensor_reading;       

int ADC_min;
int ADC_max; 

int delta;
int thresholds[5];

// variables for use with serial plotter
// int i = 0;
// const int N = 50;
// int plot1[N];
// int plot2[N];
// int plot3[N];
// int plot4[N];

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
   ADC_max = map(max_adjust_reading,0,1023,400,600);

  
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
  
  // Serial.print("Sensor: ");
  // Serial.print(sensor_reading);
  // Serial.print(" | ADC min: ");
  // Serial.print(ADC_min);
  // Serial.print(" | ADC max: ");
  // Serial.println(ADC_max);
 Serial.println(map(sensor_reading,300, 500, -1, 1 ));
  
  }
