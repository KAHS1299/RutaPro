# Modelo relacional: inventario, reconciliación y pendientes

**Estado:** auditoría y propuesta de preparación para EF Core; no es un esquema aprobado ni una migración.

Este documento separa el modelo C# existente en `Models/` de la línea base aprobada del equipo, los diagramas del Sprint 1 y las decisiones pendientes. El directorio `Models/` contiene clases con atributos de EF Core, pero no una base MySQL ni forma parte actualmente de un proyecto compilado.

## 1. Inventario de las clases existentes

Las clases originales se conservaron y se agruparon en `Models/Entidades/` y `Models/Vistas/`. Los campos siguientes describen las propiedades encontradas; **su presencia en el código de referencia no los convierte automáticamente en requisitos aprobados**.

| Modelo / tabla | Propiedades principales actualmente en `Models/Entidades/` | Relaciones anotadas en el modelo existente |
|---|---|---|
| Cliente | `Id:int`, `RazonSocial`, `NitDocumento`, `Telefono`, `Direccion`, `Email`, `FechaRegistro` | Cliente 1:N órdenes; el modelo de referencia tiene índice único de NIT |
| OrdenesServicio | `Id:int`, `ClienteId`, `FechaSolicitud`, `Estado`, `Observaciones` | N:1 cliente; 1:N remesas; 1:1 factura opcional |
| Remesa | `Id:int`, `NumeroRemesa`, `OrdenServicioId`, `RutaId`, `DescripcionMercancia`, `PesoTotal`, `FechaRegistro` | N:1 orden y ruta; 1:N evidencias; 1:1 viaje opcional en navegación actual |
| Viaje | `Id:int`, `RemesaId`, `VehiculoId`, `ConductorId`, `FechaSalida`, `FechaLlegadaEstimada`, `FechaLlegadaReal`, `Estado` | N:1 remesa, vehículo y conductor; 1:1 manifiesto opcional |
| Ruta | `Id:int`, `Origen`, `Destino`, `DistanciaKm`, `TarifaBase` | 1:N remesas en el modelo existente |
| Vehiculo | `Id:int`, `Placa`, `CapacidadKg`, `TipoCargaId`, `KilometrajeActual`, `KilometrajeUltimoMantenimiento`, `FrecuenciaMantenimientoKm`, `Estado`, `FechaRegistro` | 1:N viajes, mantenimientos y ubicaciones; N:1 tipo de carga |
| Conductore | `Id:int`, `NombreCompleto`, `NumeroLicencia`, `Telefono`, `Estado`, `FechaRegistro` | 1:N viajes |
| Manifiesto | `Id:int`, `ViajeId`, `CodigoRadicadoRndc`, `ValorFleteConductor`, `Estado`, `FechaExpedicion`, `FechaCumplimiento` | N:1 viaje; índice único actual en `ViajeId` |
| Factura | `Id:int`, `OrdenServicioId`, `NumeroFactura`, `FechaEmision`, `Subtotal`, `Impuestos`, `Total`, `Estado` | N:1 orden en la FK, pero índice único actual en `OrdenServicioId` restringe a una factura por orden |
| EvidenciasRemesa | `Id:long`, `RemesaId`, `NombreArchivo`, `UrlOcontenido`, `FechaSubida` | N:1 remesa |
| Mantenimiento | `Id:int`, `VehiculoId`, `FechaMantenimiento`, `Tipo`, `KilometrajeAlMomento`, `Costo`, `Observaciones` | N:1 vehículo |
| TiposCarga | `Id:int`, `Nombre`, `Descripcion` | 1:N vehículos; índice único en `Nombre` |
| Ubicacione | `Id:long`, `VehiculoId`, `Latitud`, `Longitud`, `FechaHoraReporte` | N:1 vehículo |
| Usuario | `Id:int`, `Nombre`, `Rol`, `Email`, `PasswordHash`, `Activo`, `FechaCreacion` | 1:N bitácoras; índice único en `Email` |
| Bitacora | `Id:long`, `UsuarioId`, `Accion`, `Fecha`, `Detalles` | N:1 usuario opcional; índice por fecha |

También hay seis modelos keyless de consultas en `Models/Vistas/`: `VwAlertasMantenimientoFlotum`, `VwErpManifiestosRndc`, `VwFacturasYcliente`, `VwMonitoreoFlotaGp`, `VwViajesDetallado` y `VwViajesYmanifiesto`. Son proyecciones de lectura, no tablas adicionales del modelo conceptual aprobado.

## 2. Relaciones objetivo y nivel de aprobación

| Relación objetivo | Base actual | Estado para RutaPro |
|---|---|---|
| Cliente 1:N OrdenServicio | Presente | Documentada; la orden requiere cliente existente y activo. |
| OrdenServicio 1:N Remesa | Presente | Aprobada por el equipo. |
| Remesa 1:N EvidenciaRemesa | Presente | Documentada en el ER. |
| Remesa N:M Viaje por `RemesaViaje` | El modelo de referencia tiene `Viaje.RemesaId` y una unicidad por remesa, limitándolo al 1:1 | Aprobación expresa: reemplazar esa asociación directa por la entidad intermedia `RemesaViaje`. Cada asociación enlaza una remesa y un viaje. No agregar atributos a la asociación sin aprobación. |
| Ruta 1:N Viaje | No coincide: el FK actual `RutaId` está en Remesa | El ER/documentación sitúa la ruta en Viaje; la arquitectura indica conflicto con el prototipo, que la guarda en OrdenServicio. Pendiente decidir ubicación definitiva. |
| Vehículo 1:N Viaje | Presente | Documentada en arquitectura. Reglas detalladas de disponibilidad/capacidad deben implementarse solo según aprobación funcional. |
| Conductor 1:N Viaje | Presente | Documentada en arquitectura. |
| Viaje–Manifiesto | El modelo actual aplica 1:1 mediante índice único de `ViajeId` | Pendiente confirmar cardinalidad; el diagrama Sprint 1 es ambiguo. |
| OrdenServicio–Factura | FK N:1 en Factura, pero índice único hace efectiva la relación 1:1 | El ER y la arquitectura documentan 1:N. Hay que retirar la unicidad por orden si se confirma esa línea base antes de adoptar el modelo. |
| Vehículo–Mantenimiento | Presente | Propuesta 1:N en arquitectura, por confirmar. |
| TipoCarga–Vehículo | Presente | Propuesta 1:N en arquitectura, por confirmar. |
| Vehículo–Ubicaciones | Presente | Propuesta en arquitectura; confirmar si la ubicación corresponde a vehículo o viaje y si se conserva para alcance inicial. |
| Usuario–Bitácora | Presente, con usuario opcional en Bitacora | Propuesta 1:N en arquitectura; confirmar obligatoriedad y alcance. |

El modelo conceptual acordado debe contener las 15 entidades identificadas y `RemesaViaje` como entidad asociativa adicional. El diagrama de Sprint 1 no prevalece sobre la aprobación posterior de la relación Remesa–Viaje N:M.

## 3. Reconciliación de Cliente

La clase existente en `Models/Entidades/Cliente.cs` no coincide con la entidad `Cliente` ya implementada en `RutaPro.Domain` ni con la línea base aprobada:

| Aspecto | Modelo C# de referencia | Aprobado / Domain actual | Tratamiento de integración |
|---|---|---|---|
| Id | `int` | `Guid`, generado automáticamente | Mantener `Guid`; configurar la clave EF para el tipo aprobado. |
| Código | No existe | `string`, obligatorio y único | Mantener `Codigo`; persistencia deberá respaldar su unicidad. |
| Razón social | `RazonSocial` | `RazonSocial`, obligatoria | Usar la propiedad aprobada sin duplicarla con `Nombre`. |
| NIT/documento | `NitDocumento`, índice único | Obligatorio y expresamente no único | No trasladar el índice único de referencia. |
| Teléfono | `Telefono` | Opcional | Coincide con el campo aprobado. |
| Dirección | `Direccion` | Opcional | Coincide con el campo aprobado. |
| Email | `Email` | Opcional | Coincide con el campo aprobado; sin validación de formato en esta etapa. |
| FechaRegistro | `FechaRegistro` | `DateTime`, generado automáticamente (implementación actual UTC) | Mantener y mapear según la decisión aprobada. |
| Estado | No existe | Activo/Inactivo; inicial Activo; baja lógica | Persistir el estado y conservar desactivación lógica. |

El modelo de referencia contiene un índice único para NIT. La decisión posterior lo resolvió expresamente: `Codigo` es único y `NitDocumento` no. Por tanto, el índice único de NIT no debe trasladarse a EF Core. No aplicar validación de formatos de NIT, teléfono ni correo electrónico.

## 4. Otras discrepancias que deben resolverse antes de EF Core

- **Remesa–Viaje:** quitar del diseño objetivo el FK directo obligatorio `RemesaId` en Viaje y el índice único que fuerza una única asociación; usar `RemesaViaje`. Las clases de `RutaPro.Domain` siguen siendo objetos en memoria sin IDs definitivos, así que las columnas FK se definirán al aprobar IDs para Remesa y Viaje.
- **Ruta:** el modelo existente relaciona Ruta con Remesa; el diagrama ER la relaciona con Viaje. La arquitectura también señala el conflicto con la pantalla de OrdenServicio.
- **Factura:** el índice único en `OrdenServicioId` contradice la cardinalidad documentada OrdenServicio 1:N Factura.
- **Manifiesto:** índice único en `ViajeId` implica máximo un manifiesto por viaje, decisión todavía no aprobada.
- **Cliente:** el ID `int`, la ausencia de `Codigo`/`Estado` y la unicidad de NIT del modelo de referencia difieren de la línea base aprobada. La entidad de Domain debe conservar `Guid`, `Codigo` único y NIT no único; `Telefono`, `Direccion`, `Email` y `FechaRegistro` forman ahora parte de la línea base.
- **Vehículo / tipo de carga / ubicaciones / mantenimiento:** sus relaciones y campos en las clases son más específicos que las decisiones actualmente aprobadas. Tratar kilometrajes, frecuencia de mantenimiento, GPS y demás propiedades como elementos del modelo de referencia, no como requisitos ratificados.
- **Roles:** `Usuario.Rol` es un texto, pero la arquitectura deja por decidir si Roles y Permisos serán entidades/tablas.
- **Nombres de clase:** `Conductore` y `Ubicacione` son nombres generados singulares irregulares; se dejan intactos en esta organización para no alterar los originales. En una implementación aprobada pueden usarse nombres C# regulares y mapear explícitamente a nombres SQL.

## 5. Estructura objetivo recomendada

Mantener las clases de negocio en `RutaPro.Domain`; ubicar el `DbContext`, configuraciones Fluent API y modelos de persistencia en `RutaPro.Infrastructure/Persistence/`. Las anotaciones actuales de `Models/` son referencias que se deben reconciliar antes de migrar; no se deben copiar automáticamente al dominio ni exponer desde la API.

La futura estructura de persistencia podría organizarse así, después de aprobar el modelo:

```text
src/RutaPro.Infrastructure/Persistence/
  RutaProDbContext.cs
  Configurations/
  Models/              # solo si se decide separar entidades EF del dominio
  ReadModels/          # proyecciones equivalentes a las vistas, si se requieren
```

No se crea ahora esta estructura de infraestructura porque aún faltan decisiones que determinan claves y cardinalidades.

## 6. Decisiones pendientes antes de implementar EF Core

1. Confirmar ubicación de Ruta y las cardinalidades de Manifiesto y Factura por OrdenServicio.
2. Confirmar relaciones de TiposCarga, Ubicaciones, Mantenimientos y Bitácora.
3. Definir campos aprobados de entidades no cubiertas por la línea base de Clientes y por los diagramas.
4. Definir identificadores persistentes de Remesa y Viaje para construir las claves de `RemesaViaje`.
5. Decidir si la tabla intermedia tendrá solamente sus dos referencias o un identificador propio; no agregar peso asignado sin aprobación.
6. Aprobar qué vistas son necesarias y sus contratos; las seis vistas de referencia no son requisitos de base de datos por sí mismas.

Hasta resolver estas decisiones, este documento sirve como mapa de reconciliación. No se han creado tablas, migraciones, conexión MySQL ni un `DbContext`.
