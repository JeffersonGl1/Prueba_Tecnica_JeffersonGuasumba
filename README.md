# Prueba_Tecnica_JeffersonGuasumba
Detalles:

1. La cadena de conexión a la base de datos se puede modificar de acuerdo al host en los archivos DockerFile de cada Microservicio.
2. El proyecto se encuentra dockerizado con docker compose y dockerfile para cada microservicio. El sistema operativo para el despliegue es Linux.
3. Cada uno de los microservicios funcionan independientemente y se comunican entre si mediante reques de HTTP Client.
4. Se adjunta el script de la base de datos utilizada llamada "BaseDatos.sql"
5. Se adjunta el archivo json de postman llamado "Pruebas Microservicios_PruebaTecnica_Jefferson Guasumba.postman_collection.json" para probar la ejecucíon de los endpoints de los microservicios.
6. El proyecto "Microservicio1.UnitTest" ejecuta dos pruebas unitarias para validar la existencia o no de un cliente.
7. El proyecto "Microservicio1.IntegrationTest" ejecuta una prueba de integración combinada en donde comprueba la inserción de un cliente y su repectiva consulta con valores de request ok(200).
