# Consideraciones previas
Este repositorio forma parte del proyecto de fin de grado para [Ingeniería Informática](https://www.uclm.es/es/toledo/fcsociales/grado-informatica) en la [Univesidad de Castilla La Mancha](https://www.uclm.es/).<br/>
Se puede encontrar todo el backend del proyecto contenerizado con [Docker](https://www.docker.com/), desde la base de datos, la API y los intermediarios que exponen la API al exterior desde una conexión que no dispone de un IP fija.<br/>
Un elemento a tener en cuenta es que se ha priorizado el uso de herramientas y frameworks diseñados y publicados por Micosft con la finalidad de mejorar la comunicación entre componentes.<br/>
# Estructura del backend
Los contenedores necesarios para el correcto funcionamiento del backend son los siguentes:
- Para el almacenamiento de los datos se ha optado por [SQL Server](https://learn.microsoft.com/es-es/sql/sql-server/what-is-sql-server?view=sql-server-ver17).<br/>
- Para la API se ha optado por [ASP .NET CORE](https://learn.microsoft.com/es-es/aspnet/core/introduction-to-aspnet-core?view=aspnetcore-9.0).<br/>
- Para hacer que la API sea visible fuera de la red se ha creado un contenedor con [ngrok](https://ngrok.com/).
- La documentación de los endpoints de la API se realiza mediante [swagger](https://swagger.io/). Lo cual permite probar los endpoints de una manera más intuitiva sin tener que ejecutar comandos ni emplear programas de terceros. La documentación se genera de manera automática en la API gracias a que a la hora de generar la api tambien se puede añadiendo una sola linea generar la documentación de swagger.
  ```c#
  var builder = WebApplication.CreateBuilder(args);
  builder.Services.AddDbContext<MiDbContext>(options =>
      options.UseSqlServer(builder.Configuration.GetConnectionString("MiConexionSql")));
  builder.Services.AddControllers();
  builder.Services.AddEndpointsApiExplorer();
  builder.Services.AddSwaggerGen();
  ```
# Visitar el backend
Para poder consultar el estado de los endpoints los parámetros requeridos para invocarlos y las respuestas esperadas se puede acceder mediante la
[Documentación swagger](https://native-supreme-locust.ngrok-free.app/swagger/index.html)
# Código del frontend
El código de la aplicación que usa esta en el repositorio [TFGAgricola](https://github.com/loleandote22/TFGAgricola)
