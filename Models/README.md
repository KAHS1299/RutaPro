# Modelos de referencia de RutaPro

Esta carpeta contiene clases C# con anotaciones de Entity Framework Core como referencia del esquema recibido. **No contiene una base de datos MySQL**, un `DbContext` ni migraciones; tampoco está integrada actualmente en los proyectos de la solución.

## Organización

- `Entidades/`: clases que representan tablas del esquema de referencia.
- `Vistas/`: modelos de solo lectura para vistas de consulta (`Vw...`).

Los archivos se reubicaron sin alterar su contenido. Por tanto, reflejan el esquema de referencia original y todavía no son el modelo EF Core aprobado para RutaPro.

## Siguiente paso

Usar `../docs/base-datos/modelo-relacional-y-auditoria.md` como guía de reconciliación. No crear `DbContext`, configuración EF Core ni migraciones hasta resolver allí las decisiones pendientes y aprobar el modelo definitivo.
