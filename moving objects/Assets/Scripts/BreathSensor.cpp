void loop() { 
  min_adjust_reading = analogRead(min_adjust_pin);   
  max_adjust_reading = analogRead(max_adjust_pin);   
  sensor_reading = analogRead(sensor_pin);     

  ADC_min = map(min_adjust_reading, 0, 1023, 200, 400);
  ADC_max = map(max_adjust_reading, 0, 1023, 400, 600);

  delta = (ADC_max - ADC_min) / (num_LEDs + 1);
  thresholds[0] = ADC_min + delta;
  for (int i = 1; i < num_LEDs; i++) {
    thresholds[i] = thresholds[i - 1] + delta;   
  }
  
  for (int i = 0; i < num_LEDs; i++) {
    if (sensor_reading > thresholds[i]) {
      digitalWrite(LED_pins[i], HIGH);      
    } else {
      digitalWrite(LED_pins[i], LOW);
    }
  }

  // Output mapping for Unity (now -1 to 1)
  float mappedValue = map(sensor_reading, 300, 500, -1, 1);
  Serial.println(mappedValue);  
}
