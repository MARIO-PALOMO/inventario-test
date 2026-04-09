# Gestión de Inventarios

## Requisitos
- Visual Studio 2026 Community, con el SDK para .Net Core 10
- SQL Server 2025 Express
- SQL Server Management Studio 22
- Node.js v24.13.0
- Angular CLI 21.1.0
- Package Manager npm 11.6.2

## Ejecución SQL
- Abrir SQL Server Management Studio 22
- Crear un usuario nuevo con las siguientes credenciales: 
    - Usuario: sa
    - Contraseña: 123
- Iniciar sesión con las credenciales creadas
- En el menú de opciones seleccionar **Nueva consulta**
- En la nueva consulta copiar el texto que se encuentra en el archivo **\resources\scripts\database.sql**
- Ejecutar el script y se creará la base de datos con todos sus catálogos

## Ejecución del Backend

### Cambiar credenciales de conexión a la base de datos

- Ubicarse en la raíz del proyecto en este caso **\inventario-api**
- Ingresar a la siguiente ruta **inventario-api\Inventario.Api\\**
- En esa ruta estará un archivo llamado **appsettings.json**, abrirlo para editarlo
- Abierto el archivo **appsettings.json** en la sección con el nombre **InventoryDB** agregar el nombre del servidor de la base de datos de SQL Server del ordenador ("Server=[CambiarAqui];Database=Inventory;User Id=sa;Password=123;TrustServerCertificate=True;")
- Editado el archivo lo guardamos y lo cerramos

- Para saber el nombre del servidor de SQL Server podemos verlos abriendo el SQL Server Management Studio 22, apenas se abra mostrará una pantalla donde en la sección **Nombre del servidor** estará como se llama el mismo

### Ejecutar 

- Ubicarse en la raíz del proyecto en este caso **\inventario-api**
- En la raíz del proyecto ejecutar/abrir una consola **CMD**
- En la consola ejecutar el comando **dotnet build**
- En la consola ingresar al directorio Inventario.Api con el comando **cd  Inventario.Api**
- En la consola ejecutar el comando **dotnet run**
- Se ejecutara el backend en la siguiente url **http://localhost:5296/index.html**

NOTA: En la ruta que se ejecuta nuestro API se prodrá realizar las pruebas de los endpoints respectivos mediante el uso de swagger

## Ejecución del Frontend
- Ubicarse en la raíz del proyecto en este caso **\inventario-app**
- En la raíz del proyecto ejecutar/abrir una consola **CMD**
- En la consola ejecutar el comando **npm install**
- En la consola ejecutar el comando **ng serve**
- Se ejecutara el frontend en la siguiente url **http://localhost:4200**

## Evidencias

- Listado dinámico de productos y transacciones con paginación.
    - Productos
  <img width="1703" height="869" alt="Imagen" src="https://github.com/MARIO-PALOMO/inventario-test/blob/main/resources/images/Lista%20Productos.png?raw=true" />
    - Transacciones
<img width="1703" height="869" alt="Imagen" src="https://github.com/MARIO-PALOMO/inventario-test/blob/main/resources/images/Lista%20Transacciones.png?raw=true" />
    - Transacciones por Producto y Tipo
<img width="1703" height="869" alt="Imagen" src="https://github.com/MARIO-PALOMO/inventario-test/blob/main/resources/images/Lista%20Trasacciona%20Agrupado%20por%20producto%20y%20tipo.png?raw=true" />

- Pantalla para la creación de productos.
<img width="1703" height="869" alt="Imagen" src="https://github.com/MARIO-PALOMO/inventario-test/blob/main/resources/images/Agregar%20Productos.png?raw=true" />
<img width="1703" height="869" alt="Imagen" src="https://github.com/MARIO-PALOMO/inventario-test/blob/main/resources/images/Agregar%20Productos%20Validacion.png?raw=true" />

- Pantalla para la edición de productos.
<img width="1703" height="869" alt="Imagen" src="https://github.com/MARIO-PALOMO/inventario-test/blob/main/resources/images/Modificar%20Productos.png?raw=true" />
<img width="1703" height="869" alt="Imagen" src="https://github.com/MARIO-PALOMO/inventario-test/blob/main/resources/images/Modificar%20Productos%20Validacion.png?raw=true" />

- Pantalla para la creación de transacciones.
<img width="1703" height="869" alt="Imagen" src="https://github.com/MARIO-PALOMO/inventario-test/blob/main/resources/images/Agregar%20Transaccion.png?raw=true" />
<img width="1703" height="869" alt="Imagen" src="https://github.com/MARIO-PALOMO/inventario-test/blob/main/resources/images/Agregar%20Transaccion%20Validacion.png?raw=true" />

- Pantalla para la edición de transacciones.
<img width="1703" height="869" alt="Imagen" src="https://github.com/MARIO-PALOMO/inventario-test/blob/main/resources/images/Modificar%20Transaccion.png?raw=true" />
<img width="1703" height="869" alt="Imagen" src="https://github.com/MARIO-PALOMO/inventario-test/blob/main/resources/images/Modificar%20Transaccion%20Validacion.png?raw=true" />

- Pantalla de filtros dinámicos.
<img width="1703" height="869" alt="Imagen" src="https://github.com/MARIO-PALOMO/inventario-test/blob/main/resources/images/Filtros%20y%20Paginacion%20Productos.png?raw=true" />
<img width="1703" height="869" alt="Imagen" src="https://github.com/MARIO-PALOMO/inventario-test/blob/main/resources/images/Filtros%20y%20Paginacion%20Transacciones.png?raw=true" />
