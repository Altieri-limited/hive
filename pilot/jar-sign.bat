rem keytool -genkey -keystore pilotKeyStore -alias pilot 
rem keytool -selfcert -keystore pilotKeyStore -alias pilot 
jarsigner -keystore pilotKeyStore -storepass password -keypass password pilot.jar pilot
jarsigner -verbose -certs -verify pilot.jar


 