# 📚 Sistema de Préstamo de Libros - Web API (ASP.NET Core)

Proyecto desarrollado como parte de la evaluación 2 para la asignatura de **Programación .NET**, carrera de Ingeniería en Ejecución Informática. Esta API RESTful permite gestionar libros, usuarios y préstamos en una biblioteca municipal.

---

## Libros
### Listar todos los libros
![image](https://github.com/user-attachments/assets/2404b73f-e7ac-4c66-b742-6584d6f7d60a)

### Muestra los detalles de un libro
![image](https://github.com/user-attachments/assets/18b59a9b-75b8-4969-87f2-eda8a2f1306b)
![image](https://github.com/user-attachments/assets/6e5d8979-7235-44b4-bc21-8b08c0266a6a)
### Crea un nuevo libro
![image](https://github.com/user-attachments/assets/4ee1ad56-d8e8-4a2c-a292-0fd17bbc2a3a)
![image](https://github.com/user-attachments/assets/d0d13250-ffe9-401f-ac2a-d18ee7b936e2)

*El campo id del JSON  no es nescesario de llenar, ya que se genera automaticamente
![image](https://github.com/user-attachments/assets/55f874cb-0670-4ece-9bfd-65f68abc091b)

---

### Validacion del metodo PUT
Con error
![image](https://github.com/user-attachments/assets/4e3aaefa-2f94-42b8-b559-fe2e96c22703)

este es el metodo PUT funcionalidad
![image](https://github.com/user-attachments/assets/c6e18bd1-259e-409f-8e6c-354bcc2f112e)

Respuesta
![image](https://github.com/user-attachments/assets/4a98d8f1-be9f-4bdd-8ba7-42dc539c1ebd)

Esto es lo que sigue devolviendo el método PUT por que no remplaza los valores
![image](https://github.com/user-attachments/assets/2dd30055-2b9d-4ef4-b9d9-49f55d495aaa)

Sin error

![image](https://github.com/user-attachments/assets/68c1b14c-76a7-4809-a07f-5c75d1713f19)

Ejecucion del metodo PUT
![image](https://github.com/user-attachments/assets/4f498104-9e60-4c7a-afa2-6af60bede8f7)

Respuesta
![image](https://github.com/user-attachments/assets/db4073d5-d021-4369-a69a-4baae8557803)

Ahora corregido el método PUT pide que se le asigne cierta cantidad de valores en el campo Isbn

---

### Eliminacion de un Libro
![image](https://github.com/user-attachments/assets/ea569d82-e492-4b22-9248-0ede9b15c970)

El metodo Delete da error al intentar eliminar un libro prestado 
![image](https://github.com/user-attachments/assets/258ac5f0-a635-4a5c-84a7-929dcc6e984a)

Una vez quitado el prestamo ya se puede eliminar un libro
![image](https://github.com/user-attachments/assets/550393e5-9bcd-40b1-bc2e-610c93468bc5)

---

## Usuarios
### Listar los usuarios registrados
![image](https://github.com/user-attachments/assets/09130ad8-295b-428e-bbcf-e30059093d89)

### Registro de un nuevo usuario
![image](https://github.com/user-attachments/assets/f4d22361-bc1f-4b65-8c80-f323805f3b85)
![image](https://github.com/user-attachments/assets/33a62669-fe20-43d1-a7fc-63dbd13008e8)

### Ver el historial de prestamos de un nuevo usuario
![image](https://github.com/user-attachments/assets/704ace81-37e5-472c-80db-8fdc1b88dbb4)
![image](https://github.com/user-attachments/assets/bd5e595d-3c3a-480a-8fee-7eed5d42cc79)

---

## Prestamos
### Registrar un nuevo prestamo(libro --> usuario)
![image](https://github.com/user-attachments/assets/13ea5608-1a9c-4e6b-8b0d-7c117b07007b)
![image](https://github.com/user-attachments/assets/c9933590-64b9-41c9-90ff-5709b4926c6d)
![image](https://github.com/user-attachments/assets/43a3ab96-8ef4-4888-8e20-b4a52e8de3f6)

### Registrar la devolucion de un libro
![image](https://github.com/user-attachments/assets/86e46ceb-7a61-455e-95a8-3ac7bc361c07)
![image](https://github.com/user-attachments/assets/e7834925-55b1-417f-bb8c-a802532972c4)
![image](https://github.com/user-attachments/assets/c54c20e2-83e5-4b89-bfbd-0c48f945cf0b)

## 👨‍💻 Integrantes

- Jordan Murillo
- Benjamín Ponce

---

## 📅 Fecha de entrega

**10 de junio de 2025**

