// Program used to test the USB Joystick object on the
// Arduino Leonardo or Arduino Micro.
//
// Matthew Heironimus
// 2015-03-28 - Original Version
// 2015-11-18 - Updated to use the new Joystick library
//              written for Arduino IDE Version 1.6.6 and
//              above.
// 2016-05-13   Updated to use new dynamic Joystick library
//              that can be customized.
//------------------------------------------------------------
// Modified by Andrey Zhuravlev
//------------------------------------------------------------
//

#include "Joystick.h" // By Matthew Heironimus (https://github.com/MHeironimus/ArduinoJoystickLibrary)
#include "HX711.h" // By Bogdan Necula (https://github.com/bogde/HX711)

#define LOADCELL_DOUT_PIN 3 // Change to your PIN number if needed
#define LOADCELL_SCK_PIN 2 // Change to your PIN number if needed

#define VIRTUAL_BTN // Comment this line if you don't want a virtual handbrake button

#ifdef VIRTUAL_BTN
#define BTN_CNT 1
#else
#define BTN_CNT 0
#endif

HX711 loadcell;

// Create Joystick
Joystick_ Joystick(JOYSTICK_DEFAULT_REPORT_ID,
                   JOYSTICK_TYPE_JOYSTICK, BTN_CNT, 0,
                   false, false, false, false, false, false,
                   false, // Rudder
                   false, // Throttle
                   false, // Accelerator
                   true,  // Brake
                   false  // Steering
);

// uncomment to test "Auto Send" mode or false to test "Manual Send" mode.
#define AUTO_SEND_MODE

#define MIN_VAL 0
#define BTN_VAL 16384
#define MAX_VAL 32767

#define LOADCELL_MIN 0.0 //Kg
#define LOADCELL_MAX 20.0 //Kg
#define LAODCELL_SCALE 103229 // Scale value->Kg

void setup()
{
  loadcell.begin(LOADCELL_DOUT_PIN, LOADCELL_SCK_PIN);
  loadcell.set_scale(LAODCELL_SCALE);
  loadcell.tare();
  
  // Set Range Values
  Joystick.setBrakeRange(MIN_VAL, MAX_VAL);

#ifdef AUTO_SEND_MODE
  Joystick.begin();
#else
  Joystick.begin(false);
#endif

  pinMode(13, OUTPUT);
  
   // Turn indicator light on.
  digitalWrite(13, 1);
}

inline double mapf(double x, double in_min, double in_max, double out_min, double out_max)
{
  return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
}

void loop()
{
  double val = loadcell.get_units();
  val = mapf(val, (double)LOADCELL_MIN, (double)LOADCELL_MAX, (double)MIN_VAL, (double)MAX_VAL);
  val = constrain(val, MIN_VAL, MAX_VAL);

  Joystick.setBrake((short)val);

#ifdef VIRTUAL_BTN
  Joystick.setButton(0, val >= BTN_VAL ? 1:0);
#endif
}