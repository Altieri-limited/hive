#keytool -genkey -keystore pilotKeyStore -alias pilot 
#keytool -selfcert -keystore pilotKeyStore -alias pilot 
jarsigner -keystore pilotKeyStore -storepass password -keypass password pilot.jar pilot
jarsigner -verify pilot.jar


 