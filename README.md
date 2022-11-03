# Explicación
## Código en Arduino

Primero, empezamos declarando las bibliotecas necesarias 

![image](https://user-images.githubusercontent.com/83350102/199744360-73ddcb16-90df-4cf5-a285-eb4d9522f92e.png)

Ahora, declaramos las variables que usaremos para recibir los datos enviados por el MPU en quaternions (las variables q y el array Quack)

![image](https://user-images.githubusercontent.com/83350102/199744921-eeae63c9-5625-425e-8ee2-cf549695b7fc.png)

En el Setup, declaramos la velocidad de envío, usamos las variables de la biblioteca del MPU para verificar que no hayan errores y para calibrar los datos enviados por el MPU (GyroOffset, AccelOffset y CalibrateAccel)

![image](https://user-images.githubusercontent.com/83350102/199746284-ad77cce2-4118-4547-af7f-05532eb7b247.png)

En el Loop, primero pusimos que si hubieran errores, entonces no se continuara la operación. Si todo va correctamente, en el if se convierten los datos enviados por el MPU y se almacenan en el array Quack cada una de las variables en quaternion

![image](https://user-images.githubusercontent.com/83350102/199746710-526a7ec2-0e78-4a1d-8f9c-bf9e4a1007a2.png)

Finalmente, para no saturar de datos enviados a la memoria, pusimos la variable orden para que solo se envie los datos almacenados a Unity después de recibir una señal enviada por el mismo Unity, en este caso 'O'

![image](https://user-images.githubusercontent.com/83350102/199747382-c162fdd0-c672-4c55-90f2-83372d069366.png)

## Código en Unity

Declaramos las bibliotecas que vamos a utilizar

![image](https://user-images.githubusercontent.com/83350102/199748612-2078e10c-834a-49b2-af25-998c2fda3403.png)

Declaramos las variables que vamos a utilizar: el serial port, un buffer para almacenar los bytes localmente, dos variables de contadores para que el proyecto vaya fluido y los floats de los quaternions

![image](https://user-images.githubusercontent.com/83350102/199748919-8131bf37-c633-4ad1-b6f2-252b924ffe76.png)

En el start, iniciamos el puerto serial en Unity

![image](https://user-images.githubusercontent.com/83350102/199749291-5fab51a0-3b3b-4aac-9896-486e7b91ff79.png)

Ahora, se verifica que se hayan recibido los datos de Arduino, y si los recibe almacena cada uno de los datos del MPU en los floats en Quaternions, y así con estos floats se rota el cubo usando el código transform.rotation 

![image](https://user-images.githubusercontent.com/83350102/199749479-2b3c7b39-3cca-4b5a-9799-423d3801c139.png)

Finalmente, usamos el contador para que al pasar el tiempo envie la respuesta 'O' para que le pida al Arduino los datos recibidos del MPU 

![image](https://user-images.githubusercontent.com/83350102/199750199-2b29ac21-0ff8-4e99-bc0f-72ff2328ced5.png)
