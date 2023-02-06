# Detalle del proyecto
#### . Desarrollado con SDK .Net 5
#### . Baje el patrón de diseño Clean Architecture
#### . Acceso a datos con Dapper y validaciones con FluentValidation
#### . Base de datos SQL server 18

# Recursos

#### Tabla
![image](https://user-images.githubusercontent.com/124599625/217033390-ca0d5aab-e3ab-4336-8822-4397f683c8ea.png)

#### Store procedure
```
CREATE PROCEDURE dbo.sp_crud
(
	@Id int,
	@Nombre nvarchar(max),
	@Precio decimal(5,2),
	@Stock int,
	@FechaRegistro datetime,
	@Operation int
)
AS 
BEGIN
	
	IF @Operation = 1 -- create
	BEGIN
		set @Id = (select Top(1) Id  from Product order by Id DESC) + 1;
				
			Insert into Product(Id, Nombre, Precio, Stock, FechaRegistro)
			values (@Id, @Nombre, @Precio, @Stock, @FechaRegistro);
		
		select * from Product where Id = @Id;
	END
	
	IF @Operation = 2 -- update
	BEGIN
		update Product set Nombre = @Nombre, Precio = @Precio, Stock = @Stock
		where Id = @Id;
	
		select * from Product where Id = @Id;
	END
	
	IF @Operation = 3 -- get all
	BEGIN
		select * from Product;
	END
	
	IF @Operation = 4 -- get by id
	BEGIN
		select * from Product where Id = @Id;
	END
	
	IF @Operation = 5 -- delete
	BEGIN
		Delete from Product where Id = @Id;
		select * from Product where Id = @Id;
	END
	
END
```

# Ejecución
#### Luego de clonar el proyecto no olvidar ejecutar `dotnet build`


# Resultado
![image](https://user-images.githubusercontent.com/124599625/217031141-549ff8e3-1b56-4f4b-87a4-7141774222ef.png)
