# RutaPro ERP — arquitectura inicial

**Estado:** propuesta para revisión. Este documento separa la línea base funcional del Sprint 1 de las decisiones técnicas aún pendientes. No constituye autorización para implementar las decisiones marcadas como propuestas.

## 1. Propósito y alcance documentado

RutaPro es un sistema web académico para gestionar operaciones de transporte terrestre de carga. La línea base prioriza el flujo cliente → orden de servicio → remesa → planificación del viaje → entrega y evidencia. También contempla flota, conductores, manifiestos internos, mantenimiento, facturación interna, usuarios, reportes y bitácora.

Se excluyen del alcance inicial el GPS en tiempo real, la emisión legal de manifiestos, la facturación electrónica ante la DIAN, los pagos en línea, la contabilidad general, una aplicación móvil nativa y la predicción avanzada.

## 2. Requisitos documentados y propuestas

### Requisitos documentados

- Administrar clientes, órdenes, remesas/evidencias, rutas, vehículos/tipos de carga, conductores, viajes, manifiestos, mantenimiento, facturación, usuarios/roles, dashboard/reportes y bitácora.
- Validar relaciones y disponibilidad de recursos; conservar trazabilidad de cambios críticos.
- Aplicar autorización en el servidor. Las bajas de registros históricos deben ser lógicas, controladas o anulaciones justificadas, no borrados físicos ordinarios.
- Mantener los datos de demostración ficticios y no presentar GPS ni integraciones fiscales como capacidades implementadas.
- Atributos de calidad: seguridad, integridad, disponibilidad, usabilidad, accesibilidad, compatibilidad, mantenibilidad, auditabilidad, portabilidad, interoperabilidad y trazabilidad documental.
- Objetivos iniciales: percentil 95 de consultas de listados ≤ 2 s con hasta 50 sesiones concurrentes y cobertura unitaria objetivo ≥ 80 % cuando el entorno permita medirla. Son objetivos; no resultados medidos.

### Propuestas técnicas

- Backend en C#/.NET 10 con ASP.NET Core Web API, EF Core y MySQL 8.4, según la selección tecnológica del proyecto.
- Monolito modular por capas para conservar límites claros entre API, casos de uso, dominio y persistencia.
- Mantener la exportación React/TypeScript separada como frontend solo si el equipo aprueba conservarla; el prototipo actual no tiene integración API/DB.
- Usar DTOs y validaciones de aplicación para no exponer entidades de persistencia directamente por HTTP.
- Guardar secretos fuera del repositorio y proteger las evidencias mediante un servicio de almacenamiento reemplazable.

## 3. Roles y permisos

Los ocho actores documentados son: administrador; gerente/supervisor; coordinador logístico; gestor de flota; gestor de conductores; conductor; responsable de facturación; y usuario de consulta/auditor.

El prototipo presenta cinco perfiles: administradora, gestor de flota, coordinador logístico, responsable de facturación y conductor/operador. No representa por separado gerente/supervisor, gestor de conductores ni auditor/consulta. Además, el rol de administrador del prototipo tiene menos módulos que el acceso transversal condicionado por permisos previsto en el documento.

La matriz siguiente resume responsabilidades documentadas, no una configuración definitiva de permisos:

| Rol | Acceso funcional documentado |
|---|---|
| Administrador | Gestionar usuarios, roles y permisos; consultar bitácora; acceso transversal sujeto a autorización. |
| Gerente / supervisor | Consultar indicadores, reportes y estados generales; información sensible solo según permiso. |
| Coordinador logístico | Gestionar clientes, órdenes, remesas, rutas y viajes; consultar disponibilidad y asignar recursos; gestionar manifiestos según autorización. |
| Gestor de flota | Administrar vehículos y mantenimientos; consultar disponibilidad e historial de flota. |
| Gestor de conductores | Administrar datos operativos de conductores y revisar disponibilidad e historial. |
| Conductor | Consultar viajes asignados y reportar estados, novedades o evidencias autorizadas; no administra datos maestros. |
| Responsable de facturación | Crear y consultar facturas; revisar importes y actualizar estados permitidos. |
| Usuario de consulta / auditor | Consultar registros y reportes autorizados sin modificar datos operativos. |

**Pendiente de aprobación:** equivalencias entre los ocho roles del documento y los cinco del prototipo; permisos por operación; lectura de datos de clientes/órdenes por facturación; lectura de disponibilidad de flota por logística; y separación entre gestor de flota y gestor de conductores.

## 4. Entidades identificadas y relaciones

Las 15 entidades conceptuales identificadas en el diagnóstico son `Clientes`, `OrdenesServicio`, `Remesas`, `EvidenciasRemesa`, `Rutas`, `Vehiculos`, `TiposCarga`, `Conductores`, `Viajes`, `Ubicaciones`, `Mantenimientos`, `Manifiestos`, `Facturas`, `Usuarios` y `Bitacora`. El equipo aprobó agregar `RemesaViaje` como entidad de asociación para expresar la relación muchos-a-muchos; por tanto, el modelo conceptual actualizado tiene 16 entidades.

Relaciones indicadas o propuestas a partir de responsabilidades documentadas:

- `Cliente 1 — N OrdenServicio`: cada orden requiere un cliente existente y activo.
- `OrdenServicio 1 — N Remesa`: requisito aprobado por el equipo; cada remesa pertenece a una orden.
- `Remesa 1 — N EvidenciaRemesa`: las evidencias conservan referencia y metadatos asociados a la remesa.
- `Remesa N — M Viaje`, mediante `RemesaViaje`: decisión aprobada por el equipo. Una orden puede tener varias remesas; una remesa puede distribuirse en distintos viajes y un viaje puede transportar varias remesas.
- `Remesa 1 — N RemesaViaje` y `Viaje 1 — N RemesaViaje`: cada fila de asociación vincula una remesa con un viaje; el par debe representar una asignación única (restricción técnica propuesta).
- `Ruta 1 — N Viaje`, `Vehiculo 1 — N Viaje` y `Conductor 1 — N Viaje`: el viaje selecciona esos recursos; las asignaciones deben respetar disponibilidad y capacidad.
- `Vehiculo 1 — N Mantenimiento`: el mantenimiento conserva el historial del vehículo.
- `OrdenServicio 1 — N Factura`: relación mostrada en el ER conceptual.
- `Vehiculo 1 — N Ubicacion`, `TipoCarga 1 — N Vehiculo`, `Viaje 1 — N Manifiesto` y `Usuario 1 — N Bitacora` son relaciones propuestas por las responsabilidades descritas; sus cardinalidades deben confirmarse.

### Cardinalidades y campos por decidir

1. En el ER la ruta pertenece al viaje, mientras el prototipo también la guarda en la orden. Aprobar dónde reside la ruta.
2. Definir si un viaje tiene uno o varios manifiestos.
3. Confirmar la relación de `TiposCarga` con vehículos y de `Ubicaciones` con vehículo o viaje.
4. El documento deja pendiente una posible entidad de pagos; no se incluye en el modelo hasta aprobar ese alcance.
5. `Roles` y `Permisos` no aparecen como entidades dentro de las 15 identificadas originalmente; decidir si serán catálogos/tablas o una representación distinta. `RemesaViaje` es la entidad adicional aprobada para el vínculo muchos-a-muchos.

## 5. Campos funcionales mínimos

Estos campos resumen lo identificado; campos adicionales deben justificarse contra requisitos aprobados.

| Entidad | Campos documentados o necesarios para los criterios existentes | Pendientes |
|---|---|---|
| `Clientes` | `Id` (`Guid`), `Codigo` único, `RazonSocial`, `NitDocumento` (sin unicidad), `Telefono`, `Direccion`, `Email`, `FechaRegistro` (`DateTime`) y `Estado` (`Activo`/`Inactivo`). Campos obligatorios: `Codigo`, `RazonSocial` y `NitDocumento`. | Ciudad fuera del alcance inicial. Definir comparación de códigos y campos editables; no validar formatos específicos de NIT, teléfono o correo. |
| `OrdenesServicio` | Identificador único; `ClienteId`; fecha de solicitud; estado; observaciones; motivo de cancelación cuando aplique. | El prototipo agrega ruta, carga y peso en la orden; el documento ubica ruta en viaje y descripción/peso en remesa. Resolver la fuente única de esos campos. |
| `Remesas` | Identificador/número único; `OrdenId`; descripción de mercancía; peso; estado. | Origen/destino solo si el modelo los define; decidir si se derivan de la ruta. |
| `Viajes` | Identificador; `RutaId`; `VehiculoId`; `ConductorId`; fecha de salida; fecha estimada; fecha real; estado. | Precisar fechas estimadas/reales de salida y llegada, y cómo registrar una excepción aprobada al cierre. La remesa se relaciona por `RemesaViaje`, no con un `RemesaId` directo. |
| `RemesaViaje` | **Aprobado:** referencias a `Remesa` y `Viaje` para materializar la relación muchos-a-muchos. | **Propuesta técnica mínima:** clave primaria compuesta (`RemesaId`, `ViajeId`) o identificador propio con restricción única sobre el par; escoger estrategia antes de implementar. `PesoAsignadoKg` se propone si se debe registrar cuánto de la remesa va en cada viaje. El equipo aún debe aprobar si esa cantidad es requisito y cómo validar sumas contra peso de remesa/capacidad del vehículo. |

## 6. Estados y transiciones propuestos

El documento exige estados y transiciones controlados, pero no fija el catálogo completo. Los nombres y flujos siguientes son propuestas que deben aprobarse y unificarse con las etiquetas del prototipo.

### Orden de servicio

```text
Programada → En tránsito → Entregada
     └──────────────→ Cancelada
```

La cancelación se permitiría solo si aún no se ejecutó el transporte, con motivo. No se reactivaría una orden entregada o cancelada mediante edición ordinaria. La transición agregada desde los viajes depende de resolver las cardinalidades de órdenes, remesas y viajes.

### Viaje

```text
Programado → En curso → Completado
     └──────────────→ Cancelado
En curso → Con novedad (tratamiento por aprobar)
```

La cancelación requiere que el viaje no haya iniciado, motivo y liberación de recursos. El cierre requiere llegada real o excepción aprobada. El documento menciona “con novedad” como estado posible y también requiere registrar novedades; se propone tratarla como evento asociado al viaje hasta que el equipo defina sus transiciones.

## 7. Evidencias

**Requisito documentado:** asociar evidencias a remesas y conservar metadatos y referencia al archivo. El almacenamiento físico no está definido.

**Propuesta pendiente:** persistir metadatos en MySQL y contenido en un almacenamiento privado fuera de la raíz pública; acceder al contenido mediante un servicio de aplicación autorizado y mantener una interfaz que permita sustituir almacenamiento local por otro servicio si se aprueba. Acordar tipos, tamaño máximo, retención y permisos antes de implementar. El límite mostrado por el prototipo no se toma como requisito aprobado.

## 8. Arquitectura lógica propuesta

```text
Frontend React (si se aprueba conservarlo)
                  │ HTTP/JSON
                  ▼
          RutaPro.Api
                  │
          RutaPro.Application
                  │
           RutaPro.Domain
                  ▲
          RutaPro.Infrastructure
             ├── EF Core / MySQL
             ├── almacenamiento de evidencias
             └── auditoría
```

- **API:** endpoints HTTP, DTOs, autenticación/autorización y respuestas.
- **Application:** casos de uso, validación de entrada y coordinación de transacciones.
- **Domain:** entidades, estados y reglas de negocio sin dependencia de HTTP/EF Core.
- **Infrastructure:** `DbContext`, configuraciones y proveedor MySQL, persistencia de auditoría e integración de almacenamiento.
- **Frontend:** el prototipo React es una referencia visual; su uso como interfaz final queda por aprobar.

No se ha elegido mecanismo de autenticación (sesión/JWT), proveedor EF Core concreto ni estrategia de despliegue. Son decisiones previas a implementación.

## 9. Organización de solución y pruebas

La estructura autorizada en el repositorio puede alojar:

```text
docs/                 Requisitos, arquitectura, modelo, TDD y BDD
src/                  API, aplicación, dominio e infraestructura
tests/                xUnit y pruebas de integración
web/                  Frontend, si se confirma
assets/               Recursos aprobados y libres de datos privados
```

TDD con xUnit: comenzar por reglas de unicidad/estado de clientes; orden para cliente activo; remesa con orden válida; asignación muchos-a-muchos Remesa–Viaje (varias remesas por viaje y varios viajes por remesa); rechazo de referencias inexistentes y duplicidad del mismo par; disponibilidad, capacidad y fechas del viaje; transiciones y cancelaciones; cálculo de facturas; y autorización/auditoría. Si se aprueba `PesoAsignadoKg`, agregar pruebas de suma asignada por remesa y de capacidad del vehículo. Reqnroll/Gherkin debe cubrir criterios de aceptación del flujo cliente → orden → remesa → viaje y restricciones de recursos. Las pruebas de integración utilizarán un entorno de prueba aislado cuando se apruebe su configuración.

## 10. Orden de implementación propuesto

1. Aprobar roles/permisos, cardinalidades, catálogos de estados y campos pendientes.
2. Acordar autenticación, almacenamiento de evidencias y configuración segura de secretos.
3. Preparar la solución y las pruebas iniciales.
4. Implementar autorización y auditoría de servidor.
5. Implementar el flujo cliente → orden → remesa → recursos → viaje → evidencia.
6. Incorporar manifiestos internos, mantenimiento y facturación interna.
7. Completar dashboard/reportes y verificar RNF y trazabilidad.

GPS en tiempo real y facturación electrónica quedan fuera hasta aprobar integraciones y requisitos específicos.

## 11. Seguridad del repositorio

- No versionar contraseñas, tokens, `.env`, credenciales MySQL, claves privadas ni datos personales.
- No agregar el ZIP ni el PDF originales mientras no se revisen y autorice su publicación.
- Mantener ejemplos de configuración sin valores secretos.
- Conservar originales fuera de los cambios de organización del repositorio.
