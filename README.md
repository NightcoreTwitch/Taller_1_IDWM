# 🌐 Taller 1: Introducción al desarrollo web/móvil

## 📌 Integrantes
* Gabriel Cruz M. (21.126.127-7) gabriel.cruz@alumnos.ucn.cl
* Catalina Sanchez T. (21.190.860-2) catalina.sanchez@alumnos.ucn.cl
## 📌 Ayudantes
* Fernando Chaves B. fernando.chavez@alumnos.ucn.cl
* Ernes Fuenzalida T. ernes.fuenzalida@alumnos.ucn.cl
* Ignacio Valenzuela G. ignacio.valenzuela01@alumnos.ucn.cl

## 📖 Descripción

Este es un proyecto escrito en el lenguaje de programación C# con el entorno .NET el cual es una coledcción de controladores
para poder manipular productos y usuarios de una tienda de la Universidad Católica del Norte.
---
## Tecnologías
El proyecto utiliza las siguientes tecnologías y herramientas:
- **C#**: Lenguaje de programación.  
- **.NET 9**: Framework para construir la API REST.  
- **SQLite**: Base de datos para almacenar los datos del proyecto.  
- **Postman**: Herramienta para pruebas y documentación de los endpoints.  

## ⚙️ Requisitos Previos

Asegúrate de tener instalado:
1. [.NET SDK 9](https://builds.dotnet.microsoft.com/dotnet/Sdk/9.0.203/dotnet-sdk-9.0.203-win-x64.exe)  
2. [SQLite](https://www.sqlite.org/download.html)  
3. **Git** para clonar el repositorio.  
4. **Postman** (opcional, para probar los endpoints).  

---

## 🚀 Construcción

### 1️⃣ Clonar el Repositorio

Clonar el repositorio utilizando git
```bash
  git clone https://github.com/NightcoreTwitch/Taller_1_IDWM  
```
### 2️⃣ Ir a la carpeta que contiene el proyecto
```bash
  cd Taller_1_IDWM
```
```bash
  cd Backend
```
---

### 3️⃣ Configuración del Archivo `appsettings.json`

El archivo `appsettings.json` contiene las configuraciones esenciales para el funcionamiento de la API.  
Asegúrate de que tenga la siguiente estructura:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=Ellen_Joe.db"
  }
}
```
#### 🔍 Explicación de Configuración

- **ConnectionStrings.DefaultConnection**:  
  Define la cadena de conexión para la base de datos SQLite (`Ellen_Joe.db`).  
  - **Valor Ejemplo**: `"Data Source=Ellen_Joe.db"`  
    Este valor indica que el archivo de base de datos `Ellen_Joe.db` estará ubicado en la raíz del proyecto.
  - **Cómo Configurarlo**:  
    Si deseas cambiar la ubicación o el nombre del archivo, edita la cadena de conexión. Por ejemplo:
    ```json
    "Data Source=./Data/DataBase.db"
    ```
    
- **AllowedHosts**:  
  Define los dominios desde los cuales se permite acceder a la API.  
  - **Valor Ejemplo**: `"*"`  
    El asterisco `"*"` permite acceso desde cualquier dominio. En entornos de producción, limita esta configuración a tus dominios específicos para mejorar la seguridad.

---

### 4️⃣ Base de Datos

Si estás utilizando **Entity Framework** para manejar la base de datos, debes aplicar las migraciones necesarias con los siguientes pasos:

1. **Migraciones**:
   El proyecto contiene las migraciones ya establecidas para compilar y crear la base de datos.

2. **DataSeeder:**
   El proyecto contiene un DataSeeder que se inicializa al momento de ejecutar el proyecto, este contiene datos para pruebas.
---
### 5️⃣ Ejecutar el Proyecto
  Una vez completados los pasos anteriores, puedes reestablecer las dependencias del servidor localmente con el siguiente comando:
  ```bash
   dotnet restore
  ```
  Y finalmente ejecutar el proyecto:
 ```bash
   dotnet run
  ```
  
