USE DB_DispositivosElectronicos;
GO

CREATE NONCLUSTERED INDEX IX_Cliente_Identificacion
ON Cliente (Identificacion);
GO

CREATE NONCLUSTERED INDEX IX_Cliente_Correo
ON Cliente (Correo);
GO

CREATE NONCLUSTERED INDEX IX_Modelo_IdMarca
ON Modelo (IdMarca);
GO

CREATE NONCLUSTERED INDEX IX_Producto_Filtros
ON Producto (IdMarca, IdModelo, IdTipoDispositivo, IdColor, Estado);
GO

CREATE NONCLUSTERED INDEX IX_Producto_CodigoBarras
ON Producto (CodigoBarras);
GO

CREATE NONCLUSTERED INDEX IX_Factura_Fecha
ON Factura (FechaFactura);
GO

CREATE NONCLUSTERED INDEX IX_Factura_Cliente
ON Factura (IdCliente);
GO

CREATE NONCLUSTERED INDEX IX_Factura_Usuario
ON Factura (IdUsuario);
GO

CREATE NONCLUSTERED INDEX IX_FacturaDetalle_Factura
ON FacturaDetalle (IdFactura);
GO

CREATE NONCLUSTERED INDEX IX_FacturaDetalle_Producto
ON FacturaDetalle (IdProducto);
GO

CREATE OR ALTER PROCEDURE sp_Cliente_Insertar
    @TipoIdentificacion VARCHAR(20),
    @Identificacion VARCHAR(20),
    @Nombre VARCHAR(80),
    @Apellido1 VARCHAR(80),
    @Apellido2 VARCHAR(80) = NULL,
    @Sexo CHAR(1),
    @Telefono VARCHAR(20),
    @Correo VARCHAR(150),
    @DireccionExacta VARCHAR(300),
    @IdProvincia INT,
    @Fotografia VARBINARY(MAX) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Cliente
    (
        TipoIdentificacion,
        Identificacion,
        Nombre,
        Apellido1,
        Apellido2,
        Sexo,
        Telefono,
        Correo,
        DireccionExacta,
        IdProvincia,
        Fotografia
    )
    VALUES
    (
        @TipoIdentificacion,
        @Identificacion,
        @Nombre,
        @Apellido1,
        @Apellido2,
        @Sexo,
        @Telefono,
        @Correo,
        @DireccionExacta,
        @IdProvincia,
        @Fotografia
    );
END;
GO

CREATE OR ALTER PROCEDURE sp_Cliente_Actualizar
    @IdCliente INT,
    @TipoIdentificacion VARCHAR(20),
    @Identificacion VARCHAR(20),
    @Nombre VARCHAR(80),
    @Apellido1 VARCHAR(80),
    @Apellido2 VARCHAR(80) = NULL,
    @Sexo CHAR(1),
    @Telefono VARCHAR(20),
    @Correo VARCHAR(150),
    @DireccionExacta VARCHAR(300),
    @IdProvincia INT,
    @Fotografia VARBINARY(MAX) = NULL,
    @Estado BIT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Cliente
    SET
        TipoIdentificacion = @TipoIdentificacion,
        Identificacion = @Identificacion,
        Nombre = @Nombre,
        Apellido1 = @Apellido1,
        Apellido2 = @Apellido2,
        Sexo = @Sexo,
        Telefono = @Telefono,
        Correo = @Correo,
        DireccionExacta = @DireccionExacta,
        IdProvincia = @IdProvincia,
        Fotografia = @Fotografia,
        Estado = @Estado
    WHERE IdCliente = @IdCliente;
END;
GO

CREATE OR ALTER PROCEDURE sp_Cliente_Eliminar
    @IdCliente INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Cliente
    SET Estado = 0
    WHERE IdCliente = @IdCliente;
END;
GO

CREATE OR ALTER PROCEDURE sp_Cliente_ObtenerPorId
    @IdCliente INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        c.IdCliente,
        c.TipoIdentificacion,
        c.Identificacion,
        c.Nombre,
        c.Apellido1,
        c.Apellido2,
        c.Sexo,
        c.Telefono,
        c.Correo,
        c.DireccionExacta,
        c.IdProvincia,
        p.NombreProvincia,
        c.Fotografia,
        c.FechaRegistro,
        c.Estado
    FROM Cliente c
    INNER JOIN Provincia p
        ON c.IdProvincia = p.IdProvincia
    WHERE c.IdCliente = @IdCliente;
END;
GO

CREATE OR ALTER PROCEDURE sp_Cliente_Listar
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        c.IdCliente,
        c.TipoIdentificacion,
        c.Identificacion,
        c.Nombre,
        c.Apellido1,
        c.Apellido2,
        c.Sexo,
        c.Telefono,
        c.Correo,
        c.DireccionExacta,
        c.IdProvincia,
        p.NombreProvincia,
        c.FechaRegistro,
        c.Estado
    FROM Cliente c
    INNER JOIN Provincia p
        ON c.IdProvincia = p.IdProvincia
    ORDER BY c.IdCliente DESC;
END;
GO

CREATE OR ALTER PROCEDURE sp_Marca_Insertar
    @CodigoMarca VARCHAR(20),
    @Descripcion VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Marca (CodigoMarca, Descripcion)
    VALUES (@CodigoMarca, @Descripcion);
END;
GO

CREATE OR ALTER PROCEDURE sp_Marca_Actualizar
    @IdMarca INT,
    @CodigoMarca VARCHAR(20),
    @Descripcion VARCHAR(100),
    @Estado BIT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Marca
    SET
        CodigoMarca = @CodigoMarca,
        Descripcion = @Descripcion,
        Estado = @Estado
    WHERE IdMarca = @IdMarca;
END;
GO

CREATE OR ALTER PROCEDURE sp_Marca_Eliminar
    @IdMarca INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Marca
    SET Estado = 0
    WHERE IdMarca = @IdMarca;
END;
GO

CREATE OR ALTER PROCEDURE sp_Marca_ObtenerPorId
    @IdMarca INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        IdMarca,
        CodigoMarca,
        Descripcion,
        Estado
    FROM Marca
    WHERE IdMarca = @IdMarca;
END;
GO

CREATE OR ALTER PROCEDURE sp_Marca_Listar
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        IdMarca,
        CodigoMarca,
        Descripcion,
        Estado
    FROM Marca
    ORDER BY IdMarca DESC;
END;
GO

EXEC sp_Cliente_Listar;
GO

EXEC sp_Marca_Listar;
GO