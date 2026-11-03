# Casos TDD: asignación de remesas a viajes

**Estado:** especificación de pruebas; aún no hay código ni proyecto de pruebas.

## Criterios aprobados por el equipo

- Una orden puede tener varias remesas.
- Una remesa puede distribuirse en diferentes viajes.
- Un viaje puede transportar varias remesas.
- La relación Remesa–Viaje se representa mediante `RemesaViaje`.

## Casos unitarios a implementar

| ID | Caso | Resultado esperado | Base |
|---|---|---|---|
| RV-T01 | Asociar una remesa válida a un viaje válido. | Se registra la asociación Remesa–Viaje. | Relación aprobada; validación técnica por definir. |
| RV-T02 | Asociar una remesa a dos viajes distintos. | Ambas asociaciones se conservan. | Una remesa puede distribuirse en diferentes viajes. |
| RV-T03 | Asociar dos remesas distintas al mismo viaje. | Ambas asociaciones se conservan. | Un viaje puede transportar varias remesas. |
| RV-T04 | Consultar las remesas de un viaje. | Se obtienen todas las remesas asociadas al viaje. | Se deriva de la cardinalidad aprobada. |
| RV-T05 | Consultar los viajes de una remesa. | Se obtienen todos los viajes asociados a la remesa. | Se deriva de la cardinalidad aprobada. |

## Casos propuestos, pendientes de aprobación técnica/funcional

| ID | Caso | Resultado esperado propuesto | Decisión pendiente |
|---|---|---|---|
| RV-P01 | Intentar repetir el mismo par Remesa–Viaje. | Rechazar el duplicado mediante validación y/o restricción única. | Aprobar identidad de la asociación y tratamiento del duplicado. |
| RV-P02 | Asociar identificadores inexistentes. | Rechazar la asociación; no dejar referencias huérfanas. | Confirmar respuesta funcional y mecanismo de integridad. |
| RV-P03 | Asignar un peso a cada vínculo y sumar el peso de una remesa. | La suma no supera el peso total de la remesa. | Aprobar `PesoAsignadoKg`, unidad, precisión y tratamiento de diferencias. |
| RV-P04 | Sumar los pesos vinculados a un viaje. | La carga no supera la capacidad disponible del vehículo. | Aprobar cálculo de capacidad, unidades y momento de validación. |
| RV-P05 | Cancelar un viaje que tiene remesas asociadas. | Mantener trazabilidad de asociaciones y aplicar una política explícita. | Definir si se liberan, reasignan o conservan como histórico. |

La clave de `RemesaViaje` puede ser compuesta (`RemesaId`, `ViajeId`) o usar un identificador propio con unicidad del par. Es una decisión técnica pendiente. No se asume una regla de peso ni una política de cancelación hasta que el equipo las apruebe.
