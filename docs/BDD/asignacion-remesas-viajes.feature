# language: es
Característica: Asociar remesas y viajes
  Para organizar el transporte de las remesas de una orden
  Como usuario autorizado de logística
  Quiero asociar remesas a viajes mediante RemesaViaje

  Escenario: Llevar una remesa desde una orden de un cliente hasta un viaje
    Dado que existe un cliente
    Y existe una orden de servicio asociada con ese cliente
    Y existe una remesa asociada con esa orden
    Y existe un viaje válido
    Cuando se asocia la remesa con el viaje
    Entonces la remesa queda vinculada a la orden de ese cliente
    Y la remesa queda relacionada con el viaje mediante RemesaViaje

  Escenario: Una remesa se distribuye en viajes diferentes
    Dado que existe una remesa válida perteneciente a una orden
    Y existen dos viajes válidos diferentes
    Cuando se asocia la remesa con cada uno de los viajes
    Entonces la remesa queda relacionada con ambos viajes

  Escenario: Un viaje transporta varias remesas
    Dado que existen dos remesas válidas
    Y existe un viaje válido
    Cuando se asocia cada remesa con el viaje
    Entonces el viaje queda relacionado con ambas remesas

  @propuesta_tecnica @pendiente_aprobacion
  Escenario: Rechazar una asociación duplicada
    Dado que una remesa ya está asociada con un viaje
    Cuando se intenta registrar nuevamente el mismo par de remesa y viaje
    Entonces la asociación duplicada se rechaza

  @propuesta_funcional @pendiente_aprobacion
  Escenario: Validar la distribución del peso de una remesa
    Dado que una remesa tiene un peso total definido
    Y la remesa se distribuye entre varios viajes con peso asignado
    Cuando se registra una asignación que hace que la suma supere el peso total
    Entonces la asignación se rechaza
    Y el sistema conserva las asignaciones previas

  @propuesta_funcional @pendiente_aprobacion
  Escenario: Validar la capacidad del vehículo del viaje
    Dado que un viaje tiene un vehículo con capacidad definida
    Y varias remesas tienen peso asignado a ese viaje
    Cuando la suma del peso asignado supera la capacidad del vehículo
    Entonces la asignación que excede la capacidad se rechaza
