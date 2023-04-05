CREATE DATABASE JSanchezEstructura
USE JSanchezEstructura
GO

CREATE TABLE Puesto(
	PuestoID INT IDENTITY(1,1) PRIMARY KEY,
	Descripcion VARCHAR(20)
);
GO

CREATE TABLE Departamento(
	DepartamentoID INT IDENTITY(1,1) PRIMARY KEY,
	Descripcion VARCHAR(50)
);
GO

CREATE TABLE Empleado(
	EmpleadoID INT IDENTITY(1,1) PRIMARY KEY,
	Nombre VARCHAR(50),
	PuestoID INT,
	DepartamentoID INT
	CONSTRAINT fk_EmpleadoPuesto FOREIGN KEY (PuestoID) REFERENCES Puesto (PuestoID),
	CONSTRAINT fk_EmpleadoDepartamento FOREIGN KEY (DepartamentoID) REFERENCES Departamento (DepartamentoID)
);
GO

INSERT INTO Departamento(Descripcion)VALUES('Soporte Técnico')
INSERT INTO Departamento(Descripcion)VALUES('Administración')
INSERT INTO Departamento(Descripcion)VALUES('Compras')
INSERT INTO Departamento(Descripcion)VALUES('Ventas')
INSERT INTO Departamento(Descripcion)VALUES('Recursos Humanos')
GO

INSERT INTO Puesto(Descripcion)VALUES('Gerente')
INSERT INTO Puesto(Descripcion)VALUES('Jefe')
INSERT INTO Puesto(Descripcion)VALUES('Supervisor')
INSERT INTO Puesto(Descripcion)VALUES('Analista')
INSERT INTO Puesto(Descripcion)VALUES('Secretaria')
GO

INSERT INTO Empleado(Nombre,DepartamentoID,PuestoID)VALUES('Alfredo Pérez Torres',1,1)
INSERT INTO Empleado(Nombre,DepartamentoID,PuestoID)VALUES('Fernando Gonzalez Jiménez',1,2)
INSERT INTO Empleado(Nombre,DepartamentoID,PuestoID)VALUES('Luis Martinez Pérez',1,3)
INSERT INTO Empleado(Nombre,DepartamentoID,PuestoID)VALUES('José Robles Torres',2,2)
INSERT INTO Empleado(Nombre,DepartamentoID,PuestoID)VALUES('Karla Garcia Higareda',2,3)
INSERT INTO Empleado(Nombre,DepartamentoID,PuestoID)VALUES('Lorena Duran Castro',2,4)
INSERT INTO Empleado(Nombre,DepartamentoID,PuestoID)VALUES('Jessica Ramírez Estrada',3,3)
INSERT INTO Empleado(Nombre,DepartamentoID,PuestoID)VALUES('Jimena Zarate Vélez',3,4)
INSERT INTO Empleado(Nombre,DepartamentoID,PuestoID)VALUES('Daniel Garcia Buendía',3,5)
INSERT INTO Empleado(Nombre,DepartamentoID,PuestoID)VALUES('Rubén Núñez López',4,1)
INSERT INTO Empleado(Nombre,DepartamentoID,PuestoID)VALUES('Jorge Franco Quiroz',4,3)
INSERT INTO Empleado(Nombre,DepartamentoID,PuestoID)VALUES('Gloria Velazquez Pérez',4,5)
INSERT INTO Empleado(Nombre,DepartamentoID,PuestoID)VALUES('Wendy Salgado Olguín',5,2)
INSERT INTO Empleado(Nombre,DepartamentoID,PuestoID)VALUES('Arturo Frías Robles',5,4)
INSERT INTO Empleado(Nombre,DepartamentoID,PuestoID)VALUES('Francisco Torres Duran',5,1)
GO

--Vista
CREATE VIEW VistaEmpleado
AS
SELECT Empleado.EmpleadoID,Empleado.Nombre,Puesto.PuestoID,Puesto.Descripcion AS Puesto,Departamento.DepartamentoID,Departamento.Descripcion AS Departamento FROM Empleado
INNER JOIN Puesto ON Empleado.PuestoID = Puesto.PuestoID
INNER JOIN Departamento ON Empleado.DepartamentoID = Departamento.DepartamentoID
GO
--SP
--Permitir consultar todos los empleados son sus puestos y departamentos
ALTER PROCEDURE PuestoDepartamento --null,0,0
@Nombre VARCHAR(20),
@PuestoID INT,
@DepartamentoID INT
AS
IF(@Nombre IS NULL AND @PuestoID = 0 AND @DepartamentoID = 0)
SELECT Empleado.EmpleadoID,Empleado.Nombre,Puesto.PuestoID,Puesto.Descripcion AS Puesto, Departamento.DepartamentoID,Departamento.Descripcion AS Departamento FROM Empleado
INNER JOIN Puesto ON Empleado.PuestoID = Puesto.PuestoID
INNER JOIN Departamento ON Empleado.DepartamentoID = Departamento.DepartamentoID

ELSE IF(@Nombre IS NULL AND @PuestoID >= 1 AND @DepartamentoID = 0)
SELECT Empleado.EmpleadoID,Empleado.Nombre,Puesto.PuestoID,Puesto.Descripcion AS Puesto, Departamento.DepartamentoID,Departamento.Descripcion AS Departamento FROM Empleado
INNER JOIN Puesto ON Empleado.PuestoID = Puesto.PuestoID
INNER JOIN Departamento ON Empleado.DepartamentoID = Departamento.DepartamentoID
WHERE Puesto.PuestoID LIKE @PuestoID

ELSE IF(@Nombre IS NULL AND @PuestoID >= 1 AND @DepartamentoID >= 1)
SELECT Empleado.EmpleadoID,Empleado.Nombre,Puesto.PuestoID,Puesto.Descripcion AS Puesto, Departamento.DepartamentoID,Departamento.Descripcion AS Departamento FROM Empleado
INNER JOIN Puesto ON Empleado.PuestoID = Puesto.PuestoID
INNER JOIN Departamento ON Empleado.DepartamentoID = Departamento.DepartamentoID
WHERE Puesto.PuestoID LIKE @PuestoID AND Departamento.DepartamentoID LIKE @DepartamentoID

ELSE IF(@Nombre IS NULL AND @PuestoID = 0 AND @DepartamentoID >= 1)
SELECT Empleado.EmpleadoID,Empleado.Nombre,Puesto.PuestoID,Puesto.Descripcion AS Puesto, Departamento.DepartamentoID,Departamento.Descripcion AS Departamento FROM Empleado
INNER JOIN Puesto ON Empleado.PuestoID = Puesto.PuestoID
INNER JOIN Departamento ON Empleado.DepartamentoID = Departamento.DepartamentoID
WHERE Departamento.DepartamentoID LIKE @DepartamentoID

ELSE IF(@PuestoID >= 1 AND @DepartamentoID = 0)
SELECT Empleado.EmpleadoID,Empleado.Nombre,Puesto.PuestoID,Puesto.Descripcion AS Puesto, Departamento.DepartamentoID,Departamento.Descripcion AS Departamento FROM Empleado
INNER JOIN Puesto ON Empleado.PuestoID = Puesto.PuestoID
INNER JOIN Departamento ON Empleado.DepartamentoID = Departamento.DepartamentoID
WHERE Nombre LIKE '%' + @Nombre + '%' AND Puesto.PuestoID LIKE @PuestoID

ELSE IF(@PuestoID = 0 AND @DepartamentoID >= 1)
SELECT Empleado.EmpleadoID,Empleado.Nombre,Puesto.PuestoID,Puesto.Descripcion AS Puesto, Departamento.DepartamentoID,Departamento.Descripcion AS Departamento FROM Empleado
INNER JOIN Puesto ON Empleado.PuestoID = Puesto.PuestoID
INNER JOIN Departamento ON Empleado.DepartamentoID = Departamento.DepartamentoID
WHERE Nombre LIKE '%' + @Nombre + '%' AND Departamento.DepartamentoID LIKE @DepartamentoID

ELSE
SELECT Empleado.EmpleadoID,Empleado.Nombre,Puesto.PuestoID,Puesto.Descripcion AS Puesto, Departamento.DepartamentoID,Departamento.Descripcion AS Departamento FROM Empleado
INNER JOIN Puesto ON Empleado.PuestoID = Puesto.PuestoID
INNER JOIN Departamento ON Empleado.DepartamentoID = Departamento.DepartamentoID
WHERE Nombre LIKE '%' + @Nombre + '%'
GO
--Permitir consultar todos los puestos
ALTER PROCEDURE Puestos 
AS
SELECT PuestoID,Descripcion AS Puestos FROM Puesto
GO
--Permitir consultar todos los departamentos
ALTER PROCEDURE Departamentos
AS
SELECT DepartamentoID,Descripcion AS Departamento FROM Departamento
GO
--Permitir insertar empleados recibiendo como parámetros(Nombre,PuestoID,DepartamentoID)
CREATE PROCEDURE EmpleadoAdd
@Nombre VARCHAR(50),
@PuestoID INT,
@DepartamentoID INT
AS
INSERT INTO Empleado(Nombre,PuestoID,DepartamentoID)VALUES(@Nombre,@PuestoID,@DepartamentoID)
GO
--Permitir eliminar empleados por medio del ID
CREATE PROCEDURE EmpleadoDelete
@EmpleadoID INT
AS
DELETE FROM Empleado WHERE EmpleadoID = @EmpleadoID
GO
--Consultar empleados por coincidencia del nombre
CREATE PROCEDURE BuscarNombreEmpleado
@Nombre VARCHAR(50)
AS
SELECT Nombre FROM Empleado
WHERE Nombre LIKE '%' + @Nombre + '%'
GO
--Actualizar
CREATE PROCEDURE EmpleadoUpdate
@EmpleadoID INT,
@Nombre VARCHAR(50),
@PuestoID INT,
@DepartamentoID INT
AS
UPDATE Empleado 
SET
Nombre=@Nombre,
PuestoID=@PuestoID,
DepartamentoID=@DepartamentoID
WHERE EmpleadoID=@EmpleadoID
GO
--GetById
CREATE PROCEDURE EmpleadoGetById
@EmpleadoID INT
AS
SELECT Empleado.EmpleadoID,Empleado.Nombre,Puesto.PuestoID,Puesto.Descripcion AS Puesto,Departamento.DepartamentoID,Departamento.Descripcion AS Departamento FROM Empleado
INNER JOIN Puesto ON Empleado.PuestoID = Puesto.PuestoID
INNER JOIN Departamento ON Empleado.DepartamentoID = Departamento.DepartamentoID
WHERE EmpleadoID = @EmpleadoID
GO
