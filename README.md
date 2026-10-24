# Olive Garden: sistema de gestión y facturación para un restaurante

Trabajo final de la carrera **Analista Programador** (Universidad Abierta Interamericana).
Aplicación de escritorio para gestionar la operación de un restaurante: reservas, pedidos, cocina, inventario y facturación.

## Qué hace

- **Login y usuarios:** alta, baja y modificación de usuarios.
- **Roles y permisos:** cada usuario ve solo lo que su rol le permite (patrón Composite).
- **Reservas y mesas:** registro de reservas y estado de las mesas.
- **Pedidos y cocina:** el mozo toma el pedido, la cocina lo prepara y lo marca como listo.
- **Inventario:** alta y baja de productos, alerta de stock crítico y generación de órdenes de compra.
- **Facturación:** cálculo del total, descuentos y registro del pago (efectivo, tarjeta o Mercado Pago).
- **Reportes:** total facturado y ticket promedio.
- **Bitácora:** registro de las acciones realizadas en el sistema.
- **Backup y restore:** copia y recuperación de la base de datos.

## Tecnologías

- C# con .NET Framework 4.8
- Windows Forms
- Persistencia en archivo XML (`BaseDatos/BD.Xml`)
- Programación orientada a objetos y arquitectura en capas

## Arquitectura

La solución está dividida en proyectos con responsabilidades separadas:

| Proyecto | Responsabilidad |
|---|---|
| `BE` | Entidades de negocio (Cliente, Pedido, Factura, Producto, etc.) |
| `BLL_Negocio` | Lógica de negocio |
| `MPP` | Mapeo y acceso a datos (lee y escribe el XML) |
| `Abstraccion` | Interfaces compartidas |
| `Seguridad` | Manejo de contraseñas |
| `Backup` | Copias de seguridad y restauración |
| `OliveGarden_UI` | Interfaz de usuario (Windows Forms) |

## Cómo ejecutarlo

Necesitás Windows y Visual Studio 2019 o superior con la carga de trabajo **Desarrollo de escritorio con .NET**.

1. Cloná el repositorio.
2. Abrí `src/OliveGarden.sln` con Visual Studio.
3. Dejá `OliveGarden_UI` como proyecto de inicio.
4. Ejecutá con **F5**.

Al iniciar, la aplicación recrea la base de datos con datos de ejemplo (mesas, productos, permisos) y un usuario administrador:

- Usuario: `admin`
- Contraseña: `admin123`

Por ahora el arranque empieza siempre de cero, así que los datos cargados no se conservan entre ejecuciones.

Es un usuario de demostración. Cambiá la contraseña si vas a usar el sistema en serio.

## Seguridad

- Las contraseñas **no se guardan**: se guarda un hash **PBKDF2-HMAC-SHA256** con sal aleatoria de 128 bits y 600.000 iteraciones.
- La verificación compara en tiempo constante y los mensajes de error no revelan si falló el usuario o la contraseña.
- Se bloquea al usuario tras varios intentos fallidos, y el bloqueo no altera la contraseña guardada.
- Compatibilidad: las cuentas guardadas con el formato anterior (Base64) siguen funcionando y se migran solas al hash la próxima vez que inicia sesión.

## Documentación

En la carpeta [`docs`](docs) está la entrega de análisis y diseño, con los diagramas:

- [Casos de uso](docs/diagramas/casos-de-uso.png)
- [Diagrama de clases](docs/diagramas/diagrama-de-clases.png)
- [Diagrama de actividad](docs/diagramas/diagrama-de-actividad.png)
- [Diagrama de secuencia](docs/diagramas/diagrama-de-secuencia.png)

## Mejoras pendientes

- Que el arranque conserve la base de datos existente y quitar los mensajes de depuración.
- Migrar la persistencia de XML a una base de datos SQL.
- Agregar pruebas automáticas.

## Autora

**Macarena Negreira**
[LinkedIn](https://www.linkedin.com/in/macarena-negreira-86a08b238/) · macarenanahirnegreira@gmail.com
