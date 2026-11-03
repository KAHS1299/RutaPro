# Requisitos aprobados: módulo Clientes

**Estado:** línea base aprobada por el equipo.

## Entidad Cliente

| Campo | Requisito aprobado |
|---|---|
| `Id` | Tipo `Guid`, generado automáticamente. |
| `Codigo` | `string`, obligatorio y único. |
| `RazonSocial` | `string`, obligatorio. |
| `NitDocumento` | `string`, obligatorio; no es único. |
| `Telefono` | `string`, opcional. |
| `Direccion` | `string`, opcional. |
| `Email` | `string`, opcional. |
| `FechaRegistro` | `DateTime`, generado automáticamente. |
| `Estado` | `EstadoCliente`; inicialmente `Activo`. Estados permitidos: `Activo` e `Inactivo`. |

## Reglas aprobadas

- No se permiten códigos internos duplicados.
- No se permite crear un cliente sin código, razón social o NIT/documento.
- No se impone unicidad a `NitDocumento`.
- Un cliente inactivo no puede recibir nuevas órdenes de servicio.
- Las órdenes históricas se conservan.
- La eliminación de un cliente es lógica, mediante desactivación.
- El profesor exige operaciones CRUD para el módulo. La eliminación debe respetar la desactivación lógica.
- No se validan formatos específicos de NIT, teléfono o correo electrónico.

## Decisiones técnicas para la implementación actual

- `FechaRegistro` se genera automáticamente en UTC (`DateTime.UtcNow`).
- La aplicación comprueba unicidad de `Codigo`; la futura persistencia deberá respaldarla para evitar duplicados concurrentes.
- `RazonSocial`, `Telefono` y `Email` sustituyen los nombres previos `Nombre` y `Contacto`, siguiendo la estructura aprobada.

## Alcance fuera de esta implementación

- No configurar MySQL ni Entity Framework, ni crear tablas o migraciones.
- No implementar endpoints HTTP en esta funcionalidad.

## Decisiones técnicas o funcionales aún abiertas

- Sensibilidad a mayúsculas y espacios al comparar códigos duplicados.
- Campos que podrán modificarse después de la creación.
- Si se permitirá reactivar un cliente inactivo y bajo qué condiciones.
- Comportamiento de desactivación frente a órdenes existentes, aparte de conservar el historial e impedir nuevas órdenes.
- Mensajes o formato externo de los errores; el caso de uso usa resultados tipados internamente.
- Garantía de unicidad ante solicitudes concurrentes al incorporar persistencia.
