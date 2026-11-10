# Sistema de Inventario Tienda #

Sistema de escritorio para la gestión de inventario de una tienda: productos, proveedores, departamentos y compras. Los empleados ingresan al sistema con su usuario y contraseña.

--ESTRUCTURADEL PROYECTO--


| `CapaDatos` | Conexión y consultas a SQL Server (`CD_Conexion`, `CD_Usuario`, `CD_Proveedores`, `CD_Productos`, `CD_Departamentos`, ...) |

| `CapaNegocio` | Validaciones y reglas del sistema (`CN_Usuario`, ...) |

| `CapaPresentacion` | Formularios de la aplicación (`FrmLogin`, `FrmProveedores`, `FrmDepartamentos`, ...) |

| `BaseDatos` | Script SQL para crear la base de datos (`Tienda.sql`) |


-- Cómo ejecutarlo

-- 1. Clonar el repositorio

git clone https://github.com/EsmeraldaPineda-hub/SistemaInventarioTienda.git


--2. Crear la base de datos

1. Abrir SQL Server Management Studio y conectarse a su servidor SQL Server.
2. Abrir el archivo `BaseDatos/Tienda_BaseDeDatos.sql` (**Archivo > Abrir > Archivo**).
3. Sin seleccionar nada, pulsar **F5**.
4. Al final debe aparecer una tabla de comprobación con: Departamentos 4, Proveedores 2, Productos 3, Usuarios 4.

El script se puede ejecutar varias veces sin duplicar datos. Crea la base `Tienda` con sus tablas, datos de ejemplo, procedimientos almacenados y los usuarios de prueba.

--3. Configurar la conexión

Abrir `CapaDatos/CD_Conexion.cs` y revisar la cadena de conexión.


-- 4. Ejecutar

1. Abrir `SistemaInventarioTienda.sln` en Visual Studio.
2. Establecer `CapaPresentacion` como proyecto de inicio.
3. Pulsar **F5**. Primero se muestra la ventana de inicio de sesión.

 -- Usuarios de prueba

| Usuario | Contraseña |

| `admin` | `admin123` |
| `dueno` | `Dueno123` |
| `empleado1` | `Emp1_123` |
| `empleado2` | `Emp2_123` |


-- Integrantes

- Xohitl Henriquez.
- Alejandra Deleon.
- Mayeli Santos.
- Diego Cerna.
