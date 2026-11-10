/* =====================================================================
   SISTEMA DE INVENTARIO PARA TIENDA 
   ===================================================================== */

IF DB_ID(N'Tienda') IS NULL
    CREATE DATABASE Tienda;
GO

USE Tienda;
GO

/* ===================== TABLAS ===================== */

IF OBJECT_ID(N'dbo.Departamento', N'U') IS NULL
CREATE TABLE Departamento
(
    Id_Departamento int identity(1,1) not null,
    Nombre nvarchar(50) not null,
    constraint PK_Departamento primary key (Id_Departamento),
    constraint UQ_Departamento_Nombre unique (Nombre)
);
GO

IF OBJECT_ID(N'dbo.Proveedor', N'U') IS NULL
CREATE TABLE Proveedor
(
    Id_Proveedor int identity(1,1) not null,
    Nombre nvarchar(80) not null,
    Telefono nvarchar(15) null,
    constraint PK_Proveedor primary key (Id_Proveedor),
    constraint UQ_Proveedor_Nombre unique (Nombre)
);
GO

IF OBJECT_ID(N'dbo.Producto', N'U') IS NULL
CREATE TABLE Producto
(
    Id_Producto int identity(1,1) not null,
    Descripcion nvarchar(100) not null,
    Id_Departamento int not null,
    Id_Proveedor int not null,
    UnidadesPorCaja int not null,
    CostoUnidad decimal(10,4) not null,
    PrecioVenta decimal(10,2) not null,
    FechaVencimiento date null,
    StockActual int not null default 0,
    StockMinimo int not null default 3,
    constraint PK_Producto primary key (Id_Producto),
    constraint FK_Producto_Departamento foreign key (Id_Departamento) references Departamento (Id_Departamento),
    constraint FK_Producto_Proveedor foreign key (Id_Proveedor) references Proveedor (Id_Proveedor),
    constraint CK_Producto_Precio check (PrecioVenta >= 0),
    constraint CK_Producto_Stock check (StockActual >= 0)
);
GO

IF OBJECT_ID(N'dbo.Usuario', N'U') IS NULL
CREATE TABLE Usuario
(
    Id_Usuario int identity(1,1) not null,
    NombreUsuario nvarchar(30) not null,
    ClaveHash varchar(64) not null,
    NombreCompleto nvarchar(80) not null,
    constraint PK_Usuario primary key (Id_Usuario),
    constraint UQ_Usuario_NombreUsuario unique (NombreUsuario)
);
GO

IF OBJECT_ID(N'dbo.Compra', N'U') IS NULL
CREATE TABLE Compra
(
    Id_Compra int identity(1,1) not null,
    NumeroFactura nvarchar(20) null,
    Id_Proveedor int not null,
    Fecha date not null default cast(getdate() as date),
    Id_Usuario int not null,
    Observacion nvarchar(200) null,
    constraint PK_Compra primary key (Id_Compra),
    constraint FK_Compra_Proveedor foreign key (Id_Proveedor) references Proveedor (Id_Proveedor),
    constraint FK_Compra_Usuario foreign key (Id_Usuario) references Usuario (Id_Usuario)
);
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'UX_Compra_NumeroFactura')
    CREATE UNIQUE INDEX UX_Compra_NumeroFactura ON Compra (NumeroFactura) WHERE NumeroFactura IS NOT NULL;
GO

IF OBJECT_ID(N'dbo.DetalleCompra', N'U') IS NULL
CREATE TABLE DetalleCompra
(
    Id_DetalleCompra int identity(1,1) not null,
    Id_Compra int not null,
    Id_Producto int not null,
    Cantidad int not null,
    CostoUnitario decimal(10,4) not null,
    Subtotal as (Cantidad * CostoUnitario) persisted,
    constraint PK_DetalleCompra primary key (Id_DetalleCompra),
    constraint UQ_DetalleCompra unique (Id_Compra, Id_Producto),
    constraint FK_DetalleCompra_Compra foreign key (Id_Compra) references Compra (Id_Compra) on delete cascade,
    constraint FK_DetalleCompra_Producto foreign key (Id_Producto) references Producto (Id_Producto),
    constraint CK_DetalleCompra_Cantidad check (Cantidad > 0),
    constraint CK_DetalleCompra_Costo check (CostoUnitario >= 0)
);
GO

/* ===================== DATOS DE EJEMPLO ===================== */

IF NOT EXISTS (SELECT 1 FROM Departamento)
    INSERT INTO Departamento (Nombre) VALUES (N'Comida'), (N'Bebidas'), (N'Limpieza'), (N'Higiene');

IF NOT EXISTS (SELECT 1 FROM Proveedor)
    INSERT INTO Proveedor (Nombre, Telefono) VALUES (N'IMPULSA', N'7823-4561'), (N'Distribuidora Central', N'2200-1122');

IF NOT EXISTS (SELECT 1 FROM Producto)
    INSERT INTO Producto (Descripcion, Id_Departamento, Id_Proveedor, UnidadesPorCaja, CostoUnidad, PrecioVenta, FechaVencimiento, StockActual, StockMinimo)
    VALUES
    (N'Aceite Rico 700ml', (SELECT Id_Departamento FROM Departamento WHERE Nombre = N'Comida'),  (SELECT Id_Proveedor FROM Proveedor WHERE Nombre = N'IMPULSA'),               6, 1.00, 1.15, '2027-03-01', 24, 5),
    (N'Gaseosa 3 litros',  (SELECT Id_Departamento FROM Departamento WHERE Nombre = N'Bebidas'), (SELECT Id_Proveedor FROM Proveedor WHERE Nombre = N'Distribuidora Central'), 4, 1.80, 2.25, '2027-01-15',  2, 6),
    (N'Jabón de barra',    (SELECT Id_Departamento FROM Departamento WHERE Nombre = N'Higiene'), (SELECT Id_Proveedor FROM Proveedor WHERE Nombre = N'Distribuidora Central'), 12, 0.35, 0.50, NULL,         40, 10);

/* ===================== USUARIOS DEL SISTEMA ===================== */
/* Contraseñas de prueba (se guardan como hash SHA-256, nunca en texto plano):
   admin / admin123   dueno / Dueno123   empleado1 / Emp1_123   empleado2 / Emp2_123 */

IF NOT EXISTS (SELECT 1 FROM Usuario WHERE NombreUsuario = N'admin')
    INSERT INTO Usuario (NombreUsuario, ClaveHash, NombreCompleto)
    VALUES (N'admin', CONVERT(VARCHAR(64), HASHBYTES('SHA2_256', 'admin123'), 2), N'Administrador');

IF NOT EXISTS (SELECT 1 FROM Usuario WHERE NombreUsuario = N'dueno')
    INSERT INTO Usuario (NombreUsuario, ClaveHash, NombreCompleto)
    VALUES (N'dueno', CONVERT(VARCHAR(64), HASHBYTES('SHA2_256', 'Dueno123'), 2), N'Dueño de la tienda');

IF NOT EXISTS (SELECT 1 FROM Usuario WHERE NombreUsuario = N'empleado1')
    INSERT INTO Usuario (NombreUsuario, ClaveHash, NombreCompleto)
    VALUES (N'empleado1', CONVERT(VARCHAR(64), HASHBYTES('SHA2_256', 'Emp1_123'), 2), N'Empleado 1');

IF NOT EXISTS (SELECT 1 FROM Usuario WHERE NombreUsuario = N'empleado2')
    INSERT INTO Usuario (NombreUsuario, ClaveHash, NombreCompleto)
    VALUES (N'empleado2', CONVERT(VARCHAR(64), HASHBYTES('SHA2_256', 'Emp2_123'), 2), N'Empleado 2');
GO

/* ===================== VISTA ===================== */

CREATE OR ALTER VIEW vw_ProductoDetalle AS
SELECT  p.Id_Producto, p.Descripcion,
        p.Id_Departamento, d.Nombre AS Departamento,
        p.Id_Proveedor,    pr.Nombre AS Proveedor,
        p.UnidadesPorCaja, p.CostoUnidad, p.PrecioVenta,
        p.FechaVencimiento, p.StockActual, p.StockMinimo
FROM Producto p
JOIN Departamento d ON d.Id_Departamento = p.Id_Departamento
JOIN Proveedor pr   ON pr.Id_Proveedor   = p.Id_Proveedor
GO

/* ===================== PROCEDIMIENTOS: PRODUCTOS ===================== */

CREATE OR ALTER PROC sp_MostrarProductos
AS
SELECT * FROM vw_ProductoDetalle ORDER BY Descripcion
GO

CREATE OR ALTER PROC sp_ProductosBajoStock
AS
SELECT * FROM vw_ProductoDetalle WHERE StockActual <= StockMinimo ORDER BY StockActual
GO

CREATE OR ALTER PROC sp_ProductosPorVencer
AS
SELECT * FROM vw_ProductoDetalle
WHERE FechaVencimiento IS NOT NULL AND FechaVencimiento <= DATEADD(DAY, 30, CAST(GETDATE() AS date))
ORDER BY FechaVencimiento
GO

CREATE OR ALTER PROC sp_InsertarProducto
    @descripcion nvarchar(100),
    @idDepartamento int,
    @idProveedor int,
    @unidadesPorCaja int,
    @costoUnidad decimal(10,4),
    @precioVenta decimal(10,2),
    @fechaVencimiento date = NULL,
    @stockActual int,
    @stockMinimo int
AS
INSERT INTO Producto (Descripcion, Id_Departamento, Id_Proveedor, UnidadesPorCaja, CostoUnidad, PrecioVenta, FechaVencimiento, StockActual, StockMinimo)
VALUES (@descripcion, @idDepartamento, @idProveedor, @unidadesPorCaja, @costoUnidad, @precioVenta, @fechaVencimiento, @stockActual, @stockMinimo)
GO

CREATE OR ALTER PROC sp_EditarProducto
    @id int,
    @descripcion nvarchar(100),
    @idDepartamento int,
    @idProveedor int,
    @unidadesPorCaja int,
    @costoUnidad decimal(10,4),
    @precioVenta decimal(10,2),
    @fechaVencimiento date = NULL,
    @stockActual int,
    @stockMinimo int
AS
UPDATE Producto SET
    Descripcion = @descripcion, Id_Departamento = @idDepartamento, Id_Proveedor = @idProveedor,
    UnidadesPorCaja = @unidadesPorCaja, CostoUnidad = @costoUnidad, PrecioVenta = @precioVenta,
    FechaVencimiento = @fechaVencimiento, StockActual = @stockActual, StockMinimo = @stockMinimo
WHERE Id_Producto = @id
GO

CREATE OR ALTER PROC sp_EliminarProducto
    @id int
AS
DELETE FROM Producto WHERE Id_Producto = @id
GO

/* ===================== PROCEDIMIENTOS: DEPARTAMENTOS ===================== */

CREATE OR ALTER PROC sp_MostrarDepartamentos
AS
SELECT Id_Departamento, Nombre FROM Departamento ORDER BY Nombre
GO

CREATE OR ALTER PROC sp_InsertarDepartamento
    @nombre nvarchar(50)
AS
BEGIN
    IF EXISTS (SELECT 1 FROM Departamento WHERE Nombre = @nombre)
        THROW 50001, N'Ya existe un departamento con ese nombre.', 1;
    INSERT INTO Departamento (Nombre) VALUES (@nombre);
END
GO

CREATE OR ALTER PROC sp_EditarDepartamento
    @id int,
    @nombre nvarchar(50)
AS
BEGIN
    IF EXISTS (SELECT 1 FROM Departamento WHERE Nombre = @nombre AND Id_Departamento <> @id)
        THROW 50001, N'Ya existe un departamento con ese nombre.', 1;
    UPDATE Departamento SET Nombre = @nombre WHERE Id_Departamento = @id;
END
GO

CREATE OR ALTER PROC sp_EliminarDepartamento
    @id int
AS
BEGIN
    IF EXISTS (SELECT 1 FROM Producto WHERE Id_Departamento = @id)
        THROW 50002, N'No se puede eliminar: el departamento tiene productos asociados.', 1;
    DELETE FROM Departamento WHERE Id_Departamento = @id;
END
GO

/* ===================== PROCEDIMIENTOS: PROVEEDORES ===================== */

CREATE OR ALTER PROC sp_MostrarProveedores
AS
SELECT Id_Proveedor, Nombre, Telefono FROM Proveedor ORDER BY Nombre
GO

CREATE OR ALTER PROC sp_InsertarProveedor
    @nombre nvarchar(80),
    @telefono nvarchar(15) = NULL
AS
BEGIN
    IF EXISTS (SELECT 1 FROM Proveedor WHERE Nombre = @nombre)
        THROW 50001, N'Ya existe un proveedor con ese nombre.', 1;
    INSERT INTO Proveedor (Nombre, Telefono) VALUES (@nombre, @telefono);
END
GO

CREATE OR ALTER PROC sp_EditarProveedor
    @id int,
    @nombre nvarchar(80),
    @telefono nvarchar(15) = NULL
AS
BEGIN
    IF EXISTS (SELECT 1 FROM Proveedor WHERE Nombre = @nombre AND Id_Proveedor <> @id)
        THROW 50001, N'Ya existe un proveedor con ese nombre.', 1;
    UPDATE Proveedor SET Nombre = @nombre, Telefono = @telefono WHERE Id_Proveedor = @id;
END
GO

CREATE OR ALTER PROC sp_EliminarProveedor
    @id int
AS
BEGIN
    IF EXISTS (SELECT 1 FROM Producto WHERE Id_Proveedor = @id)
        THROW 50002, N'No se puede eliminar: el proveedor tiene productos asociados.', 1;
    IF EXISTS (SELECT 1 FROM Compra WHERE Id_Proveedor = @id)
        THROW 50003, N'No se puede eliminar: el proveedor tiene compras registradas.', 1;
    DELETE FROM Proveedor WHERE Id_Proveedor = @id;
END
GO

/* ===================== COMPRAS (entrada de mercancía) ===================== */

IF NOT EXISTS (SELECT 1 FROM sys.types WHERE name = N'TipoDetalleCompra')
    CREATE TYPE TipoDetalleCompra AS TABLE
    (
        Id_Producto   int NOT NULL,
        Cantidad      int NOT NULL,
        CostoUnitario decimal(10,4) NOT NULL
    );
GO

CREATE OR ALTER PROC sp_RegistrarCompra
    @numeroFactura nvarchar(20) = NULL,
    @idProveedor   int,
    @fecha         date,
    @observacion   nvarchar(200) = NULL,
    @detalle       TipoDetalleCompra READONLY,
    @idUsuario     int = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    IF NOT EXISTS (SELECT 1 FROM @detalle)
        THROW 50010, N'La compra debe tener al menos un producto.', 1;
    IF EXISTS (SELECT Id_Producto FROM @detalle GROUP BY Id_Producto HAVING COUNT(*) > 1)
        THROW 50011, N'Hay productos repetidos en la compra.', 1;
    IF @numeroFactura IS NOT NULL AND EXISTS (SELECT 1 FROM Compra WHERE NumeroFactura = @numeroFactura)
        THROW 50012, N'Ya existe una compra con ese número de factura.', 1;

    IF @idUsuario IS NULL
        SELECT @idUsuario = Id_Usuario FROM Usuario WHERE NombreUsuario = N'admin';
    IF @idUsuario IS NULL
        THROW 50013, N'No existe un usuario para registrar la compra.', 1;

    BEGIN TRAN;

        INSERT INTO Compra (NumeroFactura, Id_Proveedor, Fecha, Id_Usuario, Observacion)
        VALUES (@numeroFactura, @idProveedor, @fecha, @idUsuario, @observacion);

        DECLARE @idCompra int = SCOPE_IDENTITY();

        INSERT INTO DetalleCompra (Id_Compra, Id_Producto, Cantidad, CostoUnitario)
        SELECT @idCompra, Id_Producto, Cantidad, CostoUnitario FROM @detalle;

        UPDATE p
        SET p.StockActual = p.StockActual + d.Cantidad
        FROM Producto p
        JOIN @detalle d ON d.Id_Producto = p.Id_Producto;

    COMMIT;
END
GO

CREATE OR ALTER PROC sp_MostrarCompras
AS
SELECT  c.Id_Compra, c.NumeroFactura, c.Fecha,
        pr.Nombre AS Proveedor, u.NombreCompleto AS Usuario,
        ISNULL(SUM(d.Subtotal), 0) AS Total, c.Observacion
FROM Compra c
JOIN Proveedor pr ON pr.Id_Proveedor = c.Id_Proveedor
JOIN Usuario u    ON u.Id_Usuario    = c.Id_Usuario
LEFT JOIN DetalleCompra d ON d.Id_Compra = c.Id_Compra
GROUP BY c.Id_Compra, c.NumeroFactura, c.Fecha, pr.Nombre, u.NombreCompleto, c.Observacion
ORDER BY c.Fecha DESC, c.Id_Compra DESC
GO

CREATE OR ALTER PROC sp_MostrarDetalleCompra
    @idCompra int
AS
SELECT  p.Descripcion AS Producto, d.Cantidad, d.CostoUnitario, d.Subtotal
FROM DetalleCompra d
JOIN Producto p ON p.Id_Producto = d.Id_Producto
WHERE d.Id_Compra = @idCompra
GO

/* ===================== COMPROBACIÓN ===================== */
SELECT 'Departamentos' AS Tabla, COUNT(*) AS Registros FROM Departamento
UNION ALL SELECT 'Proveedores', COUNT(*) FROM Proveedor
UNION ALL SELECT 'Productos',   COUNT(*) FROM Producto
UNION ALL SELECT 'Usuarios',    COUNT(*) FROM Usuario;
GO
