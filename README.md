## Contenido

- Este proyecto es un sistema de autenticación de usuarios desarrollado en .NET 8 bajo el patrón MVC, que implementa autenticación basada en tokens utilizando JWT (JSON Web Token). Se sigue un enfoque de Clean Architecture (ONION), lo que permite una modular la aplicación. Además, el sistema incluye prácticas para gestionar la autenticación y validación de usuarios, empleando el patrón Result para manejar los mensajes y respuestas del sistema, garantizando un control más detallado y efectivo en la presentación de errores o confirmaciones.




## Correr aplicación

### 1. Crear base de datos SQL SERVER

Se debe de crear una base datos que correspona con el nombre indicado, para salidad de la creación.

```
UserAuthentication
```


### 2. Ir al AppSettingJson

Configurar ambiente

Modificar servidor de base de datos (SQL SERVER)

```
"DefaultConnection": "Server={VALOR_A_MODIFICAR};Database=UserAuthentication;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
```

### 3. Comandos

Una vez completado los pasos anteriores, se debe abrir el Package manager Console. Para que las configuraciones determinadas se registren en la base de datos creada.
Las tablas, propiedades e identificadores de los mismos.

```
Update-Database
```
